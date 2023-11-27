using MaterialSkin;
using MaterialSkin.Controls;
using SuperSimpleTcp;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CommTest
{
    public partial class TcpPage : MaterialForm
    {
        public delegate void DebugMessageHandler(string msg, bool IsVisible);
        public event DebugMessageHandler DebugMessageEvent;

        private readonly MaterialSkinManager materialSkinManager;
        static SimpleTcpServer _Server;
        static SimpleTcpClient _Client;

        System.Threading.Timer timerAutoSend;

        /// <summary>
        /// 로그 출력 Class
        /// </summary>
        LogTextBox Log;

        String[] AutoSendData = new String[9];
        MaterialButton[] MbtnTextSend;
        MaterialSwitch[] MswSelectHexAscii;
        MaterialTextBox[] MtbSerialData;

        public TcpPage()
        {
            InitializeComponent();

            // Initialize MaterialSkinManager
            materialSkinManager = MaterialSkinManager.Instance;

            // Set this to false to disable backcolor enforcing on non-materialSkin components
            // This HAS to be set before the AddFormToManage()
            materialSkinManager.EnforceBackcolorOnAllComponents = true;

            // MaterialSkinManager properties
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo400, Primary.Indigo700, Primary.Indigo100, Accent.Pink100, TextShade.WHITE);
        }

        /// <summary>
        /// 폼 로드 시 실행되는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPage_Load(object sender, EventArgs e)
        {
            // Title
            mlbProjectTitle.Text = Text;

            // 자동 전송 버튼, Hex/Ascii 선택 스위치, 시리얼 데이터 텍스트 박스 배열로 저장
            MbtnTextSend = new MaterialButton[] { MbtnTextSend00, MbtnTextSend01, MbtnTextSend02, MbtnTextSend03, MbtnTextSend04, MbtnTextSend05, MbtnTextSend06, MbtnTextSend07, MbtnTextSend08 };
            MswSelectHexAscii = new MaterialSwitch[] { MswSelectHexAscii00, MswSelectHexAscii01, MswSelectHexAscii02, MswSelectHexAscii03, MswSelectHexAscii04, MswSelectHexAscii05, MswSelectHexAscii06, MswSelectHexAscii07, MswSelectHexAscii08 };
            MtbSerialData = new MaterialTextBox[] { MbtnText00, MbtnText01, MbtnText02, MbtnText03, MbtnText04, MbtnText05, MbtnText06, MbtnText07, MbtnText08 };

            // 로그 설정
            Log = new LogTextBox(mtbDebug);
            MtbDebugMaxLines.TextChanged += new System.EventHandler(MtbDebugMaxLines_TextChanged);
            Log.IsWiteFile = false;

            // 자동 전송 타이머 설정
            timerAutoSend = new System.Threading.Timer(new System.Threading.TimerCallback(AutoSendText), null, 0, 1000);

            // Hex 및 Ascii Hint 입력
            MbtnText00.Hint = "0x12 0x23 0x34 0x56";
            MbtnText01.Hint = "12 23 34 56";
            MbtnText02.Hint = "ASCII DATA";
            MswSelectHexAscii02.Checked = true;

            // Local IP 출력
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    lbLocalIPList.Items.Add(ip.ToString());
                    PrintDebugMsg($"Local IP: {ip}");
                    MtbTcpIP.Text = ip.ToString();
                }
            }
        }

        private void AutoSendText(object state)
        {
            // if (serialPort.IsOpen)
            {
                for (int i = 0; i < MtbSerialData.Length; i++)
                {
                    if (MbtnTextSend[i].UseAccentColor && !String.IsNullOrEmpty(AutoSendData[i]))
                    {
                        SendData(AutoSendData[i], true);
                    }
                }
            }
        }

        /// <summary>
        /// 폼 종료 시 실행되는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Client?.Disconnect();
            _Server?.Stop();
            Log?.Close();
            timerAutoSend?.Dispose();
        }

        /// <summary>
        /// 시리얼포트 연결 버튼 클릭 시 실행되는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MbtnConnect_Click(object sender, EventArgs e)
        {
            if (MbtnConnect.UseAccentColor)
            {
                try
                {
                    _Client?.Disconnect();
                    _Client?.Dispose();
                    _Client = null;
                    _Server?.Stop();
                    _Server?.Dispose();
                    _Server = null;
                }
                catch { }
            }
            else
            {
                int TcpPort = 0;

                try
                {
                    TcpPort = Convert.ToInt32(MtbPortLocal.Text);
                    if (!IsIPAddr(MtbTcpIP.Text)) throw new Exception();
                }
                catch
                {
                    PrintDebugMsg("IP 정보가 올바르지 않습니다.", true);
                    return;
                }

                try
                {
                    if (IsTcpServer)
                    {
                        _Server = new SimpleTcpServer(MtbTcpIP.Text, TcpPort);

                        _Server.Events.ClientConnected += ClientConnected;
                        _Server.Events.ClientDisconnected += ClientDisconnected;
                        _Server.Events.DataReceived += DataReceived;
                        _Server.Events.DataSent += DataSent;
                        _Server.Keepalive.EnableTcpKeepAlives = true;
                        _Server.Settings.IdleClientTimeoutMs = 0;
                        _Server.Settings.MutuallyAuthenticate = false;
                        _Server.Settings.AcceptInvalidCertificates = true;
                        _Server.Settings.NoDelay = true;
                        // _Server.Logger = PrintDebugMsg;
                        _Server.Start();
                    }
                    else
                    {
                        _Client = new SimpleTcpClient(MtbTcpIP.Text, TcpPort);

                        _Client.Events.Connected += ServerConnected;
                        _Client.Events.Disconnected += ServerDisconnected;
                        _Client.Events.DataReceived += DataReceived;
                        _Client.Events.DataSent += DataSent;
                        _Client.Keepalive.EnableTcpKeepAlives = true;
                        _Client.Settings.MutuallyAuthenticate = false;
                        _Client.Settings.AcceptInvalidCertificates = true;
                        _Client.Settings.ConnectTimeoutMs = 1000;
                        _Client.Settings.NoDelay = true;
                        // _Client.Logger = PrintDebugMsg;
                        _Client.ConnectWithRetries(5000);
                    }
                }
                catch (Exception ex)
                {
                    PrintDebugMsg(ex.Message, true);
                    return;
                }

            }

            IsConnected = (_Server?.IsListening ?? false) || (_Client?.IsConnected ?? false);

        }

        void ClientConnected(object sender, ConnectionEventArgs e)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                lbClientList.Items.Add(e.IpPort);
                lbClientList.SelectedItem = (e.IpPort);
            }));

            PrintDebugMsg("[" + e.IpPort + "] client connected");
        }

        void ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                lbClientList.Items.Remove(e.IpPort);
            }));

            PrintDebugMsg("[" + e.IpPort + "] client disconnected: " + e.Reason.ToString());
        }

        void DataReceived(object sender, DataReceivedEventArgs e)
        {
            PrintDebugMsg("[RX] [" + e.IpPort + "]: " + BitConverter.ToString(e.Data.Array, 0, e.Data.Count).Replace("-", " "));
            // PrintDebugMsg("[RX] [" + e.IpPort + "]: " + Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count));
        }

        void DataSent(object sender, DataSentEventArgs e)
        {
            // Console.WriteLine("[" + e.IpPort + "] sent " + e.BytesSent + " bytes");
        }

        void ServerConnected(object sender, ConnectionEventArgs e)
        {
            PrintDebugMsg("Server " + e.IpPort + " connected");
        }

        void ServerDisconnected(object sender, ConnectionEventArgs e)
        {
            PrintDebugMsg("Server " + e.IpPort + " disconnected");
        }

        /// <summary>
        /// 디버그 메시지 텍스트 박스에 출력하고 Main 폼에 이벤트 발생
        /// </summary>
        /// <param name="msg"></param>
        private void PrintDebugMsg(string msg)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                Log.Write(msg, true);
                DebugMessageEvent?.Invoke(msg, true);
            }));
        }

        /// <summary>
        /// 디버그 메시지 텍스트 박스에 출력 여부를 선택할 수 있고 Main 폼에 이벤트 발생
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="IsVisible"></param>
        private void PrintDebugMsg(string msg, bool IsVisible)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                if (IsVisible) new MaterialSnackBar(msg, 1000, "OK", true).Show(this);
                Log.Write(msg, IsVisible);
                DebugMessageEvent?.Invoke(msg, IsVisible);
            }));
        }

        /// <summary>
        /// 키보드 Control 키와 마우스 왼족 클릭 시 자동 전송 여부를 선택할 수 있음
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MbtnTextSend_MouseDown(object sender, MouseEventArgs e)
        {
            MaterialButton mbtn = (MaterialButton)sender;
            int mbtnNumber = Convert.ToInt32(mbtn.Name.Substring(mbtn.Name.Length - 2, 2));

            if (e.Button == MouseButtons.Left && (ModifierKeys & Keys.Control) == Keys.Control && IsConnected)
            {
                mbtn.UseAccentColor = !mbtn.UseAccentColor;
                PrintDebugMsg($"{mbtnNumber} Send automatically. {mbtn.UseAccentColor}");
            }
        }

        private void MbtnTextSend_Click(object sender, EventArgs e)
        {
            MaterialButton mbtn = (MaterialButton)sender;
            int mbtnNumber = Convert.ToInt32(mbtn.Name.Substring(mbtn.Name.Length - 2, 2));

            AutoSendData[mbtnNumber] = SendData(MtbSerialData[mbtnNumber].Text, !MswSelectHexAscii[mbtnNumber].Checked);
            if (!MswSelectHexAscii[mbtnNumber].Checked) MtbSerialData[mbtnNumber].Text = AutoSendData[mbtnNumber];
        }

        private byte[] ConvertStringHexToByteArray(string hexString)
        {
            hexString = hexString.ToUpper();         // HEX 체크박스 체크되어 있으면 대문자로 변환
            hexString = hexString.Replace("0X", ""); //16진수 표시 0x 제거

            string[] hexValuesSplit = hexString.Split(' ');
            byte[] byteArray = new byte[4096];
            int dataLength = 0;
            foreach (string hex in hexValuesSplit)
            {
                try
                {
                    byteArray[dataLength++] = (byte)Convert.ToInt32(hex, 16);
                }
                catch
                {
                    dataLength--;
                }
            }
            Array.Resize(ref byteArray, dataLength);

            return byteArray;
        }

        /// <summary>
        /// String hex 또는 Ascii 데이터를 전송하고 전송한 데이터를 리턴
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="IsHexString"></param>
        /// <returns></returns>
        private string SendData(string Data, bool IsHexString)
        {
            if (IsHexString)
            {
                byte[] txBuffer = ConvertStringHexToByteArray(Data);

                SendData(txBuffer);
                return BitConverter.ToString(txBuffer).Replace("-", " ");
            }
            else
            {
                byte[] txBuffer = new byte[4096];
                int dataLength = 0;
                foreach (char ch in Data.ToCharArray())
                {
                    txBuffer[dataLength++] = (byte)Convert.ToInt32(ch);
                }
                Array.Resize(ref txBuffer, dataLength);

                SendData(txBuffer);
                return BitConverter.ToString(txBuffer).Replace("-", " ");
            }
        }

        private void SendData(byte[] txBuffer)
        {
            try
            {
                if (IsTcpServer)
                {
                    foreach (var ipPort in lbClientList.SelectedItems)
                    {
                        PrintDebugMsg("[TX] [" + ipPort + "]: " + BitConverter.ToString(txBuffer).Replace("-", " "));
                        _Server?.Send(ipPort.ToString(), txBuffer);
                    }
                }
                else
                {
                    PrintDebugMsg("[TX] : " + BitConverter.ToString(txBuffer).Replace("-", " "));
                    _Client?.Send(txBuffer);
                }
            }
            catch (Exception ex)
            {
                PrintDebugMsg(ex.Message, true);
            }

        }

        /// <summary>
        /// 전송하려는 데이터가 Hex 인지 Ascii 인지 선택할 수 있음
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MswSelectHexAscii_CheckedChanged(object sender, EventArgs e)
        {
            MaterialSwitch msw = (MaterialSwitch)sender;

            if (!msw.Checked) msw.Text = "HEX";
            else msw.Text = "ASCII";
        }

        /// <summary>
        /// 디버그 텍스트 박스 클리어
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MbtnDebugClear_Click(object sender, EventArgs e)
        {
            mtbDebug.Clear();
        }

        /// <summary>
        /// 디버그 텍스트 박스 출력을 ASCII or HEX로 선택할 수 있음
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MswDebugIsAscii_CheckedChanged(object sender, EventArgs e)
        {
            MswSelectHexAscii_CheckedChanged(sender, e);
        }

        /// <summary>
        /// 자동 전송 인터벌 변경 시 실행되는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MtbAutoSendInterval_TextChanged(object sender, EventArgs e)
        {
            try
            {
                timerAutoSend.Change(0, Convert.ToInt32(MtbAutoSendInterval.Text));
            }
            catch
            {
                PrintDebugMsg("Inverval is not number.", true);
            }
        }

        public bool IsLayoutHeader
        {
            get => pHeader.Visible;
            set
            {
                pHeader.Visible = value;
                if (value)
                {
                    //pHeader.Location = new System.Drawing.Point(0, 0);
                    pBody.Location = new System.Drawing.Point(0, 30);
                    Size = new System.Drawing.Size(985, 699 + 30);
                    FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
                }
                else
                {
                    pBody.Location = new System.Drawing.Point(0, 0);
                    Size = new System.Drawing.Size(985, 699);
                    FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.StatusAndActionBar_None;
                }
            }
        }

        private void panelTOP_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mlbProjectTitle.Capture = false;
                pHeader.Capture = false;
                const int WM_NCLBUTTONDOWN = 0x00A1;
                const int HTCAPTION = 2;
                Message message = Message.Create(Handle, WM_NCLBUTTONDOWN, new IntPtr(HTCAPTION), IntPtr.Zero); DefWndProc(ref message);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private int DebugMaxLines
        {
            set
            {
                Log.LimitLines = value;
                MtbDebugMaxLines.Text = Log.LimitLines.ToString();
            }
            get => Log.LimitLines;
        }

        private void MtbDebugMaxLines_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DebugMaxLines = Convert.ToInt32(MtbDebugMaxLines.Text);
            }
            catch
            {
                PrintDebugMsg("Max line is not number.", true);
            }
        }

        private void MswDebugIsWriteFile_CheckedChanged(object sender, EventArgs e)
        {
            Log.IsWiteFile = MswDebugIsWriteFile.Checked;
        }

        public static bool IsIPAddr(string sIPAddr)
        {
            bool isIPAddr = false;

            Regex regex = new Regex(@"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
            if (regex.IsMatch(sIPAddr))
            {
                isIPAddr = true;
            }

            return isIPAddr;
        }

        private bool IsTcpServer
        {
            get => MrbTcpMethodServer.Checked;
            set
            {
                MrbTcpMethodServer.Checked = value;
                MrbTcpMethodClient.Checked = !value;
                MbtnConnect.Text = value ? "Listen" : "Connect";
            }
        }

        private bool IsConnected
        {
            set
            {
                MbtnConnect.UseAccentColor = value;
                if (value)
                {
                    MrbTcpMethodClient.Enabled = false;
                    MrbTcpMethodServer.Enabled = false;
                }
                else
                {
                    MrbTcpMethodClient.Enabled = true;
                    MrbTcpMethodServer.Enabled = true;
                }
            }
            get => MbtnConnect.UseAccentColor;
        }

        private void MrbTcpMethod_Click(object sender, EventArgs e)
        {
            IsTcpServer = (sender == MrbTcpMethodServer);
        }

        private void lbLocalIPList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsConnected)
            {
                MtbTcpIP.Text = lbLocalIPList.SelectedItem.ToString();
            }
        }
    }
}
