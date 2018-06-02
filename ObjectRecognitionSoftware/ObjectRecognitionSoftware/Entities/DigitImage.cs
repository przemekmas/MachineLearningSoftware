// The code has been retrieved from the following site https://jamesmccaffrey.wordpress.com/2013/11/23/reading-the-mnist-data-set-with-c/
// The code has been refactored to be more readable and clear as to what is happening
using System.Collections.Generic;

namespace ObjectRecognitionSoftware.Entities
{
    public class DigitImage
    {
        private byte[][] _imagePixels;
        private byte _imageLabel;
        private string _emptyPixel = " ";
        private string _blackPixel = "0";
        private string _grayPixel = ".";

        public DigitImage(byte[][] pixels, byte label)
        {
            _imagePixels = new byte[28][];
            for (int i = 0; i < _imagePixels.Length; ++i)
            {
                _imagePixels[i] = new byte[28];
            }

            for (int i = 0; i < 28; ++i)
            {
                for (int j = 0; j < 28; ++j)
                {
                    _imagePixels[i][j] = pixels[i][j];
                }
            }

            _imageLabel = label;
        }

        public override string ToString()
        {
            var pixelChar = string.Empty;
            for (int i = 0; i < 28; ++i)
            {
                for (int j = 0; j < 28; ++j)
                {
                    if (_imagePixels[i][j] == 0)
                    {
                        pixelChar += _emptyPixel; // represents empty pixels
                    }
                    else if (_imagePixels[i][j] == 255)
                    {
                        pixelChar += _blackPixel; // represents black pixels
                    }
                    else
                    {
                        pixelChar += _grayPixel; // represents gray pixels
                    }
                }
                pixelChar += "\n";
            }
            pixelChar += _imageLabel.ToString();
            return pixelChar;
        }

        public List<int> GetImageArray()
        {
            var pixelIntensity = new List<int>();
            for (int i = 0; i < 28; ++i)
            {
                for (int j = 0; j < 28; ++j)
                {
                    if (_imagePixels[i][j] == 0)
                    {
                        pixelIntensity.Add(0); // represents empty pixels
                    }
                    else if (_imagePixels[i][j] == 255)
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
