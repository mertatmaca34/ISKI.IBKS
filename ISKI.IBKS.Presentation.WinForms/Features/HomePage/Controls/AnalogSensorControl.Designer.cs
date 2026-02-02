namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls
{
    partial class AnalogSensorControl
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

        #region BileÅŸen TasarÄ±mcÄ±sÄ± Ã¼retimi kod

        private void InitializeComponent()
        {
            TableLayoutPanelBg = new TableLayoutPanel();
            LabelSensorInstantValue = new Label();
            PanelStatusIndicator = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            LabelSensorHourlyAvgValue = new Label();
            LabelSensorName = new Label();
            TableLayoutPanelBg.SuspendLayout();
            SuspendLayout();
            
            
            
            TableLayoutPanelBg.BackColor = Color.White;
            TableLayoutPanelBg.ColumnCount = 6;
            TableLayoutPanelBg.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
            TableLayoutPanelBg.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            TableLayoutPanelBg.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            TableLayoutPanelBg.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            TableLayoutPanelBg.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            TableLayoutPanelBg.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            TableLayoutPanelBg.Controls.Add(LabelSensorInstantValue, 3, 0);
            TableLayoutPanelBg.Controls.Add(PanelStatusIndicator, 0, 0);
            TableLayoutPanelBg.Controls.Add(panel2, 2, 0);
            TableLayoutPanelBg.Controls.Add(panel3, 4, 0);
            TableLayoutPanelBg.Controls.Add(LabelSensorHourlyAvgValue, 5, 0);
            TableLayoutPanelBg.Controls.Add(LabelSensorName, 1, 0);
            TableLayoutPanelBg.Dock = DockStyle.Fill;
            TableLayoutPanelBg.Location = new Point(0, 0);
            TableLayoutPanelBg.Name = "TableLayoutPanelBg";
            TableLayoutPanelBg.RowCount = 1;
            TableLayoutPanelBg.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayoutPanelBg.Size = new Size(579, 63);
            TableLayoutPanelBg.TabIndex = 1;
            
            
            
            LabelSensorInstantValue.Anchor = AnchorStyles.None;
            LabelSensorInstantValue.AutoSize = true;
            LabelSensorInstantValue.Font = new Font("Calibri", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            LabelSensorInstantValue.Location = new Point(286, 22);
            LabelSensorInstantValue.Name = "LabelSensorInstantValue";
            LabelSensorInstantValue.Size = new Size(13, 18);
            LabelSensorInstantValue.TabIndex = 2;
            LabelSensorInstantValue.Text = "-";
            
            
            
            PanelStatusIndicator.BackColor = Color.FromArgb(19, 162, 97);
            PanelStatusIndicator.Dock = DockStyle.Fill;
            PanelStatusIndicator.Location = new Point(0, 0);
            PanelStatusIndicator.Margin = new Padding(0);
            PanelStatusIndicator.Name = "PanelStatusIndicator";
            PanelStatusIndicator.Size = new Size(8, 63);
            PanelStatusIndicator.TabIndex = 0;
            
            
            
            panel2.Anchor = AnchorStyles.None;
            panel2.BackColor = Color.FromArgb(235, 235, 235);
            panel2.Location = new Point(195, 13);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1, 37);
            panel2.TabIndex = 0;
            
            
            
            panel3.Anchor = AnchorStyles.None;
            panel3.BackColor = Color.FromArgb(235, 235, 235);
            panel3.Location = new Point(388, 13);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1, 37);
            panel3.TabIndex = 0;
            
            
            
            LabelSensorHourlyAvgValue.Anchor = AnchorStyles.None;
            LabelSensorHourlyAvgValue.AutoSize = true;
            LabelSensorHourlyAvgValue.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            LabelSensorHourlyAvgValue.Location = new Point(480, 22);
            LabelSensorHourlyAvgValue.Name = "LabelSensorHourlyAvgValue";
            LabelSensorHourlyAvgValue.Size = new Size(13, 18);
            LabelSensorHourlyAvgValue.TabIndex = 2;
            LabelSensorHourlyAvgValue.Text = "-";
            
            
            
            LabelSensorName.Anchor = AnchorStyles.None;
            LabelSensorName.AutoSize = true;
            LabelSensorName.Font = new Font("Calibri", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            LabelSensorName.Location = new Point(93, 22);
            LabelSensorName.Name = "LabelSensorName";
            LabelSensorName.Size = new Size(13, 18);
            LabelSensorName.TabIndex = 2;
            LabelSensorName.Text = "-";
            
            
            
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(TableLayoutPanelBg);
            Name = "AnalogSensorControl";
            Size = new Size(579, 63);
            TableLayoutPanelBg.ResumeLayout(false);
            TableLayoutPanelBg.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel TableLayoutPanelBg;
        private Panel PanelStatusIndicator;
        private Panel panel2;
        private Panel panel3;
        private Label LabelSensorName;
        private Label LabelSensorHourlyAvgValue;
        private Label LabelSensorInstantValue;
    }
}

