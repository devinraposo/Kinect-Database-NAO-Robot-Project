namespace Database_Connectivity_Module
{
    partial class Form1
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
            this.connectButton = new System.Windows.Forms.Button();
            this.serverName = new System.Windows.Forms.TextBox();
            this.databaseName = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.uploadListBox = new System.Windows.Forms.ListBox();
            this.downloadButton = new System.Windows.Forms.Button();
            this.gesturalPath = new System.Windows.Forms.TextBox();
            this.naoPath = new System.Windows.Forms.TextBox();
            this.midiPath = new System.Windows.Forms.TextBox();
            this.loadFileButton = new System.Windows.Forms.Button();
            this.uploadButton = new System.Windows.Forms.Button();
            this.portName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(70, 136);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(127, 23);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "Connect to Database";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // serverName
            // 
            this.serverName.AcceptsReturn = true;
            this.serverName.AllowDrop = true;
            this.serverName.Location = new System.Drawing.Point(12, 97);
            this.serverName.Name = "serverName";
            this.serverName.Size = new System.Drawing.Size(115, 20);
            this.serverName.TabIndex = 1;
            this.serverName.Text = "Enter server name...";
            this.serverName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // databaseName
            // 
            this.databaseName.AcceptsReturn = true;
            this.databaseName.AllowDrop = true;
            this.databaseName.Location = new System.Drawing.Point(133, 97);
            this.databaseName.Name = "databaseName";
            this.databaseName.Size = new System.Drawing.Size(136, 20);
            this.databaseName.TabIndex = 2;
            this.databaseName.Text = "Enter database name...";
            this.databaseName.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // username
            // 
            this.username.AcceptsReturn = true;
            this.username.AllowDrop = true;
            this.username.Location = new System.Drawing.Point(285, 97);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(109, 20);
            this.username.TabIndex = 3;
            this.username.Text = "Enter username...";
            // 
            // password
            // 
            this.password.AcceptsReturn = true;
            this.password.AllowDrop = true;
            this.password.Location = new System.Drawing.Point(400, 97);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(109, 20);
            this.password.TabIndex = 4;
            this.password.Text = "Enter password...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 6;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // uploadListBox
            // 
            this.uploadListBox.FormattingEnabled = true;
            this.uploadListBox.Location = new System.Drawing.Point(214, 136);
            this.uploadListBox.Name = "uploadListBox";
            this.uploadListBox.Size = new System.Drawing.Size(180, 95);
            this.uploadListBox.TabIndex = 7;
            this.uploadListBox.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(423, 167);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(189, 23);
            this.downloadButton.TabIndex = 8;
            this.downloadButton.Text = "Downloaded Selected Data To Files";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // gesturalPath
            // 
            this.gesturalPath.AcceptsReturn = true;
            this.gesturalPath.AllowDrop = true;
            this.gesturalPath.Location = new System.Drawing.Point(15, 12);
            this.gesturalPath.Name = "gesturalPath";
            this.gesturalPath.Size = new System.Drawing.Size(211, 20);
            this.gesturalPath.TabIndex = 10;
            this.gesturalPath.Text = "Enter gestural file path...";
            // 
            // naoPath
            // 
            this.naoPath.AcceptsReturn = true;
            this.naoPath.AllowDrop = true;
            this.naoPath.Location = new System.Drawing.Point(266, 12);
            this.naoPath.Name = "naoPath";
            this.naoPath.Size = new System.Drawing.Size(211, 20);
            this.naoPath.TabIndex = 11;
            this.naoPath.Text = "Enter NAO file path...";
            // 
            // midiPath
            // 
            this.midiPath.AcceptsReturn = true;
            this.midiPath.AllowDrop = true;
            this.midiPath.Location = new System.Drawing.Point(15, 49);
            this.midiPath.Name = "midiPath";
            this.midiPath.Size = new System.Drawing.Size(211, 20);
            this.midiPath.TabIndex = 12;
            this.midiPath.Text = "Enter MIDI file path...";
            // 
            // loadFileButton
            // 
            this.loadFileButton.Location = new System.Drawing.Point(319, 47);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(75, 23);
            this.loadFileButton.TabIndex = 13;
            this.loadFileButton.Text = "Load Files";
            this.loadFileButton.UseVisualStyleBackColor = true;
            this.loadFileButton.Click += new System.EventHandler(this.loadFileButton_Click);
            // 
            // uploadButton
            // 
            this.uploadButton.Location = new System.Drawing.Point(70, 188);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(127, 23);
            this.uploadButton.TabIndex = 14;
            this.uploadButton.Text = "Upload Data";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // portName
            // 
            this.portName.Location = new System.Drawing.Point(528, 97);
            this.portName.Name = "portName";
            this.portName.Size = new System.Drawing.Size(100, 20);
            this.portName.TabIndex = 15;
            this.portName.Text = "Enter port number...";
            this.portName.TextChanged += new System.EventHandler(this.portName_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 257);
            this.Controls.Add(this.portName);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.loadFileButton);
            this.Controls.Add(this.midiPath);
            this.Controls.Add(this.naoPath);
            this.Controls.Add(this.gesturalPath);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.uploadListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.password);
            this.Controls.Add(this.username);
            this.Controls.Add(this.databaseName);
            this.Controls.Add(this.serverName);
            this.Controls.Add(this.connectButton);
            this.Name = "Form1";
            this.Text = "Database Connectivity Module";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TextBox serverName;
        private System.Windows.Forms.TextBox databaseName;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox uploadListBox;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.TextBox gesturalPath;
        private System.Windows.Forms.TextBox naoPath;
        private System.Windows.Forms.TextBox midiPath;
        private System.Windows.Forms.Button loadFileButton;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.TextBox portName;
    }
}

