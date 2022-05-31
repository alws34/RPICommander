
namespace RPICommander
{
    partial class frmEditDBs
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
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxCommands = new System.Windows.Forms.ListBox();
            this.listBoxDevices = new System.Windows.Forms.ListBox();
            this.fileSystemWatcherDevice = new System.IO.FileSystemWatcher();
            this.fileSystemWatcherCommands = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherDevice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherCommands)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Location = new System.Drawing.Point(127, 182);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(86, 23);
            this.btnSaveChanges.TabIndex = 37;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Device";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Commands";
            // 
            // listBoxCommands
            // 
            this.listBoxCommands.FormattingEnabled = true;
            this.listBoxCommands.Location = new System.Drawing.Point(14, 30);
            this.listBoxCommands.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxCommands.Name = "listBoxCommands";
            this.listBoxCommands.Size = new System.Drawing.Size(151, 147);
            this.listBoxCommands.Sorted = true;
            this.listBoxCommands.TabIndex = 38;
            // 
            // listBoxDevices
            // 
            this.listBoxDevices.FormattingEnabled = true;
            this.listBoxDevices.Location = new System.Drawing.Point(177, 30);
            this.listBoxDevices.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxDevices.Name = "listBoxDevices";
            this.listBoxDevices.Size = new System.Drawing.Size(165, 147);
            this.listBoxDevices.Sorted = true;
            this.listBoxDevices.TabIndex = 39;
            // 
            // fileSystemWatcherDevice
            // 
            this.fileSystemWatcherDevice.EnableRaisingEvents = true;
            this.fileSystemWatcherDevice.SynchronizingObject = this;
            this.fileSystemWatcherDevice.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Changed);
            // 
            // fileSystemWatcherCommands
            // 
            this.fileSystemWatcherCommands.EnableRaisingEvents = true;
            this.fileSystemWatcherCommands.SynchronizingObject = this;
            this.fileSystemWatcherCommands.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Changed);
            // 
            // frmEditDBs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 225);
            this.Controls.Add(this.listBoxDevices);
            this.Controls.Add(this.listBoxCommands);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmEditDBs";
            this.Text = "Edit DB";
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherDevice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherCommands)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxCommands;
        private System.Windows.Forms.ListBox listBoxDevices;
        private System.IO.FileSystemWatcher fileSystemWatcherDevice;
        private System.IO.FileSystemWatcher fileSystemWatcherCommands;
    }
}