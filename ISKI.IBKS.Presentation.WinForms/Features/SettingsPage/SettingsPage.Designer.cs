using ISKI.IBKS.Presentation.WinForms.Common.Controls;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage
{
    partial class SettingsPage
    {
        /// <summary> 
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            ButtonStationSettings = new ToolStripMenuItem();
            ButtonApiSettings = new ToolStripMenuItem();
            ButtonPlcSettings = new ToolStripMenuItem();
            ButtonCalibrationSettings = new ToolStripMenuItem();
            PanelContent = new Panel();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.White;
            menuStrip1.Items.AddRange(new ToolStripItem[] { ButtonStationSettings, ButtonApiSettings, ButtonPlcSettings, ButtonCalibrationSettings });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1170, 56);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // ButtonStationSettings
            // 
            ButtonStationSettings.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            ButtonStationSettings.Image = Properties.Resources.place_marker_48px;
            ButtonStationSettings.ImageScaling = ToolStripItemImageScaling.None;
            ButtonStationSettings.Name = "ButtonStationSettings";
            ButtonStationSettings.Size = new Size(182, 52);
            ButtonStationSettings.Text = "İSTASYON AYARLARI";
            // 
            // ButtonApiSettings
            // 
            ButtonApiSettings.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            ButtonApiSettings.Image = Properties.Resources.rest_api_48px;
            ButtonApiSettings.ImageScaling = ToolStripItemImageScaling.None;
            ButtonApiSettings.Name = "ButtonApiSettings";
            ButtonApiSettings.Size = new Size(133, 52);
            ButtonApiSettings.Text = "API Ayarları";
            // 
            // ButtonPlcSettings
            // 
            ButtonPlcSettings.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            ButtonPlcSettings.Image = Properties.Resources.electronics_48px;
            ButtonPlcSettings.ImageScaling = ToolStripItemImageScaling.None;
            ButtonPlcSettings.Name = "ButtonPlcSettings";
            ButtonPlcSettings.Size = new Size(132, 52);
            ButtonPlcSettings.Text = "Plc Ayarları";
            // 
            // ButtonCalibrationSettings
            // 
            ButtonCalibrationSettings.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            ButtonCalibrationSettings.Image = Properties.Resources.azimuth_48px;
            ButtonCalibrationSettings.ImageScaling = ToolStripItemImageScaling.None;
            ButtonCalibrationSettings.Name = "ButtonCalibrationSettings";
            ButtonCalibrationSettings.Size = new Size(181, 52);
            ButtonCalibrationSettings.Text = "Kalibrasyon Ayarları";
            // 
            // PanelContent
            // 
            PanelContent.Dock = DockStyle.Fill;
            PanelContent.Location = new Point(0, 56);
            PanelContent.Name = "PanelContent";
            PanelContent.Size = new Size(1170, 621);
            PanelContent.TabIndex = 1;
            // 
            // SettingsPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1170, 677);
            Controls.Add(PanelContent);
            Controls.Add(menuStrip1);
            Name = "SettingsPage";
            Text = "SettingsPage";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem ButtonStationSettings;
        private ToolStripMenuItem ButtonApiSettings;
        private ToolStripMenuItem ButtonPlcSettings;
        private ToolStripMenuItem ButtonCalibrationSettings;
        private Panel PanelContent;
    }
}