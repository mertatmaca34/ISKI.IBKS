namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailStatements.View
{
    partial class AlarmUsersEditDialog
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
            LabelInfo = new Label();
            CheckedListBoxUsers = new CheckedListBox();
            ButtonSave = new Button();
            ButtonCancel = new Button();
            SuspendLayout();
            // 
            // LabelInfo
            // 
            LabelInfo.AutoSize = true;
            LabelInfo.Location = new Point(20, 15);
            LabelInfo.Name = "LabelInfo";
            LabelInfo.Size = new Size(249, 15);
            LabelInfo.TabIndex = 0;
            LabelInfo.Text = "Bu alarm için bildirim alacak kullanıcıları seçin\r\n";
            // 
            // CheckedListBoxUsers
            // 
            CheckedListBoxUsers.BorderStyle = BorderStyle.FixedSingle;
            CheckedListBoxUsers.CheckOnClick = true;
            CheckedListBoxUsers.Font = new Font("Segoe UI", 10F);
            CheckedListBoxUsers.Location = new Point(20, 40);
            CheckedListBoxUsers.Name = "CheckedListBoxUsers";
            CheckedListBoxUsers.Size = new Size(344, 302);
            CheckedListBoxUsers.TabIndex = 1;
            // 
            // ButtonSave
            // 
            ButtonSave.BackColor = Color.FromArgb(0, 131, 200);
            ButtonSave.FlatAppearance.BorderSize = 0;
            ButtonSave.FlatStyle = FlatStyle.Flat;
            ButtonSave.ForeColor = Color.White;
            ButtonSave.Location = new Point(140, 370);
            ButtonSave.Name = "ButtonSave";
            ButtonSave.Size = new Size(100, 32);
            ButtonSave.TabIndex = 2;
            ButtonSave.Text = "Kaydet";
            ButtonSave.UseVisualStyleBackColor = false;
            // 
            // ButtonCancel
            // 
            ButtonCancel.DialogResult = DialogResult.Cancel;
            ButtonCancel.Location = new Point(250, 370);
            ButtonCancel.Name = "ButtonCancel";
            ButtonCancel.Size = new Size(100, 32);
            ButtonCancel.TabIndex = 3;
            ButtonCancel.Text = "İptal";
            // 
            // AlarmUsersEditDialog
            // 
            AcceptButton = ButtonSave;
            BackColor = Color.White;
            CancelButton = ButtonCancel;
            ClientSize = new Size(384, 411);
            Controls.Add(LabelInfo);
            Controls.Add(CheckedListBoxUsers);
            Controls.Add(ButtonSave);
            Controls.Add(ButtonCancel);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AlarmUsersEditDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Alarm Kullanıcıları Listesi:";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LabelInfo;
        private CheckedListBox CheckedListBoxUsers;
        private Button ButtonSave;
        private Button ButtonCancel;
    }
}
