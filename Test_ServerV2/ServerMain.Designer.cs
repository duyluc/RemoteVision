
namespace Test_ServerV2
{
    partial class ServerMain
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cogDisplay1 = new SupportServer.CogDisplay();
            this.cogDisplay3 = new SupportServer.CogDisplay();
            this.cogDisplay4 = new SupportServer.CogDisplay();
            this.cogDisplay2 = new SupportServer.CogDisplay();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tbServerAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvConnectedClient = new System.Windows.Forms.ListView();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1211, 860);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.cogDisplay1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cogDisplay3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cogDisplay4, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.cogDisplay2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(253, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(955, 854);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // cogDisplay1
            // 
            this.cogDisplay1.Location = new System.Drawing.Point(3, 3);
            this.cogDisplay1.Name = "cogDisplay1";
            this.cogDisplay1.Size = new System.Drawing.Size(471, 421);
            this.cogDisplay1.TabIndex = 0;
            // 
            // cogDisplay3
            // 
            this.cogDisplay3.Location = new System.Drawing.Point(3, 430);
            this.cogDisplay3.Name = "cogDisplay3";
            this.cogDisplay3.Size = new System.Drawing.Size(471, 421);
            this.cogDisplay3.TabIndex = 2;
            // 
            // cogDisplay4
            // 
            this.cogDisplay4.Location = new System.Drawing.Point(480, 430);
            this.cogDisplay4.Name = "cogDisplay4";
            this.cogDisplay4.Size = new System.Drawing.Size(472, 421);
            this.cogDisplay4.TabIndex = 3;
            // 
            // cogDisplay2
            // 
            this.cogDisplay2.Location = new System.Drawing.Point(480, 3);
            this.cogDisplay2.Name = "cogDisplay2";
            this.cogDisplay2.Size = new System.Drawing.Size(472, 421);
            this.cogDisplay2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lvConnectedClient, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 149F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(244, 854);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.tbServerAddress);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 59);
            this.panel1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 33);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(234, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(84, 17);
            this.toolStripStatusLabel1.Text = "Status: Offline!";
            // 
            // tbServerAddress
            // 
            this.tbServerAddress.Location = new System.Drawing.Point(48, 7);
            this.tbServerAddress.Name = "tbServerAddress";
            this.tbServerAddress.ReadOnly = true;
            this.tbServerAddress.Size = new System.Drawing.Size(174, 20);
            this.tbServerAddress.TabIndex = 1;
            this.tbServerAddress.Text = "127.0.0.1:999";
            this.tbServerAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-2, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SERVER";
            // 
            // lvConnectedClient
            // 
            this.lvConnectedClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvConnectedClient.HideSelection = false;
            this.lvConnectedClient.Location = new System.Drawing.Point(3, 68);
            this.lvConnectedClient.MultiSelect = false;
            this.lvConnectedClient.Name = "lvConnectedClient";
            this.lvConnectedClient.Size = new System.Drawing.Size(238, 143);
            this.lvConnectedClient.TabIndex = 1;
            this.lvConnectedClient.UseCompatibleStateImageBehavior = false;
            // 
            // ServerMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 860);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ServerMain";
            this.Text = "Server";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TextBox tbServerAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvConnectedClient;
        private SupportServer.CogDisplay cogDisplay1;
        private SupportServer.CogDisplay cogDisplay3;
        private SupportServer.CogDisplay cogDisplay4;
        private SupportServer.CogDisplay cogDisplay2;
    }
}

