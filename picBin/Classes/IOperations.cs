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
    class IOperations
    {
        static IOperations()
        {
        }

        static public Bitmap loadBitmap(string fileName)
        {
            Stream BitmapStream = System.IO.File.Open(fileName, System.IO.FileMode.Open);
            System.Drawing.Image img = System.Drawing.Image.FromStream(BitmapStream);
            Bitmap loadedBitmap = new Bitmap(img);
            return loadedBitmap;
        }

        static public Bitmap ConvertToBnW(Bitmap inputBMP)
        {
            for (int i = 0; i < inputBMP.Width; i++)
            {
                for (int j = 0; j < inputBMP.Height; j++)
                {
                    System.Drawing.Color inputBMPColor = inputBMP.GetPixel(i, j);

                    int grayScale = (int)((inputBMPColor.R * .3) + (inputBMPColor.G * .59)
                        + (inputBMPColor.B * .11));

                    System.Drawing.Color newColor = System.Drawing.Color.FromArgb(grayScale, grayScale, grayScale);

                    inputBMP.SetPixel(i, j, newColor);
                }
            }
            return inputBMP;
        }

        static public int[] RedHistogram(Bitmap inputBMP)
        {
            int[] histogram = new int[256];
            int red = 0;
            for (int i = 0; i < histogram.Length; i++) histogram[i] = 0;

            for (int i =0 ; i < inputBMP.Width; i++)
                for (int k = 0; k < inputBMP.Height; k++)
                {
                    red = inputBMP.GetPixel(i, k).R;
                    histogram[red]++;
                    //Console.WriteLine(histogram[red]);
                }
            

            return histogram;
        }

        static public Bitmap ApplyOtsu(Bitmap inputBMP)
        {
            System.Drawing.Color newColor;
            int otsuValue = Methods.Otsu(inputBMP);

            for(int i =0 ; i<inputBMP.Width; i++)
                for (int k = 0; k < inputBMP.Height; k++)
                {
                    if (inputBMP.GetPixel(i, k).R > otsuValue)
                    {
                        newColor = newColor = System.Drawing.Color.FromArgb(255, 255, 255);

                        inputBMP.SetPixel(i, k, newColor);
                    }
                    else
                    {
                        newColor = newColor = System.Drawing.Color.FromArgb(0, 0, 0);

                        inputBMP.SetPixel(i, k, newColor);
                    }
                }
             
            return inputBMP;
        }

        static public int getTreshold(Bitmap inputBMP, int x, int y,int range)
        {
            int localMin = int.MaxValue;
            int localMax = 0;
            double treshold = 0;
            int result;
            int mainPixel = inputBMP.GetPixel(x, y).R;

           for(int i = x-range; i<x+range; i++)
               for (int k = y - range; k < y + range; k++)
               {
                   if (inputBMP.GetPixel(i, k).R < localMin)
                   {
                       localMin = inputBMP.GetPixel(i, k).R;
                   }
                   if (inputBMP.GetPixel(i, k).R > localMax)
                   {
                       localMax = inputBMP.GetPixel(i, k).R;
                   }
               }
           treshold = (localMax + localMin) / 2;
           result = (int)treshold;
           return result;

        }
    }
}
