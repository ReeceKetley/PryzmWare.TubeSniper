namespace TubeSniper.Presentation.Views
{
    partial class MainWindowControl
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CampaignId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumProxiesLoaded = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountsLoaded = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Completed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CampaignId,
            this.Message,
            this.NumProxiesLoaded,
            this.AccountsLoaded,
            this.Status,
            this.Completed});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(665, 520);
            this.dataGridView1.TabIndex = 0;
            // 
            // CampaignId
            // 
            this.CampaignId.HeaderText = "Campaign";
            this.CampaignId.Name = "CampaignId";
            // 
            // Comment
            // 
            this.Message.HeaderText = "Comment";
            this.Message.Name = "Message";
            // 
            // NumProxiesLoaded
            // 
            this.NumProxiesLoaded.HeaderText = "Proxies Loaded";
            this.NumProxiesLoaded.Name = "NumProxiesLoaded";
            // 
            // AccountsLoaded
            // 
            this.AccountsLoaded.HeaderText = "Accounts Loaded";
            this.AccountsLoaded.Name = "AccountsLoaded";
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            // 
            // Completed
            // 
            this.Completed.HeaderText = "Succesful Comments";
            this.Completed.Name = "Completed";
            // 
            // MainWindowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "MainWindowControl";
            this.Size = new System.Drawing.Size(665, 520);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CampaignId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumProxiesLoaded;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountsLoaded;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Completed;
    }
}
