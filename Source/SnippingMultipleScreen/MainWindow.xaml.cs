using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Threading;

namespace SnippingMultipleScreen
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


        private void btnNew_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Hide();

            Thread.Sleep(500);
            //
            // Capture Screen ---------------------------------------------------------
            CaptureScreenWindow CS = new CaptureScreenWindow();
            CS.SetImageSource = ScreenCapture.GetImageStream(new ScreenCapture().CaptureScreen());

            CS.Captured += (s, ea) =>
            {
                Image image = new Image();
                image.Source = (ImageSource)s;
                image.Stretch = Stretch.Uniform;
                image.Width = 350;
                image.Height = 200;

                lstImages.Items.Add(image);
            };

            CS.ShowDialog();
            // ---------------------------------------------------------
            this.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            lstImages.Items.Remove(lstImages.SelectedItem);
        }

        private void btnDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            if (lstImages.Items.Count > 0 && MessageBox.Show("Are you sure to delete all screen shots from list?", "Delete All",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                lstImages.Items.Clear();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<BitmapSource> images = new List<BitmapSource>();
            foreach (var item in lstImages.Items)
                images.Add((BitmapSource)(item as Image).Source);

            System.Drawing.Bitmap bmp = MargeImages.Combine(images.ToArray());

            //
            // Open Save Dialog
            //
            System.Windows.Forms.SaveFileDialog SFD = new System.Windows.Forms.SaveFileDialog();
            SFD.Title = "Select Save Path";
            SFD.DefaultExt = ".png";
            SFD.FileName = "ScreenSnips.png";
            if (SFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                bmp.Save(SFD.FileName);
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutBox.AboutForm about = new AboutBox.AboutForm((this.Icon as BitmapSource).BitmapFromSource());
            about.Show();
        }
    }
}
