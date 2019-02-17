using TubeSniper.Presentation.Views.Nested.Pages;
using TubeSniper.Presentation.Controls;
namespace TubeSniper.Presentation.Views.Nested
{
    partial class MainContentView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.spPages = new TubeSniper.Presentation.Controls.StackPanel();
            this.tpDashboard = new System.Windows.Forms.TabPage();
            this.dashboardPage1 = new TubeSniper.Presentation.Views.Nested.Pages.DashboardPage();
            this.tpUsers = new System.Windows.Forms.TabPage();
            this.tpComments = new System.Windows.Forms.TabPage();
            this.tpLicence = new System.Windows.Forms.TabPage();
            this.tblContainer.SuspendLayout();
            this.spPages.SuspendLayout();
            this.tpDashboard.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblContainer
            // 
            this.tblContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblContainer.Controls.Add(this.spPages, 0, 0);
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 1;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 451F));
            this.tblContainer.Size = new System.Drawing.Size(679, 451);
            this.tblContainer.TabIndex = 0;
            // 
            // spPages
            // 
            this.spPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spPages.Controls.Add(this.tpDashboard);
            this.spPages.Controls.Add(this.tpUsers);
            this.spPages.Controls.Add(this.tpComments);
            this.spPages.Controls.Add(this.tpLicence);
            this.spPages.Location = new System.Drawing.Point(0, 0);
            this.spPages.Margin = new System.Windows.Forms.Padding(0);
            this.spPages.Name = "spPages";
            this.spPages.SelectedIndex = 0;
            this.spPages.Size = new System.Drawing.Size(679, 451);
            this.spPages.TabIndex = 0;
            // 
            // tpDashboard
            // 
            this.tpDashboard.Controls.Add(this.dashboardPage1);
            this.tpDashboard.Location = new System.Drawing.Point(4, 22);
            this.tpDashboard.Name = "tpDashboard";
            this.tpDashboard.Size = new System.Drawing.Size(671, 425);
            this.tpDashboard.TabIndex = 0;
            this.tpDashboard.Text = "Dashboard";
            this.tpDashboard.UseVisualStyleBackColor = true;
            // 
            // dashboardPage1
            // 
            this.dashboardPage1.BackColor = System.Drawing.Color.White;
            this.dashboardPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashboardPage1.Location = new System.Drawing.Point(0, 0);
            this.dashboardPage1.Margin = new System.Windows.Forms.Padding(0);
            this.dashboardPage1.Name = "dashboardPage1";
            this.dashboardPage1.Size = new System.Drawing.Size(671, 425);
            this.dashboardPage1.TabIndex = 0;
            this.dashboardPage1.Load += new System.EventHandler(this.dashboardPage1_Load);
            // 
            // tpUsers
            // 
            this.tpUsers.Location = new System.Drawing.Point(4, 22);
            this.tpUsers.Name = "tpUsers";
            this.tpUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tpUsers.Size = new System.Drawing.Size(671, 425);
            this.tpUsers.TabIndex = 1;
            this.tpUsers.Text = "Users && Proxies";
            this.tpUsers.UseVisualStyleBackColor = true;
            // 
            // tpComments
            // 
            this.tpComments.Location = new System.Drawing.Point(4, 22);
            this.tpComments.Name = "tpComments";
            this.tpComments.Size = new System.Drawing.Size(671, 425);
            this.tpComments.TabIndex = 2;
            this.tpComments.Text = "Successful Comments";
            this.tpComments.UseVisualStyleBackColor = true;
            // 
            // tpLicence
            // 
            this.tpLicence.Location = new System.Drawing.Point(4, 22);
            this.tpLicence.Name = "tpLicence";
            this.tpLicence.Padding = new System.Windows.Forms.Padding(3);
            this.tpLicence.Size = new System.Drawing.Size(671, 425);
            this.tpLicence.TabIndex = 3;
            this.tpLicence.Text = "License";
            this.tpLicence.UseVisualStyleBackColor = true;
            // 
            // MainContentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tblContainer);
            this.Name = "MainContentView";
            this.Size = new System.Drawing.Size(679, 451);
            this.tblContainer.ResumeLayout(false);
            this.spPages.ResumeLayout(false);
            this.tpDashboard.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private StackPanel spPages;
        private System.Windows.Forms.TabPage tpDashboard;
        private System.Windows.Forms.TabPage tpUsers;
        private Pages.DashboardPage dashboardPage1;
        private System.Windows.Forms.TabPage tpComments;
        private System.Windows.Forms.TabPage tpLicence;
    }
}
