using System;
using System.Drawing;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using tarkov_settings.Setting;
using tarkov_settings.GPU;

namespace tarkov_settings
{
    public partial class MainForm : Form
    {
        private ProcessMonitor pMonitor = ProcessMonitor.Instance;
        private IGPU gpu = GPUDevice.Instance;
        private AppSetting appSetting;

        private bool minimizeOnStart = false;

        public MainForm()
        {
            InitializeComponent();

            #region Load App Settings
            // Load Settings
            appSetting = AppSetting.Load();

            LoadProfile(appSetting.activeProfile);
            minimizeOnStart = appSetting.minimizeOnStart;
            this.minimizeStartCheckBox.Checked = minimizeOnStart;
            this.alwaysOnCheckBox.Checked = appSetting.alwaysOn;
            this.darkModeCheckBox.Checked = appSetting.darkMode;
            #endregion
            
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = String.Format("Tarkov Settings {0}", version);
            _ = new UpdateNotifier(version);

            // Saturation Initialize
            if (gpu.Vendor != GPUVendor.NVIDIA)
                DVLGroupBox.Enabled = false;

            #region Initialize Display
            // Initialize Display Dropdown
            foreach (string display in Display.displays)
            {
                DisplayCombo.Items.Add(display);
            }
            
            if(DisplayCombo.FindString(appSetting.display) != -1)
                DisplayCombo.SelectedIndex = DisplayCombo.FindString(appSetting.display);

            Display.Primary = (string)DisplayCombo.SelectedItem;

            // GPU is now loaded — if the active profile's DVL has never been set (sentinel -1),
            // sync to the system's current level so Always On causes no visible change.
            if (appSetting.profiles[appSetting.activeProfile].saturation < 0)
                DVL = gpu.Vendor == GPUVendor.NVIDIA ? ColorController.Instance.DVL : 50;
            #endregion

            // Initialize Process Monitor
            pMonitor.Parent = this;
            foreach (string pTarget in appSetting.pTargets)
            {
                pMonitor.Add(pTarget.ToLower());
            }
            pMonitor.Init();

            UpdateProfileButtons();

            if (appSetting.darkMode)
                ApplyTheme(dark: true);
        }

        #region BCGS Getter/Setter
        public double Brightness
        {
            get => BrightnessBar.Value / 100.0;
            set => BrightnessBar.Value = (int)(value * 100);
        }

        public double Contrast
        {
            get => ContrastBar.Value / 100.0;
            set => ContrastBar.Value = (int)(value * 100);
        }

        public double Gamma
        {
            get => GammaBar.Value / 100.0;
            set => GammaBar.Value = (int)(value * 100);
        }

        public int DVL
        {
            get => DVLBar.Value;
            set => DVLBar.Value = value;
        }

        public (double, double, double, int) GetColorValue()
        {
            return (
                BrightnessBar.Value / 100.0,
                ContrastBar.Value / 100.0,
                GammaBar.Value / 100.0,
                DVLBar.Value
                );
        }
        #endregion

        public bool IsEnabled { get => this.enableToolStripMenuItem.Checked; }
        public bool IsAlwaysOn { get => this.alwaysOnCheckBox.Checked; }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (minimizeOnStart)
            {
                this.Visible = false;
                this.ShowInTaskbar = false;
                this.trayIcon.ShowBalloonTip(
                    2500,
                    "Tarkov Settings Initailized!",
                    "Check out tray to modify your color setting",
                    ToolTipIcon.Info
                    );
            }
        }

        #region Control Event Handlers
        private void ColorLabel_DClick(object sender, EventArgs e)
        {
            var label = sender as Label;
            
            if (label.Equals(BrightnessLabel))
            {
                BrightnessBar.Value = 50;
            }
            else if (label.Equals(ContrastLabel))
            {
                ContrastBar.Value = 50;
            }
            else if (label.Equals(GammaLabel))
            {
                GammaBar.Value = 100;
            }
            else if (label.Equals(DVLLabel))
            {
                DVLBar.Value = 50;
            }
        }
        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            var trackBar = sender as TrackBar;

            if (trackBar.Equals(BrightnessBar))
            {
                BrightnessText.Text = (BrightnessBar.Value / 100.0).ToString("0.00");
            }
            else if (trackBar.Equals(ContrastBar))
            {
                ContrastText.Text = (ContrastBar.Value / 100.0).ToString("0.00");
            }
            else if (trackBar.Equals(GammaBar))
            {
                GammaText.Text = (GammaBar.Value / 100.0).ToString("0.00");
            }
            else if (trackBar.Equals(DVLBar))
            {
                DVLText.Text = DVLBar.Value.ToString();
            }

