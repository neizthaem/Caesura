namespace Client
{
    partial class ClientUI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientUI));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mESSAGEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hELPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jAPANESEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.downloadButton = new System.Windows.Forms.Button();
            this.messagesLabel = new System.Windows.Forms.Label();
            this.inboxButton = new System.Windows.Forms.Button();
            this.tagsButton = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.locationsList = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.viewer = new System.Windows.Forms.ListView();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 346);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(540, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mESSAGEToolStripMenuItem,
            this.hELPToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(540, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.fileToolStripMenuItem.Text = "FILE";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mESSAGEToolStripMenuItem
            // 
            this.mESSAGEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inboxToolStripMenuItem,
            this.sendToolStripMenuItem});
            this.mESSAGEToolStripMenuItem.Name = "mESSAGEToolStripMenuItem";
            this.mESSAGEToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.mESSAGEToolStripMenuItem.Text = "MESSAGES";
            // 
            // inboxToolStripMenuItem
            // 
            this.inboxToolStripMenuItem.Name = "inboxToolStripMenuItem";
            this.inboxToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.inboxToolStripMenuItem.Text = "Inbox";
            this.inboxToolStripMenuItem.Click += new System.EventHandler(this.inboxToolStripMenuItem_Click);
            // 
            // sendToolStripMenuItem
            // 
            this.sendToolStripMenuItem.Name = "sendToolStripMenuItem";
            this.sendToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sendToolStripMenuItem.Text = "Send";
            // 
            // hELPToolStripMenuItem
            // 
            this.hELPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.hELPToolStripMenuItem.Name = "hELPToolStripMenuItem";
            this.hELPToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.hELPToolStripMenuItem.Text = "HELP";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About Caesura";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePasswordToolStripMenuItem,
            this.jAPANESEToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(80, 20);
            this.toolStripMenuItem1.Text = "LANGUAGE";
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.changePasswordToolStripMenuItem.Text = "ENGLISH";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // jAPANESEToolStripMenuItem
            // 
            this.jAPANESEToolStripMenuItem.Name = "jAPANESEToolStripMenuItem";
            this.jAPANESEToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.jAPANESEToolStripMenuItem.Text = "日本語";
            this.jAPANESEToolStripMenuItem.Click += new System.EventHandler(this.jAPANESEToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.downloadButton);
            this.panel1.Controls.Add(this.messagesLabel);
            this.panel1.Controls.Add(this.inboxButton);
            this.panel1.Controls.Add(this.tagsButton);
            this.panel1.Controls.Add(this.searchButton);
            this.panel1.Controls.Add(this.locationsList);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.searchBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(165, 316);
            this.panel1.TabIndex = 2;
            // 
            // downloadButton
            // 
            this.downloadButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.downloadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadButton.Location = new System.Drawing.Point(43, 212);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(85, 23);
            this.downloadButton.TabIndex = 8;
            this.downloadButton.Text = global::Client.strings.Download;
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // messagesLabel
            // 
            this.messagesLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.messagesLabel.AutoSize = true;
            this.messagesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messagesLabel.Location = new System.Drawing.Point(34, 266);
            this.messagesLabel.Name = "messagesLabel";
            this.messagesLabel.Size = new System.Drawing.Size(102, 15);
            this.messagesLabel.TabIndex = 7;
            this.messagesLabel.Text = "0 New Messages";
            // 
            // inboxButton
            // 
            this.inboxButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.inboxButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inboxButton.Location = new System.Drawing.Point(43, 284);
            this.inboxButton.Name = "inboxButton";
            this.inboxButton.Size = new System.Drawing.Size(85, 23);
            this.inboxButton.TabIndex = 6;
            this.inboxButton.Text = "Open Inbox";
            this.inboxButton.UseVisualStyleBackColor = true;
            // 
            // tagsButton
            // 
            this.tagsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tagsButton.Location = new System.Drawing.Point(6, 154);
            this.tagsButton.Name = "tagsButton";
            this.tagsButton.Size = new System.Drawing.Size(74, 23);
            this.tagsButton.TabIndex = 5;
            this.tagsButton.Text = "Tags";
            this.tagsButton.UseVisualStyleBackColor = true;
            this.tagsButton.Click += new System.EventHandler(this.tagsButton_Click);
            // 
            // searchButton
            // 
            this.searchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchButton.Location = new System.Drawing.Point(85, 154);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(74, 23);
            this.searchButton.TabIndex = 4;
            this.searchButton.Text = global::Client.strings.Search;
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // locationsList
            // 
            this.locationsList.CheckOnClick = true;
            this.locationsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.locationsList.FormattingEnabled = true;
            this.locationsList.Items.AddRange(new object[] {
            "Owned",
            "Server"});
            this.locationsList.Location = new System.Drawing.Point(9, 96);
            this.locationsList.Name = "locationsList";
            this.locationsList.Size = new System.Drawing.Size(150, 52);
            this.locationsList.Sorted = true;
            this.locationsList.TabIndex = 3;
            this.locationsList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.locationsList_ItemCheck);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Search Locations";
            // 
            // searchBox
            // 
            this.searchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBox.Location = new System.Drawing.Point(9, 35);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(150, 21);
            this.searchBox.TabIndex = 1;
            this.searchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tag Search";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "unknown_file.png");
            this.imageList1.Images.SetKeyName(1, "text_file.png");
            this.imageList1.Images.SetKeyName(2, "jpeg_file.png");
            this.imageList1.Images.SetKeyName(3, "png_file.png");
            this.imageList1.Images.SetKeyName(4, "mp3_file.png");
            this.imageList1.Images.SetKeyName(5, "video_file.png");
            // 
            // viewer
            // 
            this.viewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewer.LargeImageList = this.imageList1;
            this.viewer.Location = new System.Drawing.Point(183, 27);
            this.viewer.MultiSelect = false;
            this.viewer.Name = "viewer";
            this.viewer.Size = new System.Drawing.Size(357, 316);
            this.viewer.TabIndex = 3;
            this.viewer.UseCompatibleStateImageBehavior = false;
            // 
            // ClientUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 368);
            this.Controls.Add(this.viewer);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(550, 350);
            this.Name = "ClientUI";
            this.Text = "Caesura";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientUI_FormClosing);
            this.Load += new System.EventHandler(this.ClientUI_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mESSAGEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inboxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hELPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label messagesLabel;
        private System.Windows.Forms.Button inboxButton;
        private System.Windows.Forms.Button tagsButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.CheckedListBox locationsList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView viewer;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.ToolStripMenuItem jAPANESEToolStripMenuItem;
    }
}