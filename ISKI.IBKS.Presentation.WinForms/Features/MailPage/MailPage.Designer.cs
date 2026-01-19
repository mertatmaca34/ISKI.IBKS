using ISKI.IBKS.Presentation.WinForms.Common.Controls;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage
{
    partial class MailPage
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
            PanelContent = new Panel();
            menuStrip1 = new MenuStrip();
            ButtonMailStatements = new ToolStripMenuItem();
            ButtonMailUsers = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // PanelContent
            // 
            PanelContent.Dock = DockStyle.Fill;
            PanelContent.Location = new Point(0, 56);
            PanelContent.Name = "PanelContent";
            PanelContent.Size = new Size(1170, 621);
            PanelContent.TabIndex = 3;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.White;
            menuStrip1.Items.AddRange(new ToolStripItem[] { ButtonMailStatements, ButtonMailUsers });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1170, 56);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // ButtonMailStatements
            // 
            ButtonMailStatements.Checked = true;
            ButtonMailStatements.CheckState = CheckState.Indeterminate;
            ButtonMailStatements.Font = new Font("Arial", 9F, FontStyle.Bold);
            ButtonMailStatements.Image = Properties.Resources.mail_statements_48px;
            ButtonMailStatements.ImageScaling = ToolStripItemImageScaling.None;
            ButtonMailStatements.Name = "ButtonMailStatements";
            ButtonMailStatements.Size = new Size(166, 52);
            ButtonMailStatements.Text = "MAİL DURUMLARI";
            // 
            // ButtonMailUsers
            // 
            ButtonMailUsers.Font = new Font("Arial", 9F, FontStyle.Bold);
            ButtonMailUsers.Image = Properties.Resources.mail_user48px;
            ButtonMailUsers.ImageScaling = ToolStripItemImageScaling.None;
            ButtonMailUsers.Name = "ButtonMailUsers";
            ButtonMailUsers.Size = new Size(150, 52);
            ButtonMailUsers.Text = "KULLANICILAR";
            // 
            // MailPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(PanelContent);
            Controls.Add(menuStrip1);
            Name = "MailPage";
            Size = new Size(1170, 677);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel PanelContent;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem ButtonMailStatements;
        private ToolStripMenuItem ButtonMailUsers;
    }
}
