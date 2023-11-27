using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;

namespace CommTest
{
    public class UDP
    {
        public delegate void UDPReceivedHandler(byte[] data);
        public delegate void DebugMessageHandler(string msg);

        public event UDPReceivedHandler ReceivedUDPEvent;
        public event DebugMessageHandler DebugMessageEvent;

        private string UDP_DST_IP = "192.168.200.61";
        private int UDP_DST_PORT = 0;
        private int UDP_SRC_PORT = 0;
        private Thread tUDP_Recived;
        private UdpClient udpClient;
        public IPEndPoint ipep;

        /// <summary>
        /// 이더넷 연결 체크 1초 타이머
        /// </summary>
        readonly System.Timers.Timer timer_1s;
        private int TimeoutCnt;

        /// <summary>
        /// 생성자
        /// </summary>
        public UDP()
        {
            // 1초 타이머 생성 및 시작
            timer_1s = new System.Timers.Timer() { Interval = 1000 };
            timer_1s.Elapsed += new ElapsedEventHandler(CheckConnect);
            //timer_1s.Start();
        }

        ~UDP()
        {
            timer_1s.Stop();
            tUDP_Recived?.Abort();
        }

        /// <summary>
        /// 1초마다 UDP 메시지 패킷 확인. 3초 동안 메시지 패킷 수신이 없을경우 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckConnect(object sender, ElapsedEventArgs e)
        {
            TimeoutCnt++;

            if (IsConnected && (TimeoutCnt > 3))
            {
                Disconnect();
            }
        }

        public void UpdateDestIP(string IP, int Port)
        {
            UDP_DST_IP = IP;
            UDP_DST_PORT = Port;
        }

        public void UpdateListenPort(int Port)
        {
            UDP_SRC_PORT = Port;
        }

        /// <summary>
        /// 디버그 메시지 출력
        /// </summary>
        /// <param name="msg"></param>
        private void PrintDebugMsg(string msg)
        {
            DebugMessageEvent?.Invoke(msg);
        }

        public bool IsConnected { set; get; } = false;

        /// <summary>
        /// UDP 연결.
        /// </summary>
        /// <returns>true: 연결, false: 종료</returns>
        public bool Connect()
        {
            RxTestPacket(); // rx 시험용 패킷

            if ((tUDP_Recived == null) || (tUDP_Recived.IsAlive == false))
            {
                try
                {
                    if (UDP_SRC_PORT == 0)
                    {
                        // Client
                        ipep = new IPEndPoint(IPAddress.Any, 0);
                        udpClient = new UdpClient(UDP_DST_IP, UDP_DST_PORT);
                        PrintDebugMsg("UDP :" + UDP_DST_IP + ":" + Convert.ToString(UDP_DST_PORT) + " Conncected.");
                    }
                    else if (UDP_DST_PORT == 0)
                    {
                        // Server
                        ipep = new IPEndPoint(IPAddress.Any, 0);
                        udpClient = new UdpClient(UDP_SRC_PORT);
                        PrintDebugMsg("UDP Port :" + Convert.ToString(UDP_SRC_PORT) + " Listening.");
                    }
                    else
                    {
                        // src, dst 각 포트 사용
                        ipep = new IPEndPoint(IPAddress.Parse(UDP_DST_IP), UDP_DST_PORT);
                        udpClient = new UdpClient(UDP_SRC_PORT);
                        PrintDebugMsg("UDP Port :" + Convert.ToString(UDP_SRC_PORT) + " Listening.");
                        PrintDebugMsg("UDP :" + UDP_DST_IP + ":" + Convert.ToString(UDP_DST_PORT) + " Conncected.");
                    }

                    tUDP_Recived = new Thread(new ThreadStart(UDP_Packet_Recived));
                    tUDP_Recived.Start();
                    IsConnected = true;

                    return true;
                }
                catch (Exception)
                {
                    DebugMessageEvent("소켓 연결 불가\r\n");
                    return false;
                }
            }
            else
            {
                Disconnect();
                return false;
            }
        }

        public void Disconnect()
        {


            tUDP_Recived?.Abort();
            udpClient?.Close();
            udpClient = null;


            IsConnected = false;

            try
            {
                if (ipep.Address.Equals(IPAddress.Parse("0.0.0.0")))
                {
                    PrintDebugMsg("UDP Disconnected. \r\n");
                }
                else
                {
                    PrintDebugMsg(UDP_DST_IP + ":" + UDP_DST_PORT.ToString() + " Disconnected. \r\n");
                }
            }
            catch { };
        }


        /// <summary>
        /// UDP 패킷 전송
        /// </summary>
        /// <param name="data"></param>
        public void Send(byte[] txData)
        {
            if ((txData.Length > 0) && (txData.Length < 5000))
            {
                try
                {
                    if (udpClient != null)
                    {
                        //udpClient.Send(data, data.Length, UDP_DST_IP, UDP_DST_PORT);
                        udpClient.Send(txData, txData.Length, ipep);
                        PrintDebugMsg($"[TX] {BitConverter.ToString(txData).Replace("-", " ")}");
                    }
                    else
                    {
                        throw new Exception("이더넷 연결 오류\r\n");
                    }
                }
                catch (Exception e)
                {
                    PrintDebugMsg(e.Message);
                }
            }
        }

        /// <summary>
        /// UDP 패킷 수신 시 메시지 처리
        /// </summary>
        public void UDP_Packet_Recived()
        {
            while (true)
            {
                try
                {
                    TimeoutCnt = 0;
                    byte[] rxData = udpClient.Receive(ref ipep);


                    //PrintDebugMsg(ipep.ToString());
                    //PrintDebugMsg($"[RX] {Encoding.ASCII.GetString(udpRxData)}");
                    PrintDebugMsg($"[RX] {BitConverter.ToString(rxData).Replace("-", " ")}");

                    ReceivedUDPEvent?.Invoke(rxData);
                }
                catch
                {
                }
            }
        }

        private void RxTestPacket()
        {
#if false
            // 상태 데이터 프레임
            MessagePacket.StatusData status = new MessagePacket.StatusData();
            status.length = 24;
            status.bit_tx = 0x8888;
            status.bit_vg_vd = 0xf0;
            status.bit_msg = 0x04;
            status.tempA = -10;
            status.tempB = 10;
            status.tempC = 20;
            status.tempD = 50;
            status.version = 2;
            status.opSatus = 0x60;

            byte[] rxPacket = new Utils().StructureToByteArray(status);
            ReceivedEvent(rxPacket);
#endif
        }
    }
}

