using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _32DNoiseGen
{
    static class BitmapUtility
    {
        public static void SetGrayscaleBitmap(this Bitmap bitmap, float[] grayscaleData, int startX, int startY, int endX, int endY)
        {
            // Width and height of region
            int width = endX - startX;
            int height = endY - startY;

            // Lock the bitmap bits for direct access
            BitmapData bmpData = bitmap.LockBits(new Rectangle(startX, startY, width, height), ImageLockMode.WriteOnly, bitmap.PixelFormat);

            // Calculate bytes per pixel
            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;

            // Get the stride
            int stride = bmpData.Stride;

            unsafe
            {
                byte* ptr = (byte*)bmpData.Scan0;

                // Iterate over each row and column
                for (int y = startY; y < endY; y++)
                {
                    for (int x = startX; x < endX; x++)
                    {
                        // Get value from the start of the texture to the width
                        float grayscaleValue = grayscaleData[(y - startY) * width + (x - startX)];

                        if (grayscaleValue < 0)
                            grayscaleValue = 0;
                        if (grayscaleValue > 1.0f)
                            grayscaleValue = 1.0f;

                        byte gray = (byte)(grayscaleValue * 255); // Convert float value to byte

                        // Set pixel values
                        ptr[x * bytesPerPixel] = gray; // Blue channel
                        ptr[x * bytesPerPixel + 1] = gray; // Green channel
                        ptr[x * bytesPerPixel + 2] = gray; // Red channel
                        ptr[x * bytesPerPixel + 3] = 255; // Alpha channel
                    }

                    // Move to the next row
                    ptr += stride;
                }
            }

            // Unlock the bitmap
            bitmap.UnlockBits(bmpData);
        }
    }
}
