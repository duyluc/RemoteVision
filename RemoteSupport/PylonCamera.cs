using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basler.Pylon;

namespace RemoteSupport
{
    public class PylonCamera:Camera
    {
        public enum Status
        {
            Free,
            Started,
            Stoped,
            GrabFault,
            GrabSucc,
            Opened,
            Closeed,
        }

        public Status GrabStatus { get; set; }
        public Status CameraStatus { get; set; }
        public PylonCamera()
        {
            this.GrabStatus = Status.Free;
            
        }
        
        public PylonCamera(string _cameraSerialNumber):base(_cameraSerialNumber)
        {
            this.GrabStatus = Status.Free;
        }

        public PylonCamera(ICameraInfo _cameraInfo) : base(_cameraInfo)
        {
            this.GrabStatus = Status.Free;
        }

        static public List<ICameraInfo> FindCameras()
        {
            return CameraFinder.Enumerate();
        }

        private void SetInit()
        {
            this.GrabStatus = Status.Stoped;
            this.CameraStatus = Status.Closeed;

            this.CameraClosed += PylonCamera_CameraClosed;
            this.CameraOpened += PylonCamera_CameraOpened;
            this.CameraOpening += PylonCamera_CameraOpening;
            this.ConnectionLost += PylonCamera_ConnectionLost;
            if(this.StreamGrabber != null)
            {
                this.StreamGrabber.GrabStarted += StreamGrabber_GrabStarted;
                this.StreamGrabber.GrabStopped += StreamGrabber_GrabStopped;
                this.StreamGrabber.ImageGrabbed += StreamGrabber_ImageGrabbed;
            }
        }

        private void StreamGrabber_ImageGrabbed(object sender, ImageGrabbedEventArgs e)
        {
            
        }

        private void StreamGrabber_GrabStopped(object sender, GrabStopEventArgs e)
        {
            this.GrabStatus = Status.Stoped;
        }

        private void StreamGrabber_GrabStarted(object sender, EventArgs e)
        {
            this.GrabStatus = Status.Stoped;
        }

        private void PylonCamera_ConnectionLost(object sender, EventArgs e)
        {
            
        }

        private void PylonCamera_CameraOpening(object sender, EventArgs e)
        {
            
        }

        private void PylonCamera_CameraOpened(object sender, EventArgs e)
        {
            this.CameraStatus = Status.Opened;
        }

        private void PylonCamera_CameraClosed(object sender, EventArgs e)
        {
            this.CameraStatus = Status.Closeed;
        }
    }
}
