﻿
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
            this.btnSaveDevice = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAddDeviceName = new System.Windows.Forms.TextBox();
            this.listBoxDevices = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAddDeviceToList = new System.Windows.Forms.Button();
            this.btnsaveonedevice = new System.Windows.Forms.Button();
            this.textBoxdeviceUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxdevicePassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDevicePort = new System.Windows.Forms.TextBox();
            this.lblDevicePort = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSaveDevice
            // 
            this.btnSaveDevice.Location = new System.Drawing.Point(131, 240);
            this.btnSaveDevice.Name = "btnSaveDevice";
            this.btnSaveDevice.Size = new System.Drawing.Size(93, 23);
            this.btnSaveDevice.TabIndex = 0;
            this.btnSaveDevice.Text = "Save Devices";
            this.btnSaveDevice.UseVisualStyleBackColor = true;
            this.btnSaveDevice.Click += new System.EventHandler(this.buttonSaveDevices_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Device Name";
            // 
            // textBoxAddDeviceName
            // 
            this.textBoxAddDeviceName.Location = new System.Drawing.Point(115, 6);
            this.textBoxAddDeviceName.Name = "textBoxAddDeviceName";
            this.textBoxAddDeviceName.Size = new System.Drawing.Size(206, 20);
            this.textBoxAddDeviceName.TabIndex = 2;
            // 
            // listBoxDevices
            // 
            this.listBoxDevices.FormattingEnabled = true;
            this.listBoxDevices.Location = new System.Drawing.Point(115, 152);
            this.listBoxDevices.Name = "listBoxDevices";
            this.listBoxDevices.Size = new System.Drawing.Size(206, 82);
            this.listBoxDevices.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Devices to Add";
            // 
            // buttonAddDeviceToList
            // 
            this.buttonAddDeviceToList.Location = new System.Drawing.Point(115, 123);
            this.buttonAddDeviceToList.Name = "buttonAddDeviceToList";
            this.buttonAddDeviceToList.Size = new System.Drawing.Size(93, 23);
            this.buttonAddDeviceToList.TabIndex = 5;
            this.buttonAddDeviceToList.Text = "add to list";
            this.buttonAddDeviceToList.UseVisualStyleBackColor = true;
            this.buttonAddDeviceToList.Click += new System.EventHandler(this.buttonAddDeviceToList_Click);
            // 
            // btnsaveonedevice
            // 
            this.btnsaveonedevice.Location = new System.Drawing.Point(228, 123);
            this.btnsaveonedevice.Name = "btnsaveonedevice";
            this.btnsaveonedevice.Size = new System.Drawing.Size(93, 23);
            this.btnsaveonedevice.TabIndex = 6;
            this.btnsaveonedevice.Text = "Save Device";
            this.btnsaveonedevice.UseVisualStyleBackColor = true;
            this.btnsaveonedevice.Click += new System.EventHandler(this.btnsaveonedevice_Click);
            // 
            // textBoxdeviceUsername
            // 
            this.textBoxdeviceUsername.Location = new System.Drawing.Point(115, 32);
            this.textBoxdeviceUsername.Name = "textBoxdeviceUsername";
            this.textBoxdeviceUsername.Size = new System.Drawing.Size(206, 20);
            this.textBoxdeviceUsername.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Device User Name";
            // 
            // textBoxdevicePassword
            // 
            this.textBoxdevicePassword.Location = new System.Drawing.Point(115, 58);
            this.textBoxdevicePassword.Name = "textBoxdevicePassword";
            this.textBoxdevicePassword.Size = new System.Drawing.Size(206, 20);
            this.textBoxdevicePassword.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Device Password";
            // 
            // textBoxDevicePort
            // 
            this.textBoxDevicePort.Location = new System.Drawing.Point(115, 84);
            this.textBoxDevicePort.Name = "textBoxDevicePort";
            this.textBoxDevicePort.Size = new System.Drawing.Size(206, 20);
            this.textBoxDevicePort.TabIndex = 12;
            // 
            // lblDevicePort
            // 
            this.lblDevicePort.AutoSize = true;
            this.lblDevicePort.Location = new System.Drawing.Point(12, 87);
            this.lblDevicePort.Name = "lblDevicePort";
            this.lblDevicePort.Size = new System.Drawing.Size(63, 13);
            this.lblDevicePort.TabIndex = 11;
            this.lblDevicePort.Text = "Device Port";
            // 
            // frmAddDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 276);
            this.Controls.Add(this.textBoxDevicePort);
            this.Controls.Add(this.lblDevicePort);
            this.Controls.Add(this.textBoxdevicePassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxdeviceUsername);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnsaveonedevice);
            this.Controls.Add(this.buttonAddDeviceToList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxDevices);
            this.Controls.Add(this.textBoxAddDeviceName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSaveDevice);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAddDevice";
            this.Text = "Add Device";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveDevice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAddDeviceName;
        private System.Windows.Forms.ListBox listBoxDevices;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonAddDeviceToList;
        private System.Windows.Forms.Button btnsaveonedevice;
        private System.Windows.Forms.TextBox textBoxdeviceUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxdevicePassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDevicePort;
        private System.Windows.Forms.Label lblDevicePort;
    }
}