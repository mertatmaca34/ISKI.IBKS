using ISKI.IBKS.Presentation.WinForms.Common.Controls;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.Controls;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.CalibrationSettings.View
{
    partial class CalibrationSettingsPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            ButtonSave = new Button();
            TableLayoutPanelMain = new TableLayoutPanel();
            CalibrationSettingsBarIletkenlik = new CalibrationSettingsBar();
            CalibrationSettingsBarPh = new CalibrationSettingsBar();
            CalibrationSettingsBarKoi = new CalibrationSettingsBar();
            CalibrationSettingsBarAkm = new CalibrationSettingsBar();
            titleBarControl1 = new TitleBarControl();
            tableLayoutPanel1.SuspendLayout();
            TableLayoutPanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(ButtonSave, 0, 1);
            tableLayoutPanel1.Controls.Add(TableLayoutPanelMain, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1170, 621);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // ButtonSave
            // 
            ButtonSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonSave.BackColor = Color.FromArgb(0, 131, 200);
            ButtonSave.FlatAppearance.BorderColor = Color.FromArgb(235, 235, 235);
            ButtonSave.FlatAppearance.MouseDownBackColor = Color.WhiteSmoke;
            ButtonSave.FlatAppearance.MouseOverBackColor = SystemColors.ButtonFace;
            ButtonSave.ForeColor = Color.White;
            ButtonSave.Location = new Point(938, 318);
            ButtonSave.Margin = new Padding(8);
            ButtonSave.Name = "ButtonSave";
            ButtonSave.Size = new Size(224, 43);
            ButtonSave.TabIndex = 3;
            ButtonSave.Text = "Kaydet";
            ButtonSave.UseVisualStyleBackColor = false;
            // 
            // TableLayoutPanelMain
            // 
            TableLayoutPanelMain.ColumnCount = 1;
            TableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelMain.Controls.Add(CalibrationSettingsBarIletkenlik, 0, 4);
            TableLayoutPanelMain.Controls.Add(CalibrationSettingsBarPh, 0, 3);
            TableLayoutPanelMain.Controls.Add(CalibrationSettingsBarKoi, 0, 2);
            TableLayoutPanelMain.Controls.Add(CalibrationSettingsBarAkm, 0, 1);
            TableLayoutPanelMain.Controls.Add(titleBarControl1, 0, 0);
            TableLayoutPanelMain.Dock = DockStyle.Fill;
            TableLayoutPanelMain.Location = new Point(8, 8);
            TableLayoutPanelMain.Margin = new Padding(8);
            TableLayoutPanelMain.Name = "TableLayoutPanelMain";
            TableLayoutPanelMain.RowCount = 5;
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            TableLayoutPanelMain.Size = new Size(1154, 294);
            TableLayoutPanelMain.TabIndex = 0;
            // 
            // CalibrationSettingsBarIletkenlik
            // 
            CalibrationSettingsBarIletkenlik.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            CalibrationSettingsBarIletkenlik.BackColor = Color.FromArgb(235, 235, 235);
            CalibrationSettingsBarIletkenlik.Dock = DockStyle.Fill;
            CalibrationSettingsBarIletkenlik.Location = new Point(3, 231);
            CalibrationSettingsBarIletkenlik.Margin = new Padding(3, 1, 3, 1);
            CalibrationSettingsBarIletkenlik.Name = "CalibrationSettingsBarIletkenlik";
            CalibrationSettingsBarIletkenlik.Parameter = "Iletkenlik";
            CalibrationSettingsBarIletkenlik.Size = new Size(1148, 62);
            CalibrationSettingsBarIletkenlik.SpanRef = "";
            CalibrationSettingsBarIletkenlik.SpanTime = "";
            CalibrationSettingsBarIletkenlik.TabIndex = 4;
            CalibrationSettingsBarIletkenlik.ZeroRef = "";
            CalibrationSettingsBarIletkenlik.ZeroTime = "";
            // 
            // CalibrationSettingsBarPh
            // 
            CalibrationSettingsBarPh.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            CalibrationSettingsBarPh.BackColor = Color.FromArgb(235, 235, 235);
            CalibrationSettingsBarPh.Dock = DockStyle.Fill;
            CalibrationSettingsBarPh.Location = new Point(3, 167);
            CalibrationSettingsBarPh.Margin = new Padding(3, 1, 3, 1);
            CalibrationSettingsBarPh.Name = "CalibrationSettingsBarPh";
            CalibrationSettingsBarPh.Parameter = "pH";
            CalibrationSettingsBarPh.Size = new Size(1148, 62);
            CalibrationSettingsBarPh.SpanRef = "";
            CalibrationSettingsBarPh.SpanTime = "";
            CalibrationSettingsBarPh.TabIndex = 3;
            CalibrationSettingsBarPh.ZeroRef = "";
            CalibrationSettingsBarPh.ZeroTime = "";
            // 
            // CalibrationSettingsBarKoi
            // 
            CalibrationSettingsBarKoi.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            CalibrationSettingsBarKoi.BackColor = Color.FromArgb(235, 235, 235);
            CalibrationSettingsBarKoi.Dock = DockStyle.Fill;
            CalibrationSettingsBarKoi.Location = new Point(3, 103);
            CalibrationSettingsBarKoi.Margin = new Padding(3, 1, 3, 1);
            CalibrationSettingsBarKoi.Name = "CalibrationSettingsBarKoi";
            CalibrationSettingsBarKoi.Parameter = "KOi";
            CalibrationSettingsBarKoi.Size = new Size(1148, 62);
            CalibrationSettingsBarKoi.SpanRef = "";
            CalibrationSettingsBarKoi.SpanTime = "";
            CalibrationSettingsBarKoi.TabIndex = 2;
            CalibrationSettingsBarKoi.ZeroRef = "";
            CalibrationSettingsBarKoi.ZeroTime = "";
            // 
            // CalibrationSettingsBarAkm
            // 
            CalibrationSettingsBarAkm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            CalibrationSettingsBarAkm.BackColor = Color.FromArgb(235, 235, 235);
            CalibrationSettingsBarAkm.Dock = DockStyle.Fill;
            CalibrationSettingsBarAkm.Location = new Point(3, 41);
            CalibrationSettingsBarAkm.Margin = new Padding(3, 3, 3, 1);
            CalibrationSettingsBarAkm.Name = "CalibrationSettingsBarAkm";
            CalibrationSettingsBarAkm.Parameter = "AKM";
            CalibrationSettingsBarAkm.Size = new Size(1148, 60);
            CalibrationSettingsBarAkm.SpanRef = "";
            CalibrationSettingsBarAkm.SpanTime = "";
            CalibrationSettingsBarAkm.TabIndex = 0;
            CalibrationSettingsBarAkm.ZeroRef = "";
            CalibrationSettingsBarAkm.ZeroTime = "";
            // 
            // titleBarControl1
            // 
            titleBarControl1.BackColor = Color.FromArgb(235, 235, 235);
            titleBarControl1.Dock = DockStyle.Fill;
            titleBarControl1.Location = new Point(3, 3);
            titleBarControl1.Name = "titleBarControl1";
            titleBarControl1.Padding = new Padding(1);
            titleBarControl1.Size = new Size(1148, 32);
            titleBarControl1.TabIndex = 1;
            titleBarControl1.TitleBarText = "KALİBRASYON AYARLARI";
            // 
            // CalibrationSettingsPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(tableLayoutPanel1);
            Name = "CalibrationSettingsPage";
            Size = new Size(1170, 621);
            tableLayoutPanel1.ResumeLayout(false);
            TableLayoutPanelMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel TableLayoutPanelMain;
        private TitleBarControl titleBarControl1;
        private CalibrationSettingsBar CalibrationSettingsBarAkm;
        private CalibrationSettingsBar CalibrationSettingsBarKoi;
        private CalibrationSettingsBar CalibrationSettingsBarPh;
        private Button ButtonSave;
        private CalibrationSettingsBar CalibrationSettingsBarIletkenlik;
    }
}

