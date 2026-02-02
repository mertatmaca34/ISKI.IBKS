namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls
{
    partial class HealthSummaryCardControl
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
            LabelKey = new Label();
            panel2 = new Panel();
            TableLayoutPanelRight = new TableLayoutPanel();
            LabelValue = new Label();
            PictureStatus = new PictureBox();
            TableLayoutPanelBg.SuspendLayout();
            TableLayoutPanelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureStatus).BeginInit();
            SuspendLayout();
            
            
            
            TableLayoutPanelBg.BackColor = Color.White;
            TableLayoutPanelBg.ColumnCount = 3;
            TableLayoutPanelBg.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TableLayoutPanelBg.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            TableLayoutPanelBg.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TableLayoutPanelBg.Controls.Add(LabelKey, 0, 0);
            TableLayoutPanelBg.Controls.Add(panel2, 1, 0);
            TableLayoutPanelBg.Controls.Add(TableLayoutPanelRight, 2, 0);
            TableLayoutPanelBg.Dock = DockStyle.Fill;
            TableLayoutPanelBg.Location = new Point(1, 1);
            TableLayoutPanelBg.Name = "TableLayoutPanelBg";
            TableLayoutPanelBg.RowCount = 1;
            TableLayoutPanelBg.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            TableLayoutPanelBg.Size = new Size(577, 71);
            TableLayoutPanelBg.TabIndex = 2;
            
            
            
            LabelKey.Anchor = AnchorStyles.None;
            LabelKey.AutoSize = true;
            LabelKey.Font = new Font("Calibri", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            LabelKey.Location = new Point(135, 26);
            LabelKey.Name = "LabelKey";
            LabelKey.Size = new Size(13, 18);
            LabelKey.TabIndex = 2;
            LabelKey.Text = "-";
            
            
            
            panel2.Anchor = AnchorStyles.None;
            panel2.BackColor = Color.FromArgb(235, 235, 235);
            panel2.Location = new Point(287, 17);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1, 37);
            panel2.TabIndex = 0;
            
            
            
            TableLayoutPanelRight.ColumnCount = 2;
            TableLayoutPanelRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TableLayoutPanelRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TableLayoutPanelRight.Controls.Add(LabelValue, 0, 0);
            TableLayoutPanelRight.Controls.Add(PictureStatus, 1, 0);
            TableLayoutPanelRight.Dock = DockStyle.Fill;
            TableLayoutPanelRight.Location = new Point(296, 3);
            TableLayoutPanelRight.Name = "TableLayoutPanelRight";
            TableLayoutPanelRight.RowCount = 1;
            TableLayoutPanelRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayoutPanelRight.Size = new Size(278, 65);
            TableLayoutPanelRight.TabIndex = 3;
            
            
            
            LabelValue.Anchor = AnchorStyles.Right;
            LabelValue.AutoSize = true;
            LabelValue.Font = new Font("Calibri", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            LabelValue.Location = new Point(123, 23);
            LabelValue.Name = "LabelValue";
            LabelValue.Size = new Size(13, 18);
            LabelValue.TabIndex = 2;
            LabelValue.Text = "-";
            LabelValue.TextAlign = ContentAlignment.MiddleCenter;
            
            
            
            PictureStatus.Anchor = AnchorStyles.Left;
            PictureStatus.Location = new Point(142, 24);
            PictureStatus.Name = "PictureStatus";
            PictureStatus.Size = new Size(16, 16);
            PictureStatus.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureStatus.TabIndex = 3;
            PictureStatus.TabStop = false;
            
            
            
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(235, 235, 235);
            Controls.Add(TableLayoutPanelBg);
            Name = "HealthSummaryCardControl";
            Padding = new Padding(1);
            Size = new Size(579, 73);
            TableLayoutPanelBg.ResumeLayout(false);
            TableLayoutPanelBg.PerformLayout();
            TableLayoutPanelRight.ResumeLayout(false);
            TableLayoutPanelRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PictureStatus).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel TableLayoutPanelBg;
        private Label LabelKey;
        private Panel panel2;
        private TableLayoutPanel TableLayoutPanelRight;
        private Label LabelValue;
        private PictureBox PictureStatus;
    }
}

