namespace QuickLaunch
{
    partial class Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblInfo = new System.Windows.Forms.Label();
            this.boxConfig = new System.Windows.Forms.GroupBox();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.boxLocation = new System.Windows.Forms.GroupBox();
            this.rbMouseLocation = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudLeft = new System.Windows.Forms.NumericUpDown();
            this.nudBottom = new System.Windows.Forms.NumericUpDown();
            this.rbCurrentScreen = new System.Windows.Forms.RadioButton();
            this.rbPrimaryScreen = new System.Windows.Forms.RadioButton();
            this.treeViewLinks = new System.Windows.Forms.TreeView();
            this.btnFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtShortcutFolder = new System.Windows.Forms.TextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip.SuspendLayout();
            this.boxConfig.SuspendLayout();
            this.boxLocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBottom)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.infoToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(604, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadConfigToolStripMenuItem,
            this.saveConfigToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadConfigToolStripMenuItem
            // 
            this.loadConfigToolStripMenuItem.Name = "loadConfigToolStripMenuItem";
            this.loadConfigToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.loadConfigToolStripMenuItem.Text = "Create/Load Config";
            this.loadConfigToolStripMenuItem.Click += new System.EventHandler(this.CreateOrLoadConfigToolStripMenuItem_Click);
            // 
            // saveConfigToolStripMenuItem
            // 
            this.saveConfigToolStripMenuItem.Enabled = false;
            this.saveConfigToolStripMenuItem.Name = "saveConfigToolStripMenuItem";
            this.saveConfigToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.saveConfigToolStripMenuItem.Text = "Save Config";
            this.saveConfigToolStripMenuItem.Click += new System.EventHandler(this.SaveConfigToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(175, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.infoToolStripMenuItem.Text = "Info";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click_1);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 34);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(245, 13);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.Text = "Config not loaded, click File -> Create/Load Config";
            // 
            // boxConfig
            // 
            this.boxConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxConfig.Controls.Add(this.btnMoveDown);
            this.boxConfig.Controls.Add(this.btnMoveUp);
            this.boxConfig.Controls.Add(this.boxLocation);
            this.boxConfig.Controls.Add(this.treeViewLinks);
            this.boxConfig.Controls.Add(this.btnFolder);
            this.boxConfig.Controls.Add(this.label1);
            this.boxConfig.Controls.Add(this.txtShortcutFolder);
            this.boxConfig.Location = new System.Drawing.Point(15, 60);
            this.boxConfig.Name = "boxConfig";
            this.boxConfig.Size = new System.Drawing.Size(577, 346);
            this.boxConfig.TabIndex = 9;
            this.boxConfig.TabStop = false;
            this.boxConfig.Text = "Config";
            this.boxConfig.Visible = false;
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDown.Font = new System.Drawing.Font("Wingdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnMoveDown.Location = new System.Drawing.Point(305, 211);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(42, 30);
            this.btnMoveDown.TabIndex = 20;
            this.btnMoveDown.Text = "";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.BtnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveUp.Font = new System.Drawing.Font("Wingdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnMoveUp.Location = new System.Drawing.Point(305, 162);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(42, 30);
            this.btnMoveUp.TabIndex = 19;
            this.btnMoveUp.Text = "";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.BtnMoveUp_Click);
            // 
            // boxLocation
            // 
            this.boxLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.boxLocation.Controls.Add(this.rbMouseLocation);
            this.boxLocation.Controls.Add(this.label3);
            this.boxLocation.Controls.Add(this.label2);
            this.boxLocation.Controls.Add(this.nudLeft);
            this.boxLocation.Controls.Add(this.nudBottom);
            this.boxLocation.Controls.Add(this.rbCurrentScreen);
            this.boxLocation.Controls.Add(this.rbPrimaryScreen);
            this.boxLocation.Location = new System.Drawing.Point(359, 19);
            this.boxLocation.Name = "boxLocation";
            this.boxLocation.Size = new System.Drawing.Size(199, 161);
            this.boxLocation.TabIndex = 18;
            this.boxLocation.TabStop = false;
            this.boxLocation.Text = "Menu Location:";
            // 
            // rbMouseLocation
            // 
            this.rbMouseLocation.AutoSize = true;
            this.rbMouseLocation.Location = new System.Drawing.Point(15, 65);
            this.rbMouseLocation.Name = "rbMouseLocation";
            this.rbMouseLocation.Size = new System.Drawing.Size(101, 17);
            this.rbMouseLocation.TabIndex = 24;
            this.rbMouseLocation.Text = "Mouse Location";
            this.rbMouseLocation.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Bottom:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Left:";
            // 
            // nudLeft
            // 
            this.nudLeft.Location = new System.Drawing.Point(61, 99);
            this.nudLeft.Maximum = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            this.nudLeft.Minimum = new decimal(new int[] {
            6000,
            0,
            0,
            -2147483648});
            this.nudLeft.Name = "nudLeft";
            this.nudLeft.Size = new System.Drawing.Size(69, 20);
            this.nudLeft.TabIndex = 21;
            this.nudLeft.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudLeft.ValueChanged += new System.EventHandler(this.NudLeft_ValueChanged);
            // 
            // nudBottom
            // 
            this.nudBottom.Location = new System.Drawing.Point(61, 125);
            this.nudBottom.Maximum = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            this.nudBottom.Minimum = new decimal(new int[] {
            6000,
            0,
            0,
            -2147483648});
            this.nudBottom.Name = "nudBottom";
            this.nudBottom.Size = new System.Drawing.Size(69, 20);
            this.nudBottom.TabIndex = 20;
            this.nudBottom.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudBottom.ValueChanged += new System.EventHandler(this.NudBottom_ValueChanged);
            // 
            // rbCurrentScreen
            // 
            this.rbCurrentScreen.AutoSize = true;
            this.rbCurrentScreen.Location = new System.Drawing.Point(15, 42);
            this.rbCurrentScreen.Name = "rbCurrentScreen";
            this.rbCurrentScreen.Size = new System.Drawing.Size(96, 17);
            this.rbCurrentScreen.TabIndex = 19;
            this.rbCurrentScreen.Text = "Current Screen";
            this.rbCurrentScreen.UseVisualStyleBackColor = true;
            // 
            // rbPrimaryScreen
            // 
            this.rbPrimaryScreen.AutoSize = true;
            this.rbPrimaryScreen.Checked = true;
            this.rbPrimaryScreen.Location = new System.Drawing.Point(15, 19);
            this.rbPrimaryScreen.Name = "rbPrimaryScreen";
            this.rbPrimaryScreen.Size = new System.Drawing.Size(96, 17);
            this.rbPrimaryScreen.TabIndex = 18;
            this.rbPrimaryScreen.TabStop = true;
            this.rbPrimaryScreen.Text = "Primary Screen";
            this.rbPrimaryScreen.UseVisualStyleBackColor = true;
            // 
            // treeViewLinks
            // 
            this.treeViewLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewLinks.Location = new System.Drawing.Point(9, 67);
            this.treeViewLinks.Name = "treeViewLinks";
            this.treeViewLinks.Size = new System.Drawing.Size(290, 263);
            this.treeViewLinks.TabIndex = 14;
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(242, 29);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(42, 23);
            this.btnFolder.TabIndex = 13;
            this.btnFolder.Text = "...";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.BtnFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Shortcut Folder:";
            // 
            // txtShortcutFolder
            // 
            this.txtShortcutFolder.Location = new System.Drawing.Point(94, 29);
            this.txtShortcutFolder.Name = "txtShortcutFolder";
            this.txtShortcutFolder.ReadOnly = true;
            this.txtShortcutFolder.Size = new System.Drawing.Size(142, 20);
            this.txtShortcutFolder.TabIndex = 11;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Text = "notifyIcon1";
            this.notifyIcon.Visible = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 418);
            this.Controls.Add(this.boxConfig);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(620, 450);
            this.Name = "Main";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick Launch";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.boxConfig.ResumeLayout(false);
            this.boxConfig.PerformLayout();
            this.boxLocation.ResumeLayout(false);
            this.boxLocation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBottom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.GroupBox boxConfig;
        private System.Windows.Forms.TreeView treeViewLinks;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtShortcutFolder;
        private System.Windows.Forms.GroupBox boxLocation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudLeft;
        private System.Windows.Forms.NumericUpDown nudBottom;
        private System.Windows.Forms.RadioButton rbCurrentScreen;
        private System.Windows.Forms.RadioButton rbPrimaryScreen;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.RadioButton rbMouseLocation;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

