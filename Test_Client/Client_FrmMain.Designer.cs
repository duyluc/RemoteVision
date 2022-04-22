﻿
namespace Test_Client
{
    partial class Client_FrmMain
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
            this.Display = new System.Windows.Forms.PictureBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.tbxMessage = new System.Windows.Forms.TextBox();
            this.btnCloseCam = new System.Windows.Forms.Button();
            this.btnOpenCam = new System.Windows.Forms.Button();
            this.btnRecon = new System.Windows.Forms.Button();
            this.btnCapture = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Display)).BeginInit();
            this.SuspendLayout();
            // 
            // Display
            // 
            this.Display.Location = new System.Drawing.Point(12, 12);
            this.Display.Name = "Display";
            this.Display.Size = new System.Drawing.Size(690, 689);
            this.Display.TabIndex = 0;
            this.Display.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(708, 312);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 49);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tbxMessage
            // 
            this.tbxMessage.Location = new System.Drawing.Point(708, 12);
            this.tbxMessage.Multiline = true;
            this.tbxMessage.Name = "tbxMessage";
            this.tbxMessage.ReadOnly = true;
            this.tbxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxMessage.Size = new System.Drawing.Size(613, 294);
            this.tbxMessage.TabIndex = 2;
            // 
            // btnCloseCam
            // 
            this.btnCloseCam.Location = new System.Drawing.Point(955, 312);
            this.btnCloseCam.Name = "btnCloseCam";
            this.btnCloseCam.Size = new System.Drawing.Size(75, 49);
            this.btnCloseCam.TabIndex = 3;
            this.btnCloseCam.Text = "Close Cam";
            this.btnCloseCam.UseVisualStyleBackColor = true;
            this.btnCloseCam.Click += new System.EventHandler(this.btnCloseCam_Click);
            // 
            // btnOpenCam
            // 
            this.btnOpenCam.Location = new System.Drawing.Point(874, 312);
            this.btnOpenCam.Name = "btnOpenCam";
            this.btnOpenCam.Size = new System.Drawing.Size(75, 49);
            this.btnOpenCam.TabIndex = 4;
            this.btnOpenCam.Text = "Open Cam";
            this.btnOpenCam.UseVisualStyleBackColor = true;
            this.btnOpenCam.Click += new System.EventHandler(this.btnOpenCam_Click);
            // 
            // btnRecon
            // 
            this.btnRecon.Location = new System.Drawing.Point(789, 312);
            this.btnRecon.Name = "btnRecon";
            this.btnRecon.Size = new System.Drawing.Size(75, 49);
            this.btnRecon.TabIndex = 5;
            this.btnRecon.Text = "Recon TCP";
            this.btnRecon.UseVisualStyleBackColor = true;
            this.btnRecon.Click += new System.EventHandler(this.btnRecon_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(1036, 312);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(75, 49);
            this.btnCapture.TabIndex = 6;
            this.btnCapture.Text = "Capture";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // Client_FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 713);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.btnRecon);
            this.Controls.Add(this.btnOpenCam);
            this.Controls.Add(this.btnCloseCam);
            this.Controls.Add(this.tbxMessage);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.Display);
            this.Name = "Client_FrmMain";
            this.Text = "CLIENT";
            ((System.ComponentModel.ISupportInitialize)(this.Display)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Display;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox tbxMessage;
        private System.Windows.Forms.Button btnCloseCam;
        private System.Windows.Forms.Button btnOpenCam;
        private System.Windows.Forms.Button btnRecon;
        private System.Windows.Forms.Button btnCapture;
    }
}

