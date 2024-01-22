using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaintZTP.Model.Shapes;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace PaintZTP.Model.Adapter
{
    public class WriteableBitmapAdapter
    {

        public WriteableBitmapAdapter() { }



        public void Clear()
        {
            // Refresh(0, 0, Bitmap.PixelWidth - 1, Bitmap.PixelHeight - 1);
        }


        public WriteableBitmap DrawDot(WriteableBitmap Bitmap, int x, int y, System.Windows.Media.Color color)
        {
            //Trace.WriteLine("Drawing dot" + x.ToString() + " " + y.ToString());
            if (x < 0 || x >= Bitmap.PixelWidth || y < 0 || y >= Bitmap.PixelHeight)
            {
                throw new ArgumentOutOfRangeException("Invalid coordinates");
            }

            int bytesPerPixel = (Bitmap.Format.BitsPerPixel + 7) / 8;
            int stride = bytesPerPixel * Bitmap.PixelWidth;

            byte[] pixelData = new byte[stride * Bitmap.PixelHeight];
            Bitmap.CopyPixels(pixelData, stride, 0);

            int index = y * stride + x * bytesPerPixel;

            pixelData[index] = color.B; // Blue
            pixelData[index + 1] = color.G; // Green
            pixelData[index + 2] = color.R; // Red
            pixelData[index + 3] = color.A; // Alpha

            Bitmap.WritePixels(new Int32Rect(0, 0, Bitmap.PixelWidth, Bitmap.PixelHeight), pixelData, stride, 0);
            return Bitmap;
        }

        public void ClearBitmapToWhite(WriteableBitmap bitmap)
        {
            bitmap.Lock();
            IntPtr backBuffer = bitmap.BackBuffer;
            int stride = bitmap.BackBufferStride;
            unsafe
            {
                byte* pPixels = (byte*)backBuffer.ToPointer();

                for (int y = 0; y < bitmap.PixelHeight; y++)
                {
                    for (int x = 0; x < bitmap.PixelWidth; x++)
                    {
                        pPixels[0] = 255;   // Blue channel
                        pPixels[1] = 255;   // Green channel
                        pPixels[2] = 255;   // Red channel
                        pPixels[3] = 255;   // Alpha channel (transparency)
                        pPixels += 4;       // Move to the next pixel
                    }
                    pPixels += stride - (bitmap.PixelWidth * 4);
                }
            }
            bitmap.Unlock();
        }
    }
}
 