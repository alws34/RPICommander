
namespace RPICommander
{
    partial class FrmRPICommander
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRPICommander));
            this.label1 = new System.Windows.Forms.Label();
            this.flpCommands = new System.Windows.Forms.FlowLayoutPanel();
            this.flpDevices = new System.Windows.Forms.FlowLayoutPanel();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnAddCommand = new System.Windows.Forms.Button();
            this.btnAddDevice = new System.Windows.Forms.Button();
            this.btnEditDB = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.fileSystemWatcherCommandsWatch = new System.IO.FileSystemWatcher();
            this.fileSystemWatcherDevicesDB = new System.IO.FileSystemWatcher();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblDevices = new System.Windows.Forms.Label();
            this.textBoxConsole = new System.Windows.Forms.TextBox();
            this.buttonAboutfrm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherCommandsWatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherDevicesDB)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Commands";
            // 
            // flpCommands
            // 
            this.flpCommands.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpCommands.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.flpCommands.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpCommands.Location = new System.Drawing.Point(6, 36);
            this.flpCommands.Name = "flpCommands";
            this.flpCommands.Size = new System.Drawing.Size(300, 279);
            this.flpCommands.TabIndex = 30;
            this.toolTip1.SetToolTip(this.flpCommands, "Commands");
            // 
            // flpDevices
            // 
            this.flpDevices.AutoScroll = true;
            this.flpDevices.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpDevices.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.flpDevices.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpDevices.Location = new System.Drawing.Point(310, 36);
            this.flpDevices.Name = "flpDevices";
            this.flpDevices.Size = new System.Drawing.Size(300, 279);
            this.flpDevices.TabIndex = 31;
            this.toolTip1.SetToolTip(this.flpDevices, "Devices");
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.BackColor = System.Drawing.Color.DarkGray;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.btnStart.Location = new System.Drawing.Point(614, 252);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(132, 63);
            this.btnStart.TabIndex = 32;
            this.btnStart.Text = "GO";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.Start);
            // 
            // btnAddCommand
            // 
            this.btnAddCommand.BackColor = System.Drawing.Color.DarkGray;
            this.btnAddCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCommand.Location = new System.Drawing.Point(614, 162);
            this.btnAddCommand.Name = "btnAddCommand";
            this.btnAddCommand.Size = new System.Drawing.Size(132, 53);
            this.btnAddCommand.TabIndex = 33;
            this.btnAddCommand.Text = "Add Command";
            this.btnAddCommand.UseVisualStyleBackColor = false;
            this.btnAddCommand.Click += new System.EventHandler(this.BtnAddCommand_Click);
            // 
            // btnAddDevice
            // 
            this.btnAddDevice.BackColor = System.Drawing.Color.DarkGray;
            this.btnAddDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDevice.Location = new System.Drawing.Point(614, 79);
            this.btnAddDevice.Name = "btnAddDevice";
            this.btnAddDevice.Size = new System.Drawing.Size(132, 42);
            this.btnAddDevice.TabIndex = 34;
            this.btnAddDevice.Text = "Add Device";
            this.btnAddDevice.UseVisualStyleBackColor = false;
            this.btnAddDevice.Click += new System.EventHandler(this.BtnAddDevice_Click);
            // 
            // btnEditDB
            // 
            this.btnEditDB.BackColor = System.Drawing.Color.DarkGray;
            this.btnEditDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditDB.Location = new System.Drawing.Point(614, 122);
            this.btnEditDB.Name = "btnEditDB";
            this.btnEditDB.Size = new System.Drawing.Size(132, 40);
            this.btnEditDB.TabIndex = 35;
            this.btnEditDB.Text = "Edit DBs";
            this.btnEditDB.UseVisualStyleBackColor = false;
            this.btnEditDB.Click += new System.EventHandler(this.BtnEditDB_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.BackColor = System.Drawing.Color.DarkGray;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(614, 36);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(132, 42);
            this.btnReset.TabIndex = 36;
            this.btnReset.Text = "Reset Form";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // fileSystemWatcherCommandsWatch
            // 
            this.fileSystemWatcherCommandsWatch.EnableRaisingEvents = true;
            this.fileSystemWatcherCommandsWatch.SynchronizingObject = this;
            this.fileSystemWatcherCommandsWatch.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcherWatch_Changed);
            // 
            // fileSystemWatcherDevicesDB
            // 
            this.fileSystemWatcherDevicesDB.EnableRaisingEvents = true;
            this.fileSystemWatcherDevicesDB.SynchronizingObject = this;
            this.fileSystemWatcherDevicesDB.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcherWatch_Changed);
            // 
            // lblDevices
            // 
            this.lblDevices.AutoSize = true;
            this.lblDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDevices.Location = new System.Drawing.Point(307, 14);
            this.lblDevices.Name = "lblDevices";
            this.lblDevices.Size = new System.Drawing.Size(72, 20);
            this.lblDevices.TabIndex = 37;
            this.lblDevices.Text = "Devices";
            // 
            // textBoxConsole
            // 
            this.textBoxConsole.AcceptsReturn = true;
            this.textBoxConsole.AcceptsTab = true;
            this.textBoxConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxConsole.Location = new System.Drawing.Point(6, 320);
            this.textBoxConsole.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxConsole.Multiline = true;
            this.textBoxConsole.Name = "textBoxConsole";
            this.textBoxConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxConsole.Size = new System.Drawing.Size(741, 186);
            this.textBoxConsole.TabIndex = 38;
            // 
            // buttonAboutfrm
            // 
            this.buttonAboutfrm.BackColor = System.Drawing.Color.DarkGray;
            this.buttonAboutfrm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAboutfrm.Location = new System.Drawing.Point(614, 214);
            this.buttonAboutfrm.Name = "buttonAboutfrm";
            this.buttonAboutfrm.Size = new System.Drawing.Size(132, 40);
            this.buttonAboutfrm.TabIndex = 39;
            this.buttonAboutfrm.Text = "About";
            this.buttonAboutfrm.UseVisualStyleBackColor = false;
            this.buttonAboutfrm.Click += new System.EventHandler(this.buttonAboutfrm_Click);
            // 
            // FrmRPICommander
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(754, 511);
            this.Controls.Add(this.buttonAboutfrm);
            this.Controls.Add(this.textBoxConsole);
            this.Controls.Add(this.lblDevices);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnAddDevice);
            this.Controls.Add(this.btnEditDB);
            this.Controls.Add(this.btnAddCommand);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.flpDevices);
            this.Controls.Add(this.flpCommands);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRPICommander";
            this.Text = "RPI Commander";
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherCommandsWatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherDevicesDB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flpCommands;
        private System.Windows.Forms.FlowLayoutPanel flpDevices;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnAddCommand;
        private System.Windows.Forms.Button btnAddDevice;
        private System.Windows.Forms.Button btnEditDB;
        private System.Windows.Forms.Button btnReset;
        private System.IO.FileSystemWatcher fileSystemWatcherCommandsWatch;
        private System.IO.FileSystemWatcher fileSystemWatcherDevicesDB;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblDevices;
        private System.Windows.Forms.TextBox textBoxConsole;
        private System.Windows.Forms.Button buttonAboutfrm;
    }
}

