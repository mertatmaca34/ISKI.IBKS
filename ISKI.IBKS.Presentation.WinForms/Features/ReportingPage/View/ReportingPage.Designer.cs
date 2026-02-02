using ISKI.IBKS.Presentation.WinForms.Common.Controls;

namespace ISKI.IBKS.Presentation.WinForms.Features.ReportingPage.View
{
    partial class ReportingPage
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            tableLayoutPanel1 = new TableLayoutPanel();
            titleBarControl1 = new TitleBarControl();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            GroupBoxReportTypes = new GroupBox();
            ComboBoxReportType = new ComboBox();
            groupBox3 = new GroupBox();
            RadioButtonSortByLast = new RadioButton();
            RadioButtonSortByFirst = new RadioButton();
            GroupBoxDate = new GroupBox();
            LabelEndDate = new Label();
            LabelStartDate = new Label();
            DateTimePickerLastTime = new DateTimePicker();
            DateTimePickerLastDate = new DateTimePicker();
            DateTimePickerFirstTime = new DateTimePicker();
            DateTimePickerFirstDate = new DateTimePicker();
            groupBox1 = new GroupBox();
            RadioButtonCustom = new RadioButton();
            RadioButtonMonthly = new RadioButton();
            RadioButtonWeekly = new RadioButton();
            RadioButtonDaily = new RadioButton();
            GroupBoxLogLevel = new GroupBox();
            CheckBoxLogInfo = new CheckBox();
            CheckBoxLogWarning = new CheckBox();
            CheckBoxLogError = new CheckBox();
            ButtonGenerate = new Button();
            tableLayoutPanel4 = new TableLayoutPanel();
            DataGridViewDatas = new DataGridView();
            tableLayoutPanel5 = new TableLayoutPanel();
            tableLayoutPanel6 = new TableLayoutPanel();
            ButtonSaveAsExcel = new Button();
            ButtonSaveAsPdf = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            GroupBoxReportTypes.SuspendLayout();
            groupBox3.SuspendLayout();
            GroupBoxDate.SuspendLayout();
            groupBox1.SuspendLayout();
            GroupBoxLogLevel.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewDatas).BeginInit();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            SuspendLayout();
            
            
            
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(titleBarControl1, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 1, 2);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel5, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(8);
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1170, 677);
            tableLayoutPanel1.TabIndex = 0;
            
            
            
            titleBarControl1.BackColor = Color.White;
            tableLayoutPanel1.SetColumnSpan(titleBarControl1, 2);
            titleBarControl1.Dock = DockStyle.Fill;
            titleBarControl1.Location = new Point(11, 11);
            titleBarControl1.Name = "titleBarControl1";
            titleBarControl1.Padding = new Padding(1);
            titleBarControl1.Size = new Size(1148, 32);
            titleBarControl1.TabIndex = 0;
            titleBarControl1.TitleBarText = "Raporlama EkranÄ±";
            
            
            
            tableLayoutPanel2.BackColor = Color.FromArgb(248, 249, 250);
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(11, 49);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel1.SetRowSpan(tableLayoutPanel2, 2);
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(214, 617);
            tableLayoutPanel2.TabIndex = 1;
            
            
            
            tableLayoutPanel3.BackColor = Color.White;
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(GroupBoxReportTypes, 0, 0);
            tableLayoutPanel3.Controls.Add(groupBox3, 0, 3);
            tableLayoutPanel3.Controls.Add(GroupBoxDate, 0, 2);
            tableLayoutPanel3.Controls.Add(groupBox1, 0, 1);
            tableLayoutPanel3.Controls.Add(GroupBoxLogLevel, 0, 4);
            tableLayoutPanel3.Controls.Add(ButtonGenerate, 0, 5);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            tableLayoutPanel3.Location = new Point(1, 1);
            tableLayoutPanel3.Margin = new Padding(1);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.Padding = new Padding(3);
            tableLayoutPanel3.RowCount = 7;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 85F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 185F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 85F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(212, 615);
            tableLayoutPanel3.TabIndex = 2;
            
            
            
            GroupBoxReportTypes.Controls.Add(ComboBoxReportType);
            GroupBoxReportTypes.Dock = DockStyle.Fill;
            GroupBoxReportTypes.FlatStyle = FlatStyle.Flat;
            GroupBoxReportTypes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            GroupBoxReportTypes.ForeColor = Color.FromArgb(0, 120, 215);
            GroupBoxReportTypes.Location = new Point(6, 6);
            GroupBoxReportTypes.Name = "GroupBoxReportTypes";
            GroupBoxReportTypes.Size = new Size(200, 49);
            GroupBoxReportTypes.TabIndex = 5;
            GroupBoxReportTypes.TabStop = false;
            GroupBoxReportTypes.Text = "RAPOR TÄ°PÄ°";
            
            
            
            ComboBoxReportType.Dock = DockStyle.Top;
            ComboBoxReportType.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxReportType.FormattingEnabled = true;
            ComboBoxReportType.Items.AddRange(new object[] { "Ã–lÃ§Ã¼m", "Kalibrasyon", "Numune", "KayÄ±t", "Veri GeÃ§erlilik Durumu" });
            ComboBoxReportType.Location = new Point(3, 19);
            ComboBoxReportType.Name = "ComboBoxReportType";
            ComboBoxReportType.Size = new Size(194, 23);
            ComboBoxReportType.TabIndex = 0;
            
            
            
            groupBox3.Controls.Add(RadioButtonSortByLast);
            groupBox3.Controls.Add(RadioButtonSortByFirst);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.FlatStyle = FlatStyle.Flat;
            groupBox3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox3.ForeColor = Color.FromArgb(0, 120, 215);
            groupBox3.Location = new Point(6, 331);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(200, 79);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "SIRALAMA";
            
            
            
            RadioButtonSortByLast.AutoSize = true;
            RadioButtonSortByLast.Checked = true;
            RadioButtonSortByLast.ForeColor = Color.Black;
            RadioButtonSortByLast.Location = new Point(10, 50);
            RadioButtonSortByLast.Name = "RadioButtonSortByLast";
            RadioButtonSortByLast.Size = new Size(115, 19);
            RadioButtonSortByLast.TabIndex = 2;
            RadioButtonSortByLast.TabStop = true;
            RadioButtonSortByLast.Text = "Son Veriye GÃ¶re";
            RadioButtonSortByLast.UseVisualStyleBackColor = true;
            
            
            
            RadioButtonSortByFirst.AutoSize = true;
            RadioButtonSortByFirst.ForeColor = Color.Black;
            RadioButtonSortByFirst.Location = new Point(10, 25);
            RadioButtonSortByFirst.Name = "RadioButtonSortByFirst";
            RadioButtonSortByFirst.Size = new Size(108, 19);
            RadioButtonSortByFirst.TabIndex = 2;
            RadioButtonSortByFirst.Text = "Ä°lk Veriye GÃ¶re";
            RadioButtonSortByFirst.UseVisualStyleBackColor = true;
            
            
            
            GroupBoxDate.Controls.Add(LabelEndDate);
            GroupBoxDate.Controls.Add(LabelStartDate);
            GroupBoxDate.Controls.Add(DateTimePickerLastTime);
            GroupBoxDate.Controls.Add(DateTimePickerLastDate);
            GroupBoxDate.Controls.Add(DateTimePickerFirstTime);
            GroupBoxDate.Controls.Add(DateTimePickerFirstDate);
            GroupBoxDate.Dock = DockStyle.Fill;
            GroupBoxDate.Enabled = false;
            GroupBoxDate.FlatStyle = FlatStyle.Flat;
            GroupBoxDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            GroupBoxDate.ForeColor = Color.FromArgb(0, 120, 215);
            GroupBoxDate.Location = new Point(6, 146);
            GroupBoxDate.Name = "GroupBoxDate";
            GroupBoxDate.Size = new Size(200, 179);
            GroupBoxDate.TabIndex = 3;
            GroupBoxDate.TabStop = false;
            GroupBoxDate.Text = "TARÄ°H";
            
            
            
            LabelEndDate.AutoSize = true;
            LabelEndDate.Font = new Font("Segoe UI", 8.5F);
            LabelEndDate.ForeColor = Color.Gray;
            LabelEndDate.Location = new Point(6, 95);
            LabelEndDate.Name = "LabelEndDate";
            LabelEndDate.Size = new Size(29, 15);
            LabelEndDate.TabIndex = 5;
            LabelEndDate.Text = "BitiÅŸ";
            
            
            
            LabelStartDate.AutoSize = true;
            LabelStartDate.Font = new Font("Segoe UI", 8.5F);
            LabelStartDate.ForeColor = Color.Gray;
            LabelStartDate.Location = new Point(6, 22);
            LabelStartDate.Name = "LabelStartDate";
            LabelStartDate.Size = new Size(57, 15);
            LabelStartDate.TabIndex = 4;
            LabelStartDate.Text = "BaÅŸlangÄ±Ã§";
            
            
            
            DateTimePickerLastTime.Format = DateTimePickerFormat.Time;
            DateTimePickerLastTime.Location = new Point(6, 140);
            DateTimePickerLastTime.Name = "DateTimePickerLastTime";
            DateTimePickerLastTime.ShowUpDown = true;
            DateTimePickerLastTime.Size = new Size(188, 23);
            DateTimePickerLastTime.TabIndex = 3;
            
            
            
            DateTimePickerLastDate.Location = new Point(6, 113);
            DateTimePickerLastDate.Name = "DateTimePickerLastDate";
            DateTimePickerLastDate.Size = new Size(188, 23);
            DateTimePickerLastDate.TabIndex = 2;
            
            
            
            DateTimePickerFirstTime.Format = DateTimePickerFormat.Time;
            DateTimePickerFirstTime.Location = new Point(6, 67);
            DateTimePickerFirstTime.Name = "DateTimePickerFirstTime";
            DateTimePickerFirstTime.ShowUpDown = true;
            DateTimePickerFirstTime.Size = new Size(188, 23);
            DateTimePickerFirstTime.TabIndex = 3;
            
            
            
            DateTimePickerFirstDate.Location = new Point(6, 40);
            DateTimePickerFirstDate.Name = "DateTimePickerFirstDate";
            DateTimePickerFirstDate.Size = new Size(188, 23);
            DateTimePickerFirstDate.TabIndex = 2;
            
            
            
            groupBox1.Controls.Add(RadioButtonCustom);
            groupBox1.Controls.Add(RadioButtonMonthly);
            groupBox1.Controls.Add(RadioButtonWeekly);
            groupBox1.Controls.Add(RadioButtonDaily);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.FlatStyle = FlatStyle.Flat;
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox1.ForeColor = Color.FromArgb(0, 120, 215);
            groupBox1.Location = new Point(6, 61);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 79);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "PERÄ°YOT";
            
            
            
            RadioButtonCustom.AutoSize = true;
            RadioButtonCustom.ForeColor = Color.Black;
            RadioButtonCustom.Location = new Point(105, 50);
            RadioButtonCustom.Name = "RadioButtonCustom";
            RadioButtonCustom.Size = new Size(50, 19);
            RadioButtonCustom.TabIndex = 2;
            RadioButtonCustom.TabStop = true;
            RadioButtonCustom.Text = "Ã–zel";
            RadioButtonCustom.UseVisualStyleBackColor = true;
            
            
            
            RadioButtonMonthly.AutoSize = true;
            RadioButtonMonthly.ForeColor = Color.Black;
            RadioButtonMonthly.Location = new Point(10, 50);
            RadioButtonMonthly.Name = "RadioButtonMonthly";
            RadioButtonMonthly.Size = new Size(52, 19);
            RadioButtonMonthly.TabIndex = 2;
            RadioButtonMonthly.TabStop = true;
            RadioButtonMonthly.Text = "AylÄ±k";
            RadioButtonMonthly.UseVisualStyleBackColor = true;
            
            
            
            RadioButtonWeekly.AutoSize = true;
            RadioButtonWeekly.ForeColor = Color.Black;
            RadioButtonWeekly.Location = new Point(105, 25);
            RadioButtonWeekly.Name = "RadioButtonWeekly";
            RadioButtonWeekly.Size = new Size(69, 19);
            RadioButtonWeekly.TabIndex = 2;
            RadioButtonWeekly.TabStop = true;
            RadioButtonWeekly.Text = "HaftalÄ±k";
            RadioButtonWeekly.UseVisualStyleBackColor = true;
            
            
            
            RadioButtonDaily.AutoSize = true;
            RadioButtonDaily.ForeColor = Color.Black;
            RadioButtonDaily.Location = new Point(10, 25);
            RadioButtonDaily.Name = "RadioButtonDaily";
            RadioButtonDaily.Size = new Size(65, 19);
            RadioButtonDaily.TabIndex = 2;
            RadioButtonDaily.TabStop = true;
            RadioButtonDaily.Text = "GÃ¼nlÃ¼k";
            RadioButtonDaily.UseVisualStyleBackColor = true;
            
            
            
            GroupBoxLogLevel.Controls.Add(CheckBoxLogInfo);
            GroupBoxLogLevel.Controls.Add(CheckBoxLogWarning);
            GroupBoxLogLevel.Controls.Add(CheckBoxLogError);
            GroupBoxLogLevel.Dock = DockStyle.Fill;
            GroupBoxLogLevel.FlatStyle = FlatStyle.Flat;
            GroupBoxLogLevel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            GroupBoxLogLevel.ForeColor = Color.FromArgb(0, 120, 215);
            GroupBoxLogLevel.Location = new Point(6, 416);
            GroupBoxLogLevel.Name = "GroupBoxLogLevel";
            GroupBoxLogLevel.Size = new Size(200, 94);
            GroupBoxLogLevel.TabIndex = 6;
            GroupBoxLogLevel.TabStop = false;
            GroupBoxLogLevel.Text = "LOG SEVÄ°YESÄ°";
            GroupBoxLogLevel.Visible = false;
            
            
            
            CheckBoxLogInfo.AutoSize = true;
            CheckBoxLogInfo.Checked = true;
            CheckBoxLogInfo.CheckState = CheckState.Checked;
            CheckBoxLogInfo.ForeColor = Color.Black;
            CheckBoxLogInfo.Location = new Point(10, 20);
            CheckBoxLogInfo.Name = "CheckBoxLogInfo";
            CheckBoxLogInfo.Size = new Size(49, 19);
            CheckBoxLogInfo.TabIndex = 0;
            CheckBoxLogInfo.Text = "Info";
            CheckBoxLogInfo.UseVisualStyleBackColor = true;
            
            
            
            CheckBoxLogWarning.AutoSize = true;
            CheckBoxLogWarning.Checked = true;
            CheckBoxLogWarning.CheckState = CheckState.Checked;
            CheckBoxLogWarning.ForeColor = Color.Black;
            CheckBoxLogWarning.Location = new Point(10, 41);
            CheckBoxLogWarning.Name = "CheckBoxLogWarning";
            CheckBoxLogWarning.Size = new Size(73, 19);
            CheckBoxLogWarning.TabIndex = 1;
            CheckBoxLogWarning.Text = "Warning";
            CheckBoxLogWarning.UseVisualStyleBackColor = true;
            
            
            
            CheckBoxLogError.AutoSize = true;
            CheckBoxLogError.Checked = true;
            CheckBoxLogError.CheckState = CheckState.Checked;
            CheckBoxLogError.ForeColor = Color.Black;
            CheckBoxLogError.Location = new Point(10, 62);
            CheckBoxLogError.Name = "CheckBoxLogError";
            CheckBoxLogError.Size = new Size(54, 19);
            CheckBoxLogError.TabIndex = 2;
            CheckBoxLogError.Text = "Error";
            CheckBoxLogError.UseVisualStyleBackColor = true;
            
            
            
            ButtonGenerate.BackColor = Color.FromArgb(0, 131, 200);
            ButtonGenerate.Cursor = Cursors.Hand;
            ButtonGenerate.Dock = DockStyle.Top;
            ButtonGenerate.FlatAppearance.BorderSize = 0;
            ButtonGenerate.FlatStyle = FlatStyle.Flat;
            ButtonGenerate.ForeColor = Color.White;
            ButtonGenerate.Location = new Point(6, 516);
            ButtonGenerate.Name = "ButtonGenerate";
            ButtonGenerate.Size = new Size(200, 40);
            ButtonGenerate.TabIndex = 2;
            ButtonGenerate.Text = "OLUÅTUR";
            ButtonGenerate.UseVisualStyleBackColor = false;
            
            
            
            tableLayoutPanel4.BackColor = Color.FromArgb(248, 249, 250);
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(DataGridViewDatas, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(231, 89);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(928, 577);
            tableLayoutPanel4.TabIndex = 2;
            
            
            
            DataGridViewDatas.AllowUserToDeleteRows = false;
            DataGridViewDatas.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(250, 251, 252);
            DataGridViewDatas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DataGridViewDatas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewDatas.BackgroundColor = Color.White;
            DataGridViewDatas.BorderStyle = BorderStyle.None;
            DataGridViewDatas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridViewDatas.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            DataGridViewDatas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(0, 120, 215);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DataGridViewDatas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            DataGridViewDatas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewDatas.Dock = DockStyle.Fill;
            DataGridViewDatas.EnableHeadersVisualStyles = false;
            DataGridViewDatas.GridColor = Color.FromArgb(224, 224, 224);
            DataGridViewDatas.Location = new Point(1, 1);
            DataGridViewDatas.Margin = new Padding(1);
            DataGridViewDatas.Name = "DataGridViewDatas";
            DataGridViewDatas.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(33, 37, 41);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(33, 37, 41);
            DataGridViewDatas.RowsDefaultCellStyle = dataGridViewCellStyle3;
            DataGridViewDatas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewDatas.Size = new Size(926, 575);
            DataGridViewDatas.TabIndex = 0;
            
            
            
            tableLayoutPanel5.BackColor = Color.FromArgb(248, 249, 250);
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Controls.Add(tableLayoutPanel6, 0, 0);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(231, 49);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(928, 34);
            tableLayoutPanel5.TabIndex = 3;
            
            
            
            tableLayoutPanel6.BackColor = Color.White;
            tableLayoutPanel6.ColumnCount = 3;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 32F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 32F));
            tableLayoutPanel6.Controls.Add(ButtonSaveAsExcel, 1, 0);
            tableLayoutPanel6.Controls.Add(ButtonSaveAsPdf, 2, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(1, 1);
            tableLayoutPanel6.Margin = new Padding(1);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Size = new Size(926, 32);
            tableLayoutPanel6.TabIndex = 0;
            
            
            
            ButtonSaveAsExcel.BackgroundImage = Properties.Resources.microsoft_excel_2019_32px;
            ButtonSaveAsExcel.BackgroundImageLayout = ImageLayout.Zoom;
            ButtonSaveAsExcel.Cursor = Cursors.Hand;
            ButtonSaveAsExcel.Dock = DockStyle.Fill;
            ButtonSaveAsExcel.FlatAppearance.BorderSize = 0;
            ButtonSaveAsExcel.FlatStyle = FlatStyle.Flat;
            ButtonSaveAsExcel.Location = new Point(865, 3);
            ButtonSaveAsExcel.Name = "ButtonSaveAsExcel";
            ButtonSaveAsExcel.Size = new Size(26, 26);
            ButtonSaveAsExcel.TabIndex = 2;
            ButtonSaveAsExcel.UseVisualStyleBackColor = true;
            
            
            
            ButtonSaveAsPdf.BackgroundImage = Properties.Resources.pdf_32px;
            ButtonSaveAsPdf.BackgroundImageLayout = ImageLayout.Zoom;
            ButtonSaveAsPdf.Cursor = Cursors.Hand;
            ButtonSaveAsPdf.Dock = DockStyle.Fill;
            ButtonSaveAsPdf.FlatAppearance.BorderSize = 0;
            ButtonSaveAsPdf.FlatStyle = FlatStyle.Flat;
            ButtonSaveAsPdf.Location = new Point(897, 3);
            ButtonSaveAsPdf.Name = "ButtonSaveAsPdf";
            ButtonSaveAsPdf.Size = new Size(26, 26);
            ButtonSaveAsPdf.TabIndex = 1;
            ButtonSaveAsPdf.UseVisualStyleBackColor = true;
            
            
            
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(tableLayoutPanel1);
            Name = "ReportingPage";
            Size = new Size(1170, 677);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            GroupBoxReportTypes.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            GroupBoxDate.ResumeLayout(false);
            GroupBoxDate.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            GroupBoxLogLevel.ResumeLayout(false);
            GroupBoxLogLevel.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridViewDatas).EndInit();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel6.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TitleBarControl titleBarControl1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private GroupBox groupBox1;
        private GroupBox GroupBoxDate;
        private GroupBox groupBox3;
        private Button ButtonGenerate;
        private RadioButton RadioButtonCustom;
        private RadioButton RadioButtonMonthly;
        private RadioButton RadioButtonWeekly;
        private RadioButton RadioButtonDaily;
        private DateTimePicker DateTimePickerFirstTime;
        private DateTimePicker DateTimePickerFirstDate;
        private DateTimePicker DateTimePickerLastTime;
        private DateTimePicker DateTimePickerLastDate;
        private RadioButton RadioButtonSortByFirst;
        private RadioButton RadioButtonSortByLast;
        private GroupBox GroupBoxReportTypes;
        private ComboBox ComboBoxReportType;
        private TableLayoutPanel tableLayoutPanel4;
        private DataGridView DataGridViewDatas;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel6;
        private Button ButtonSaveAsPdf;
        private Button ButtonSaveAsExcel;
        private GroupBox GroupBoxLogLevel;
        private CheckBox CheckBoxLogInfo;
        private CheckBox CheckBoxLogWarning;
        private CheckBox CheckBoxLogError;
        private Label LabelStartDate;
        private Label LabelEndDate;
    }
}

