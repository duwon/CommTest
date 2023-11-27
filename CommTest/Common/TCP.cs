using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CommTest
{

    public class TCP
    {
        //public delegate void TCPReceivedHandler(byte[] data, int length, int port);
        public delegate void TCPReceivedHandler(byte[] data);
        public delegate void DebugMessageHandler(string msg);
        public delegate void TCPDisconnectHandler();

        public event TCPReceivedHandler ReceivedTCPEvent;
        public event DebugMessageHandler DebugMessageEvent;
        public event TCPDisconnectHandler TCPDisconnectEvent;

        private TcpClient client;
        private TcpListener listener;

        private Thread ListenThread;
        private Thread TcpReaderThread;

        // public string RemoteEndpointAddress { get; private set; }

        private readonly Queue ReceivedStringQueue = new Queue();

        public bool IsConnected
        {
            get => (client != null && client.Client != null && client.Connected) || listener != null;
        }

        private readonly byte[] receiveBuffer = new byte[4096];
        private readonly object syncLock = new object();

        //methods:
        public bool Connect(string IP, int port)
        {
            try
            {
                bool successFlag = false;

                lock (syncLock)
                {
                    try
                    {
                        client = new TcpClient();

                        IAsyncResult ar = client.BeginConnect(IP, port, null, null);
                        System.Threading.WaitHandle wh = ar.AsyncWaitHandle;
                        try
                        {
                            if (!ar.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1), false))
                            {
                                client.Close();
                                throw new TimeoutException();
                            }
                            client.EndConnect(ar);
                        }

                        finally
                        {
                            wh.Close();
                        }

                        //client.Connect(IP, port);
                        client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

                        TcpReaderThread?.Abort();
                        TcpReaderThread = new Thread(ReadData) { IsBackground = true };
                        TcpReaderThread.Start();
                        PrintDebugMsg($"TCP :{IP}:{port} Conncected.");
                        successFlag = true;
                    }
                    catch (Exception e)
                    {
                        PrintDebugMsg(e.Message);
                    }
                }
                return successFlag;
            }
            catch (Exception e)
            {
                PrintDebugMsg(e.ToString());
                return false;
            }
        }

        public bool Disconnect()
        {
            try
            {
                lock (syncLock)
                {
                    try
                    {
                        TcpReaderThread?.Abort();
                        TcpReaderThread = null;

                        client?.Client?.Close();
                        client?.Close();
                        client = null;

                        if (ReceivedStringQueue.Count > 0)
                        {
                            ReceivedStringQueue.Clear();
                        }

                        PrintDebugMsg("TCP disconnected");
                    }
                    catch (Exception e)
                    {
                        PrintDebugMsg(e.ToString());
                        return false;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                PrintDebugMsg(e.ToString());
                return false;
            }
        }

        public bool Send(byte[] txData)
        {
            try
            {
                bool successFlag = false;

                lock (syncLock)
                {
                    try
                    {
                        client?.Client?.Send(txData);
                        successFlag = true;
                        PrintDebugMsg($"[TX] {BitConverter.ToString(txData).Replace("-", " ")}");
                    }
                    catch
                    {
                        TCPDisconnectEvent?.Invoke();
                    }
                }
                return successFlag;
            }
            catch (Exception e)
            {
                PrintDebugMsg(e.ToString());
                return false;
            }
        }

        public string GetReceivedString()
        {
            try
            {
                string returnString = "";

                lock (ReceivedStringQueue.SyncRoot)
                {
                    try
                    {
                        if (ReceivedStringQueue.Count > 0)
                        {
                            returnString = ReceivedStringQueue.Dequeue().ToString();
                        }
                    }
                    catch { }
                }
                return returnString;
            }
            catch
            {
                return "";
            }
        }

        public bool Listen(int port)
        {
            try
            {
                IPEndPoint ipLocalEndPoint = new IPEndPoint(IPAddress.Any, port);
                listener = new TcpListener(ipLocalEndPoint);
                listener.Start(port);

                ListenThread.Abort();
                ListenThread = null;

                ListenThread = new Thread(ListeningMethod) { IsBackground = true };
                ListenThread.Start();
                return true;
            }
            catch (Exception e)
            {
                Dispose();
                PrintDebugMsg("TCP Listen Error");
                // PrintDebugMsg(e.ToString());
                return false;
            }
        }

        public void Dispose()
        {
            try
            {
                lock (syncLock)
                {
                    try
                    {
                        Disconnect();

                        listener?.Stop();
                        listener = null;

                        client?.Close();
                        client = null;

                        ListenThread?.Abort();
                        ListenThread = null;

                        TcpReaderThread?.Abort();
                        TcpReaderThread = null;

                        if (ReceivedStringQueue.Count > 0) ReceivedStringQueue.Clear();
                    }
                    catch (Exception e)
                    {
                        PrintDebugMsg(e.ToString());
                        return;
                    }
                }
                GC.SuppressFinalize(this);
            }
            catch (Exception e)
            {
                PrintDebugMsg(e.ToString());
                return;
            }
        }

        List<TcpClient> connectedClients = new List<TcpClient>();
        List<Thread> connectedReaderThreads = new List<Thread>();
        private void ListeningMethod()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        TcpClient _client = listener.AcceptTcpClient();
                        _client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                        connectedClients.Add(_client);

                        string RemoteEndpointAddress = _client.Client.RemoteEndPoint.ToString();
                        PrintDebugMsg("TCP :" + RemoteEndpointAddress + " Connected");

                        Thread _tcpReaderThread = new Thread(ReadData) { IsBackground = true };
                        _tcpReaderThread.Start();
                        connectedReaderThreads.Add(_tcpReaderThread);

                        return;

                        TcpReaderThread?.Abort();
                        TcpReaderThread = null;

                        TcpReaderThread = new Thread(ReadData) { IsBackground = true };
                        TcpReaderThread.Start();
                    }
                    catch
                    {

                        listener?.Stop();
                        listener = null;
                        break;
                    }
                }
            }
            catch { }
        }


        //char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
        private void ReadData()
        {
            try
            {
                int bytesRead = 0;

                while (true)
                {
                    if (!client.Connected)
                    {
                        break;
                    }

                    // 어떤 TCP 에서 받았는지 확인하기 위한 RemoteEndPoint 확인
                    //string strport = Convert.ToString(client.Client.RemoteEndPoint);
                    string strport = Convert.ToString(client.Client.LocalEndPoint);
                    string[] words = strport.Split(':');
                    int port = Convert.ToInt32(words[1]);

                    bytesRead = (int)client?.GetStream().Read(receiveBuffer, 0, receiveBuffer.Length);

                    if (bytesRead == 0)
                    {
                        break;
                    }


                    byte[] rxData = new byte[bytesRead];
                    Array.Copy(receiveBuffer, rxData, bytesRead);
                    PrintDebugMsg($"[RX] {BitConverter.ToString(rxData).Replace("-", " ")}");
                    ReceivedTCPEvent?.Invoke(rxData);

                }
            }
            catch (Exception e)
            {
                //PrintDebugMsg(e.ToString());
                Console.WriteLine(e.ToString());
                TCPDisconnectEvent?.Invoke();
                return;
            }
        }

        private void PrintDebugMsg(string msg)
        {
            DebugMessageEvent?.Invoke(msg);
        }
    }
}
