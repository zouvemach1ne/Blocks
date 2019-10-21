namespace Blocks
{
    partial class AvailableBlocks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AvailableBlocks));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.HeaderMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blocksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestBlock = new System.Windows.Forms.ToolStripMenuItem();
            this.ScrollPanel = new System.Windows.Forms.Panel();
            this.PanelDiagram = new System.Windows.Forms.Panel();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolBarCoord = new System.Windows.Forms.ToolStrip();
            this.Coordinates = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.BlocksNumber = new System.Windows.Forms.ToolStripLabel();
            this.menuStrip1.SuspendLayout();
            this.ScrollPanel.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.ToolBarCoord.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HeaderMenu,
            this.blocksToolStripMenuItem,
            this.TestBlock});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(622, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // HeaderMenu
            // 
            this.HeaderMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem});
            this.HeaderMenu.Name = "HeaderMenu";
            this.HeaderMenu.Size = new System.Drawing.Size(42, 20);
            this.HeaderMenu.Text = "Files";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // blocksToolStripMenuItem
            // 
            this.blocksToolStripMenuItem.Name = "blocksToolStripMenuItem";
            this.blocksToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.blocksToolStripMenuItem.Text = "Blocks";
            this.blocksToolStripMenuItem.Click += new System.EventHandler(this.blocksToolStripMenuItem_Click);
            // 
            // TestBlock
            // 
            this.TestBlock.Name = "TestBlock";
            this.TestBlock.Size = new System.Drawing.Size(72, 20);
            this.TestBlock.Text = "Test Block";
            this.TestBlock.Click += new System.EventHandler(this.TestBlock_Click);
            // 
            // ScrollPanel
            // 
            this.ScrollPanel.AutoScroll = true;
            this.ScrollPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ScrollPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScrollPanel.Controls.Add(this.PanelDiagram);
            this.ScrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScrollPanel.Location = new System.Drawing.Point(0, 48);
            this.ScrollPanel.Name = "ScrollPanel";
            this.ScrollPanel.Size = new System.Drawing.Size(622, 451);
            this.ScrollPanel.TabIndex = 1;
            this.ScrollPanel.Visible = false;
            this.ScrollPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ScrollPanel_Paint);
            this.ScrollPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ScrollPanel_MouseMove);
            // 
            // PanelDiagram
            // 
            this.PanelDiagram.AutoScroll = true;
            this.PanelDiagram.BackColor = System.Drawing.Color.Transparent;
            this.PanelDiagram.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PanelDiagram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelDiagram.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.PanelDiagram.Location = new System.Drawing.Point(-1, 0);
            this.PanelDiagram.Name = "PanelDiagram";
            this.PanelDiagram.Size = new System.Drawing.Size(611, 418);
            this.PanelDiagram.TabIndex = 0;
            this.PanelDiagram.Scroll += new System.Windows.Forms.ScrollEventHandler(this.PanelDiagram_Scroll);
            this.PanelDiagram.LocationChanged += new System.EventHandler(this.PanelDiagram_LocationChanged);
            this.PanelDiagram.Click += new System.EventHandler(this.PanelDiagram_Click);
            this.PanelDiagram.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelDiagram_Paint);
            this.PanelDiagram.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelDiagram_MouseDown);
            this.PanelDiagram.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelDiagram_MouseMove);
            this.PanelDiagram.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelDiagram_MouseUp);
            this.PanelDiagram.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PanelDiagram_PreviewKeyDown);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInToolStripMenuItem,
            this.zoomOutToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 24);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(622, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // zoomInToolStripMenuItem
            // 
            this.zoomInToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomInToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("zoomInToolStripMenuItem.Image")));
            this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(28, 20);
            this.zoomInToolStripMenuItem.Text = " ";
            this.zoomInToolStripMenuItem.Click += new System.EventHandler(this.zoomInToolStripMenuItem_Click);
            // 
            // zoomOutToolStripMenuItem
            // 
            this.zoomOutToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOutToolStripMenuItem.Image = global::Blocks.Properties.Resources.zoom_out_16;
            this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(28, 20);
            this.zoomOutToolStripMenuItem.Text = " ";
            this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomOutToolStripMenuItem_Click);
            // 
            // ToolBarCoord
            // 
            this.ToolBarCoord.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ToolBarCoord.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Coordinates,
            this.toolStripSeparator1,
            this.BlocksNumber});
            this.ToolBarCoord.Location = new System.Drawing.Point(0, 499);
            this.ToolBarCoord.Name = "ToolBarCoord";
            this.ToolBarCoord.Size = new System.Drawing.Size(622, 25);
            this.ToolBarCoord.TabIndex = 3;
            this.ToolBarCoord.Text = "toolStrip1";
            this.ToolBarCoord.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ToolBar_MouseMove);
            // 
            // Coordinates
            // 
            this.Coordinates.Name = "Coordinates";
            this.Coordinates.Size = new System.Drawing.Size(81, 22);
            this.Coordinates.Text = "(X , Y) = (0 , 0)";
            this.Coordinates.Paint += new System.Windows.Forms.PaintEventHandler(this.Coordinates_Paint);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // BlocksNumber
            // 
            this.BlocksNumber.Name = "BlocksNumber";
            this.BlocksNumber.Size = new System.Drawing.Size(61, 22);
            this.BlocksNumber.Text = "Blocks = 0";
            this.BlocksNumber.Paint += new System.Windows.Forms.PaintEventHandler(this.BlocksNumber_Paint);
            // 
            // AvailableBlocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 524);
            this.Controls.Add(this.ScrollPanel);
            this.Controls.Add(this.ToolBarCoord);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AvailableBlocks";
            this.Text = "Blocks";
            this.Load += new System.EventHandler(this.Blocks_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ScrollPanel.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ToolBarCoord.ResumeLayout(false);
            this.ToolBarCoord.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem HeaderMenu;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.Panel ScrollPanel;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel Coordinates;
        private System.Windows.Forms.ToolStripMenuItem blocksToolStripMenuItem;
        public System.Windows.Forms.ToolStrip ToolBarCoord;
        public System.Windows.Forms.Panel PanelDiagram;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel BlocksNumber;
        private System.Windows.Forms.ToolStripMenuItem TestBlock;

    }
}

