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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private Bitmap mainImageBitmap = null;
        private string mainImageFileName = string.Empty;
        private string dlgFilter = "JPG Files (*.jpg)|*.jpg";
        private string dlgDefault = ".jpg";
        private int method = -1;

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = dlgDefault;
            dlg.Filter = dlgFilter;

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                this.mainImageFileName = dlg.FileName;
                this.mainImageBitmap = IOperations.loadBitmap(mainImageFileName);
                this.mainImageBitmap = IOperations.ConvertToBnW(mainImageBitmap);
                MemoryStream ms = new MemoryStream();
                mainImageBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                BitmapImage imageSource = new BitmapImage();

                imageSource.BeginInit();
                ms.Seek(0, SeekOrigin.Begin);
                imageSource.StreamSource = ms;
                imageSource.EndInit();

                imgClean.Source = imageSource;

                IOperations.RedHistogram(mainImageBitmap);
            }
        }

        private void checkBoxUp_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxDown.IsChecked = false;
            this.method = 1;
        }

        private void checkBoxDown_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxUp.IsChecked = false;
            this.method = 2;
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            Bitmap newImage;
            switch (method)
            {
                case -1:
                    lblProcessInfo.Content = "ERROR: No method selected.";
                    break;
                case 1:
                    newImage = IOperations.ApplyOtsu(mainImageBitmap);
                    MemoryStream ms = new MemoryStream();
                    newImage.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    BitmapImage imageSource = new BitmapImage();

                    imageSource.BeginInit();
                    ms.Seek(0, SeekOrigin.Begin);
                    imageSource.StreamSource = ms;
                    imageSource.EndInit();

                    imgProc.Source = imageSource;
                    break;
                case 2:
                    newImage = Methods.Bernsen(mainImageBitmap,10);
                    MemoryStream ms2 = new MemoryStream();
                    newImage.Save(ms2, System.Drawing.Imaging.ImageFormat.Bmp);
                    BitmapImage imageSource2 = new BitmapImage();

                    imageSource2.BeginInit();
                    ms2.Seek(0, SeekOrigin.Begin);
                    imageSource2.StreamSource = ms2;
                    imageSource2.EndInit();

                    imgProc.Source = imageSource2;

                    break;
            }
        }
    }
}
