using MaterialSkin;
using MaterialSkin.Controls;
using System.Windows.Forms;
using System;

namespace CommTest
{
    public partial class Main : MaterialForm
    {
        /// <summary>
        /// materialSkinManager를 사용하기 위한 변수
        /// </summary>
        private readonly MaterialSkinManager materialSkinManager;

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
            this.MaximumSize = new System.Drawing.Size(1048, 760);
            this.MinimumSize = new System.Drawing.Size(1048, 760);

        }

        /// <summary>
        /// 폼 종료 시 실행되는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {

        }

        /// <summary>
        /// 폼 종료 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MbtnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
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
    }
}
