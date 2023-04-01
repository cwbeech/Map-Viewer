namespace Ksu.Cis300.MapViewer
{
    partial class UserInterface
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
            this.uxMenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.uxOpenFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxZoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxZoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxFlowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.uxOpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.map1 = new Ksu.Cis300.MapViewer.Map();
            this.uxMenuStrip1.SuspendLayout();
            this.uxFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxMenuStrip1
            // 
            this.uxMenuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.uxMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.uxMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxOpenFileToolStripMenuItem,
            this.uxZoomInToolStripMenuItem,
            this.uxZoomOutToolStripMenuItem});
            this.uxMenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.uxMenuStrip1.Name = "uxMenuStrip1";
            this.uxMenuStrip1.Size = new System.Drawing.Size(800, 33);
            this.uxMenuStrip1.TabIndex = 0;
            this.uxMenuStrip1.Text = "menuStrip1";
            // 
            // uxOpenFileToolStripMenuItem
            // 
            this.uxOpenFileToolStripMenuItem.Name = "uxOpenFileToolStripMenuItem";
            this.uxOpenFileToolStripMenuItem.Size = new System.Drawing.Size(103, 30);
            this.uxOpenFileToolStripMenuItem.Text = "Open File";
            this.uxOpenFileToolStripMenuItem.Click += new System.EventHandler(this.uxOpenFileToolStripMenuItem_Click);
            // 
            // uxZoomInToolStripMenuItem
            // 
            this.uxZoomInToolStripMenuItem.Enabled = false;
            this.uxZoomInToolStripMenuItem.Name = "uxZoomInToolStripMenuItem";
            this.uxZoomInToolStripMenuItem.Size = new System.Drawing.Size(96, 30);
            this.uxZoomInToolStripMenuItem.Text = "Zoom In";
            this.uxZoomInToolStripMenuItem.Click += new System.EventHandler(this.uxZoomInToolStripMenuItem_Click);
            // 
            // uxZoomOutToolStripMenuItem
            // 
            this.uxZoomOutToolStripMenuItem.Enabled = false;
            this.uxZoomOutToolStripMenuItem.Name = "uxZoomOutToolStripMenuItem";
            this.uxZoomOutToolStripMenuItem.Size = new System.Drawing.Size(111, 29);
            this.uxZoomOutToolStripMenuItem.Text = "Zoom Out";
            this.uxZoomOutToolStripMenuItem.Click += new System.EventHandler(this.uxZoomOutToolStripMenuItem_Click);
            // 
            // uxFlowLayoutPanel1
            // 
            this.uxFlowLayoutPanel1.AutoScroll = true;
            this.uxFlowLayoutPanel1.Controls.Add(this.map1);
            this.uxFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxFlowLayoutPanel1.Location = new System.Drawing.Point(0, 33);
            this.uxFlowLayoutPanel1.Name = "uxFlowLayoutPanel1";
            this.uxFlowLayoutPanel1.Size = new System.Drawing.Size(800, 417);
            this.uxFlowLayoutPanel1.TabIndex = 1;
            // 
            // uxOpenFileDialog1
            // 
            this.uxOpenFileDialog1.Filter = "CSV files|*.csv|All files|*.*";
            // 
            // map1
            // 
            this.map1.BackColor = System.Drawing.Color.White;
            this.map1.Enabled = false;
            this.map1.Location = new System.Drawing.Point(3, 3);
            this.map1.Name = "map1";
            this.map1.QuadTreeLocal = null;
            this.map1.Size = new System.Drawing.Size(150, 150);
            this.map1.TabIndex = 0;
            this.map1.ZoomLevel = 1;
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.uxFlowLayoutPanel1);
            this.Controls.Add(this.uxMenuStrip1);
            this.MainMenuStrip = this.uxMenuStrip1;
            this.Name = "UserInterface";
            this.Text = "Map Viewer";
            this.uxMenuStrip1.ResumeLayout(false);
            this.uxMenuStrip1.PerformLayout();
            this.uxFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip uxMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem uxOpenFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uxZoomInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uxZoomOutToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel uxFlowLayoutPanel1;
        private Map map1;
        private System.Windows.Forms.OpenFileDialog uxOpenFileDialog1;
    }
}

