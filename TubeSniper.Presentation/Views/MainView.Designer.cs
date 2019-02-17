using TubeSniper.Presentation.Views.Nested;

namespace TubeSniper.Presentation.Views
{
    partial class MainView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.sideMenuView = new TubeSniper.Presentation.Views.Nested.SideMenuView();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mainFooterView1 = new TubeSniper.Presentation.Views.Nested.MainFooterView();
            this.contentView = new TubeSniper.Presentation.Views.Nested.MainContentView();
            this.tblContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 3;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.sideMenuView, 0, 0);
            this.tblContainer.Controls.Add(this.pnlLine, 1, 0);
            this.tblContainer.Controls.Add(this.tableLayoutPanel1, 2, 0);
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 1;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Size = new System.Drawing.Size(596, 388);
            this.tblContainer.TabIndex = 0;
            this.tblContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.tblContainer_Paint);
            // 
            // sideMenuView
            // 
            this.sideMenuView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sideMenuView.BackColor = System.Drawing.Color.IndianRed;
            this.sideMenuView.Location = new System.Drawing.Point(0, 0);
            this.sideMenuView.Margin = new System.Windows.Forms.Padding(0);
            this.sideMenuView.Name = "sideMenuView";
            this.sideMenuView.Size = new System.Drawing.Size(65, 388);
            this.sideMenuView.TabIndex = 0;
            this.sideMenuView.Load += new System.EventHandler(this.sideMenuView_Load);
            // 
            // pnlLine
            // 
            this.pnlLine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(73)))), ((int)(((byte)(72)))));
            this.pnlLine.Location = new System.Drawing.Point(65, 0);
            this.pnlLine.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLine.Name = "pnlLine";
            this.pnlLine.Size = new System.Drawing.Size(2, 388);
            this.pnlLine.TabIndex = 1;
            this.pnlLine.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlLine_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.mainFooterView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.contentView, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(67, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(529, 388);
            this.tableLayoutPanel1.TabIndex = 2;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // mainFooterView1
            // 
            this.mainFooterView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainFooterView1.BackColor = System.Drawing.Color.IndianRed;
            this.mainFooterView1.Location = new System.Drawing.Point(0, 368);
            this.mainFooterView1.Margin = new System.Windows.Forms.Padding(0);
            this.mainFooterView1.Name = "mainFooterView1";
            this.mainFooterView1.Size = new System.Drawing.Size(529, 20);
            this.mainFooterView1.TabIndex = 6;
            this.mainFooterView1.Load += new System.EventHandler(this.mainFooterView1_Load);
            // 
            // contentView
            // 
            this.contentView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contentView.Location = new System.Drawing.Point(0, 0);
            this.contentView.Margin = new System.Windows.Forms.Padding(0);
            this.contentView.Name = "contentView";
            this.contentView.Size = new System.Drawing.Size(529, 368);
            this.contentView.TabIndex = 7;
            this.contentView.Load += new System.EventHandler(this.contentView_Load);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(599, 386);
            this.Controls.Add(this.tblContainer);
            this.Name = "MainView";
            this.Text = "MainView";
            this.Load += new System.EventHandler(this.MainView_Load);
            this.tblContainer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private SideMenuView sideMenuView;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MainFooterView mainFooterView1;
        private MainContentView contentView;
    }
}