﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RemoteSupport;
using Basler.Pylon;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using SupportServer;

namespace Test_ClientVer2
{
    public partial class ClientMain : Form
    {
        private PylonCamera mCamera { get; set; }
        private string UnitId { get; set; }
        private string SerialNumber { get; set; }
        private Shipper mShipper { get; set; }
        private Terminal OutputImage { get; set; }
        private Stopwatch StopWatch { get; set; }
        public ClientTcp ClientTcp { get; set; }

        public ClientMain()
        {
            InitializeComponent();
            this.UnitId = "0x01";
            this.cbImageFormat.DefaultName = "Image Format";
            this.slExposure.DefaultName = "Exposure";
            this.slGain.DefaultName = "Gain";
            this.slWidth.DefaultName = "Width";
            this.slHeight.DefaultName = "Height";
            UpdateDevice();
            this.FormClosing += ClientMain_FormClosing;
            this.mShipper = new Shipper();
            OutputImage = new Terminal("OutputImage");
            this.mShipper.AddTerminal(OutputImage);
            this.EnableButton(false);
            this.StopWatch = new Stopwatch();
            this.ClientTcp = new ClientTcp();
            ///TCP
            this.ClientTcp.Connected += ClientTcp_Connected;
            this.ClientTcp.Disconnected += ClientTcp_Disconnected;
        }

        private void ClientMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DestroyCamera();
        }

        private void UpdateDevice()
        {
            try
            {
                List<ICameraInfo> _listCameraInfo = PylonCamera.FindCameras();
                ListView.ListViewItemCollection items = this.lvCameras.Items;
                foreach (ICameraInfo camerainfo in _listCameraInfo)
                {
                    bool newitem = true;
                    foreach (ListViewItem item in items)
                    {
                        ICameraInfo tag = item.Tag as ICameraInfo;
                        if (tag[CameraInfoKey.FullName] == camerainfo[CameraInfoKey.FullName])
                        {
                            tag = camerainfo;
                            newitem = false;
                            break;
                        }
                    }

                    if (newitem)
                    {
                        ListViewItem item = new ListViewItem(camerainfo[CameraInfoKey.FriendlyName]);
                        item.Tag = camerainfo;
                        this.lvCameras.Items.Add(item);
                    }
                }

                foreach (ListViewItem item in items)
                {
                    bool exists = false;
                    foreach (ICameraInfo camerainfor in _listCameraInfo)
                    {
                        if (((ICameraInfo)item.Tag)[CameraInfoKey.FullName] == camerainfor[CameraInfoKey.FullName])
                        {
                            exists = true;
                            break;
                        }
                        if (!exists)
                        {
                            this.lvCameras.Items.Remove(item);
                        }
                    }
                }
            }
            catch (Exception t)
            {

            }
        }

        public void EnableButton(bool enable = true)
        {
            if (enable)
            {
                if (ClientTcp == null) return;
                if (mCamera == null) return;
                if (this.ClientTcp.ConnectStatus != ClientTcp.Status.Connected ||
                    this.mCamera.CameraStatus == PylonCamera.Status.Closeed ||
                    this.mCamera.GrabStatus == PylonCamera.Status.Started) return;
            }
            try
            {
                this.btnCapture.Invoke(new Action(() => { this.btnCapture.Enabled = enable; }));
            }
            catch
            {
                this.btnCapture.Enabled = enable;
            }
        }

        private void btnRefreshLV_Click(object sender, EventArgs e)
        {
            this.UpdateDevice();
        }

        private void lvCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.lvCameras.SelectedItems.Count;
            if (this.mCamera != null)
            {
                this.DestroyCamera();
            }

