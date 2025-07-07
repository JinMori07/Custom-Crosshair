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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Microsoft.Win32;
using WpfAnimatedGif;

namespace JinsCrosshair
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var KanekiBannerGif = new BitmapImage(new Uri("pack://application:,,,/Images/kaneki.gif"));
            ImageBehavior.SetAnimatedSource(KanekiBanner, KanekiBannerGif);
        }

        private void WindowTop_MouseButton(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFilePath);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                crosshair_image.Source = bitmap;
            }
        }

        private void ConfirmImage_Click(object senderm, RoutedEventArgs e)
        {
            if (crosshair_image.Source is BitmapImage selectedBitmap)
            {
                double height = crosshair_image.Height;
                double width = crosshair_image.Width;
                double opacity = crosshair_image.Opacity;

                CrosshairWindow crosshairWindow = new CrosshairWindow(selectedBitmap, width, height, opacity);
                crosshairWindow.Show();
            }
            else
            {
                MessageBox.Show("Please load a image first");
            }
        }

        private void QuitApp_click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeApp_click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void SliderOpacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (crosshair_image==null)
            {
                return;
            }

            double value = e.NewValue;

            crosshair_image.Opacity = value / 100;
        }

        private void SliderSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (crosshair_image == null)
            {
                return;
            }

            double scalefactor = 20;
            double value = e.NewValue * scalefactor;

            crosshair_image.Width = value;
            crosshair_image.Height = value;
        }
    }
}
