using System;
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

namespace Test_ClientVer2
{
    public partial class ClientMain : Form
    {
        private Camera mCamera { get; set; }

        public ClientMain()
        {
            InitializeComponent();
            this.cbImageFormat.DefaultName = "Image Format";
            this.slExposure.DefaultName = "Exposure";
            this.slGain.DefaultName = "Gain";
            this.slWidth.DefaultName = "Width";
            this.slHeight.DefaultName = "Height";
            UpdateDevice();
            this.FormClosing += ClientMain_FormClosing;

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
                foreach(ICameraInfo camerainfo in _listCameraInfo)
                {
                    bool newitem = true;
                    foreach(ListViewItem item in items)
                    {
                        ICameraInfo tag = item.Tag as ICameraInfo;
                        if(tag[CameraInfoKey.FullName] == camerainfo[CameraInfoKey.FullName])
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

                foreach(ListViewItem item in items)
                {
                    bool exists = false;
                    foreach(ICameraInfo camerainfor in _listCameraInfo)
                    {
                        if(((ICameraInfo)item.Tag)[CameraInfoKey.FullName] == camerainfor[CameraInfoKey.FullName]){
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
            catch(Exception t)
            {
                
            }
        }

        private void btnRefreshLV_Click(object sender, EventArgs e)
        {
            this.UpdateDevice();
        }

        private void lvCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.lvCameras.SelectedItems.Count;
            if(this.mCamera != null)
            {
                this.DestroyCamera();
            }

            if(index > 0)
            {
                ListViewItem item = this.lvCameras.SelectedItems[0];
                ICameraInfo _selectedCamera = item.Tag as ICameraInfo;
                try
                {
                    this.mCamera = new Camera(_selectedCamera);
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

                }
                catch(Exception t)
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
                    LImage outputimage = new LImage(grabResult, "0x00", "0x00");
                    this.Display.Image = outputimage.BitmapImage;
                }
            }
            catch(Exception t)
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
            //EnableButtons(true, false);
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
                Configuration.AcquireSingleFrame(mCamera, null);
                mCamera.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
            }
            catch(Exception t)
            {
                this.ShowException(t);
            }
        }
    }
}
