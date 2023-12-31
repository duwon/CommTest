﻿namespace CommTest
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
            this.tabSerial = new System.Windows.Forms.TabPage();
            this.tabConfig = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.MTabControl.SuspendLayout();
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
            this.tabMain.ImageKey = "main-menu.png";
            this.tabMain.Location = new System.Drawing.Point(4, 24);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(1034, 670);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Main";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // tabSerial
            // 
            this.tabSerial.ImageKey = "serialport.png";
            this.tabSerial.Location = new System.Drawing.Point(4, 24);
            this.tabSerial.Name = "tabSerial";
            this.tabSerial.Padding = new System.Windows.Forms.Padding(3);
            this.tabSerial.Size = new System.Drawing.Size(1034, 670);
            this.tabSerial.TabIndex = 1;
            this.tabSerial.Text = "Serial";
            // 
            // tabConfig
            // 
            this.tabConfig.ImageKey = "setting.png";
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
            this.Text = "CommTest";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.MTabControl.ResumeLayout(false);
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
    }
}

