using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommTest
{
    public partial class ConfigPage : MaterialForm
    {
        public delegate void DebugMessageHandler(string msg, bool IsVisible);
        public event DebugMessageHandler DebugMessageEvent;

        private readonly MaterialSkinManager materialSkinManager;

        public ConfigPage()
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
        private void ConfigPage_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 폼 종료 시 실행되는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigPage_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
