using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basler.Pylon;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using RemoteSupport;
using System.Drawing;

namespace TestProvider
{
    class ClientVision
    {

        public class TestBasler
        {
            List<ICameraInfo> allCameras = null; //Create a list of ICameraInfo objects, used to save all camera information traversed 
            Camera camera = null; //Create a camera object 
            bool grabbing = false;
            
            public TestBasler()
            {
                using(camera = new Camera())
                {
                    Socket sender = null;
                    try
                    {
                        camera.Close();
                        camera.Open();

                        // DeviceVendorName, DeviceModelName, and DeviceFirmwareVersion are string parameters.
                        Console.WriteLine("Camera Device Information");
                        Console.WriteLine("=========================");
                        Console.WriteLine("Vendor           : {0}", camera.Parameters[PLCamera.DeviceVendorName].GetValue());
                        Console.WriteLine("Model            : {0}", camera.Parameters[PLCamera.DeviceModelName].GetValue());
                        Console.WriteLine("Firmware version : {0}", camera.Parameters[PLCamera.DeviceFirmwareVersion].GetValue());
                        Console.WriteLine("");
                        Console.WriteLine("Camera Device Settings");
                        Console.WriteLine("======================");


                        // Setting the AOI. OffsetX, OffsetY, Width, and Height are integer parameters.
                        // On some cameras, the offsets are read-only. If they are writable, set the offsets to min.
                        camera.Parameters[PLCamera.OffsetX].TrySetToMinimum();
                        camera.Parameters[PLCamera.OffsetY].TrySetToMinimum();
                        // Some parameters have restrictions. You can use GetIncrement/GetMinimum/GetMaximum to make sure you set a valid value.
                        // Here, we let pylon correct the value if needed.
                        camera.Parameters[PLCamera.Width].SetValue(500, IntegerValueCorrection.Nearest);
                        camera.Parameters[PLCamera.Height].SetValue(500, IntegerValueCorrection.Nearest);
                        camera.Parameters[PLCamEmuCamera.ExposureTimeAbs].SetValue(35000);
                        camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.Mono16);



                        Console.WriteLine("OffsetX          : {0}", camera.Parameters[PLCamera.OffsetX].GetValue());
                        Console.WriteLine("OffsetY          : {0}", camera.Parameters[PLCamera.OffsetY].GetValue());
                        Console.WriteLine("Width            : {0}", camera.Parameters[PLCamera.Width].GetValue());
                        Console.WriteLine("Height           : {0}", camera.Parameters[PLCamera.Height].GetValue());


                        // Set an enum parameter.

                        string oldPixelFormat = camera.Parameters[PLCamera.PixelFormat].GetValue(); // Remember the current pixel format.
                        Console.WriteLine("Old PixelFormat  : {0} ({1})", camera.Parameters[PLCamera.PixelFormat].GetValue(), oldPixelFormat);

                        // Set pixel format to Mono8 if available.
                        if (camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.Mono8))
                        {
                            Console.WriteLine("New PixelFormat  : {0} ({1})", camera.Parameters[PLCamera.PixelFormat].GetValue(), oldPixelFormat);
                        }


                        // Some camera models may have auto functions enabled. To set the gain value to a specific value,
                        // the Gain Auto function must be disabled first (if gain auto is available).
                        // Set GainAuto to Off if it is writable.

                        // Start grabbing.
                        // Grab a number of images.
                        IPEndPoint ServerEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
                        sender = new Socket(SocketType.Stream, ProtocolType.Tcp);
                        sender.Connect(ServerEP);
                        while (true)
                        {
                            // Wait for an image and then retrieve it. A timeout of 5000 ms is used.
                            try
                            {
                                camera.Parameters[PLCamera.GainAuto].TrySetValue(PLCamera.GainAuto.Off);
                                IGrabResult grabResult = camera.StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);
                                using (grabResult)
                                {
                                    ImageShipper imageShipper = new ImageShipper(grabResult, "0x00", "xxx");
                                    // Image grabbed successfully?
                                    if (grabResult.GrabSucceeded)
                                    {
                                        ImageWindow.DisplayImage(0, grabResult);
                                        byte[] data = ImageShipper.ObjectToByteArray(imageShipper);
                                        Console.WriteLine(">> DataLength: " + data.Length.ToString());
                                        Console.WriteLine(">> Connect Successfully");
                                        sender.Send(data);
                                        Console.WriteLine(">> Send Successfully");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error: {0} {1}", grabResult.ErrorCode, grabResult.ErrorDescription);
                                    }
                                    if (Console.ReadLine() == "exit")
                                    {
                                        break;
                                    }
                                    camera.StreamGrabber.Stop();
                                }
                                
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            finally
                            {
                                camera.StreamGrabber.Stop();
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        camera.Close();
                        sender.Close();
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            TestBasler test = new TestBasler();
            Console.ReadKey();
        }
    }
}
