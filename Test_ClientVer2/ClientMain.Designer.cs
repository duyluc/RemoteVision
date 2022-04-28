
namespace Test_ClientVer2
{
    partial class ClientMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientMain));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Display = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbConnectStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbIPAddress = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRefreshLV = new System.Windows.Forms.Button();
            this.lvCameras = new System.Windows.Forms.ListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbTactTime = new System.Windows.Forms.StatusStrip();
            this.lbTactTimer = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnCapture = new System.Windows.Forms.Button();
            this.probSendImage = new System.Windows.Forms.ToolStripProgressBar();
            this.slHeight = new PylonController.IntSliderUserControl();
            this.slWidth = new PylonController.IntSliderUserControl();
            this.slGain = new PylonController.FloatSliderUserControl();
            this.slExposure = new PylonController.FloatSliderUserControl();
            this.cbImageFormat = new PylonController.EnumerationComboBoxUserControl();
            this.lbSendData = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Display)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.lbTactTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.Display, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1311, 812);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Display
            // 
            this.Display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Display.Location = new System.Drawing.Point(303, 3);
            this.Display.Name = "Display";
            this.Display.Size = new System.Drawing.Size(1005, 806);
            this.Display.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Display.TabIndex = 0;
            this.Display.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 121F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 207F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(294, 806);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDisconnect);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.btnConnect);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbIPAddress);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(288, 115);
            this.panel1.TabIndex = 0;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.BackColor = System.Drawing.Color.Transparent;
            this.btnDisconnect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDisconnect.BackgroundImage")));
            this.btnDisconnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDisconnect.Location = new System.Drawing.Point(223, 46);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(51, 39);
            this.btnDisconnect.TabIndex = 4;
            this.btnDisconnect.UseVisualStyleBackColor = false;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbConnectStatus,
            this.lbSendData});
            this.statusStrip1.Location = new System.Drawing.Point(0, 93);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(288, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbConnectStatus
            // 
            this.lbConnectStatus.Name = "lbConnectStatus";
            this.lbConnectStatus.Size = new System.Drawing.Size(117, 17);
            this.lbConnectStatus.Text = "Status: Disconnected";
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.Transparent;
            this.btnConnect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnConnect.BackgroundImage")));
            this.btnConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConnect.Location = new System.Drawing.Point(166, 46);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(51, 39);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP:";
            // 
            // tbIPAddress
            // 
            this.tbIPAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbIPAddress.Location = new System.Drawing.Point(52, 14);
            this.tbIPAddress.Name = "tbIPAddress";
            this.tbIPAddress.Size = new System.Drawing.Size(222, 26);
            this.tbIPAddress.TabIndex = 0;
            this.tbIPAddress.Text = "127.0.0.1:9999";
            this.tbIPAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnRefreshLV);
            this.panel2.Controls.Add(this.lvCameras);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 124);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(288, 201);
            this.panel2.TabIndex = 1;
            // 
            // btnRefreshLV
            // 
            this.btnRefreshLV.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefreshLV.BackgroundImage")));
            this.btnRefreshLV.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRefreshLV.Location = new System.Drawing.Point(252, 166);
            this.btnRefreshLV.Name = "btnRefreshLV";
            this.btnRefreshLV.Size = new System.Drawing.Size(33, 29);
            this.btnRefreshLV.TabIndex = 3;
            this.btnRefreshLV.UseVisualStyleBackColor = true;
            this.btnRefreshLV.Click += new System.EventHandler(this.btnRefreshLV_Click);
            // 
            // lvCameras
            // 
            this.lvCameras.HideSelection = false;
            this.lvCameras.Location = new System.Drawing.Point(0, 3);
            this.lvCameras.Name = "lvCameras";
            this.lvCameras.Size = new System.Drawing.Size(288, 195);
            this.lvCameras.TabIndex = 2;
            this.lvCameras.UseCompatibleStateImageBehavior = false;
            this.lvCameras.View = System.Windows.Forms.View.Tile;
            this.lvCameras.SelectedIndexChanged += new System.EventHandler(this.lvCameras_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.lbTactTime);
            this.panel3.Controls.Add(this.btnCapture);
            this.panel3.Controls.Add(this.slHeight);
            this.panel3.Controls.Add(this.slWidth);
            this.panel3.Controls.Add(this.slGain);
            this.panel3.Controls.Add(this.slExposure);
            this.panel3.Controls.Add(this.cbImageFormat);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 331);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(288, 472);
            this.panel3.TabIndex = 2;
            // 
            // lbTactTime
            // 
            this.lbTactTime.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.probSendImage,
            this.lbTactTimer});
            this.lbTactTime.Location = new System.Drawing.Point(0, 446);
            this.lbTactTime.Name = "lbTactTime";
            this.lbTactTime.Size = new System.Drawing.Size(284, 22);
            this.lbTactTime.TabIndex = 6;
            this.lbTactTime.Text = "Timer: 0 ms";
            // 
            // lbTactTimer
            // 
            this.lbTactTimer.Name = "lbTactTimer";
            this.lbTactTimer.Size = new System.Drawing.Size(68, 17);
            this.lbTactTimer.Text = "Timer: 0 ms";
            // 
            // btnCapture
            // 
            this.btnCapture.BackColor = System.Drawing.Color.Transparent;
            this.btnCapture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCapture.BackgroundImage")));
            this.btnCapture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCapture.Location = new System.Drawing.Point(230, 404);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(51, 39);
            this.btnCapture.TabIndex = 5;
            this.btnCapture.UseVisualStyleBackColor = false;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // probSendImage
            // 
            this.probSendImage.Name = "probSendImage";
            this.probSendImage.Size = new System.Drawing.Size(100, 16);
            // 
            // slHeight
            // 
            this.slHeight.DefaultName = "N/A";
            this.slHeight.Location = new System.Drawing.Point(21, 266);
            this.slHeight.MinimumSize = new System.Drawing.Size(245, 50);
            this.slHeight.Name = "slHeight";
            this.slHeight.Size = new System.Drawing.Size(245, 50);
            this.slHeight.TabIndex = 4;
            // 
            // slWidth
            // 
            this.slWidth.DefaultName = "N/A";
            this.slWidth.Location = new System.Drawing.Point(21, 206);
            this.slWidth.MinimumSize = new System.Drawing.Size(245, 50);
            this.slWidth.Name = "slWidth";
            this.slWidth.Size = new System.Drawing.Size(245, 50);
            this.slWidth.TabIndex = 3;
            // 
            // slGain
            // 
            this.slGain.DefaultName = "N/A";
            this.slGain.Location = new System.Drawing.Point(21, 146);
            this.slGain.MinimumSize = new System.Drawing.Size(245, 50);
            this.slGain.Name = "slGain";
            this.slGain.Size = new System.Drawing.Size(251, 50);
            this.slGain.TabIndex = 2;
            // 
            // slExposure
            // 
            this.slExposure.DefaultName = "N/A";
            this.slExposure.Location = new System.Drawing.Point(21, 86);
            this.slExposure.MinimumSize = new System.Drawing.Size(245, 50);
            this.slExposure.Name = "slExposure";
            this.slExposure.Size = new System.Drawing.Size(251, 50);
            this.slExposure.TabIndex = 1;
            // 
            // cbImageFormat
            // 
            this.cbImageFormat.DefaultName = "N/A";
            this.cbImageFormat.Location = new System.Drawing.Point(21, 3);
            this.cbImageFormat.Name = "cbImageFormat";
            this.cbImageFormat.Size = new System.Drawing.Size(251, 57);
            this.cbImageFormat.TabIndex = 0;
            // 
            // lbSendData
            // 
            this.lbSendData.Name = "lbSendData";
            this.lbSendData.Size = new System.Drawing.Size(71, 17);
            this.lbSendData.Text = "Send: 0 byte";
            // 
            // ClientMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1311, 812);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ClientMain";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Display)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.lbTactTime.ResumeLayout(false);
            this.lbTactTime.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox Display;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbIPAddress;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbConnectStatus;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRefreshLV;
        private System.Windows.Forms.ListView lvCameras;
        private System.Windows.Forms.Panel panel3;
        private PylonController.EnumerationComboBoxUserControl cbImageFormat;
        private PylonController.FloatSliderUserControl slGain;
        private PylonController.FloatSliderUserControl slExposure;
        private PylonController.IntSliderUserControl slHeight;
        private PylonController.IntSliderUserControl slWidth;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.StatusStrip lbTactTime;
        private System.Windows.Forms.ToolStripStatusLabel lbTactTimer;
        private System.Windows.Forms.ToolStripProgressBar probSendImage;
        private System.Windows.Forms.ToolStripStatusLabel lbSendData;
    }
}