            if (index > 0)
            {
                ListViewItem item = this.lvCameras.SelectedItems[0];
                ICameraInfo _selectedCamera = item.Tag as ICameraInfo;
                try
                {
                    this.mCamera = new PylonCamera(_selectedCamera);
                    this.mCamera.ConnectionLost += MCamera_ConnectionLost;
                    this.mCamera.CameraOpened += MCamera_CameraOpened;
                    this.mCamera.CameraClosed += MCamera_CameraClosed;
                    this.mCamera.StreamGrabber.GrabStarted += StreamGrabber_GrabStarted;
                    this.mCamera.StreamGrabber.ImageGrabbed += StreamGrabber_ImageGrabbed;
                    this.mCamera.StreamGrabber.GrabStopped += StreamGrabber_GrabStopped;

                    this.mCamera.Open();

                    this.cbImageFormat.Parameter = this.mCamera.Parameters[PLCamera.PixelFormat];
                    if (mCamera.Parameters.Contains(PLCamera.ExposureTimeAbs))
                    {
                        this.slExposure.Parameter = this.mCamera.Parameters[PLCamera.ExposureTimeAbs];
                    }
                    else
                    {
                        this.slExposure.Parameter = this.mCamera.Parameters[PLCamera.ExposureTime];
                    }
                    this.slWidth.Parameter = this.mCamera.Parameters[PLCamera.Width];
                    this.slHeight.Parameter = this.mCamera.Parameters[PLCamera.Height];
                    if (this.mCamera.Parameters.Contains(PLCamera.GainAbs))
                    {
                        this.slGain.Parameter = this.mCamera.Parameters[PLCamera.GainAbs];
                    }
                    else
                    {
                        this.slGain.Parameter = mCamera.Parameters[PLCamera.Gain];
                    }

                    this.SerialNumber = _selectedCamera[CameraInfoKey.SerialNumber];

                }
                catch (Exception t)
                {
                    this.ShowException(t);
                }
            }
        }

        private void StreamGrabber_GrabStopped(object sender, GrabStopEventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<GrabStopEventArgs>(StreamGrabber_GrabStopped), sender, e);
                return;
            }

            this.EnableButton(true);
            //// Reset the stopwatch.
            //stopWatch.Reset();

            //// Re-enable the updating of the device list.
            //updateDeviceListTimer.Start();

            //// The camera stopped grabbing. Enable the grab buttons. Disable the stop button.
            //EnableButtons(true, false);

            //// If the grabbed stop due to an error, display the error message.
            //if (e.Reason != GrabStopReason.UserRequest)
            //{
            //    MessageBox.Show("A grab error occured:\n" + e.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void StreamGrabber_ImageGrabbed(object sender, ImageGrabbedEventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper GUI thread.
                // The grab result will be disposed after the event call. Clone the event arguments for marshaling to the GUI thread.
                BeginInvoke(new EventHandler<ImageGrabbedEventArgs>(StreamGrabber_ImageGrabbed), sender, e.Clone());
                return;
            }
            try
            {
                IGrabResult grabResult = e.GrabResult;
                if (grabResult.GrabSucceeded)
                {
                    LImage outputimage = new LImage(grabResult, this.UnitId, this.SerialNumber);
                    this.OutputImage.SetValue(outputimage);
                    this.Display.Image = outputimage.BitmapImage;
                }
                byte[] senddata = Shipper.ObjectToByteArray(mShipper);
                this.ClientTcp.SendToServer(senddata);
                this.lbSendData.Text = $"Send: {senddata.Length} byte";
                this.StopWatch.Stop();
                this.ShowStopWatchTime();
            }
            catch (Exception t)
            {
                this.ShowException(t);
            }
        }

        private void StreamGrabber_GrabStarted(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(StreamGrabber_GrabStarted), sender, e);
                return;
            }
            this.EnableButton(false);
            // Reset the stopwatch used to reduce the amount of displayed images. The camera may acquire images faster than the images can be displayed.

            //stopWatch.Reset();

            // Do not update the device list while grabbing to reduce jitter. Jitter may occur because the GUI thread is blocked for a short time when enumerating.
            //updateDeviceListTimer.Stop();

            // The camera is grabbing. Disable the grab buttons. Enable the stop button.
            //EnableButtons(false, true);
        }

        private void MCamera_CameraClosed(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(MCamera_CameraClosed), sender, e);
                return;
            }
            this.EnableButton(false);
            // The camera connection is closed. Disable all buttons.
            //EnableButtons(false, false);
        }

        private void MCamera_CameraOpened(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(MCamera_CameraOpened), sender, e);
                return;
            }

            // The image provider is ready to grab. Enable the grab buttons.
            EnableButton(true);
        }

        private void MCamera_ConnectionLost(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(MCamera_ConnectionLost), sender, e);
                return;
            }

            // Close the camera object.
            DestroyCamera();
            // Because one device is gone, the list needs to be updated.
            this.UpdateDevice();
        }

        private void DestroyCamera()
        {
            try
            {
                if (mCamera != null)
                {
                    cbImageFormat.Parameter = null;
                    slExposure.Parameter = null;
                    slGain.Parameter = null;
                    slWidth.Parameter = null;
                    slHeight.Parameter = null;
                }
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }

            // Destroy the camera object.
            try
            {
                if (mCamera != null)
                {
                    mCamera.Close();
                    mCamera.Dispose();
                    mCamera = null;
                }
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
        }

        private void ShowException(Exception exception)
        {
            MessageBox.Show("Exception caught:\n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            try
            {
                this.StartStopWatch();
                Configuration.AcquireSingleFrame(mCamera, null);
                mCamera.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
            }
            catch (Exception t)
            {
                this.ShowException(t);
            }
        }

        public void StartStopWatch()
        {
            this.StopWatch.Reset();
            this.StopWatch.Start();
        }

        public void ShowStopWatchTime()
        {
            this.lbTactTime.Invoke(new Action(() => { this.lbTactTimer.Text = $"Timer: {this.StopWatch.ElapsedMilliseconds} ms"; }));
        }

        #region TCP
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string _ipstring = this.tbIPAddress.Text.Split(':')[0];
                int _port = 0;
                if (!int.TryParse(this.tbIPAddress.Text.Split(':')[1], out _port)) throw new Exception("Port is invalid");
                IPEndPoint _serverEnpoint = new IPEndPoint(IPAddress.Parse(_ipstring), _port);
                this.ClientTcp.ServerEp = _serverEnpoint;
                this.ClientTcp.Connect();
            }
            catch(Exception t)
            {
                this.ShowException(t);
            }
        }
        private void ClientTcp_Connected(object sender, EventArgs e)
        {
            this.EnableButton(true);
            this.lbConnectStatus.Text = $"Status: Connected!";
        }
        private void ClientTcp_Disconnected(object sender, EventArgs e)
        {
            this.EnableButton(false);
            this.lbConnectStatus.Text = $"Status: Disconnected!";
        }
        #endregion

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            this.ClientTcp.Disconnect();
        }
    }
}
