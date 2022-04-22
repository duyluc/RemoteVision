using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Basler.Pylon;

namespace RemoteSupport
{
    [SerializableAttribute]
    public class ImageShipper
    {
        public string UnitID;
        public string CameraSerialNumber;
        public Bitmap BitmapImage;

        public ImageShipper(IGrabResult grabresult,string unitid,string cameraSerialNumber, PixelFormat InputFormat = PixelFormat.Format8bppIndexed, PixelType OutputFormat = PixelType.Mono8)
        {
            UnitID = unitid;
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

        static public byte[] ObjectToByteArray(object obj)
        {
            // create new memory stream
            MemoryStream _MemoryStream = new System.IO.MemoryStream();

            // create new BinaryFormatter
            BinaryFormatter _BinaryFormatter = new BinaryFormatter();

            // Serializes an object, or graph of connected objects, to the given stream.
            _BinaryFormatter.Serialize(_MemoryStream, obj);

            // convert stream to byte array and return
            return _MemoryStream.ToArray();
        }

        static public object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            object obj = binForm.Deserialize(memStream);
            return obj;
        }

        //public static byte[] ImageToByteArray(Bitmap img)
        //{
        //    using (var stream = new MemoryStream())
        //    {
        //        img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        //        return stream.ToArray();
        //    }
        //}

        //public static Bitmap ByteArrayToBitmap(byte[] imageData)
        //{
        //    Bitmap bmp;
        //    using (var ms = new MemoryStream(imageData))
        //    {
        //        bmp = new Bitmap(ms);
        //    }
        //    return bmp;
        //}
    }
}
