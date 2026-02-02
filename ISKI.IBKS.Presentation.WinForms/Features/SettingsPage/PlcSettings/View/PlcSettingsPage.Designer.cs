using ISKI.IBKS.Presentation.WinForms.Common.Controls;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.Controls;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.PlcSettings.View
{
    partial class PlcSettingsPage
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
            TableLayoutPanelMain = new TableLayoutPanel();
            titleBarControl1 = new TitleBarControl();
            PlcSettingsControlIp = new SettingsBarControl();
            LabelSensors = new Label();
            FlowLayoutPanelSensors = new FlowLayoutPanel();
            ButtonSave = new Button();
            tableLayoutPanel1.SuspendLayout();
            TableLayoutPanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(TableLayoutPanelMain, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1170, 621);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // TableLayoutPanelMain
            // 
            TableLayoutPanelMain.ColumnCount = 1;
            TableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelMain.Controls.Add(titleBarControl1, 0, 0);
            TableLayoutPanelMain.Controls.Add(PlcSettingsControlIp, 0, 1);
            TableLayoutPanelMain.Controls.Add(LabelSensors, 0, 2);
            TableLayoutPanelMain.Controls.Add(FlowLayoutPanelSensors, 0, 3);
            TableLayoutPanelMain.Controls.Add(ButtonSave, 0, 4);
            TableLayoutPanelMain.Dock = DockStyle.Fill;
            TableLayoutPanelMain.Location = new Point(8, 8);
            TableLayoutPanelMain.Margin = new Padding(8);
            TableLayoutPanelMain.Name = "TableLayoutPanelMain";
            TableLayoutPanelMain.RowCount = 5;
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            TableLayoutPanelMain.RowStyles.Add(new RowStyle());
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            TableLayoutPanelMain.Size = new Size(1154, 605);
            TableLayoutPanelMain.TabIndex = 0;
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
            titleBarControl1.TitleBarText = "PLC AYARLARI";
            // 
            // PlcSettingsControlIp
            // 
            PlcSettingsControlIp.AyarAdi = "IP:";
            PlcSettingsControlIp.AyarDegeri = "";
            PlcSettingsControlIp.BackColor = Color.FromArgb(235, 235, 235);
            PlcSettingsControlIp.Dock = DockStyle.Fill;
            PlcSettingsControlIp.Location = new Point(3, 41);
            PlcSettingsControlIp.Name = "PlcSettingsControlIp";
            PlcSettingsControlIp.Size = new Size(1148, 54);
            PlcSettingsControlIp.TabIndex = 2;
            // 
            // LabelSensors
            // 
            LabelSensors.AutoSize = true;
            LabelSensors.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            LabelSensors.Location = new Point(3, 101);
            LabelSensors.Margin = new Padding(3, 3, 3, 0);
            LabelSensors.Name = "LabelSensors";
            LabelSensors.Size = new Size(156, 19);
            LabelSensors.TabIndex = 4;
            LabelSensors.Text = "Kullanılacak Sensörler";
            // 
            // FlowLayoutPanelSensors
            // 
            FlowLayoutPanelSensors.AutoScroll = true;
            FlowLayoutPanelSensors.BackColor = Color.White;
            FlowLayoutPanelSensors.Dock = DockStyle.Fill;
            FlowLayoutPanelSensors.Location = new Point(3, 131);
            FlowLayoutPanelSensors.Name = "FlowLayoutPanelSensors";
            FlowLayoutPanelSensors.Padding = new Padding(10);
            FlowLayoutPanelSensors.Size = new Size(1148, 411);
            FlowLayoutPanelSensors.TabIndex = 5;
            // 
            // ButtonSave
            // 
            ButtonSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonSave.BackColor = Color.FromArgb(0, 131, 200);
            ButtonSave.FlatAppearance.BorderColor = Color.FromArgb(235, 235, 235);
            ButtonSave.FlatAppearance.MouseDownBackColor = Color.WhiteSmoke;
            ButtonSave.FlatAppearance.MouseOverBackColor = SystemColors.ButtonFace;
            ButtonSave.ForeColor = Color.White;
            ButtonSave.Location = new Point(930, 553);
            ButtonSave.Margin = new Padding(8, 8, 0, 8);
            ButtonSave.Name = "ButtonSave";
            ButtonSave.Size = new Size(224, 43);
            ButtonSave.TabIndex = 3;
            ButtonSave.Text = "Kaydet";
            ButtonSave.UseVisualStyleBackColor = false;
            // 
            // PlcSettingsPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(tableLayoutPanel1);
            Name = "PlcSettingsPage";
            Size = new Size(1170, 621);
            tableLayoutPanel1.ResumeLayout(false);
            TableLayoutPanelMain.ResumeLayout(false);
            TableLayoutPanelMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel TableLayoutPanelMain;
        private Button ButtonSave;
        private TitleBarControl titleBarControl1;
        private SettingsBarControl PlcSettingsControlIp;
        private Label LabelSensors;
        private FlowLayoutPanel FlowLayoutPanelSensors;
    }
}

