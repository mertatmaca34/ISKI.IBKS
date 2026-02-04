using ISKI.IBKS.Presentation.WinForms.Common.Controls;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.ChildPages
{
    partial class MailServerSettingsPage
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
            tableLayoutPanel1 = new TableLayoutPanel();
            ButtonSave = new Button();
            TableLayoutPanelMain = new TableLayoutPanel();
            tableLayoutPanel15 = new TableLayoutPanel();
            label7 = new Label();
            CheckBoxCredentials = new CheckBox();
            tableLayoutPanel13 = new TableLayoutPanel();
            label6 = new Label();
            TextBoxPassword = new TextBox();
            tableLayoutPanel9 = new TableLayoutPanel();
            label4 = new Label();
            TextBoxUsername = new TextBox();
            tableLayoutPanel11 = new TableLayoutPanel();
            label5 = new Label();
            TextBoxPort = new TextBox();
            tableLayoutPanel5 = new TableLayoutPanel();
            label2 = new Label();
            TextBoxHost = new TextBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            label1 = new Label();
            CheckBoxSSL = new CheckBox();
            tableLayoutPanel17 = new TableLayoutPanel();
            label8 = new Label();
            TextBoxFromName = new TextBox();
            titleBarControl1 = new TitleBarControl();
            tableLayoutPanel6 = new TableLayoutPanel();
            tableLayoutPanel7 = new TableLayoutPanel();
            label3 = new Label();
            checkBox3 = new CheckBox();
            tableLayoutPanel1.SuspendLayout();
            TableLayoutPanelMain.SuspendLayout();
            tableLayoutPanel15.SuspendLayout();
            tableLayoutPanel13.SuspendLayout();
            tableLayoutPanel9.SuspendLayout();
            tableLayoutPanel11.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel17.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
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
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 73.7520142F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 26.2479877F));
            tableLayoutPanel1.Size = new Size(1170, 621);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // ButtonSave
            // 
            ButtonSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonSave.BackColor = Color.FromArgb(0, 131, 200);
            ButtonSave.FlatAppearance.BorderColor = Color.FromArgb(235, 235, 235);
            ButtonSave.FlatAppearance.MouseDownBackColor = Color.WhiteSmoke;
            ButtonSave.FlatAppearance.MouseOverBackColor = SystemColors.ButtonFace;
            ButtonSave.ForeColor = Color.White;
            ButtonSave.Location = new Point(938, 466);
            ButtonSave.Margin = new Padding(8);
            ButtonSave.Name = "ButtonSave";
            ButtonSave.Size = new Size(224, 43);
            ButtonSave.TabIndex = 7;
            ButtonSave.Text = "Kaydet";
            ButtonSave.UseVisualStyleBackColor = false;
            // 
            // TableLayoutPanelMain
            // 
            TableLayoutPanelMain.ColumnCount = 1;
            TableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelMain.Controls.Add(tableLayoutPanel15, 0, 7);
            TableLayoutPanelMain.Controls.Add(tableLayoutPanel13, 0, 6);
            TableLayoutPanelMain.Controls.Add(tableLayoutPanel9, 0, 5);
            TableLayoutPanelMain.Controls.Add(tableLayoutPanel11, 0, 4);
            TableLayoutPanelMain.Controls.Add(tableLayoutPanel5, 0, 3);
            TableLayoutPanelMain.Controls.Add(tableLayoutPanel3, 0, 2);
            TableLayoutPanelMain.Controls.Add(tableLayoutPanel17, 0, 1);
            TableLayoutPanelMain.Controls.Add(titleBarControl1, 0, 0);
            TableLayoutPanelMain.Dock = DockStyle.Fill;
            TableLayoutPanelMain.Location = new Point(8, 8);
            TableLayoutPanelMain.Margin = new Padding(8);
            TableLayoutPanelMain.Name = "TableLayoutPanelMain";
            TableLayoutPanelMain.RowCount = 8;
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2881632F));
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2853088F));
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 14.285306F));
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2853088F));
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 14.285306F));
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 14.285306F));
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 14.285306F));
            TableLayoutPanelMain.Size = new Size(1154, 442);
            TableLayoutPanelMain.TabIndex = 0;
            // 
            // tableLayoutPanel15
            // 
            tableLayoutPanel15.BackColor = Color.White;
            tableLayoutPanel15.ColumnCount = 2;
            tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel15.Controls.Add(label7, 0, 0);
            tableLayoutPanel15.Controls.Add(CheckBoxCredentials, 1, 0);
            tableLayoutPanel15.Dock = DockStyle.Fill;
            tableLayoutPanel15.Location = new Point(1, 381);
            tableLayoutPanel15.Margin = new Padding(1);
            tableLayoutPanel15.Name = "tableLayoutPanel15";
            tableLayoutPanel15.RowCount = 1;
            tableLayoutPanel15.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel15.Size = new Size(1152, 60);
            tableLayoutPanel15.TabIndex = 16;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.None;
            label7.AutoSize = true;
            label7.Font = new Font("Arial", 11.25F, FontStyle.Bold);
            label7.Location = new Point(246, 21);
            label7.Name = "label7";
            label7.Size = new Size(84, 18);
            label7.TabIndex = 0;
            label7.Text = "Doğrulama";
            // 
            // CheckBoxCredentials
            // 
            CheckBoxCredentials.Anchor = AnchorStyles.None;
            CheckBoxCredentials.AutoSize = true;
            CheckBoxCredentials.Location = new Point(763, 20);
            CheckBoxCredentials.Name = "CheckBoxCredentials";
            CheckBoxCredentials.Size = new Size(202, 19);
            CheckBoxCredentials.TabIndex = 6;
            CheckBoxCredentials.Text = "Varsayılan Kimlik Bilgilerini Kullan";
            CheckBoxCredentials.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel13
            // 
            tableLayoutPanel13.BackColor = Color.White;
            tableLayoutPanel13.ColumnCount = 2;
            tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel13.Controls.Add(label6, 0, 0);
            tableLayoutPanel13.Controls.Add(TextBoxPassword, 1, 0);
            tableLayoutPanel13.Dock = DockStyle.Fill;
            tableLayoutPanel13.Location = new Point(1, 324);
            tableLayoutPanel13.Margin = new Padding(1);
            tableLayoutPanel13.Name = "tableLayoutPanel13";
            tableLayoutPanel13.RowCount = 1;
            tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel13.Size = new Size(1152, 55);
            tableLayoutPanel13.TabIndex = 15;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Font = new Font("Arial", 11.25F, FontStyle.Bold);
            label6.Location = new Point(265, 18);
            label6.Name = "label6";
            label6.Size = new Size(46, 18);
            label6.TabIndex = 0;
            label6.Text = "Şifre:";
            // 
            // TextBoxPassword
            // 
            TextBoxPassword.Anchor = AnchorStyles.None;
            TextBoxPassword.Location = new Point(748, 16);
            TextBoxPassword.Name = "TextBoxPassword";
            TextBoxPassword.PlaceholderText = "şifre";
            TextBoxPassword.Size = new Size(231, 23);
            TextBoxPassword.TabIndex = 5;
            TextBoxPassword.UseSystemPasswordChar = true;
            // 
            // tableLayoutPanel9
            // 
            tableLayoutPanel9.BackColor = Color.White;
            tableLayoutPanel9.ColumnCount = 2;
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel9.Controls.Add(label4, 0, 0);
            tableLayoutPanel9.Controls.Add(TextBoxUsername, 1, 0);
            tableLayoutPanel9.Dock = DockStyle.Fill;
            tableLayoutPanel9.Location = new Point(1, 267);
            tableLayoutPanel9.Margin = new Padding(1);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.RowCount = 1;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel9.Size = new Size(1152, 55);
            tableLayoutPanel9.TabIndex = 14;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Arial", 11.25F, FontStyle.Bold);
            label4.Location = new Point(239, 18);
            label4.Name = "label4";
            label4.Size = new Size(98, 18);
            label4.TabIndex = 0;
            label4.Text = "Kullanıcı Adı:";
            // 
            // TextBoxUsername
            // 
            TextBoxUsername.Anchor = AnchorStyles.None;
            TextBoxUsername.Location = new Point(748, 16);
            TextBoxUsername.Name = "TextBoxUsername";
            TextBoxUsername.PlaceholderText = "mail@mail.com";
            TextBoxUsername.Size = new Size(231, 23);
            TextBoxUsername.TabIndex = 4;
            // 
            // tableLayoutPanel11
            // 
            tableLayoutPanel11.BackColor = Color.White;
            tableLayoutPanel11.ColumnCount = 2;
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel11.Controls.Add(label5, 0, 0);
            tableLayoutPanel11.Controls.Add(TextBoxPort, 1, 0);
            tableLayoutPanel11.Dock = DockStyle.Fill;
            tableLayoutPanel11.Location = new Point(1, 210);
            tableLayoutPanel11.Margin = new Padding(1);
            tableLayoutPanel11.Name = "tableLayoutPanel11";
            tableLayoutPanel11.RowCount = 1;
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel11.Size = new Size(1152, 55);
            tableLayoutPanel11.TabIndex = 13;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Arial", 11.25F, FontStyle.Bold);
            label5.Location = new Point(267, 18);
            label5.Name = "label5";
            label5.Size = new Size(42, 18);
            label5.TabIndex = 0;
            label5.Text = "Port:";
            // 
            // TextBoxPort
            // 
            TextBoxPort.Anchor = AnchorStyles.None;
            TextBoxPort.Location = new Point(748, 16);
            TextBoxPort.Name = "TextBoxPort";
            TextBoxPort.PlaceholderText = "80";
            TextBoxPort.Size = new Size(231, 23);
            TextBoxPort.TabIndex = 3;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.BackColor = Color.White;
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Controls.Add(label2, 0, 0);
            tableLayoutPanel5.Controls.Add(TextBoxHost, 1, 0);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(1, 153);
            tableLayoutPanel5.Margin = new Padding(1);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(1152, 55);
            tableLayoutPanel5.TabIndex = 12;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 11.25F, FontStyle.Bold);
            label2.Location = new Point(265, 18);
            label2.Name = "label2";
            label2.Size = new Size(45, 18);
            label2.TabIndex = 0;
            label2.Text = "Host:";
            // 
            // TextBoxHost
            // 
            TextBoxHost.Anchor = AnchorStyles.None;
            TextBoxHost.Location = new Point(748, 16);
            TextBoxHost.Name = "TextBoxHost";
            TextBoxHost.PlaceholderText = "10.0.0.0";
            TextBoxHost.Size = new Size(231, 23);
            TextBoxHost.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.BackColor = Color.White;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(label1, 0, 0);
            tableLayoutPanel3.Controls.Add(CheckBoxSSL, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(1, 96);
            tableLayoutPanel3.Margin = new Padding(1);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(1152, 55);
            tableLayoutPanel3.TabIndex = 11;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 11.25F, FontStyle.Bold);
            label1.Location = new Point(234, 18);
            label1.Name = "label1";
            label1.Size = new Size(107, 18);
            label1.TabIndex = 0;
            label1.Text = "SSL'i Aktif Et?";
            // 
            // CheckBoxSSL
            // 
            CheckBoxSSL.Anchor = AnchorStyles.None;
            CheckBoxSSL.AutoSize = true;
            CheckBoxSSL.Location = new Point(824, 18);
            CheckBoxSSL.Name = "CheckBoxSSL";
            CheckBoxSSL.Size = new Size(80, 19);
            CheckBoxSSL.TabIndex = 1;
            CheckBoxSSL.Text = "SSL Kullan";
            CheckBoxSSL.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel17
            // 
            tableLayoutPanel17.BackColor = Color.White;
            tableLayoutPanel17.ColumnCount = 2;
            tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel17.Controls.Add(label8, 0, 0);
            tableLayoutPanel17.Controls.Add(TextBoxFromName, 1, 0);
            tableLayoutPanel17.Dock = DockStyle.Fill;
            tableLayoutPanel17.Location = new Point(1, 41);
            tableLayoutPanel17.Margin = new Padding(1, 3, 1, 1);
            tableLayoutPanel17.Name = "tableLayoutPanel17";
            tableLayoutPanel17.RowCount = 1;
            tableLayoutPanel17.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel17.Size = new Size(1152, 53);
            tableLayoutPanel17.TabIndex = 10;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.None;
            label8.AutoSize = true;
            label8.Font = new Font("Arial", 11.25F, FontStyle.Bold);
            label8.Location = new Point(224, 17);
            label8.Name = "label8";
            label8.Size = new Size(128, 18);
            label8.TabIndex = 0;
            label8.Text = "Gönderen Bilgisi";
            // 
            // TextBoxFromName
            // 
            TextBoxFromName.Anchor = AnchorStyles.None;
            TextBoxFromName.Location = new Point(748, 15);
            TextBoxFromName.Name = "TextBoxFromName";
            TextBoxFromName.PlaceholderText = "İSKİ Alarm";
            TextBoxFromName.Size = new Size(231, 23);
            TextBoxFromName.TabIndex = 0;
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
            titleBarControl1.TitleBarText = "MAİL SUNUCUSU AYARLARI";
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.BackColor = Color.FromArgb(235, 235, 235);
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(0, 0);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel6.Size = new Size(200, 100);
            tableLayoutPanel6.TabIndex = 0;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.BackColor = Color.White;
            tableLayoutPanel7.ColumnCount = 2;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.Controls.Add(label3, 0, 0);
            tableLayoutPanel7.Controls.Add(checkBox3, 1, 0);
            tableLayoutPanel7.Dock = DockStyle.Fill;
            tableLayoutPanel7.Location = new Point(1, 1);
            tableLayoutPanel7.Margin = new Padding(1);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 1;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Size = new Size(198, 98);
            tableLayoutPanel7.TabIndex = 0;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("Arial", 11.25F, FontStyle.Bold);
            label3.Location = new Point(8, 31);
            label3.Name = "label3";
            label3.Size = new Size(83, 36);
            label3.TabIndex = 0;
            label3.Text = "SSL'i Aktif Et?";
            // 
            // checkBox3
            // 
            checkBox3.Anchor = AnchorStyles.None;
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(108, 39);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(80, 19);
            checkBox3.TabIndex = 1;
            checkBox3.Text = "SSL Kullan";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // MailServerSettingsPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(tableLayoutPanel1);
            Name = "MailServerSettingsPage";
            Size = new Size(1170, 621);
            tableLayoutPanel1.ResumeLayout(false);
            TableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanel15.ResumeLayout(false);
            tableLayoutPanel15.PerformLayout();
            tableLayoutPanel13.ResumeLayout(false);
            tableLayoutPanel13.PerformLayout();
            tableLayoutPanel9.ResumeLayout(false);
            tableLayoutPanel9.PerformLayout();
            tableLayoutPanel11.ResumeLayout(false);
            tableLayoutPanel11.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel17.ResumeLayout(false);
            tableLayoutPanel17.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button ButtonSave;
        private TableLayoutPanel TableLayoutPanelMain;
        private TitleBarControl titleBarControl1;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel7;
        private Label label3;
        private CheckBox checkBox3;
        private TableLayoutPanel tableLayoutPanel9;
        private Label label4;
        private TextBox TextBoxUsername;
        private TableLayoutPanel tableLayoutPanel11;
        private Label label5;
        private TextBox TextBoxPort;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label2;
        private TextBox TextBoxHost;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label1;
        private CheckBox CheckBoxSSL;
        private TableLayoutPanel tableLayoutPanel17;
        private Label label8;
        private TextBox TextBoxFromName;
        private TableLayoutPanel tableLayoutPanel15;
        private Label label7;
        private CheckBox CheckBoxCredentials;
        private TableLayoutPanel tableLayoutPanel13;
        private Label label6;
        private TextBox TextBoxPassword;
    }
}