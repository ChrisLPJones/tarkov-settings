namespace tarkov_settings
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.profilePanel = new System.Windows.Forms.Panel();
            this.MiscsButton = new System.Windows.Forms.Button();
            this.ColorButton = new System.Windows.Forms.Button();
            this.ColorPanel = new System.Windows.Forms.Panel();
            this.alwaysOnCheckBox = new System.Windows.Forms.CheckBox();
            this.darkModeCheckBox = new System.Windows.Forms.CheckBox();
            this.minimizeStartCheckBox = new System.Windows.Forms.CheckBox();
            this.DisplayCombo = new System.Windows.Forms.ComboBox();
            this.DVLGroupBox = new System.Windows.Forms.GroupBox();
            this.DVLPanel = new System.Windows.Forms.Panel();
            this.DVLLabel = new System.Windows.Forms.Label();
            this.DVLBar = new System.Windows.Forms.TrackBar();
            this.DVLText = new System.Windows.Forms.TextBox();
            this.colorGroupBox = new System.Windows.Forms.GroupBox();
            this.colorTablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.brightnessPanel = new System.Windows.Forms.Panel();
            this.BrightnessBar = new System.Windows.Forms.TrackBar();
            this.BrightnessLabel = new System.Windows.Forms.Label();
            this.BrightnessText = new System.Windows.Forms.TextBox();
            this.contrastPanel = new System.Windows.Forms.Panel();
            this.ContrastBar = new System.Windows.Forms.TrackBar();
            this.ContrastText = new System.Windows.Forms.TextBox();
            this.ContrastLabel = new System.Windows.Forms.Label();
            this.gammaPanel = new System.Windows.Forms.Panel();
            this.GammaText = new System.Windows.Forms.TextBox();
            this.GammaBar = new System.Windows.Forms.TrackBar();
            this.GammaLabel = new System.Windows.Forms.Label();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.enableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightnessToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contrastToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.gammaToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dvlToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.profilePanel.SuspendLayout();
            this.ColorPanel.SuspendLayout();
            this.DVLGroupBox.SuspendLayout();
            this.DVLPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DVLBar)).BeginInit();
            this.colorGroupBox.SuspendLayout();
            this.colorTablePanel.SuspendLayout();
            this.brightnessPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessBar)).BeginInit();
            this.contrastPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContrastBar)).BeginInit();
            this.gammaPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GammaBar)).BeginInit();
            this.trayMenuStrip.SuspendLayout();
            this.SuspendLayout();
            //
            // profilePanel
            //
            this.profilePanel.Controls.Add(this.MiscsButton);
            this.profilePanel.Controls.Add(this.ColorButton);
            this.profilePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.profilePanel.Name = "profilePanel";
            this.profilePanel.Size = new System.Drawing.Size(734, 36);
            this.profilePanel.TabIndex = 20;
            //
            // MiscsButton  (Profile 1)
            //
            this.MiscsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MiscsButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MiscsButton.Location = new System.Drawing.Point(0, 0);
            this.MiscsButton.Name = "MiscsButton";
            this.MiscsButton.Size = new System.Drawing.Size(367, 36);
            this.MiscsButton.TabIndex = 0;
            this.MiscsButton.Text = "Profile 1";
            this.MiscsButton.UseVisualStyleBackColor = true;
            this.MiscsButton.Click += new System.EventHandler(this.Profile1Button_Click);
            this.MiscsButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ProfileButton_MouseDown);
            //
            // ColorButton  (Profile 2)
            //
            this.ColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColorButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColorButton.Location = new System.Drawing.Point(367, 0);
            this.ColorButton.Name = "ColorButton";
            this.ColorButton.Size = new System.Drawing.Size(367, 36);
            this.ColorButton.TabIndex = 1;
            this.ColorButton.Text = "Profile 2";
            this.ColorButton.UseVisualStyleBackColor = true;
            this.ColorButton.Click += new System.EventHandler(this.Profile2Button_Click);
            this.ColorButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ProfileButton_MouseDown);
            //
            // ColorPanel
            //
            this.ColorPanel.Controls.Add(this.alwaysOnCheckBox);
            this.ColorPanel.Controls.Add(this.darkModeCheckBox);
            this.ColorPanel.Controls.Add(this.minimizeStartCheckBox);
            this.ColorPanel.Controls.Add(this.DisplayCombo);
            this.ColorPanel.Controls.Add(this.DVLGroupBox);
            this.ColorPanel.Controls.Add(this.colorGroupBox);
            this.ColorPanel.Controls.Add(this.profilePanel);
            this.ColorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ColorPanel.Location = new System.Drawing.Point(0, 0);
            this.ColorPanel.Name = "ColorPanel";
            this.ColorPanel.Size = new System.Drawing.Size(734, 372);
            this.ColorPanel.TabIndex = 0;
            //
            // alwaysOnCheckBox
            //
            this.alwaysOnCheckBox.AutoSize = true;
            this.alwaysOnCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.alwaysOnCheckBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.alwaysOnCheckBox.Location = new System.Drawing.Point(3, 336);
            this.alwaysOnCheckBox.Name = "alwaysOnCheckBox";
            this.alwaysOnCheckBox.Size = new System.Drawing.Size(120, 26);
            this.alwaysOnCheckBox.TabIndex = 17;
            this.alwaysOnCheckBox.Text = "Always On";
            this.alwaysOnCheckBox.UseVisualStyleBackColor = false;
            this.alwaysOnCheckBox.CheckedChanged += new System.EventHandler(this.AlwaysOnCheckBox_CheckedChanged);
            //
            // darkModeCheckBox
            //
            this.darkModeCheckBox.AutoSize = true;
            this.darkModeCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.darkModeCheckBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.darkModeCheckBox.Location = new System.Drawing.Point(135, 336);
            this.darkModeCheckBox.Name = "darkModeCheckBox";
            this.darkModeCheckBox.Size = new System.Drawing.Size(120, 26);
            this.darkModeCheckBox.TabIndex = 18;
            this.darkModeCheckBox.Text = "Dark Mode";
            this.darkModeCheckBox.UseVisualStyleBackColor = false;
            this.darkModeCheckBox.CheckedChanged += new System.EventHandler(this.DarkModeCheckBox_CheckedChanged);
            //
            // minimizeStartCheckBox
            //
            this.minimizeStartCheckBox.AutoSize = true;
            this.minimizeStartCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.minimizeStartCheckBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.minimizeStartCheckBox.Location = new System.Drawing.Point(289, 336);
            this.minimizeStartCheckBox.Name = "minimizeStartCheckBox";
            this.minimizeStartCheckBox.Size = new System.Drawing.Size(286, 26);
            this.minimizeStartCheckBox.TabIndex = 16;
            this.minimizeStartCheckBox.Text = "Minimize to Tray on Start";
            this.minimizeStartCheckBox.UseVisualStyleBackColor = false;
            this.minimizeStartCheckBox.CheckedChanged += new System.EventHandler(this.CheckOnMinimizeToTray);
            // 
            // DisplayCombo
            // 
            this.DisplayCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DisplayCombo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.DisplayCombo.FormattingEnabled = true;
            this.DisplayCombo.Location = new System.Drawing.Point(558, 332);
            this.DisplayCombo.Name = "DisplayCombo";
            this.DisplayCombo.Size = new System.Drawing.Size(139, 30);
            this.DisplayCombo.TabIndex = 15;
            this.DisplayCombo.SelectedValueChanged += new System.EventHandler(this.DisplayCombo_SelectedValueChanged);
            // 
            // DVLGroupBox
            // 
            this.DVLGroupBox.Controls.Add(this.DVLPanel);
            this.DVLGroupBox.Location = new System.Drawing.Point(573, 40);
            this.DVLGroupBox.Name = "DVLGroupBox";
            this.DVLGroupBox.Size = new System.Drawing.Size(155, 288);
            this.DVLGroupBox.TabIndex = 13;
            this.DVLGroupBox.TabStop = false;
            this.DVLGroupBox.Text = "DVL";
            // 
            // DVLPanel
            // 
            this.DVLPanel.Controls.Add(this.DVLLabel);
            this.DVLPanel.Controls.Add(this.DVLBar);
            this.DVLPanel.Controls.Add(this.DVLText);
            this.DVLPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DVLPanel.Location = new System.Drawing.Point(3, 25);
            this.DVLPanel.Name = "DVLPanel";
            this.DVLPanel.Size = new System.Drawing.Size(139, 279);
            this.DVLPanel.TabIndex = 0;
            // 
            // DVLLabel
            // 
            this.DVLLabel.AutoSize = true;
            this.DVLLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DVLLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DVLLabel.Location = new System.Drawing.Point(13, 11);
            this.DVLLabel.Name = "DVLLabel";
            this.DVLLabel.Size = new System.Drawing.Size(170, 44);
            this.DVLLabel.TabIndex = 10;
            this.DVLLabel.Text = "Digital Vibrance\r\n(Saturation)\r\n";
            this.DVLLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dvlToolTip.SetToolTip(this.DVLLabel, "Double-click to reset");
            this.DVLLabel.DoubleClick += new System.EventHandler(this.ColorLabel_DClick);
            // 
            // DVLBar
            // 
            this.DVLBar.Location = new System.Drawing.Point(56, 42);
            this.DVLBar.Maximum = 100;
            this.DVLBar.Name = "DVLBar";
            this.DVLBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.DVLBar.Size = new System.Drawing.Size(69, 184);
            this.DVLBar.TabIndex = 9;
            this.DVLBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.DVLBar.Value = 50;
            this.DVLBar.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
            // 
            // DVLText
            // 
            this.DVLText.Location = new System.Drawing.Point(46, 232);
            this.DVLText.Name = "DVLText";
            this.DVLText.ReadOnly = true;
            this.DVLText.Size = new System.Drawing.Size(41, 29);
            this.DVLText.TabIndex = 11;
            this.DVLText.Text = "50";
            this.DVLText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // colorGroupBox
            // 
            this.colorGroupBox.Controls.Add(this.colorTablePanel);
            this.colorGroupBox.Location = new System.Drawing.Point(3, 40);
            this.colorGroupBox.Name = "colorGroupBox";
            this.colorGroupBox.Size = new System.Drawing.Size(565, 288);
            this.colorGroupBox.TabIndex = 12;
            this.colorGroupBox.TabStop = false;
            this.colorGroupBox.Text = "Color";
            // 
            // colorTablePanel
            // 
            this.colorTablePanel.ColumnCount = 1;
            this.colorTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.colorTablePanel.Controls.Add(this.brightnessPanel, 0, 0);
            this.colorTablePanel.Controls.Add(this.contrastPanel, 0, 1);
            this.colorTablePanel.Controls.Add(this.gammaPanel, 0, 2);
            this.colorTablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorTablePanel.Location = new System.Drawing.Point(3, 25);
            this.colorTablePanel.Name = "colorTablePanel";
            this.colorTablePanel.RowCount = 3;
            this.colorTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.colorTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.colorTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.colorTablePanel.Size = new System.Drawing.Size(484, 279);
            this.colorTablePanel.TabIndex = 1;
            // 
            // brightnessPanel
            // 
            this.brightnessPanel.Controls.Add(this.BrightnessBar);
            this.brightnessPanel.Controls.Add(this.BrightnessLabel);
            this.brightnessPanel.Controls.Add(this.BrightnessText);
            this.brightnessPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brightnessPanel.Location = new System.Drawing.Point(3, 3);
            this.brightnessPanel.Name = "brightnessPanel";
            this.brightnessPanel.Size = new System.Drawing.Size(478, 87);
            this.brightnessPanel.TabIndex = 0;
            // 
            // BrightnessBar
            // 
            this.BrightnessBar.Location = new System.Drawing.Point(13, 27);
            this.BrightnessBar.Maximum = 100;
            this.BrightnessBar.Minimum = 0;
            this.BrightnessBar.Name = "BrightnessBar";
            this.BrightnessBar.Size = new System.Drawing.Size(397, 69);
            this.BrightnessBar.TabIndex = 18;
            this.BrightnessBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.BrightnessBar.Value = 50;
            this.BrightnessBar.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
            // 
            // BrightnessLabel
            // 
            this.BrightnessLabel.AutoSize = true;
            this.BrightnessLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BrightnessLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BrightnessLabel.Location = new System.Drawing.Point(20, 10);
            this.BrightnessLabel.Name = "BrightnessLabel";
            this.BrightnessLabel.Size = new System.Drawing.Size(110, 22);
            this.BrightnessLabel.TabIndex = 21;
            this.BrightnessLabel.Text = "Brightness";
            this.brightnessToolTip.SetToolTip(this.BrightnessLabel, "Double-click to reset");
            this.BrightnessLabel.DoubleClick += new System.EventHandler(this.ColorLabel_DClick);
            // 
            // BrightnessText
            // 
            this.BrightnessText.Location = new System.Drawing.Point(424, 27);
            this.BrightnessText.Name = "BrightnessText";
            this.BrightnessText.ReadOnly = true;
            this.BrightnessText.Size = new System.Drawing.Size(41, 29);
            this.BrightnessText.TabIndex = 24;
            this.BrightnessText.Text = "0.50";
            this.BrightnessText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // contrastPanel
            // 
            this.contrastPanel.Controls.Add(this.ContrastBar);
            this.contrastPanel.Controls.Add(this.ContrastText);
            this.contrastPanel.Controls.Add(this.ContrastLabel);
            this.contrastPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contrastPanel.Location = new System.Drawing.Point(3, 96);
            this.contrastPanel.Name = "contrastPanel";
            this.contrastPanel.Size = new System.Drawing.Size(478, 87);
            this.contrastPanel.TabIndex = 1;
            // 
            // ContrastBar
            // 
            this.ContrastBar.Location = new System.Drawing.Point(13, 39);
            this.ContrastBar.Maximum = 100;
            this.ContrastBar.Minimum = 0;
            this.ContrastBar.Name = "ContrastBar";
            this.ContrastBar.Size = new System.Drawing.Size(397, 69);
            this.ContrastBar.TabIndex = 19;
            this.ContrastBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ContrastBar.Value = 50;
            this.ContrastBar.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
            // 
            // ContrastText
            // 
            this.ContrastText.Location = new System.Drawing.Point(424, 39);
            this.ContrastText.Name = "ContrastText";
            this.ContrastText.ReadOnly = true;
            this.ContrastText.Size = new System.Drawing.Size(41, 29);
            this.ContrastText.TabIndex = 25;
            this.ContrastText.Text = "0.50";
            this.ContrastText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ContrastLabel
            // 
            this.ContrastLabel.AutoSize = true;
            this.ContrastLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ContrastLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ContrastLabel.Location = new System.Drawing.Point(20, 22);
            this.ContrastLabel.Name = "ContrastLabel";
            this.ContrastLabel.Size = new System.Drawing.Size(90, 22);
            this.ContrastLabel.TabIndex = 22;
            this.ContrastLabel.Text = "Contrast";
            this.contrastToolTip.SetToolTip(this.ContrastLabel, "Double-click to reset");
            this.ContrastLabel.DoubleClick += new System.EventHandler(this.ColorLabel_DClick);
            // 
            // gammaPanel
            // 
            this.gammaPanel.Controls.Add(this.GammaText);
            this.gammaPanel.Controls.Add(this.GammaBar);
            this.gammaPanel.Controls.Add(this.GammaLabel);
            this.gammaPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gammaPanel.Location = new System.Drawing.Point(3, 189);
            this.gammaPanel.Name = "gammaPanel";
            this.gammaPanel.Size = new System.Drawing.Size(478, 87);
            this.gammaPanel.TabIndex = 2;
            // 
            // GammaText
            // 
            this.GammaText.Location = new System.Drawing.Point(424, 40);
            this.GammaText.Name = "GammaText";
            this.GammaText.ReadOnly = true;
            this.GammaText.Size = new System.Drawing.Size(41, 29);
            this.GammaText.TabIndex = 26;
            this.GammaText.Text = "1.00";
            this.GammaText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GammaBar
            // 
            this.GammaBar.Location = new System.Drawing.Point(13, 40);
            this.GammaBar.Maximum = 280;
            this.GammaBar.Minimum = 40;
            this.GammaBar.Name = "GammaBar";
            this.GammaBar.Size = new System.Drawing.Size(397, 69);
            this.GammaBar.TabIndex = 20;
            this.GammaBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.GammaBar.Value = 100;
            this.GammaBar.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
            // 
            // GammaLabel
            // 
            this.GammaLabel.AutoSize = true;
            this.GammaLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GammaLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.GammaLabel.Location = new System.Drawing.Point(20, 23);
            this.GammaLabel.Name = "GammaLabel";
            this.GammaLabel.Size = new System.Drawing.Size(60, 22);
            this.GammaLabel.TabIndex = 23;
            this.GammaLabel.Text = "Gamma";
            this.gammaToolTip.SetToolTip(this.GammaLabel, "Double-click to reset");
            this.GammaLabel.DoubleClick += new System.EventHandler(this.ColorLabel_DClick);
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.trayMenuStrip;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "Tarkov Settings";
            this.trayIcon.Visible = true;
            this.trayIcon.DoubleClick += new System.EventHandler(this.ShowForm);
            // 
            // trayMenuStrip
            // 
            this.trayMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.trayMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableToolStripMenuItem,
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.trayMenuStrip.Name = "trayMenuStrip";
            this.trayMenuStrip.Size = new System.Drawing.Size(138, 100);
            // 
            // enableToolStripMenuItem
            // 
            this.enableToolStripMenuItem.Checked = true;
            this.enableToolStripMenuItem.CheckOnClick = true;
            this.enableToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableToolStripMenuItem.Name = "enableToolStripMenuItem";
            this.enableToolStripMenuItem.Size = new System.Drawing.Size(137, 32);
            this.enableToolStripMenuItem.Text = "Enable";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(137, 32);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.ShowForm);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(137, 32);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitFormClicked);
            // 
            // brightnessToolTip
            // 
            this.brightnessToolTip.IsBalloon = true;
            this.brightnessToolTip.ShowAlways = true;
            this.brightnessToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.brightnessToolTip.ToolTipTitle = "Brightness";
            // 
            // contrastToolTip
            // 
            this.contrastToolTip.IsBalloon = true;
            this.contrastToolTip.ShowAlways = true;
            this.contrastToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.contrastToolTip.ToolTipTitle = "Contrast";
            // 
            // gammaToolTip
            // 
            this.gammaToolTip.IsBalloon = true;
            this.gammaToolTip.ShowAlways = true;
            this.gammaToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.gammaToolTip.ToolTipTitle = "Gamma";
            // 
            // dvlToolTip
            // 
            this.dvlToolTip.IsBalloon = true;
            this.dvlToolTip.ShowAlways = true;
            this.dvlToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.dvlToolTip.ToolTipTitle = "Saturation";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(734, 372);
            this.Controls.Add(this.ColorPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Tarkov Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.profilePanel.ResumeLayout(false);
            this.ColorPanel.ResumeLayout(false);
            this.ColorPanel.PerformLayout();
            this.DVLGroupBox.ResumeLayout(false);
            this.DVLPanel.ResumeLayout(false);
            this.DVLPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DVLBar)).EndInit();
            this.colorGroupBox.ResumeLayout(false);
            this.colorTablePanel.ResumeLayout(false);
            this.brightnessPanel.ResumeLayout(false);
            this.brightnessPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessBar)).EndInit();
            this.contrastPanel.ResumeLayout(false);
            this.contrastPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContrastBar)).EndInit();
            this.gammaPanel.ResumeLayout(false);
            this.gammaPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GammaBar)).EndInit();
            this.trayMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel profilePanel;
        private System.Windows.Forms.Button MiscsButton;
        private System.Windows.Forms.Button ColorButton;
        private System.Windows.Forms.Panel ColorPanel;
        
        

        private System.Windows.Forms.GroupBox colorGroupBox;
        private System.Windows.Forms.TextBox DVLText;
        private System.Windows.Forms.Label DVLLabel;
        private System.Windows.Forms.TrackBar DVLBar;
        private System.Windows.Forms.TextBox GammaText;
        private System.Windows.Forms.TextBox ContrastText;
        private System.Windows.Forms.TextBox BrightnessText;
        private System.Windows.Forms.Label GammaLabel;
        private System.Windows.Forms.Label ContrastLabel;
        private System.Windows.Forms.Label BrightnessLabel;
        private System.Windows.Forms.TrackBar GammaBar;
        private System.Windows.Forms.TrackBar ContrastBar;
        private System.Windows.Forms.TrackBar BrightnessBar;
        private System.Windows.Forms.TableLayoutPanel colorTablePanel;
        private System.Windows.Forms.Panel brightnessPanel;
        private System.Windows.Forms.Panel contrastPanel;
        private System.Windows.Forms.Panel gammaPanel;
        private System.Windows.Forms.GroupBox DVLGroupBox;
        private System.Windows.Forms.Panel DVLPanel;
        private System.Windows.Forms.ComboBox DisplayCombo;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem enableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.CheckBox alwaysOnCheckBox;
        private System.Windows.Forms.CheckBox darkModeCheckBox;
        private System.Windows.Forms.CheckBox minimizeStartCheckBox;
        private System.Windows.Forms.ToolTip dvlToolTip;
        private System.Windows.Forms.ToolTip brightnessToolTip;
        private System.Windows.Forms.ToolTip contrastToolTip;
        private System.Windows.Forms.ToolTip gammaToolTip;
    }
}

