
namespace RPICommander
{
    partial class frmAddDevice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddDevice));
            this.btnSaveDevices = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAddDeviceName = new System.Windows.Forms.TextBox();
            this.listBoxDevices = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAddDeviceToList = new System.Windows.Forms.Button();
            this.btnSavedevice = new System.Windows.Forms.Button();
            this.textBoxdeviceUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxdevicePassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDevicePort = new System.Windows.Forms.TextBox();
            this.lblDevicePort = new System.Windows.Forms.Label();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.btnRemoveDevice = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveDevices
            // 
            this.btnSaveDevices.Location = new System.Drawing.Point(218, 462);
            this.btnSaveDevices.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnSaveDevices.Name = "btnSaveDevices";
            this.btnSaveDevices.Size = new System.Drawing.Size(155, 44);
            this.btnSaveDevices.TabIndex = 0;
            this.btnSaveDevices.Text = "Save Devices";
            this.btnSaveDevices.UseVisualStyleBackColor = true;
            this.btnSaveDevices.Click += new System.EventHandler(this.buttonSaveDevices_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Device Name";
            // 
            // textBoxAddDeviceName
            // 
            this.textBoxAddDeviceName.Location = new System.Drawing.Point(192, 12);
            this.textBoxAddDeviceName.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textBoxAddDeviceName.Name = "textBoxAddDeviceName";
            this.textBoxAddDeviceName.Size = new System.Drawing.Size(341, 31);
            this.textBoxAddDeviceName.TabIndex = 2;
            this.textBoxAddDeviceName.Tag = "";
            // 
            // listBoxDevices
            // 
            this.listBoxDevices.FormattingEnabled = true;
            this.listBoxDevices.ItemHeight = 25;
            this.listBoxDevices.Location = new System.Drawing.Point(192, 292);
            this.listBoxDevices.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.listBoxDevices.Name = "listBoxDevices";
            this.listBoxDevices.Size = new System.Drawing.Size(341, 154);
            this.listBoxDevices.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 352);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Devices to Add";
            // 
            // buttonAddDeviceToList
            // 
            this.buttonAddDeviceToList.Location = new System.Drawing.Point(192, 237);
            this.buttonAddDeviceToList.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonAddDeviceToList.Name = "buttonAddDeviceToList";
            this.buttonAddDeviceToList.Size = new System.Drawing.Size(155, 44);
            this.buttonAddDeviceToList.TabIndex = 5;
            this.buttonAddDeviceToList.Text = "add to list";
            this.buttonAddDeviceToList.UseVisualStyleBackColor = true;
            this.buttonAddDeviceToList.Click += new System.EventHandler(this.buttonAddDeviceToList_Click);
            // 
            // btnSavedevice
            // 
            this.btnSavedevice.Location = new System.Drawing.Point(380, 237);
            this.btnSavedevice.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnSavedevice.Name = "btnSavedevice";
            this.btnSavedevice.Size = new System.Drawing.Size(155, 44);
            this.btnSavedevice.TabIndex = 6;
            this.btnSavedevice.Text = "Save Device";
            this.btnSavedevice.UseVisualStyleBackColor = true;
            this.btnSavedevice.Click += new System.EventHandler(this.buttonSaveDevice_Click);
            // 
            // textBoxdeviceUsername
            // 
            this.textBoxdeviceUsername.Location = new System.Drawing.Point(192, 62);
            this.textBoxdeviceUsername.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textBoxdeviceUsername.Name = "textBoxdeviceUsername";
            this.textBoxdeviceUsername.Size = new System.Drawing.Size(341, 31);
            this.textBoxdeviceUsername.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 67);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Device User Name";
            // 
            // textBoxdevicePassword
            // 
            this.textBoxdevicePassword.Location = new System.Drawing.Point(192, 112);
            this.textBoxdevicePassword.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textBoxdevicePassword.Name = "textBoxdevicePassword";
            this.textBoxdevicePassword.Size = new System.Drawing.Size(341, 31);
            this.textBoxdevicePassword.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 117);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "Device Password";
            // 
            // textBoxDevicePort
            // 
            this.textBoxDevicePort.Location = new System.Drawing.Point(192, 162);
            this.textBoxDevicePort.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textBoxDevicePort.Name = "textBoxDevicePort";
            this.textBoxDevicePort.Size = new System.Drawing.Size(341, 31);
            this.textBoxDevicePort.TabIndex = 12;
            // 
            // lblDevicePort
            // 
            this.lblDevicePort.AutoSize = true;
            this.lblDevicePort.Location = new System.Drawing.Point(20, 167);
            this.lblDevicePort.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblDevicePort.Name = "lblDevicePort";
            this.lblDevicePort.Size = new System.Drawing.Size(101, 25);
            this.lblDevicePort.TabIndex = 11;
            this.lblDevicePort.Text = "Device Port";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // btnRemoveDevice
            // 
            this.btnRemoveDevice.Location = new System.Drawing.Point(25, 235);
            this.btnRemoveDevice.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnRemoveDevice.Name = "btnRemoveDevice";
            this.btnRemoveDevice.Size = new System.Drawing.Size(157, 44);
            this.btnRemoveDevice.TabIndex = 13;
            this.btnRemoveDevice.Text = "Remove Device";
            this.btnRemoveDevice.UseVisualStyleBackColor = true;
            this.btnRemoveDevice.Visible = false;
            this.btnRemoveDevice.Click += new System.EventHandler(this.btnRemoveDevice_Click);
            // 
            // frmAddDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 531);
            this.Controls.Add(this.btnRemoveDevice);
            this.Controls.Add(this.textBoxDevicePort);
            this.Controls.Add(this.lblDevicePort);
            this.Controls.Add(this.textBoxdevicePassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxdeviceUsername);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSavedevice);
            this.Controls.Add(this.buttonAddDeviceToList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxDevices);
            this.Controls.Add(this.textBoxAddDeviceName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSaveDevices);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "frmAddDevice";
            this.Text = "Add Device";
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveDevices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAddDeviceName;
        private System.Windows.Forms.ListBox listBoxDevices;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonAddDeviceToList;
        private System.Windows.Forms.Button btnSavedevice;
        private System.Windows.Forms.TextBox textBoxdeviceUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxdevicePassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDevicePort;
        private System.Windows.Forms.Label lblDevicePort;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button btnRemoveDevice;
    }
}