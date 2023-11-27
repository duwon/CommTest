using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CommTest
{
    public partial class Main : MaterialForm
    {
        /// <summary>
        /// materialSkinManager를 사용하기 위한 변수
        /// </summary>
        private readonly MaterialSkinManager materialSkinManager;

        /// <summary>
        /// 폼 - 설정 페이지
        /// </summary>
        private ConfigPage FormConfig { get; set; }

        /// <summary>
        /// 폼 - 시리얼 테스트 페이지
        /// </summary>
        private SerialPage FormSerial { get; set; }

        /// <summary>
        /// 폼 - UDP 테스트 페이지
        /// </summary>
        private UdpPage FormUdp { get; set; }

        /// <summary>
        /// 폼 - TCP 테스트 페이지
        /// </summary>
        private TcpPage FormTcp { get; set; }

        /// <summary>
        /// 로그 출력 Class
        /// </summary>
        LogTextBox Log;

        public Main()
        {
            InitializeComponent();

            // Initialize MaterialSkinManager
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo400, Primary.Indigo700, Primary.Indigo100, Accent.Pink100, TextShade.WHITE);
        }

        /// <summary>
        /// 폼 로드 시 실행되는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, System.EventArgs e)
        {
            // 폼 크기 결정
            MaximumSize = new System.Drawing.Size(1048, 760);
            MinimumSize = new System.Drawing.Size(1048, 760);

            //GUI 버전 표시
            string guiVerison = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).ProductVersion; // AssemblyInformationalVersion
            mtbVersionGUI.Text = guiVerison;

            //빌드 시간 표시
            Utils util = new Utils();
            PrintDebugMsg($"Communication Test Program {guiVerison} \r\nBuild Date: {util.Get_BuildDateTime()}\r\n");

            // 로그 설정
            Log = new LogTextBox(mtbDebug);
            // MtbDebugMaxLines.TextChanged += new System.EventHandler(MtbDebugMaxLines_TextChanged);
            Log.IsWiteFile = false;

            // 서브 폼 로드
            // 시리얼 테스트 페이지
            FormSerial = new SerialPage() { TopLevel = false, StartPosition = FormStartPosition.Manual, IsLayoutHeader = false };
            FormSerial.DebugMessageEvent += new SerialPage.DebugMessageHandler(PrintDebugMsg);
            tabSerial.Controls.Add(FormSerial);
            FormSerial.Show();

            // UDP 테스트 페이지
            FormUdp = new UdpPage() { TopLevel = false, Location = new System.Drawing.Point(0,0), IsLayoutHeader = false };
            FormUdp.DebugMessageEvent += new UdpPage.DebugMessageHandler(PrintDebugMsg);
            tabUdp.Controls.Add(FormUdp);
            FormUdp.Show();

            // TCP 테스트 페이지
            FormTcp = new TcpPage() { TopLevel = false, Location = new System.Drawing.Point(0, 0), IsLayoutHeader = false };
            FormTcp.DebugMessageEvent += new TcpPage.DebugMessageHandler(PrintDebugMsg);
            tabTcp.Controls.Add(FormTcp);
            FormTcp.Show();

            // 설정 페이지
            FormConfig = new ConfigPage() { TopLevel = false, StartPosition = FormStartPosition.Manual};
            FormConfig.DebugMessageEvent += new ConfigPage.DebugMessageHandler(PrintDebugMsg);
            tabConfig.Controls.Add(FormConfig);
            FormConfig.Show();

        }

        /// <summary>
        /// 폼 종료 시 실행되는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            // 서브 폼 종료
            FormConfig.Close();

            // 로그 파일 종료
            Log.Close();
        }

        /// <summary>
        /// 폼 종료 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MbtnClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void panelTOP_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mlbProjectTitle.Capture = false;
                panelTOP.Capture = false;
                const int WM_NCLBUTTONDOWN = 0x00A1;
                const int HTCAPTION = 2;
                Message message = Message.Create(Handle, WM_NCLBUTTONDOWN, new IntPtr(HTCAPTION), IntPtr.Zero); DefWndProc(ref message);
            }
        }

        private void mswThemaMode_CheckedChanged(object sender, EventArgs e)
        {
            if (mswThemaMode.Checked) // 만약 체크박스가 체크되어 있다면
            {
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT; // MaterialSkinManager의 테마를 LIGHT로 설정
            }
            else // 체크박스가 체크되어 있지 않다면
            {
                materialSkinManager.Theme = MaterialSkinManager.Themes.DARK; // MaterialSkinManager의 테마를 DARK로 설정
            }

            mswThemaMode.Text = mswThemaMode.Checked ? "Light" : "Dark"; // 체크박스의 텍스트를 "Light" 또는 "Dark"로 설정
            new MaterialSnackBar($"{mswThemaMode.Text} mode selected.", 1000, "OK", true).Show(this); // MaterialSnackBar를 생성하여 "Light mode selected." 또는 "Dark mode selected." 메시지를 1초 동안 보여줌
        }

        private void PrintDebugMsg(string msg)
        {
            mtbDebug.Text += msg;
            // Log?.Write(msg, true);
        }

        private void PrintDebugMsg(string msg, bool IsVisible)
        {
            if (IsVisible)
            {
                // mtbDebug.Text += msg;
                Log.Write(msg, IsVisible);
            }
            else
            {
                //mlbDebugMsg.Text = "";
            }
        }

        private void MbtnOpenSerial_Click(object sender, EventArgs e)
        {
            SerialPage _SerialForm = new SerialPage() { IsLayoutHeader = true, StartPosition = FormStartPosition.WindowsDefaultLocation};
            _SerialForm.DebugMessageEvent += new SerialPage.DebugMessageHandler(PrintDebugMsg);
            _SerialForm.Show();
        }

        private void MbtnOpenUdp_Click(object sender, EventArgs e)
        {
            UdpPage _UdpForm = new UdpPage() { IsLayoutHeader = true, StartPosition = FormStartPosition.WindowsDefaultLocation };
            _UdpForm.DebugMessageEvent += new UdpPage.DebugMessageHandler(PrintDebugMsg);
            _UdpForm.Show();
        }

        private void MbtnOpenTcp_Click(object sender, EventArgs e)
        {
            TcpPage _TcpForm = new TcpPage() { IsLayoutHeader = true, StartPosition = FormStartPosition.WindowsDefaultLocation };
            _TcpForm.DebugMessageEvent += new TcpPage.DebugMessageHandler(PrintDebugMsg);
            _TcpForm.Show();
        }
    }
}
