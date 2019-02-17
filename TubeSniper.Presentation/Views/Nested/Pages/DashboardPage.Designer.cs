using TubeSniper.Presentation.Views.Nested.Modules;

namespace TubeSniper.Presentation.Views.Nested.Pages
{
    partial class DashboardPage
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tblDashContent = new System.Windows.Forms.TableLayoutPanel();
            this.flw = new System.Windows.Forms.FlowLayoutPanel();
            this.campaignOverviewModule1 = new TubeSniper.Presentation.Views.Nested.Modules.CampaignTileView();
            this.panel1.SuspendLayout();
            this.tblDashContent.SuspendLayout();
            this.flw.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(516, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dashboard";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.IndianRed;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(516, 37);
            this.panel1.TabIndex = 1;
            // 
            // tblDashContent
            // 
            this.tblDashContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblDashContent.ColumnCount = 1;
            this.tblDashContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDashContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblDashContent.Controls.Add(this.flw, 0, 0);
            this.tblDashContent.Location = new System.Drawing.Point(0, 37);
            this.tblDashContent.Name = "tblDashContent";
            this.tblDashContent.Padding = new System.Windows.Forms.Padding(5, 5, 7, 7);
            this.tblDashContent.RowCount = 2;
            this.tblDashContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDashContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblDashContent.Size = new System.Drawing.Size(515, 299);
            this.tblDashContent.TabIndex = 2;
            // 
            // flw
            // 
            this.flw.Controls.Add(this.campaignOverviewModule1);
            this.flw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flw.Location = new System.Drawing.Point(10, 10);
            this.flw.Margin = new System.Windows.Forms.Padding(5);
            this.flw.Name = "flw";
            this.flw.Size = new System.Drawing.Size(493, 257);
            this.flw.TabIndex = 1;
            // 
            // campaignOverviewModule1
            // 
            this.campaignOverviewModule1.BackColor = System.Drawing.SystemColors.Menu;
            this.campaignOverviewModule1.Location = new System.Drawing.Point(3, 3);
            this.campaignOverviewModule1.Name = "campaignOverviewModule1";
            this.campaignOverviewModule1.Size = new System.Drawing.Size(490, 81);
            this.campaignOverviewModule1.TabIndex = 0;
            // 
            // DashboardPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tblDashContent);
            this.Controls.Add(this.panel1);
            this.Name = "DashboardPage";
            this.Size = new System.Drawing.Size(515, 336);
            this.Load += new System.EventHandler(this.DashboardPage_Load);
            this.panel1.ResumeLayout(false);
            this.tblDashContent.ResumeLayout(false);
            this.flw.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tblDashContent;
        private System.Windows.Forms.FlowLayoutPanel flw;
        private Modules.CampaignTileView campaignOverviewModule1;
    }
}
