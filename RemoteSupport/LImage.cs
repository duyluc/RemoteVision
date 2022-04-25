using Basler.Pylon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteSupport
{
    [Serializable]
    public class LImage
    {
        public string CameraSerialNumber { get; set; }
        public Bitmap BitmapImage { get; set; }
        public LImage(IGrabResult grabresult, string unitid, string cameraSerialNumber, PixelFormat InputFormat = PixelFormat.Format32bppRgb, PixelType OutputFormat = PixelType.BGRA8packed)
        {
            CameraSerialNumber = cameraSerialNumber;
            this.BitmapImage = ConvertToBitImage(grabresult, InputFormat, OutputFormat);
        }

        static public Bitmap ConvertToBitImage(IGrabResult grabResult, PixelFormat InputFormat, PixelType OutputFormat)
        {
            PixelDataConverter converter = new PixelDataConverter();
            Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, InputFormat);
            // Lock the bits of the bitmap.
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            // Place the pointer to the buffer of the bitmap.
            converter.OutputPixelFormat = OutputFormat;
            IntPtr ptrBmp = bmpData.Scan0;
            converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult);
            bitmap.UnlockBits(bmpData);
            return bitmap;
        }
    }
}
