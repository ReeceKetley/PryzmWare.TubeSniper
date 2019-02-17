namespace TubeSniper.Presentation.Views.Nested
{
    partial class SideMenuView
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
            this.tblHeader = new System.Windows.Forms.TableLayoutPanel();
            this.btnSuccess = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnAccounts = new System.Windows.Forms.Button();
            this.tblFooter = new System.Windows.Forms.TableLayoutPanel();
            this.btnLicecne = new System.Windows.Forms.Button();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.tblContainer.SuspendLayout();
            this.tblHeader.SuspendLayout();
            this.tblFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // tblContainer
            // 
            this.tblContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.tblHeader, 0, 1);
            this.tblContainer.Controls.Add(this.tblFooter, 0, 3);
            this.tblContainer.Controls.Add(this.pbLogo, 0, 0);
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 4;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblContainer.Size = new System.Drawing.Size(64, 429);
            this.tblContainer.TabIndex = 0;
            this.tblContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.tblContainer_Paint);
            // 
            // tblHeader
            // 
            this.tblHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblHeader.AutoSize = true;
            this.tblHeader.ColumnCount = 1;
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.Controls.Add(this.btnSuccess, 0, 2);
            this.tblHeader.Controls.Add(this.btnHome, 0, 0);
            this.tblHeader.Controls.Add(this.btnAccounts, 0, 1);
            this.tblHeader.Location = new System.Drawing.Point(0, 97);
            this.tblHeader.Margin = new System.Windows.Forms.Padding(0);
            this.tblHeader.Name = "tblHeader";
            this.tblHeader.RowCount = 3;
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblHeader.Size = new System.Drawing.Size(64, 150);
            this.tblHeader.TabIndex = 0;
            // 
            // btnSuccess
            // 
            this.btnSuccess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSuccess.FlatAppearance.BorderSize = 0;
            this.btnSuccess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(82)))), ((int)(((byte)(83)))));
            this.btnSuccess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(82)))), ((int)(((byte)(83)))));
            this.btnSuccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuccess.Image = global::TubeSniper.Presentation.Properties.Resources.Comments1_34px;
            this.btnSuccess.Location = new System.Drawing.Point(0, 100);
            this.btnSuccess.Margin = new System.Windows.Forms.Padding(0);
            this.btnSuccess.Name = "btnSuccess";
            this.btnSuccess.Padding = new System.Windows.Forms.Padding(12);
            this.btnSuccess.Size = new System.Drawing.Size(64, 50);
            this.btnSuccess.TabIndex = 3;
            this.btnSuccess.UseVisualStyleBackColor = true;
            this.btnSuccess.Click += new System.EventHandler(this.btnSuccess_Click);
            // 
            // btnHome
            // 
            this.btnHome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHome.BackColor = System.Drawing.Color.IndianRed;
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(82)))), ((int)(((byte)(83)))));
            this.btnHome.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(82)))), ((int)(((byte)(83)))));
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Image = global::TubeSniper.Presentation.Properties.Resources.Home_34px;
            this.btnHome.Location = new System.Drawing.Point(0, 0);
            this.btnHome.Margin = new System.Windows.Forms.Padding(0);
            this.btnHome.Name = "btnHome";
            this.btnHome.Padding = new System.Windows.Forms.Padding(12);
            this.btnHome.Size = new System.Drawing.Size(64, 50);
            this.btnHome.TabIndex = 0;
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Click += new System.EventHandler(this.btnCampaigns_Click);
            // 
            // btnAccounts
            // 
            this.btnAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccounts.FlatAppearance.BorderSize = 0;
            this.btnAccounts.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(82)))), ((int)(((byte)(83)))));
            this.btnAccounts.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(82)))), ((int)(((byte)(83)))));
            this.btnAccounts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccounts.Image = global::TubeSniper.Presentation.Properties.Resources.User_Account_34px;
            this.btnAccounts.Location = new System.Drawing.Point(0, 50);
            this.btnAccounts.Margin = new System.Windows.Forms.Padding(0);
            this.btnAccounts.Name = "btnAccounts";
            this.btnAccounts.Padding = new System.Windows.Forms.Padding(12);
            this.btnAccounts.Size = new System.Drawing.Size(64, 50);
            this.btnAccounts.TabIndex = 1;
            this.btnAccounts.UseVisualStyleBackColor = true;
            this.btnAccounts.Click += new System.EventHandler(this.btnAccounts_Click);
            // 
            // tblFooter
            // 
            this.tblFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblFooter.AutoSize = true;
            this.tblFooter.ColumnCount = 1;
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.Controls.Add(this.btnLicecne, 0, 2);
            this.tblFooter.Location = new System.Drawing.Point(0, 378);
            this.tblFooter.Margin = new System.Windows.Forms.Padding(0);
            this.tblFooter.Name = "tblFooter";
            this.tblFooter.RowCount = 3;
            this.tblFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblFooter.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblFooter.Size = new System.Drawing.Size(64, 51);
            this.tblFooter.TabIndex = 1;
            // 
            // btnLicecne
            // 
            this.btnLicecne.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLicecne.FlatAppearance.BorderSize = 0;
            this.btnLicecne.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(82)))), ((int)(((byte)(83)))));
            this.btnLicecne.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(82)))), ((int)(((byte)(83)))));
            this.btnLicecne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLicecne.Image = global::TubeSniper.Presentation.Properties.Resources.Key_34px;
            this.btnLicecne.Location = new System.Drawing.Point(0, 0);
            this.btnLicecne.Margin = new System.Windows.Forms.Padding(0);
            this.btnLicecne.Name = "btnLicecne";
            this.btnLicecne.Padding = new System.Windows.Forms.Padding(12);
            this.btnLicecne.Size = new System.Drawing.Size(64, 51);
            this.btnLicecne.TabIndex = 4;
            this.btnLicecne.UseVisualStyleBackColor = true;
            this.btnLicecne.Click += new System.EventHandler(this.btnLicecne_Click);
            // 
            // pbLogo
            // 
            this.pbLogo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbLogo.Image = global::TubeSniper.Presentation.Properties.Resources.YouTube_40px;
            this.pbLogo.Location = new System.Drawing.Point(9, 12);
            this.pbLogo.Margin = new System.Windows.Forms.Padding(0, 12, 0, 40);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(45, 45);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLogo.TabIndex = 2;
            this.pbLogo.TabStop = false;
            // 
            // SideMenuView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.Controls.Add(this.tblContainer);
            this.Name = "SideMenuView";
            this.Size = new System.Drawing.Size(64, 429);
            this.tblContainer.ResumeLayout(false);
            this.tblContainer.PerformLayout();
            this.tblHeader.ResumeLayout(false);
            this.tblFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.TableLayoutPanel tblHeader;
        private System.Windows.Forms.Button btnSuccess;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnAccounts;
        private System.Windows.Forms.TableLayoutPanel tblFooter;
        private System.Windows.Forms.Button btnLicecne;
    }
}
