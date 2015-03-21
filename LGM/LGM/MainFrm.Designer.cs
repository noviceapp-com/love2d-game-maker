namespace LGM
{
    partial class MainFrm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Sprites");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Objects");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Backgrounds");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Sounds");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Scripts");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Rooms");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.btnrslist = new System.Windows.Forms.Button();
            this.rslistpnl = new System.Windows.Forms.Panel();
            this.mainpnl = new System.Windows.Forms.Panel();
            this.rslistbtnpnl = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maintoolbar = new System.Windows.Forms.ToolStrip();
            this.resourcelist = new WindowsFormsAero.TreeView();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.rslistpnl.SuspendLayout();
            this.rslistbtnpnl.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.mainpnl);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.rslistbtnpnl);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.rslistpnl);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(948, 578);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // toolStripContainer1.LeftToolStripPanel
            // 
            this.toolStripContainer1.LeftToolStripPanel.BackColor = System.Drawing.Color.White;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.RightToolStripPanel
            // 
            this.toolStripContainer1.RightToolStripPanel.BackColor = System.Drawing.Color.White;
            this.toolStripContainer1.Size = new System.Drawing.Size(948, 636);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.BackColor = System.Drawing.Color.White;
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.maintoolbar);
            // 
            // btnrslist
            // 
            this.btnrslist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnrslist.FlatAppearance.BorderSize = 0;
            this.btnrslist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrslist.Location = new System.Drawing.Point(0, 0);
            this.btnrslist.Name = "btnrslist";
            this.btnrslist.Size = new System.Drawing.Size(22, 578);
            this.btnrslist.TabIndex = 0;
            this.btnrslist.Text = "<";
            this.btnrslist.UseVisualStyleBackColor = true;
            this.btnrslist.Click += new System.EventHandler(this.btnrslist_Click);
            // 
            // rslistpnl
            // 
            this.rslistpnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(212)))), ((int)(((byte)(242)))));
            this.rslistpnl.Controls.Add(this.resourcelist);
            this.rslistpnl.Dock = System.Windows.Forms.DockStyle.Left;
            this.rslistpnl.Location = new System.Drawing.Point(0, 0);
            this.rslistpnl.Name = "rslistpnl";
            this.rslistpnl.Size = new System.Drawing.Size(200, 578);
            this.rslistpnl.TabIndex = 1;
            // 
            // mainpnl
            // 
            this.mainpnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(212)))), ((int)(((byte)(242)))));
            this.mainpnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainpnl.Location = new System.Drawing.Point(222, 0);
            this.mainpnl.Name = "mainpnl";
            this.mainpnl.Size = new System.Drawing.Size(726, 578);
            this.mainpnl.TabIndex = 2;
            // 
            // rslistbtnpnl
            // 
            this.rslistbtnpnl.BackColor = System.Drawing.Color.White;
            this.rslistbtnpnl.Controls.Add(this.btnrslist);
            this.rslistbtnpnl.Dock = System.Windows.Forms.DockStyle.Left;
            this.rslistbtnpnl.Location = new System.Drawing.Point(200, 0);
            this.rslistbtnpnl.Name = "rslistbtnpnl";
            this.rslistbtnpnl.Size = new System.Drawing.Size(22, 578);
            this.rslistbtnpnl.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(948, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // maintoolbar
            // 
            this.maintoolbar.BackColor = System.Drawing.Color.White;
            this.maintoolbar.Dock = System.Windows.Forms.DockStyle.None;
            this.maintoolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.maintoolbar.Location = new System.Drawing.Point(3, 33);
            this.maintoolbar.Name = "maintoolbar";
            this.maintoolbar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.maintoolbar.Size = new System.Drawing.Size(111, 25);
            this.maintoolbar.TabIndex = 1;
            // 
            // resourcelist
            // 
            this.resourcelist.BackColor = System.Drawing.Color.White;
            this.resourcelist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.resourcelist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resourcelist.HotTracking = true;
            this.resourcelist.Location = new System.Drawing.Point(0, 0);
            this.resourcelist.Name = "resourcelist";
            treeNode1.Name = "spritend";
            treeNode1.Text = "Sprites";
            treeNode2.Name = "objectnd";
            treeNode2.Text = "Objects";
            treeNode3.Name = "bgnd";
            treeNode3.Text = "Backgrounds";
            treeNode4.Name = "sndnd";
            treeNode4.Text = "Sounds";
            treeNode5.Name = "scriptnd";
            treeNode5.Text = "Scripts";
            treeNode6.Name = "rmnd";
            treeNode6.Text = "Rooms";
            this.resourcelist.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            this.resourcelist.ShowLines = false;
            this.resourcelist.Size = new System.Drawing.Size(200, 578);
            this.resourcelist.TabIndex = 0;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(948, 636);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Untitled - Love Game Maker";
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.rslistpnl.ResumeLayout(false);
            this.rslistbtnpnl.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Button btnrslist;
        private System.Windows.Forms.Panel rslistpnl;
        private System.Windows.Forms.Panel mainpnl;
        private System.Windows.Forms.Panel rslistbtnpnl;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip maintoolbar;
        private WindowsFormsAero.TreeView resourcelist;
    }
}

