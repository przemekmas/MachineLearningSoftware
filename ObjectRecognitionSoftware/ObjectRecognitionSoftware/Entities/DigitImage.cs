// The code has been retrieved from the following site https://jamesmccaffrey.wordpress.com/2013/11/23/reading-the-mnist-data-set-with-c/
// The code has been refactored to be more readable and clear as to what is happening
using System.Collections.Generic;

namespace ObjectRecognitionSoftware.Entities
{
    public class DigitImage
    {
        private byte[][] imagePixels;
        private byte imageLabel;
        private string emptyPixel = " ";
        private string blackPixel = "0";
        private string grayPixel = ".";

        public DigitImage(byte[][] pixels, byte label)
        {
            imagePixels = new byte[28][];
            for (int i = 0; i < imagePixels.Length; ++i)
            {
                imagePixels[i] = new byte[28];
            }

            for (int i = 0; i < 28; ++i)
            {
                for (int j = 0; j < 28; ++j)
                {
                    imagePixels[i][j] = pixels[i][j];
                }
            }

            imageLabel = label;
        }

        public override string ToString()
        {
            var pixelChar = string.Empty;
            for (int i = 0; i < 28; ++i)
            {
                for (int j = 0; j < 28; ++j)
                {
                    if (imagePixels[i][j] == 0)
                    {
                        pixelChar += emptyPixel; // represents empty pixels
                    }
                    else if (imagePixels[i][j] == 255)
                    {
                        pixelChar += blackPixel; // represents black pixels
                    }
                    else
                    {
                        pixelChar += grayPixel; // represents gray pixels
                    }
                }
                pixelChar += "\n";
            }
            pixelChar += imageLabel.ToString();
            return pixelChar;
        }

        public List<int> GetImageArray()
        {
            var pixelIntensity = new List<int>();
            for (int i = 0; i < 28; ++i)
            {
                for (int j = 0; j < 28; ++j)
                {
                    if (imagePixels[i][j] == 0)
                    {
                        pixelIntensity.Add(0); // represents empty pixels
                    }
                    else if (imagePixels[i][j] == 255)
                    {
                        pixelIntensity.Add(1); // represents black pixels
                    }
                    else
                    {
                        pixelIntensity.Add(2); // represents gray pixels
                    }
                }
            }
            return pixelIntensity;
        }
    }
}
