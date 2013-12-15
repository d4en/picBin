using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Drawing;
using System.Collections.ObjectModel;

namespace picBin
{
    class Methods
    {
        static Methods() { }

        public static int Otsu(Bitmap inputBMP)
        {
            int result = 0;
            int varT = 0;
            int varP = 0;
            double overral = 0;
            double _overral = 0;
            double midT = 0.0;
            double midP = 0.0;
            double max = 0;
            double midValue = 0.0;

            int allPixels = inputBMP.Width * inputBMP.Height;

            int[] hist = IOperations.RedHistogram(inputBMP);

            for (int i = 0; i < 256; i++) overral += i * hist[i];

            for (int i = 0; i < 256; i++)
            {
                varT += hist[i];
                if (varT == 0) continue;

                varP = allPixels - varT;

                if (varP == 0) break;

                _overral += (double)(i * hist[i]);

                midT = _overral / varT;
                midP = (overral - _overral) / varP;

                midValue = (double)varT * varP * (midT - midP) * (midT - midP);

                if (midValue > max)
                {
                    max = midValue;
                    result = i;
                }

            }

            return result;
        }

        public static Bitmap Bernsen(Bitmap inputBmp, int range)
        {
            Bitmap newImage = inputBmp;
            System.Drawing.Color newColor;
            
            for (int i = range; i<inputBmp.Width-range; i++)
                for (int k = range; k<inputBmp.Height-range; k++)
                {
                    if (newImage.GetPixel(i, k).R > IOperations.getTreshold(inputBmp,i,k,range))
                    {
                        newColor = newColor = System.Drawing.Color.FromArgb(255, 255, 255);

                        newImage.SetPixel(i, k, newColor);
                    }
                    else
                    {
                        newColor = newColor = System.Drawing.Color.FromArgb(0, 0, 0);

                        newImage.SetPixel(i, k, newColor);
                    }

                    

                }

            return newImage;
        }
    }
}
