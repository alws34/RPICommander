
namespace RPICommander
{
    partial class frmAddCommand
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddCommand));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCommandName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCommand = new System.Windows.Forms.TextBox();
            this.btnSaveCommand = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Command Name";
            // 
            // textBoxCommandName
            // 
            this.textBoxCommandName.Location = new System.Drawing.Point(103, 6);
            this.textBoxCommandName.Name = "textBoxCommandName";
            this.textBoxCommandName.Size = new System.Drawing.Size(114, 20);
            this.textBoxCommandName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Command";
            // 
            // textBoxCommand
            // 
            this.textBoxCommand.Location = new System.Drawing.Point(103, 39);
            this.textBoxCommand.Multiline = true;
            this.textBoxCommand.Name = "textBoxCommand";
            this.textBoxCommand.Size = new System.Drawing.Size(261, 118);
            this.textBoxCommand.TabIndex = 3;
            // 
            // btnSaveCommand
            // 
            this.btnSaveCommand.Location = new System.Drawing.Point(149, 163);
            this.btnSaveCommand.Name = "btnSaveCommand";
            this.btnSaveCommand.Size = new System.Drawing.Size(95, 23);
            this.btnSaveCommand.TabIndex = 4;
            this.btnSaveCommand.Text = "Save Command";
            this.btnSaveCommand.UseVisualStyleBackColor = true;
            this.btnSaveCommand.Click += new System.EventHandler(this.btnSaveCommand_Click);
            // 
            // frmAddCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 198);
            this.Controls.Add(this.btnSaveCommand);
            this.Controls.Add(this.textBoxCommand);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxCommandName);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAddCommand";
            this.Text = "Add Command";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCommandName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCommand;
        private System.Windows.Forms.Button btnSaveCommand;
    }
}