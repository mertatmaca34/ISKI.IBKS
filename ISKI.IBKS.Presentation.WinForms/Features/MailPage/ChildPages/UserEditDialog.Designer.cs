namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.ChildPages
{
    partial class UserEditDialog
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
            LabelFullName = new Label();
            TextBoxFullName = new TextBox();
            LabelEmail = new Label();
            TextBoxEmail = new TextBox();
            LabelPhoneNumber = new Label();
            TextBoxPhoneNumber = new TextBox();
            LabelDepartment = new Label();
            TextBoxDepartment = new TextBox();
            LabelTitle = new Label();
            TextBoxTitle = new TextBox();
            CheckBoxIsActive = new CheckBox();
            CheckBoxReceiveEmail = new CheckBox();
            ButtonSave = new Button();
            ButtonCancel = new Button();
            SuspendLayout();
            // 
            // LabelFullName
            // 
            LabelFullName.AutoSize = true;
            LabelFullName.Location = new Point(22, 37);
            LabelFullName.Name = "LabelFullName";
            LabelFullName.Size = new Size(57, 15);
            LabelFullName.TabIndex = 0;
            LabelFullName.Text = "Ad Soyad";
            // 
            // TextBoxFullName
            // 
            TextBoxFullName.Location = new Point(180, 37);
            TextBoxFullName.Name = "TextBoxFullName";
            TextBoxFullName.Size = new Size(260, 23);
            TextBoxFullName.TabIndex = 1;
            // 
            // LabelEmail
            // 
            LabelEmail.AutoSize = true;
            LabelEmail.Location = new Point(22, 73);
            LabelEmail.Name = "LabelEmail";
            LabelEmail.Size = new Size(47, 15);
            LabelEmail.TabIndex = 2;
            LabelEmail.Text = "E-Posta";
            // 
            // TextBoxEmail
            // 
            TextBoxEmail.Location = new Point(180, 73);
            TextBoxEmail.Name = "TextBoxEmail";
            TextBoxEmail.Size = new Size(260, 23);
            TextBoxEmail.TabIndex = 3;
            // 
            // LabelPhoneNumber
            // 
            LabelPhoneNumber.AutoSize = true;
            LabelPhoneNumber.Location = new Point(22, 109);
            LabelPhoneNumber.Name = "LabelPhoneNumber";
            LabelPhoneNumber.Size = new Size(46, 15);
            LabelPhoneNumber.TabIndex = 4;
            LabelPhoneNumber.Text = "Telefon";
            // 
            // TextBoxPhoneNumber
            // 
            TextBoxPhoneNumber.Location = new Point(180, 109);
            TextBoxPhoneNumber.Name = "TextBoxPhoneNumber";
            TextBoxPhoneNumber.Size = new Size(260, 23);
            TextBoxPhoneNumber.TabIndex = 5;
            // 
            // LabelDepartment
            // 
            LabelDepartment.AutoSize = true;
            LabelDepartment.Location = new Point(22, 145);
            LabelDepartment.Name = "LabelDepartment";
            LabelDepartment.Size = new Size(66, 15);
            LabelDepartment.TabIndex = 6;
            LabelDepartment.Text = "Departman";
            // 
            // TextBoxDepartment
            // 
            TextBoxDepartment.Location = new Point(180, 145);
            TextBoxDepartment.Name = "TextBoxDepartment";
            TextBoxDepartment.Size = new Size(260, 23);
            TextBoxDepartment.TabIndex = 7;
            // 
            // LabelTitle
            // 
            LabelTitle.AutoSize = true;
            LabelTitle.Location = new Point(22, 181);
            LabelTitle.Name = "LabelTitle";
            LabelTitle.Size = new Size(41, 15);
            LabelTitle.TabIndex = 8;
            LabelTitle.Text = "Ünvan";
            // 
            // TextBoxTitle
            // 
            TextBoxTitle.Location = new Point(180, 181);
            TextBoxTitle.Name = "TextBoxTitle";
            TextBoxTitle.Size = new Size(260, 23);
            TextBoxTitle.TabIndex = 9;
            // 
            // CheckBoxIsActive
            // 
            CheckBoxIsActive.AutoSize = true;
            CheckBoxIsActive.Checked = true;
            CheckBoxIsActive.CheckState = CheckState.Checked;
            CheckBoxIsActive.Location = new Point(340, 220);
            CheckBoxIsActive.Name = "CheckBoxIsActive";
            CheckBoxIsActive.Size = new Size(51, 19);
            CheckBoxIsActive.TabIndex = 10;
            CheckBoxIsActive.Text = "Aktif";
            // 
            // CheckBoxReceiveEmail
            // 
            CheckBoxReceiveEmail.AutoSize = true;
            CheckBoxReceiveEmail.Checked = true;
            CheckBoxReceiveEmail.CheckState = CheckState.Checked;
            CheckBoxReceiveEmail.Location = new Point(180, 220);
            CheckBoxReceiveEmail.Name = "CheckBoxReceiveEmail";
            CheckBoxReceiveEmail.Size = new Size(140, 19);
            CheckBoxReceiveEmail.TabIndex = 11;
            CheckBoxReceiveEmail.Text = "E-Posta Bildirimleri Al";
            // 
            // ButtonSave
            // 
            ButtonSave.BackColor = Color.FromArgb(0, 131, 200);
            ButtonSave.FlatAppearance.BorderSize = 0;
            ButtonSave.FlatStyle = FlatStyle.Flat;
            ButtonSave.ForeColor = Color.White;
            ButtonSave.Location = new Point(350, 280);
            ButtonSave.Name = "ButtonSave";
            ButtonSave.Size = new Size(90, 32);
            ButtonSave.TabIndex = 14;
            ButtonSave.Text = "Kaydet";
            ButtonSave.UseVisualStyleBackColor = false;
            // 
            // ButtonCancel
            // 
            ButtonCancel.DialogResult = DialogResult.Cancel;
            ButtonCancel.Location = new Point(22, 280);
            ButtonCancel.Name = "ButtonCancel";
            ButtonCancel.Size = new Size(90, 32);
            ButtonCancel.TabIndex = 15;
            ButtonCancel.Text = "İptal";
            // 
            // UserEditDialog
            // 
            AcceptButton = ButtonSave;
            BackColor = Color.White;
            CancelButton = ButtonCancel;
            ClientSize = new Size(464, 340);
            Controls.Add(LabelFullName);
            Controls.Add(TextBoxFullName);
            Controls.Add(LabelEmail);
            Controls.Add(TextBoxEmail);
            Controls.Add(LabelPhoneNumber);
            Controls.Add(TextBoxPhoneNumber);
            Controls.Add(LabelDepartment);
            Controls.Add(TextBoxDepartment);
            Controls.Add(LabelTitle);
            Controls.Add(TextBoxTitle);
            Controls.Add(CheckBoxIsActive);
            Controls.Add(CheckBoxReceiveEmail);
            Controls.Add(ButtonSave);
            Controls.Add(ButtonCancel);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UserEditDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Kullanıcı Ekle/Düzenle";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LabelFullName;
        private TextBox TextBoxFullName;
        private Label LabelEmail;
        private TextBox TextBoxEmail;
        private Label LabelPhoneNumber;
        private TextBox TextBoxPhoneNumber;
        private Label LabelDepartment;
        private TextBox TextBoxDepartment;
        private Label LabelTitle;
        private TextBox TextBoxTitle;
        private CheckBox CheckBoxIsActive;
        private CheckBox CheckBoxReceiveEmail;
        private Button ButtonSave;
        private Button ButtonCancel;
    }
}