            if (IsAlwaysOn)
            {
                var (b, c, g, dvl) = GetColorValue();
                ColorController.Instance.SignalPreview(b, c, g, dvl);
            }
        }
        private void DisplayCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            Display.Primary = (string)DisplayCombo.SelectedItem;
        }
        #endregion

        private void ShowForm(object sender, EventArgs e)
        {
            this.Visible = true;
            this.ShowInTaskbar = true;
        }

        private void SaveSettings()
        {
            SaveCurrentToProfile(appSetting.activeProfile);
            appSetting.display = (string)DisplayCombo.SelectedItem;
            appSetting.minimizeOnStart = minimizeOnStart;
            appSetting.alwaysOn = this.alwaysOnCheckBox.Checked;
            appSetting.darkMode = this.darkModeCheckBox.Checked;
            appSetting.Save();
        }

        private void ExitFormClicked(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && minimizeOnStart)
            {
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                SaveSettings();
                this.trayIcon.Dispose();
                pMonitor.Close();
            }
        }

        private void CheckOnMinimizeToTray(object sender, EventArgs e)
        {
            this.minimizeOnStart = this.minimizeStartCheckBox.Checked;
        }

        private void AlwaysOnCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            pMonitor.ApplyCurrentState();
        }

        #region Profiles

        private void LoadProfile(int index)
        {
            var p = appSetting.profiles[index];
            Brightness = p.brightness;
            Contrast = p.contrast;
            Gamma = p.gamma;
            // If saturation has been saved use it directly.
            // If sentinel (-1), leave it; the post-GPU-load block in the constructor
            // will sync from the GPU once it is initialised.
            if (p.saturation >= 0)
                DVL = p.saturation;
        }

        private void SaveCurrentToProfile(int index)
        {
            var p = appSetting.profiles[index];
            p.brightness = Brightness;
            p.contrast = Contrast;
            p.gamma = Gamma;
            p.saturation = DVL;
        }

        private void SwitchProfile(int index)
        {
            if (index == appSetting.activeProfile) return;
            SaveCurrentToProfile(appSetting.activeProfile);
            appSetting.activeProfile = index;
            LoadProfile(index);
            UpdateProfileButtons();
            if (IsAlwaysOn) pMonitor.ApplyCurrentState();
        }

        private void UpdateProfileButtons()
        {
            MiscsButton.Text = appSetting.profiles[0].name;
            ColorButton.Text = appSetting.profiles[1].name;

            bool dark = darkModeCheckBox.Checked;

            Color active   = dark ? Color.FromArgb(75,  75,  75)  : Color.FromArgb(195, 195, 195);
            Color inactive = dark ? Color.FromArgb(42,  42,  42)  : Color.FromArgb(220, 220, 220);
            Color fg       = dark ? Color.FromArgb(210, 210, 210) : Color.FromArgb(30,  30,  30);
            Color border   = dark ? Color.FromArgb(90,  90,  90)  : Color.FromArgb(180, 180, 180);

            MiscsButton.BackColor = appSetting.activeProfile == 0 ? active : inactive;
            MiscsButton.ForeColor = fg;
            MiscsButton.FlatAppearance.BorderColor = border;

            ColorButton.BackColor = appSetting.activeProfile == 1 ? active : inactive;
            ColorButton.ForeColor = fg;
            ColorButton.FlatAppearance.BorderColor = border;
        }

        private void Profile1Button_Click(object sender, EventArgs e) => SwitchProfile(0);
        private void Profile2Button_Click(object sender, EventArgs e) => SwitchProfile(1);

        private void ProfileButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            int index = sender == MiscsButton ? 0 : 1;
            string newName = ShowRenameDialog(appSetting.profiles[index].name);
            if (!string.IsNullOrWhiteSpace(newName))
            {
                appSetting.profiles[index].name = newName;
                UpdateProfileButtons();
            }
        }

        private string ShowRenameDialog(string current)
        {
            using (var dlg = new Form())
            using (var txt = new TextBox())
            using (var ok  = new Button())
            {
                dlg.Text = "Rename Profile";
                dlg.ClientSize = new System.Drawing.Size(260, 78);
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.MaximizeBox = dlg.MinimizeBox = false;

                txt.Text = current;
                txt.Location = new System.Drawing.Point(10, 10);
                txt.Size = new System.Drawing.Size(240, 24);

                ok.Text = "OK";
                ok.DialogResult = DialogResult.OK;
                ok.Location = new System.Drawing.Point(90, 42);
                ok.Size = new System.Drawing.Size(80, 26);

                dlg.Controls.Add(txt);
                dlg.Controls.Add(ok);
                dlg.AcceptButton = ok;
                dlg.Shown += (s, e2) => { txt.SelectAll(); txt.Focus(); };

                return dlg.ShowDialog(this) == DialogResult.OK ? txt.Text.Trim() : null;
            }
        }

        #endregion

        private void DarkModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ApplyTheme(darkModeCheckBox.Checked);
            UpdateProfileButtons();
        }

        #region Dark Mode

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        // Dark palette
        private static readonly Color _darkBg      = Color.FromArgb(30, 30, 30);
        private static readonly Color _darkSurface  = Color.FromArgb(42, 42, 42);
        private static readonly Color _darkInput    = Color.FromArgb(56, 56, 56);
        private static readonly Color _darkText     = Color.FromArgb(210, 210, 210);

        // Light palette — mirrors dark structure with light values
        private static readonly Color _lightBg      = Color.FromArgb(235, 235, 235);
        private static readonly Color _lightSurface  = Color.FromArgb(245, 245, 245);
        private static readonly Color _lightInput    = Color.FromArgb(255, 255, 255);
        private static readonly Color _lightText     = Color.FromArgb(30, 30, 30);

        private void ApplyTheme(bool dark)
        {
            int flag = dark ? 1 : 0;
            DwmSetWindowAttribute(this.Handle, 20, ref flag, sizeof(int));

            this.BackColor = dark ? _darkBg : _lightBg;
            ApplyThemeToControls(this.Controls, dark);

            this.Invalidate(true);
        }

        private void ApplyThemeToControls(Control.ControlCollection controls, bool dark)
        {
            Color bg      = dark ? _darkSurface : _lightSurface;
            Color input   = dark ? _darkInput   : _lightInput;
            Color text    = dark ? _darkText     : _lightText;

            foreach (Control c in controls)
            {
                switch (c)
                {
                    case GroupBox gb:
                        gb.BackColor = bg;
                        gb.ForeColor = text;
                        break;
                    case Panel p:
                        p.BackColor = bg;
                        break;
                    case TextBox tb:
                        tb.BackColor = input;
                        tb.ForeColor = text;
                        break;
                    case ComboBox cb:
                        cb.BackColor = input;
                        cb.ForeColor = text;
                        break;
                    case TrackBar t:
                        t.BackColor = bg;
                        break;
                    case Label l:
                        l.ForeColor = text;
                        l.BackColor = Color.Transparent;
                        break;
                    case CheckBox chk:
                        chk.ForeColor = text;
                        chk.BackColor = Color.Transparent;
                        break;
                }

                if (c.HasChildren)
                    ApplyThemeToControls(c.Controls, dark);
            }
        }

        private class DarkColorTable : ProfessionalColorTable
        {
            private static readonly Color Bg      = Color.FromArgb(42, 42, 42);
            private static readonly Color Hover   = Color.FromArgb(65, 65, 65);
            private static readonly Color Pressed = Color.FromArgb(85, 85, 85);

            public override Color ToolStripGradientBegin              => Bg;
            public override Color ToolStripGradientMiddle             => Bg;
            public override Color ToolStripGradientEnd                => Bg;
            public override Color ToolStripContentPanelGradientBegin  => Bg;
            public override Color ToolStripContentPanelGradientEnd    => Bg;
            public override Color ImageMarginGradientBegin            => Bg;
            public override Color ImageMarginGradientMiddle           => Bg;
            public override Color ImageMarginGradientEnd              => Bg;
            public override Color MenuStripGradientBegin              => Bg;
            public override Color MenuStripGradientEnd                => Bg;
            public override Color OverflowButtonGradientBegin         => Bg;
            public override Color OverflowButtonGradientMiddle        => Bg;
            public override Color OverflowButtonGradientEnd           => Bg;
            public override Color ButtonSelectedHighlight             => Hover;
            public override Color ButtonSelectedGradientBegin         => Hover;
            public override Color ButtonSelectedGradientMiddle        => Hover;
            public override Color ButtonSelectedGradientEnd           => Hover;
            public override Color ButtonPressedGradientBegin          => Pressed;
            public override Color ButtonPressedGradientMiddle         => Pressed;
            public override Color ButtonPressedGradientEnd            => Pressed;
        }

        #endregion
    }
}
