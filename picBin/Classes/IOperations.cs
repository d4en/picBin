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
    }
}
