using ISKI.IBKS.Presentation.WinForms.Common.Controls;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.ChildPages
{
    partial class MailStatementsEditPage
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
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            
            TableLayoutPanelMain = new TableLayoutPanel();
            TitleBarControl = new TitleBarControl();
            TableLayoutPanelToolbar = new TableLayoutPanel();
            TextBoxSearch = new TextBox();
            ButtonAddNew = new Button();
            DataGridViewAlarms = new DataGridView();
            ColumnName = new DataGridViewTextBoxColumn();
            ColumnAlarmUsers = new DataGridViewTextBoxColumn();
            UsersColumn = new DataGridViewButtonColumn();
            ColumnDescription = new DataGridViewTextBoxColumn();
            ColumnType = new DataGridViewTextBoxColumn();
            ColumnPriority = new DataGridViewTextBoxColumn();
            ColumnStatus = new DataGridViewTextBoxColumn();
            EditColumn = new DataGridViewButtonColumn();
            DeleteColumn = new DataGridViewButtonColumn();

            TableLayoutPanelMain.SuspendLayout();
            TableLayoutPanelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewAlarms).BeginInit();
            SuspendLayout();

            // TableLayoutPanelMain
            TableLayoutPanelMain.ColumnCount = 1;
            TableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelMain.Controls.Add(TitleBarControl, 0, 0);
            TableLayoutPanelMain.Controls.Add(TableLayoutPanelToolbar, 0, 1);
            TableLayoutPanelMain.Controls.Add(DataGridViewAlarms, 0, 2);
            TableLayoutPanelMain.Dock = DockStyle.Fill;
            TableLayoutPanelMain.Location = new Point(0, 0);
            TableLayoutPanelMain.Name = "TableLayoutPanelMain";
            TableLayoutPanelMain.Padding = new Padding(10);
            TableLayoutPanelMain.RowCount = 3;
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            TableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayoutPanelMain.Size = new Size(1170, 621);
            TableLayoutPanelMain.TabIndex = 0;

            // TitleBarControl
            TitleBarControl.BackColor = Color.FromArgb(235, 235, 235);
            TitleBarControl.Dock = DockStyle.Fill;
            TitleBarControl.Location = new Point(13, 13);
            TitleBarControl.Name = "TitleBarControl";
            TitleBarControl.Padding = new Padding(1);
            TitleBarControl.Size = new Size(1144, 34);
            TitleBarControl.TabIndex = 0;
            TitleBarControl.TitleBarText = "Alarm Tanımları";

            // TableLayoutPanelToolbar
            TableLayoutPanelToolbar.BackColor = Color.White;
            TableLayoutPanelToolbar.ColumnCount = 3;
            TableLayoutPanelToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            TableLayoutPanelToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
            TableLayoutPanelToolbar.Controls.Add(TextBoxSearch, 1, 0);
            TableLayoutPanelToolbar.Controls.Add(ButtonAddNew, 2, 0);
            TableLayoutPanelToolbar.Dock = DockStyle.Fill;
            TableLayoutPanelToolbar.Location = new Point(13, 53);
            TableLayoutPanelToolbar.Name = "TableLayoutPanelToolbar";
            TableLayoutPanelToolbar.RowCount = 1;
            TableLayoutPanelToolbar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayoutPanelToolbar.Size = new Size(1144, 44);
            TableLayoutPanelToolbar.TabIndex = 1;

            // TextBoxSearch
            TextBoxSearch.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TextBoxSearch.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxSearch.Location = new Point(717, 9);
            TextBoxSearch.Name = "TextBoxSearch";
            TextBoxSearch.PlaceholderText = "Ara...";
            TextBoxSearch.Size = new Size(244, 25);
            TextBoxSearch.TabIndex = 0;

            // ButtonAddNew
            ButtonAddNew.Anchor = AnchorStyles.Right;
            ButtonAddNew.BackColor = Color.FromArgb(0, 131, 200);
            ButtonAddNew.FlatAppearance.BorderSize = 0;
            ButtonAddNew.FlatStyle = FlatStyle.Flat;
            ButtonAddNew.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonAddNew.ForeColor = Color.White;
            ButtonAddNew.Location = new Point(970, 7);
            ButtonAddNew.Name = "ButtonAddNew";
            ButtonAddNew.Size = new Size(171, 30);
            ButtonAddNew.TabIndex = 1;
            ButtonAddNew.Text = "Yeni Alarm Ekle";
            ButtonAddNew.UseVisualStyleBackColor = false;

            // DataGridViewAlarms
            DataGridViewAlarms.AllowUserToAddRows = false;
            DataGridViewAlarms.AllowUserToDeleteRows = false;
            DataGridViewAlarms.AllowUserToResizeRows = false;
            DataGridViewAlarms.AutoGenerateColumns = false;
            DataGridViewAlarms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewAlarms.BackgroundColor = Color.White;
            DataGridViewAlarms.BorderStyle = BorderStyle.None;
            DataGridViewAlarms.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = Color.FromArgb(248, 249, 250);
            headerStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            headerStyle.ForeColor = Color.FromArgb(73, 80, 87);
            headerStyle.SelectionBackColor = Color.FromArgb(248, 249, 250);
            headerStyle.SelectionForeColor = Color.FromArgb(73, 80, 87);
            headerStyle.Padding = new Padding(10, 0, 0, 0);
            DataGridViewAlarms.ColumnHeadersDefaultCellStyle = headerStyle;
            DataGridViewAlarms.ColumnHeadersHeight = 45;
            DataGridViewAlarms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewAlarms.EnableHeadersVisualStyles = false;

            DataGridViewAlarms.Columns.AddRange(new DataGridViewColumn[] { ColumnName, ColumnAlarmUsers, UsersColumn, ColumnDescription, ColumnType, ColumnPriority, ColumnStatus, EditColumn, DeleteColumn });
            DataGridViewAlarms.Dock = DockStyle.Fill;
            DataGridViewAlarms.GridColor = Color.FromArgb(233, 236, 239);
            DataGridViewAlarms.Location = new Point(13, 103);
            DataGridViewAlarms.MultiSelect = false;
            DataGridViewAlarms.Name = "DataGridViewAlarms";
            DataGridViewAlarms.ReadOnly = true;
            DataGridViewAlarms.RowHeadersVisible = false;
            
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            cellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            cellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            cellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            cellStyle.SelectionForeColor = Color.FromArgb(33, 37, 41);
            cellStyle.Padding = new Padding(10, 0, 0, 0);
            DataGridViewAlarms.RowsDefaultCellStyle = cellStyle;
            
            DataGridViewAlarms.RowTemplate.Height = 40;
            DataGridViewAlarms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewAlarms.Size = new Size(1144, 505);
            DataGridViewAlarms.TabIndex = 2;

            // ColumnName
            ColumnName.DataPropertyName = "Name";
            ColumnName.HeaderText = "İsim";
            ColumnName.Name = "Name";
            ColumnName.ReadOnly = true;
            ColumnName.FillWeight = 100F;

            // ColumnAlarmUsers
            ColumnAlarmUsers = new DataGridViewTextBoxColumn();
            ColumnAlarmUsers.DataPropertyName = "AlarmUsers";
            ColumnAlarmUsers.HeaderText = "Alarm Kullanıcıları";
            ColumnAlarmUsers.Name = "AlarmUsers";
            ColumnAlarmUsers.ReadOnly = true;
            ColumnAlarmUsers.FillWeight = 160F;
            ColumnAlarmUsers.DefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            ColumnAlarmUsers.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ColumnAlarmUsers.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // UsersColumn
            UsersColumn = new DataGridViewButtonColumn();
            UsersColumn.HeaderText = "";
            UsersColumn.Name = "UsersColumn";
            UsersColumn.ReadOnly = true;
            UsersColumn.Text = "✏️";
            UsersColumn.UseColumnTextForButtonValue = true;
            UsersColumn.FillWeight = 35F;
            UsersColumn.FlatStyle = FlatStyle.Flat;
            UsersColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // ColumnDescription
            ColumnDescription.DataPropertyName = "Description";
            ColumnDescription.HeaderText = "Açıklama";
            ColumnDescription.Name = "Description";
            ColumnDescription.ReadOnly = true;
            ColumnDescription.FillWeight = 130F;

            // ColumnType
            ColumnType.DataPropertyName = "Type";
            ColumnType.HeaderText = "Tip";
            ColumnType.Name = "Type";
            ColumnType.ReadOnly = true;
            ColumnType.FillWeight = 80F;

            // ColumnPriority
            ColumnPriority.DataPropertyName = "Priority";
            ColumnPriority.HeaderText = "Öncelik";
            ColumnPriority.Name = "Priority";
            ColumnPriority.ReadOnly = true;
            ColumnPriority.FillWeight = 70F;

            // ColumnStatus
            ColumnStatus.DataPropertyName = "Status";
            ColumnStatus.HeaderText = "Durum";
            ColumnStatus.Name = "Status";
            ColumnStatus.ReadOnly = true;
            ColumnStatus.FillWeight = 70F;

            // EditColumn
            EditColumn.HeaderText = "";
            EditColumn.Name = "EditColumn";
            EditColumn.ReadOnly = true;
            EditColumn.Text = "✏️";
            EditColumn.UseColumnTextForButtonValue = true;
            EditColumn.FillWeight = 40F;
            EditColumn.FlatStyle = FlatStyle.Flat;

            // DeleteColumn
            DeleteColumn.HeaderText = "";
            DeleteColumn.Name = "DeleteColumn";
            DeleteColumn.ReadOnly = true;
            DeleteColumn.Text = "🗑️";
            DeleteColumn.UseColumnTextForButtonValue = true;
            DeleteColumn.FillWeight = 40F;
            DeleteColumn.FlatStyle = FlatStyle.Flat;

            // MailStatementsEditPage
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            Controls.Add(TableLayoutPanelMain);
            Name = "MailStatementsEditPage";
            Size = new Size(1170, 621);

            TableLayoutPanelMain.ResumeLayout(false);
            TableLayoutPanelToolbar.ResumeLayout(false);
            TableLayoutPanelToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewAlarms).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel TableLayoutPanelMain;
        private TitleBarControl TitleBarControl;
        private TableLayoutPanel TableLayoutPanelToolbar;
        private TextBox TextBoxSearch;
        private Button ButtonAddNew;
        private DataGridView DataGridViewAlarms;
        private DataGridViewTextBoxColumn ColumnName;
        private DataGridViewTextBoxColumn ColumnDescription;
        private DataGridViewTextBoxColumn ColumnType;
        private DataGridViewTextBoxColumn ColumnPriority;
        private DataGridViewTextBoxColumn ColumnStatus;
        private DataGridViewButtonColumn EditColumn;
        private DataGridViewButtonColumn DeleteColumn;
        private DataGridViewTextBoxColumn ColumnAlarmUsers;
        private DataGridViewButtonColumn UsersColumn;
    }
}