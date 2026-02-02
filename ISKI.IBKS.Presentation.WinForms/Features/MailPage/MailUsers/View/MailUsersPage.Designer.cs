using ISKI.IBKS.Presentation.WinForms.Common.Controls;
using ISKI.IBKS.Shared.Localization;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailUsers.View
{
    partial class MailUsersPage
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
            DataGridViewUsers = new DataGridView();
            ColumnFullName = new DataGridViewTextBoxColumn();
            ColumnEmail = new DataGridViewTextBoxColumn();
            ColumnPhoneNumber = new DataGridViewTextBoxColumn();
            ColumnDepartment = new DataGridViewTextBoxColumn();
            ColumnTitle = new DataGridViewTextBoxColumn();
            ColumnStatus = new DataGridViewTextBoxColumn();
            ColumnEmailNotifications = new DataGridViewTextBoxColumn();
            ColumnMinPriorityLevel = new DataGridViewTextBoxColumn();
            EditColumn = new DataGridViewButtonColumn();
            DeleteColumn = new DataGridViewButtonColumn();

            TableLayoutPanelMain.SuspendLayout();
            TableLayoutPanelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewUsers).BeginInit();
            SuspendLayout();

            
            TableLayoutPanelMain.ColumnCount = 1;
            TableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelMain.Controls.Add(TitleBarControl, 0, 0);
            TableLayoutPanelMain.Controls.Add(TableLayoutPanelToolbar, 0, 1);
            TableLayoutPanelMain.Controls.Add(DataGridViewUsers, 0, 2);
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

            
            TitleBarControl.BackColor = Color.FromArgb(235, 235, 235);
            TitleBarControl.Dock = DockStyle.Fill;
            TitleBarControl.Location = new Point(13, 13);
            TitleBarControl.Name = "TitleBarControl";
            TitleBarControl.Padding = new Padding(1);
            TitleBarControl.Size = new Size(1144, 34);
            TitleBarControl.TabIndex = 0;
            TitleBarControl.TitleBarText = Strings.Mail_AlarmUsersTitle;

            
            TableLayoutPanelToolbar.BackColor = Color.White;
            TableLayoutPanelToolbar.ColumnCount = 3;
            TableLayoutPanelToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            TableLayoutPanelToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            TableLayoutPanelToolbar.Controls.Add(TextBoxSearch, 1, 0);
            TableLayoutPanelToolbar.Controls.Add(ButtonAddNew, 2, 0);
            TableLayoutPanelToolbar.Dock = DockStyle.Fill;
            TableLayoutPanelToolbar.Location = new Point(13, 53);
            TableLayoutPanelToolbar.Name = "TableLayoutPanelToolbar";
            TableLayoutPanelToolbar.RowCount = 1;
            TableLayoutPanelToolbar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayoutPanelToolbar.Size = new Size(1144, 44);
            TableLayoutPanelToolbar.TabIndex = 1;

            
            TextBoxSearch.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TextBoxSearch.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            TextBoxSearch.Location = new Point(737, 9);
            TextBoxSearch.Name = "TextBoxSearch";
            TextBoxSearch.PlaceholderText = Strings.Common_SearchPlaceholder;
            TextBoxSearch.Size = new Size(244, 25);
            TextBoxSearch.TabIndex = 0;

            
            ButtonAddNew.Anchor = AnchorStyles.Right;
            ButtonAddNew.BackColor = Color.FromArgb(0, 131, 200);
            ButtonAddNew.FlatAppearance.BorderSize = 0;
            ButtonAddNew.FlatStyle = FlatStyle.Flat;
            ButtonAddNew.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonAddNew.ForeColor = Color.White;
            ButtonAddNew.Location = new Point(990, 7);
            ButtonAddNew.Name = "ButtonAddNew";
            ButtonAddNew.Size = new Size(151, 30);
            ButtonAddNew.TabIndex = 1;
            ButtonAddNew.Text = Strings.Mail_AddNewUser;
            ButtonAddNew.UseVisualStyleBackColor = false;

            
            DataGridViewUsers.AllowUserToAddRows = false;
            DataGridViewUsers.AllowUserToDeleteRows = false;
            DataGridViewUsers.AllowUserToResizeRows = false;
            DataGridViewUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewUsers.BackgroundColor = Color.White;
            DataGridViewUsers.BorderStyle = BorderStyle.None;
            DataGridViewUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            headerStyle.BackColor = Color.FromArgb(248, 249, 250);
            headerStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
            headerStyle.ForeColor = Color.FromArgb(73, 80, 87);
            headerStyle.SelectionBackColor = Color.FromArgb(248, 249, 250);
            headerStyle.SelectionForeColor = Color.FromArgb(73, 80, 87);
            headerStyle.Padding = new Padding(0);
            DataGridViewUsers.ColumnHeadersDefaultCellStyle = headerStyle;
            DataGridViewUsers.ColumnHeadersHeight = 42;
            DataGridViewUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewUsers.EnableHeadersVisualStyles = false;

            DataGridViewUsers.Columns.AddRange(new DataGridViewColumn[] { 
                ColumnFullName, ColumnEmail, ColumnPhoneNumber, ColumnDepartment, ColumnTitle, 
                ColumnStatus, ColumnEmailNotifications, ColumnMinPriorityLevel, EditColumn, DeleteColumn 
            });
            DataGridViewUsers.Dock = DockStyle.Fill;
            DataGridViewUsers.GridColor = Color.FromArgb(233, 236, 239);
            DataGridViewUsers.Location = new Point(13, 103);
            DataGridViewUsers.MultiSelect = false;
            DataGridViewUsers.Name = "DataGridViewUsers";
            DataGridViewUsers.ReadOnly = true;
            DataGridViewUsers.RowHeadersVisible = false;
            
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            cellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            cellStyle.SelectionForeColor = Color.FromArgb(33, 37, 41);
            cellStyle.Padding = new Padding(0);
            DataGridViewUsers.RowsDefaultCellStyle = cellStyle;
            
            DataGridViewUsers.RowTemplate.Height = 38;
            DataGridViewUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewUsers.Size = new Size(1144, 505);
            DataGridViewUsers.TabIndex = 2;

            
            ColumnFullName.DataPropertyName = "FullName";
            ColumnFullName.HeaderText = Strings.Common_FullName;
            ColumnFullName.Name = "FullName";
            ColumnFullName.ReadOnly = true;
            ColumnFullName.FillWeight = 100F;

            
            ColumnEmail.DataPropertyName = "Email";
            ColumnEmail.HeaderText = Strings.Common_Email;
            ColumnEmail.Name = "Email";
            ColumnEmail.ReadOnly = true;
            ColumnEmail.FillWeight = 120F;

            
            ColumnPhoneNumber.DataPropertyName = "PhoneNumber";
            ColumnPhoneNumber.HeaderText = Strings.Common_Phone;
            ColumnPhoneNumber.Name = "PhoneNumber";
            ColumnPhoneNumber.ReadOnly = true;
            ColumnPhoneNumber.FillWeight = 80F;

            
            ColumnDepartment.DataPropertyName = "Department";
            ColumnDepartment.HeaderText = Strings.Common_Department;
            ColumnDepartment.Name = "Department";
            ColumnDepartment.ReadOnly = true;
            ColumnDepartment.FillWeight = 80F;

            
            ColumnTitle.DataPropertyName = "Title";
            ColumnTitle.HeaderText = Strings.Common_Title;
            ColumnTitle.Name = "Title";
            ColumnTitle.ReadOnly = true;
            ColumnTitle.FillWeight = 70F;

            
            ColumnStatus.DataPropertyName = "Status";
            ColumnStatus.HeaderText = Strings.Common_Status;
            ColumnStatus.Name = "Status";
            ColumnStatus.ReadOnly = true;
            ColumnStatus.FillWeight = 50F;

            
            ColumnEmailNotifications.DataPropertyName = "EmailNotifications";
            ColumnEmailNotifications.HeaderText = Strings.Mail_EmailNotifications;
            ColumnEmailNotifications.Name = "EmailNotifications";
            ColumnEmailNotifications.ReadOnly = true;
            ColumnEmailNotifications.FillWeight = 60F;

            
            ColumnMinPriorityLevel.DataPropertyName = "MinPriorityLevel";
            ColumnMinPriorityLevel.HeaderText = Strings.Mail_MinPriorityLevel;
            ColumnMinPriorityLevel.Name = "MinPriorityLevel";
            ColumnMinPriorityLevel.ReadOnly = true;
            ColumnMinPriorityLevel.FillWeight = 60F;

            
            EditColumn.HeaderText = "";
            EditColumn.Name = "EditColumn";
            EditColumn.ReadOnly = true;
            EditColumn.Text = "✏️";
            EditColumn.UseColumnTextForButtonValue = true;
            EditColumn.FillWeight = 35F;
            EditColumn.FlatStyle = FlatStyle.Flat;

            
            DeleteColumn.HeaderText = "";
            DeleteColumn.Name = "DeleteColumn";
            DeleteColumn.ReadOnly = true;
            DeleteColumn.Text = "🗑️";
            DeleteColumn.UseColumnTextForButtonValue = true;
            DeleteColumn.FillWeight = 35F;
            DeleteColumn.FlatStyle = FlatStyle.Flat;

            
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            Controls.Add(TableLayoutPanelMain);
            Name = "MailUsersPage";
            Size = new Size(1170, 621);

            TableLayoutPanelMain.ResumeLayout(false);
            TableLayoutPanelToolbar.ResumeLayout(false);
            TableLayoutPanelToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel TableLayoutPanelMain;
        private TitleBarControl TitleBarControl;
        private TableLayoutPanel TableLayoutPanelToolbar;
        private TextBox TextBoxSearch;
        private Button ButtonAddNew;
        private DataGridView DataGridViewUsers;
        private DataGridViewTextBoxColumn ColumnFullName;
        private DataGridViewTextBoxColumn ColumnEmail;
        private DataGridViewTextBoxColumn ColumnPhoneNumber;
        private DataGridViewTextBoxColumn ColumnDepartment;
        private DataGridViewTextBoxColumn ColumnTitle;
        private DataGridViewTextBoxColumn ColumnStatus;
        private DataGridViewTextBoxColumn ColumnEmailNotifications;
        private DataGridViewTextBoxColumn ColumnMinPriorityLevel;
        private DataGridViewButtonColumn EditColumn;
        private DataGridViewButtonColumn DeleteColumn;
    }
}
