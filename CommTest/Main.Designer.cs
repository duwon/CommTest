namespace CommTest
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.mlbProjectTitle = new MaterialSkin.Controls.MaterialLabel();
            this.mtbVersionGUI = new MaterialSkin.Controls.MaterialTextBox();
            this.panelTOP = new System.Windows.Forms.Panel();
            this.MbtnClose = new MaterialSkin.Controls.MaterialButton();
            this.mswThemaMode = new MaterialSkin.Controls.MaterialSwitch();
            this.mDividerTop = new MaterialSkin.Controls.MaterialDivider();
            this.MTabControl = new MaterialSkin.Controls.MaterialTabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.mtbDebug = new MaterialSkin.Controls.MaterialMultiLineTextBox();
            this.MbtnOpenUdp = new MaterialSkin.Controls.MaterialButton();
            this.MbtnOpenSerial = new MaterialSkin.Controls.MaterialButton();
            this.tabSerial = new System.Windows.Forms.TabPage();
            this.tabUdp = new System.Windows.Forms.TabPage();
            this.tabTcp = new System.Windows.Forms.TabPage();
            this.tabConfig = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.MbtnOpenTcp = new MaterialSkin.Controls.MaterialButton();
            this.MTabControl.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mlbProjectTitle
            // 
            this.mlbProjectTitle.AutoSize = true;
            this.mlbProjectTitle.Depth = 0;
            this.mlbProjectTitle.Font = new System.Drawing.Font("Roboto", 34F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.mlbProjectTitle.FontType = MaterialSkin.MaterialSkinManager.fontType.H4;
            this.mlbProjectTitle.Location = new System.Drawing.Point(20, 8);
            this.mlbProjectTitle.MouseState = MaterialSkin.MouseState.HOVER;
            this.mlbProjectTitle.Name = "mlbProjectTitle";
            this.mlbProjectTitle.Size = new System.Drawing.Size(457, 41);
            this.mlbProjectTitle.TabIndex = 111;
            this.mlbProjectTitle.Text = "Communication Test Program";
            this.mlbProjectTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTOP_MouseDown);
            // 
            // mtbVersionGUI
            // 
            this.mtbVersionGUI.AnimateReadOnly = false;
            this.mtbVersionGUI.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mtbVersionGUI.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.mtbVersionGUI.Depth = 0;
            this.mtbVersionGUI.Enabled = false;
            this.mtbVersionGUI.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.mtbVersionGUI.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.mtbVersionGUI.Hint = "Version";
            this.mtbVersionGUI.LeadingIcon = null;
            this.mtbVersionGUI.Location = new System.Drawing.Point(537, 5);
            this.mtbVersionGUI.MaxLength = 50;
            this.mtbVersionGUI.MouseState = MaterialSkin.MouseState.OUT;
            this.mtbVersionGUI.Multiline = false;
            this.mtbVersionGUI.Name = "mtbVersionGUI";
            this.mtbVersionGUI.Size = new System.Drawing.Size(68, 50);
            this.mtbVersionGUI.TabIndex = 106;
            this.mtbVersionGUI.Text = "1.0.0";
            this.mtbVersionGUI.TrailingIcon = null;
            // 
            // panelTOP
            // 
            this.panelTOP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTOP.BackColor = System.Drawing.Color.Transparent;
            this.panelTOP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelTOP.Location = new System.Drawing.Point(0, 0);
            this.panelTOP.Name = "panelTOP";
            this.panelTOP.Size = new System.Drawing.Size(1847, 59);
            this.panelTOP.TabIndex = 111;
            this.panelTOP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTOP_MouseDown);
            // 
            // MbtnClose
            // 
            this.MbtnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MbtnClose.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.MbtnClose.Depth = 0;
            this.MbtnClose.HighEmphasis = true;
            this.MbtnClose.Icon = null;
            this.MbtnClose.Location = new System.Drawing.Point(914, 8);
            this.MbtnClose.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MbtnClose.MouseState = MaterialSkin.MouseState.HOVER;
            this.MbtnClose.Name = "MbtnClose";
            this.MbtnClose.NoAccentTextColor = System.Drawing.Color.Empty;
            this.MbtnClose.Size = new System.Drawing.Size(66, 36);
            this.MbtnClose.TabIndex = 175;
            this.MbtnClose.Text = "CLOSE";
            this.MbtnClose.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.MbtnClose.UseAccentColor = false;
            this.MbtnClose.UseVisualStyleBackColor = true;
            this.MbtnClose.Click += new System.EventHandler(this.MbtnClose_Click);
            // 
            // mswThemaMode
            // 
            this.mswThemaMode.AutoSize = true;
            this.mswThemaMode.Depth = 0;
            this.mswThemaMode.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.mswThemaMode.Location = new System.Drawing.Point(799, 9);
            this.mswThemaMode.Margin = new System.Windows.Forms.Padding(0);
            this.mswThemaMode.MouseLocation = new System.Drawing.Point(-1, -1);
            this.mswThemaMode.MouseState = MaterialSkin.MouseState.HOVER;
            this.mswThemaMode.Name = "mswThemaMode";
            this.mswThemaMode.Ripple = true;
            this.mswThemaMode.Size = new System.Drawing.Size(91, 37);
            this.mswThemaMode.TabIndex = 163;
            this.mswThemaMode.Text = "Dark";
            this.mswThemaMode.UseVisualStyleBackColor = true;
            this.mswThemaMode.CheckedChanged += new System.EventHandler(this.mswThemaMode_CheckedChanged);
            // 
            // mDividerTop
            // 
            this.mDividerTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.mDividerTop.Depth = 0;
            this.mDividerTop.Location = new System.Drawing.Point(0, 59);
            this.mDividerTop.MouseState = MaterialSkin.MouseState.HOVER;
            this.mDividerTop.Name = "mDividerTop";
            this.mDividerTop.Size = new System.Drawing.Size(4096, 1);
            this.mDividerTop.TabIndex = 178;
            this.mDividerTop.Text = "materialDivider4";
            // 
            // MTabControl
            // 
            this.MTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MTabControl.Controls.Add(this.tabMain);
            this.MTabControl.Controls.Add(this.tabSerial);
            this.MTabControl.Controls.Add(this.tabUdp);
            this.MTabControl.Controls.Add(this.tabTcp);
            this.MTabControl.Controls.Add(this.tabConfig);
            this.MTabControl.Depth = 0;
            this.MTabControl.ImageList = this.imageList1;
            this.MTabControl.Location = new System.Drawing.Point(3, 60);
            this.MTabControl.MouseState = MaterialSkin.MouseState.HOVER;
            this.MTabControl.Multiline = true;
            this.MTabControl.Name = "MTabControl";
            this.MTabControl.SelectedIndex = 0;
            this.MTabControl.Size = new System.Drawing.Size(1042, 698);
            this.MTabControl.TabIndex = 179;
            // 
            // tabMain
            // 
            this.tabMain.BackColor = System.Drawing.SystemColors.Control;
            this.tabMain.Controls.Add(this.MbtnOpenTcp);
            this.tabMain.Controls.Add(this.materialTabSelector1);
            this.tabMain.Controls.Add(this.mtbDebug);
            this.tabMain.Controls.Add(this.MbtnOpenUdp);
            this.tabMain.Controls.Add(this.MbtnOpenSerial);
            this.tabMain.ImageKey = "main-menu.png";
            this.tabMain.Location = new System.Drawing.Point(4, 24);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(1034, 670);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Main";
            // 
            // mtbDebug
            // 
            this.mtbDebug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mtbDebug.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mtbDebug.Depth = 0;
            this.mtbDebug.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mtbDebug.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.mtbDebug.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.mtbDebug.Location = new System.Drawing.Point(3, 179);
            this.mtbDebug.MouseState = MaterialSkin.MouseState.HOVER;
            this.mtbDebug.Name = "mtbDebug";
            this.mtbDebug.Size = new System.Drawing.Size(1028, 488);
            this.mtbDebug.TabIndex = 176;
            this.mtbDebug.Text = "";
            // 
            // MbtnOpenUdp
            // 
            this.MbtnOpenUdp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MbtnOpenUdp.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.MbtnOpenUdp.Depth = 0;
            this.MbtnOpenUdp.HighEmphasis = true;
            this.MbtnOpenUdp.Icon = null;
            this.MbtnOpenUdp.Location = new System.Drawing.Point(397, 63);
            this.MbtnOpenUdp.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MbtnOpenUdp.MouseState = MaterialSkin.MouseState.HOVER;
            this.MbtnOpenUdp.Name = "MbtnOpenUdp";
            this.MbtnOpenUdp.NoAccentTextColor = System.Drawing.Color.Empty;
            this.MbtnOpenUdp.Size = new System.Drawing.Size(64, 36);
            this.MbtnOpenUdp.TabIndex = 12;
            this.MbtnOpenUdp.Text = "UDP";
            this.MbtnOpenUdp.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.MbtnOpenUdp.UseAccentColor = false;
            this.MbtnOpenUdp.UseVisualStyleBackColor = true;
            this.MbtnOpenUdp.Click += new System.EventHandler(this.MbtnOpenUdp_Click);
            // 
            // MbtnOpenSerial
            // 
            this.MbtnOpenSerial.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MbtnOpenSerial.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.MbtnOpenSerial.Depth = 0;
            this.MbtnOpenSerial.HighEmphasis = true;
            this.MbtnOpenSerial.Icon = null;
            this.MbtnOpenSerial.Location = new System.Drawing.Point(230, 63);
            this.MbtnOpenSerial.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MbtnOpenSerial.MouseState = MaterialSkin.MouseState.HOVER;
            this.MbtnOpenSerial.Name = "MbtnOpenSerial";
            this.MbtnOpenSerial.NoAccentTextColor = System.Drawing.Color.Empty;
            this.MbtnOpenSerial.Size = new System.Drawing.Size(70, 36);
            this.MbtnOpenSerial.TabIndex = 1;
            this.MbtnOpenSerial.Text = "Serial";
            this.MbtnOpenSerial.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.MbtnOpenSerial.UseAccentColor = false;
            this.MbtnOpenSerial.UseVisualStyleBackColor = true;
            this.MbtnOpenSerial.Click += new System.EventHandler(this.MbtnOpenSerial_Click);
            // 
            // tabSerial
            // 
            this.tabSerial.ImageKey = "ico_serial.png";
            this.tabSerial.Location = new System.Drawing.Point(4, 24);
            this.tabSerial.Name = "tabSerial";
            this.tabSerial.Padding = new System.Windows.Forms.Padding(3);
            this.tabSerial.Size = new System.Drawing.Size(1034, 670);
            this.tabSerial.TabIndex = 1;
            this.tabSerial.Text = "Serial";
            // 
            // tabUdp
            // 
            this.tabUdp.ImageKey = "ico_udp.png";
            this.tabUdp.Location = new System.Drawing.Point(4, 24);
            this.tabUdp.Name = "tabUdp";
            this.tabUdp.Padding = new System.Windows.Forms.Padding(3);
            this.tabUdp.Size = new System.Drawing.Size(1034, 670);
            this.tabUdp.TabIndex = 3;
            this.tabUdp.Text = "UDP";
            this.tabUdp.UseVisualStyleBackColor = true;
            // 
            // tabTcp
            // 
            this.tabTcp.ImageKey = "ico_tcp.png";
            this.tabTcp.Location = new System.Drawing.Point(4, 24);
            this.tabTcp.Name = "tabTcp";
            this.tabTcp.Padding = new System.Windows.Forms.Padding(3);
            this.tabTcp.Size = new System.Drawing.Size(1034, 670);
            this.tabTcp.TabIndex = 4;
            this.tabTcp.Text = "TCP";
            this.tabTcp.UseVisualStyleBackColor = true;
            // 
            // tabConfig
            // 
            this.tabConfig.ImageKey = "ico_gear.png";
            this.tabConfig.Location = new System.Drawing.Point(4, 24);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfig.Size = new System.Drawing.Size(1034, 670);
            this.tabConfig.TabIndex = 2;
            this.tabConfig.Text = "Config";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "main-menu.png");
            this.imageList1.Images.SetKeyName(1, "serialport.png");
            this.imageList1.Images.SetKeyName(2, "setting.png");
            this.imageList1.Images.SetKeyName(3, "ico_serial.png");
            this.imageList1.Images.SetKeyName(4, "ico_gear.png");
            this.imageList1.Images.SetKeyName(5, "ico_tcp.png");
            this.imageList1.Images.SetKeyName(6, "ico_udp.png");
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BaseTabControl = this.MTabControl;
            this.materialTabSelector1.CharacterCasing = MaterialSkin.Controls.MaterialTabSelector.CustomCharacterCasing.Normal;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTabSelector1.Location = new System.Drawing.Point(6, 6);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(1022, 48);
            this.materialTabSelector1.TabIndex = 177;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // MbtnOpenTcp
            // 
            this.MbtnOpenTcp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MbtnOpenTcp.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.MbtnOpenTcp.Depth = 0;
            this.MbtnOpenTcp.HighEmphasis = true;
            this.MbtnOpenTcp.Icon = null;
            this.MbtnOpenTcp.Location = new System.Drawing.Point(553, 63);
            this.MbtnOpenTcp.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MbtnOpenTcp.MouseState = MaterialSkin.MouseState.HOVER;
            this.MbtnOpenTcp.Name = "MbtnOpenTcp";
            this.MbtnOpenTcp.NoAccentTextColor = System.Drawing.Color.Empty;
            this.MbtnOpenTcp.Size = new System.Drawing.Size(64, 36);
            this.MbtnOpenTcp.TabIndex = 178;
            this.MbtnOpenTcp.Text = "TCP";
            this.MbtnOpenTcp.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.MbtnOpenTcp.UseAccentColor = false;
            this.MbtnOpenTcp.UseVisualStyleBackColor = true;
            this.MbtnOpenTcp.Click += new System.EventHandler(this.MbtnOpenTcp_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 760);
            this.Controls.Add(this.mtbVersionGUI);
            this.Controls.Add(this.mDividerTop);
            this.Controls.Add(this.MbtnClose);
            this.Controls.Add(this.mswThemaMode);
            this.Controls.Add(this.mlbProjectTitle);
            this.Controls.Add(this.panelTOP);
            this.Controls.Add(this.MTabControl);
            this.DrawerAutoShow = true;
            this.DrawerShowIconsWhenHidden = true;
            this.DrawerTabControl = this.MTabControl;
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.StatusAndActionBar_None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Main";
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.Text = "통신 테스트 프로그램";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.MTabControl.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel mlbProjectTitle;
        private MaterialSkin.Controls.MaterialTextBox mtbVersionGUI;
        private System.Windows.Forms.Panel panelTOP;
        private MaterialSkin.Controls.MaterialButton MbtnClose;
        private MaterialSkin.Controls.MaterialSwitch mswThemaMode;
        private MaterialSkin.Controls.MaterialDivider mDividerTop;
        private MaterialSkin.Controls.MaterialTabControl MTabControl;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TabPage tabSerial;
        private System.Windows.Forms.TabPage tabConfig;
        private System.Windows.Forms.ImageList imageList1;
        private MaterialSkin.Controls.MaterialButton MbtnOpenSerial;
        private MaterialSkin.Controls.MaterialButton MbtnOpenUdp;
        private System.Windows.Forms.TabPage tabUdp;
        private System.Windows.Forms.TabPage tabTcp;
        private MaterialSkin.Controls.MaterialMultiLineTextBox mtbDebug;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialButton MbtnOpenTcp;
    }
}

