namespace CMPG223_Project
{
    partial class Menu
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.makeAdvertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeAdvertToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewAdvertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yourAdvertsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.makeAdvertToolStripMenuItem,
            this.userDetailsToolStripMenuItem,
            this.logoutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // makeAdvertToolStripMenuItem
            // 
            this.makeAdvertToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.makeAdvertToolStripMenuItem1,
            this.viewAdvertToolStripMenuItem,
            this.yourAdvertsToolStripMenuItem});
            this.makeAdvertToolStripMenuItem.Name = "makeAdvertToolStripMenuItem";
            this.makeAdvertToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
            this.makeAdvertToolStripMenuItem.Text = " Advertisements";
            // 
            // makeAdvertToolStripMenuItem1
            // 
            this.makeAdvertToolStripMenuItem1.Name = "makeAdvertToolStripMenuItem1";
            this.makeAdvertToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.makeAdvertToolStripMenuItem1.Text = "Make Adverts";
            this.makeAdvertToolStripMenuItem1.Click += new System.EventHandler(this.makeAdvertToolStripMenuItem1_Click);
            // 
            // viewAdvertToolStripMenuItem
            // 
            this.viewAdvertToolStripMenuItem.Name = "viewAdvertToolStripMenuItem";
            this.viewAdvertToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.viewAdvertToolStripMenuItem.Text = "View Adverts";
            this.viewAdvertToolStripMenuItem.Click += new System.EventHandler(this.viewAdvertToolStripMenuItem_Click);
            // 
            // yourAdvertsToolStripMenuItem
            // 
            this.yourAdvertsToolStripMenuItem.Name = "yourAdvertsToolStripMenuItem";
            this.yourAdvertsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.yourAdvertsToolStripMenuItem.Text = "Your Adverts";
            this.yourAdvertsToolStripMenuItem.Click += new System.EventHandler(this.yourAdvertsToolStripMenuItem_Click);
            // 
            // userDetailsToolStripMenuItem
            // 
            this.userDetailsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeDetailsToolStripMenuItem});
            this.userDetailsToolStripMenuItem.Name = "userDetailsToolStripMenuItem";
            this.userDetailsToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.userDetailsToolStripMenuItem.Text = "User Details";
            // 
            // changeDetailsToolStripMenuItem
            // 
            this.changeDetailsToolStripMenuItem.Name = "changeDetailsToolStripMenuItem";
            this.changeDetailsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.changeDetailsToolStripMenuItem.Text = "Change Details";
            this.changeDetailsToolStripMenuItem.Click += new System.EventHandler(this.changeDetailsToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Menu";
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem makeAdvertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeAdvertToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem viewAdvertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yourAdvertsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
    }
}