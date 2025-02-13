using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System.IO;
using System.Windows.Media.Imaging;

namespace ParkingSystem.Helper
{
    public class ImageProcessHelper
    {
        public static BitmapSource getJsonToBitmapSource(string s)
        {
            byte[] processedImg = Convert.FromBase64String(s);
            Mat resultMat = Mat.FromImageData(processedImg, ImreadModes.Color);

            BitmapSource bitmapSource = resultMat.ToBitmapSource();

            return bitmapSource;
        }

        public static byte[] ConvertBitmapSourceToByteArray(BitmapSource bitmapSource)
        {
            if (bitmapSource == null)
                return null;

            using (MemoryStream stream = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder(); // PNG 포맷으로 변환
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(stream);

                return stream.ToArray(); // byte[] 반환
            }
        }
    }
}
