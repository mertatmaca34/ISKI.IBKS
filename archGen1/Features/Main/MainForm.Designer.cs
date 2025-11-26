using ISKI.IBKS.Presentation.WinForms.Controls;

namespace ISKI.IBKS.Presentation.WinForms.Features.Main
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            tableLayoutPanel1 = new TableLayoutPanel();
            HomePageButton = new ISKI.IBKS.Presentation.WinForms.Controls.NavigationBarButton();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.White;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(HomePageButton, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Left;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.MaximumSize = new Size(90, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            tableLayoutPanel1.Size = new Size(90, 681);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // HomePageButton
            // 
            HomePageButton.BackColor = Color.White;
            HomePageButton.CornerRadius = 10;
            HomePageButton.FlatAppearance.BorderSize = 0;
            HomePageButton.FlatStyle = FlatStyle.Flat;
            HomePageButton.Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            HomePageButton.ForeColor = Color.DimGray;
            HomePageButton.Image = Properties.Resources.home_24px;
            HomePageButton.Location = new Point(8, 8);
            HomePageButton.Margin = new Padding(8);
            HomePageButton.Name = "HomePageButton";
            HomePageButton.Size = new Size(74, 68);
            HomePageButton.TabIndex = 1;
            HomePageButton.Text = "Anasayfa";
            HomePageButton.TextAlign = ContentAlignment.BottomCenter;
            HomePageButton.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1264, 681);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private NavigationBarButton HomePageButton;
    }
}