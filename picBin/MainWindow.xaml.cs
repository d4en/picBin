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

                MemoryStream ms = new MemoryStream();
                mainImageBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                BitmapImage imageSource = new BitmapImage();

                imageSource.BeginInit();
                ms.Seek(0, SeekOrigin.Begin);
                imageSource.StreamSource = ms;
                imageSource.EndInit();

                imgClean.Source = imageSource;
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
            switch (method)
            {
                case -1:
                    lblProcessInfo.Content = "ERROR: No method selected.";
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        }
    }
}
