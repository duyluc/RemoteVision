using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basler.Pylon;

namespace RemoteSupport
{
    public class PylonCamera
    {
        public Camera Cammera { get; set; }
        public PylonCamera()
        {

        }
        
        public PylonCamera(string _cameraSerialNumber)
        {

        }

        static public List<ICameraInfo> FindCameras()
        {
            return CameraFinder.Enumerate();
        }
    }
}
