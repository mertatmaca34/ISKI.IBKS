namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.ChildPages
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

            // Form settings
            Text = "Alarm Kullanıcıları";
            Size = new Size(400, 450);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            // LabelInfo
            LabelInfo.Location = new Point(20, 15);
            LabelInfo.AutoSize = true;
            LabelInfo.Text = "Bu alarm için bildirim alacak kullanıcıları seçin:";
            Controls.Add(LabelInfo);

            // CheckedListBoxUsers
            CheckedListBoxUsers.Location = new Point(20, 40);
            CheckedListBoxUsers.Size = new Size(344, 320);
            CheckedListBoxUsers.CheckOnClick = true;
            CheckedListBoxUsers.BorderStyle = BorderStyle.FixedSingle;
            CheckedListBoxUsers.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            Controls.Add(CheckedListBoxUsers);

            // ButtonSave
            ButtonSave.Location = new Point(140, 370);
            ButtonSave.Size = new Size(100, 32);
            ButtonSave.Text = "Kaydet";
            ButtonSave.BackColor = Color.FromArgb(0, 131, 200);
            ButtonSave.ForeColor = Color.White;
            ButtonSave.FlatStyle = FlatStyle.Flat;
            ButtonSave.FlatAppearance.BorderSize = 0;
            Controls.Add(ButtonSave);

            // ButtonCancel
            ButtonCancel.Location = new Point(250, 370);
            ButtonCancel.Size = new Size(100, 32);
            ButtonCancel.Text = "İptal";
            ButtonCancel.DialogResult = DialogResult.Cancel;
            Controls.Add(ButtonCancel);

            AcceptButton = ButtonSave;
            CancelButton = ButtonCancel;

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
