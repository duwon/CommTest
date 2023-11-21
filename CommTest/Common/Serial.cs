using System;
using System.IO.Ports;
using System.Text;

namespace CommTest
{

    class Serial
    {
        public delegate void ReceivedHandler();
        public delegate void DebugMessageHandler(string msg);

        public event ReceivedHandler RceivedEvent;
        public event DebugMessageHandler DebugMessageEvent;

        private System.Collections.Concurrent.ConcurrentQueue<byte> RxBuffer = new System.Collections.Concurrent.ConcurrentQueue<byte>();
        private SerialPort _serialPort = new SerialPort();

        /// <summary>
        /// 송수신 패킷을 ASCII로 출력할지 여부. false이면 HEX로 출력
        /// </summary>
        public bool IsAsciiPrint { get; set; } = false;

        public Serial()
        {
            _serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(RxEvent);
        }

        /// <summary>
        /// 생성자 함수 호출 시 시리얼 바로 연결
        /// </summary>
        /// <param name="PortName">COM 포트</param>
        /// <param name="BaudRate">속도</param>
        public Serial(string PortName, int BaudRate)
        {
            _serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(RxEvent);
            Connect(PortName, BaudRate);
        }

        public bool Connect(string PortName, int BaudRate)
        {
            _serialPort.PortName = PortName;
            _serialPort.BaudRate = BaudRate;

            try
            {
                _serialPort.Open();
                PrintDebugMsg($"{_serialPort.PortName} {_serialPort.BaudRate} Serial Port Open");
                return true;
            }
            catch (System.Exception ex)
            {
                PrintDebugMsg(ex.Message);
                return false;
            }
        }

        public void Disconnect()
        {
            try
            {
                _serialPort.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(RxEvent);
                _serialPort.Close();
            }
            catch (System.Exception ex)
            {
                PrintDebugMsg(ex.Message);
            }
        }

        public void Send(byte[] packet)
        {
            if (!_serialPort.IsOpen)
            {
                PrintDebugMsg("Connect the serial port.");
                return;
            }

            try
            {
                if (packet.Length == 0) return;

                PrintDebugMsg($"[TX] {(IsAsciiPrint ? Encoding.ASCII.GetString(packet) : BitConverter.ToString(packet).Replace("-", " "))}");
                _serialPort?.Write(packet, 0, packet.Length);
            }
            catch (System.Exception ex)
            {
                PrintDebugMsg(ex.Message);
            }
        }


        private void PrintDebugMsg(string msg)
        {

            DebugMessageEvent?.Invoke(msg);
        }

        public string[] GetPort()
        {
            string[] portName = SerialPort.GetPortNames();
            Array.Sort(portName);
            return portName;
        }

        /// <summary>
        /// Serial 데이터 수신 인터럽트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RxEvent(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                int iRecivedSize = _serialPort.BytesToRead;

                if (iRecivedSize != 0) // 수신 데이터가 있으면
                {
                    byte[] buff = new byte[iRecivedSize];

                    _serialPort.Read(buff, 0, iRecivedSize);
                    PrintDebugMsg($"[RX] {(IsAsciiPrint ? Encoding.ASCII.GetString(buff) : BitConverter.ToString(buff).Replace("-", " "))}");


                    for (int i = 0; i < buff.Length; i++)
                    {
                        RxBuffer.Enqueue(buff[i]);
                    }

                    RceivedEvent?.Invoke();

                }
            }
            catch (System.Exception ex)
            {
                PrintDebugMsg(ex.Message);
            }
        }

        /// <summary>
        /// 버퍼에 저장된 데이터 갯수
        /// </summary>
        public int RxCount
        {
            get
            {
                return RxBuffer.Count;
            }
        }

        /// <summary>
        /// 버퍼의 첫번째 데이터
        /// </summary>
        public byte? PacketSTX
        {
            get
            {
                if (RxBuffer.Count < 1) return null;
                byte[] arrBuff = RxBuffer.ToArray();
                return arrBuff[0];
            }
        }

        /// <summary>
        /// 버퍼의 패킷 ETX, 호출 전 Packet length 체크하고 사용 할 것
        /// </summary>
        public byte? PacketETX
        {
            get
            {
                byte[] arrBuff = RxBuffer.ToArray();
                return arrBuff[PacketLength - 1];
            }
        }

        public int PacketLength
        {
            get
            {
                if (RxBuffer.Count < 2) return 0xFFFF;
                byte[] arrBuff = RxBuffer.ToArray();
                return arrBuff[1];
            }
        }

        /// <summary>
        /// 버퍼의 첫번째 데이터
        /// </summary>
        public byte RxByte
        {
            get
            {
                byte[] arrBuff = RxBuffer.ToArray();
                return arrBuff[0];
            }
        }

        /// <summary>
        /// 버퍼의 모든 데이터
        /// </summary>
        public byte[] RxData
        {
            get
            {
                return RxBuffer.ToArray();
            }
        }

        /// <summary>
        /// 버퍼에서 1바이트 읽기. 데이터 갯수 1 감소
        /// </summary>
        /// <returns></returns>
        public byte GetByte()
        {
            byte[] buff_que = new byte[1];
            RxBuffer.TryDequeue(out buff_que[0]);
            return buff_que[0];
        }

        /// <summary>
        /// 버퍼의 모든 데이터 읽기. 데이터 갯수 초기화.
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            byte[] arrBuff = RxBuffer.ToArray();
            while (RxBuffer.Count != 0)
            {
                byte[] buff_que = new byte[1];
                RxBuffer.TryDequeue(out buff_que[0]);
            }
            return arrBuff;
        }

        /// <summary>
        /// 버퍼의 n개의 바이트 읽기
        /// </summary>
        /// <returns></returns>
        public byte[] GetData(int size)
        {
            if (size > RxCount)
            {
                return null;
            }

            byte[] arrBuff = new byte[size];
            for (int iCnt = 0; iCnt < size; iCnt++)
            {
                RxBuffer.TryDequeue(out arrBuff[iCnt]);
            }
            return arrBuff;
        }

        /// <summary>
        /// 버퍼의 첫번째 UInt16 값
        /// </summary>
        public UInt16 RxUInt16
        {
            get
            {
                byte[] arrBuff = RxBuffer.ToArray();
                UInt16 returnVal = 0;
                if (RxBuffer.Count > 1)
                {
                    returnVal = (UInt16)(arrBuff[0] + (arrBuff[1] << 8));
                }
                else if (RxBuffer.Count == 1)
                {
                    returnVal = arrBuff[0];
                }
                return returnVal;
            }
        }

        public void PutData(byte[] Data)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                RxBuffer.Enqueue(Data[i]);
            }
        }

        public bool IsOpen
        {
            get => _serialPort.IsOpen;
        }
    }
}
