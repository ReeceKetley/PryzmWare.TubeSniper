namespace TubeSniper.Core.Domain.Browser
{
	partial class DebugView
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
			this.WebControl = new EO.WinForm.WebControl();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// WebControl
			// 
			this.WebControl.BackColor = System.Drawing.Color.White;
			this.WebControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WebControl.Location = new System.Drawing.Point(0, 0);
			this.WebControl.Name = "WebControl";
			this.WebControl.Size = new System.Drawing.Size(1649, 959);
			this.WebControl.TabIndex = 0;
			this.WebControl.Text = "webControl1";
			this.WebControl.Click += new System.EventHandler(this.WebControl_Click);
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.textBox1.Location = new System.Drawing.Point(0, 939);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(1649, 20);
			this.textBox1.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(856, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(793, 933);
			this.panel1.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(980, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(95, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Save Source";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// DebugView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1649, 959);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.WebControl);
			this.Name = "DebugView";
			this.Text = "DebugView";
			this.Load += new System.EventHandler(this.DebugView_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private EO.WinForm.WebControl WebControl;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
	}
}