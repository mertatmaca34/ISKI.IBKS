namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.ChildPages
{
    partial class AlarmEditDialog
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
            LabelName = new Label();
            TextBoxName = new TextBox();
            LabelDescription = new Label();
            TextBoxDescription = new TextBox();
            LabelSensorName = new Label();
            TextBoxSensorName = new TextBox();
            LabelType = new Label();
            ComboBoxType = new ComboBox();
            LabelMinThreshold = new Label();
            NumericMinThreshold = new NumericUpDown();
            LabelMaxThreshold = new Label();
            NumericMaxThreshold = new NumericUpDown();
            LabelExpectedDigitalValue = new Label();
            CheckBoxExpectedDigitalValue = new CheckBox();
            LabelIsActive = new Label();
            CheckBoxIsActive = new CheckBox();
            LabelCooldown = new Label();
            ComboBoxCooldown = new ComboBox();
            ButtonSave = new Button();
            ButtonCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)NumericMinThreshold).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumericMaxThreshold).BeginInit();
            SuspendLayout();

            // Form settings
            Text = "Alarm Tanımı Ekle/Düzenle";
            Size = new Size(500, 520);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            var y = 20;
            var labelX = 20;
            var inputX = 180;
            var inputWidth = 280;
            var spacing = 38;

            // Name
            LabelName.Location = new Point(labelX, y + 3);
            LabelName.AutoSize = true;
            LabelName.Text = "İsim";
            Controls.Add(LabelName);

            TextBoxName.Location = new Point(inputX, y);
            TextBoxName.Size = new Size(inputWidth, 25);
            Controls.Add(TextBoxName);
            y += spacing;

            // Description
            LabelDescription.Location = new Point(labelX, y + 3);
            LabelDescription.AutoSize = true;
            LabelDescription.Text = "Açıklama";
            Controls.Add(LabelDescription);

            TextBoxDescription.Location = new Point(inputX, y);
            TextBoxDescription.Size = new Size(inputWidth, 70);
            TextBoxDescription.Multiline = true;
            Controls.Add(TextBoxDescription);
            y += 80;

            // Sensor Name
            LabelSensorName.Location = new Point(labelX, y + 3);
            LabelSensorName.AutoSize = true;
            LabelSensorName.Text = "Sensör Adı";
            Controls.Add(LabelSensorName);

            TextBoxSensorName.Location = new Point(inputX, y);
            TextBoxSensorName.Size = new Size(inputWidth, 25);
            Controls.Add(TextBoxSensorName);
            y += spacing;

            // Type
            LabelType.Location = new Point(labelX, y + 3);
            LabelType.AutoSize = true;
            LabelType.Text = "Tip";
            Controls.Add(LabelType);

            ComboBoxType.Location = new Point(inputX, y);
            ComboBoxType.Size = new Size(inputWidth, 25);
            ComboBoxType.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxType.Items.AddRange(new object[] { "Eşik Değer", "Dijital", "Sistem" });
            Controls.Add(ComboBoxType);
            y += spacing;

            // Min Threshold
            LabelMinThreshold.Location = new Point(labelX, y + 3);
            LabelMinThreshold.AutoSize = true;
            LabelMinThreshold.Text = "Min Eşik (opsiyonel)";
            Controls.Add(LabelMinThreshold);

            NumericMinThreshold.Location = new Point(inputX, y);
            NumericMinThreshold.Size = new Size(inputWidth, 25);
            NumericMinThreshold.DecimalPlaces = 2;
            NumericMinThreshold.Minimum = -10000;
            NumericMinThreshold.Maximum = 10000;
            Controls.Add(NumericMinThreshold);
            y += spacing;

            // Max Threshold
            LabelMaxThreshold.Location = new Point(labelX, y + 3);
            LabelMaxThreshold.AutoSize = true;
            LabelMaxThreshold.Text = "Max Eşik (opsiyonel)";
            Controls.Add(LabelMaxThreshold);

            NumericMaxThreshold.Location = new Point(inputX, y);
            NumericMaxThreshold.Size = new Size(inputWidth, 25);
            NumericMaxThreshold.DecimalPlaces = 2;
            NumericMaxThreshold.Minimum = -10000;
            NumericMaxThreshold.Maximum = 10000;
            Controls.Add(NumericMaxThreshold);
            y += spacing;

            // Expected Digital Value
            LabelExpectedDigitalValue.Location = new Point(labelX, y + 3);
            LabelExpectedDigitalValue.AutoSize = true;
            LabelExpectedDigitalValue.Text = "Beklenen Dijital Değer";
            Controls.Add(LabelExpectedDigitalValue);

            CheckBoxExpectedDigitalValue.Location = new Point(inputX, y);
            CheckBoxExpectedDigitalValue.AutoSize = true;
            Controls.Add(CheckBoxExpectedDigitalValue);
            y += spacing;

            // Is Active
            LabelIsActive.Location = new Point(labelX, y + 3);
            LabelIsActive.AutoSize = true;
            LabelIsActive.Text = "Aktif Mi?";
            Controls.Add(LabelIsActive);

            CheckBoxIsActive.Location = new Point(inputX, y);
            CheckBoxIsActive.AutoSize = true;
            CheckBoxIsActive.Checked = true;
            Controls.Add(CheckBoxIsActive);
            y += spacing;

            // Cooldown
            LabelCooldown.Location = new Point(labelX, y + 3);
            LabelCooldown.AutoSize = true;
            LabelCooldown.Text = "Soğuma Süresi";
            Controls.Add(LabelCooldown);

            ComboBoxCooldown.Location = new Point(inputX, y);
            ComboBoxCooldown.Size = new Size(inputWidth, 25);
            ComboBoxCooldown.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxCooldown.Items.AddRange(new object[] { "1 Dakika", "10 Dakika", "30 Dakika", "1 Saat", "3 Saat", "1 Gün" });
            Controls.Add(ComboBoxCooldown);
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

            ((System.ComponentModel.ISupportInitialize)NumericMinThreshold).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumericMaxThreshold).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LabelName;
        private TextBox TextBoxName;
        private Label LabelDescription;
        private TextBox TextBoxDescription;
        private Label LabelSensorName;
        private TextBox TextBoxSensorName;
        private Label LabelType;
        private ComboBox ComboBoxType;
        private Label LabelMinThreshold;
        private NumericUpDown NumericMinThreshold;
        private Label LabelMaxThreshold;
        private NumericUpDown NumericMaxThreshold;
        private Label LabelExpectedDigitalValue;
        private CheckBox CheckBoxExpectedDigitalValue;
        private Label LabelIsActive;
        private CheckBox CheckBoxIsActive;
        private Label LabelCooldown;
        private ComboBox ComboBoxCooldown;
        private Button ButtonSave;
        private Button ButtonCancel;
    }
}
