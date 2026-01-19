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
            LabelPriority = new Label();
            ComboBoxPriority = new ComboBox();
            ButtonSave = new Button();
            ButtonCancel = new Button();
            SuspendLayout();

            // Form settings
            Text = "Kullanıcı Ekle/Düzenle";
            Size = new Size(480, 480);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            var y = 20;
            var labelX = 20;
            var inputX = 180;
            var inputWidth = 260;
            var spacing = 38;

            // Full Name
            LabelFullName.Location = new Point(labelX, y + 3);
            LabelFullName.AutoSize = true;
            LabelFullName.Text = "Ad Soyad";
            Controls.Add(LabelFullName);

            TextBoxFullName.Location = new Point(inputX, y);
            TextBoxFullName.Size = new Size(inputWidth, 25);
            Controls.Add(TextBoxFullName);
            y += spacing;

            // Email
            LabelEmail.Location = new Point(labelX, y + 3);
            LabelEmail.AutoSize = true;
            LabelEmail.Text = "E-Posta";
            Controls.Add(LabelEmail);

            TextBoxEmail.Location = new Point(inputX, y);
            TextBoxEmail.Size = new Size(inputWidth, 25);
            Controls.Add(TextBoxEmail);
            y += spacing;

            // Phone Number
            LabelPhoneNumber.Location = new Point(labelX, y + 3);
            LabelPhoneNumber.AutoSize = true;
            LabelPhoneNumber.Text = "Telefon";
            Controls.Add(LabelPhoneNumber);

            TextBoxPhoneNumber.Location = new Point(inputX, y);
            TextBoxPhoneNumber.Size = new Size(inputWidth, 25);
            Controls.Add(TextBoxPhoneNumber);
            y += spacing;

            // Department
            LabelDepartment.Location = new Point(labelX, y + 3);
            LabelDepartment.AutoSize = true;
            LabelDepartment.Text = "Departman";
            Controls.Add(LabelDepartment);

            TextBoxDepartment.Location = new Point(inputX, y);
            TextBoxDepartment.Size = new Size(inputWidth, 25);
            Controls.Add(TextBoxDepartment);
            y += spacing;

            // Title
            LabelTitle.Location = new Point(labelX, y + 3);
            LabelTitle.AutoSize = true;
            LabelTitle.Text = "Ünvan";
            Controls.Add(LabelTitle);

            TextBoxTitle.Location = new Point(inputX, y);
            TextBoxTitle.Size = new Size(inputWidth, 25);
            Controls.Add(TextBoxTitle);
            y += spacing;

            // Is Active
            CheckBoxIsActive.Location = new Point(inputX, y);
            CheckBoxIsActive.AutoSize = true;
            CheckBoxIsActive.Text = "Aktif";
            CheckBoxIsActive.Checked = true;
            Controls.Add(CheckBoxIsActive);
            y += 30;

            // Receive Email Notifications
            CheckBoxReceiveEmail.Location = new Point(inputX, y);
            CheckBoxReceiveEmail.AutoSize = true;
            CheckBoxReceiveEmail.Text = "E-Posta Bildirimleri Al";
            CheckBoxReceiveEmail.Checked = true;
            Controls.Add(CheckBoxReceiveEmail);
            y += 35;

            // Minimum Priority Level
            LabelPriority.Location = new Point(labelX, y + 3);
            LabelPriority.AutoSize = true;
            LabelPriority.Text = "Min. Öncelik Seviyesi";
            Controls.Add(LabelPriority);

            ComboBoxPriority.Location = new Point(inputX, y);
            ComboBoxPriority.Size = new Size(inputWidth, 25);
            ComboBoxPriority.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxPriority.Items.AddRange(new object[] { "Düşük", "Orta", "Yüksek", "Kritik" });
            Controls.Add(ComboBoxPriority);
            y += spacing + 15;

            // Buttons
            ButtonSave.Location = new Point(inputX, y);
            ButtonSave.Size = new Size(90, 32);
            ButtonSave.Text = "Kaydet";
            ButtonSave.BackColor = Color.FromArgb(0, 131, 200);
            ButtonSave.ForeColor = Color.White;
            ButtonSave.FlatStyle = FlatStyle.Flat;
            ButtonSave.FlatAppearance.BorderSize = 0;
            Controls.Add(ButtonSave);

            ButtonCancel.Location = new Point(inputX + 100, y);
            ButtonCancel.Size = new Size(90, 32);
            ButtonCancel.Text = "İptal";
            ButtonCancel.DialogResult = DialogResult.Cancel;
            Controls.Add(ButtonCancel);

            AcceptButton = ButtonSave;
            CancelButton = ButtonCancel;

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
        private Label LabelPriority;
        private ComboBox ComboBoxPriority;
        private Button ButtonSave;
        private Button ButtonCancel;
    }
}
