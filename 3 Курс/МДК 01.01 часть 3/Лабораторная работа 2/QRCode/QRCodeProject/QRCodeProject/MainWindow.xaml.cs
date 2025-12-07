using QRCoder;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace QRCodeGeneratorApp
{
    public partial class MainWindow : Window
    {
        private BitmapImage currentQrImage;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateQR_Click(object sender, RoutedEventArgs e)
        {
            string data = DataTextBox.Text.Trim();
            if (string.IsNullOrEmpty(data))
            {
                MessageBox.Show("Введите данные для QR-кода!");
                return;
            }

            using (var generator = new QRCodeGenerator())
            using (var qrData = generator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q))
            using (var qrCode = new QRCode(qrData))
            using (var bitmap = qrCode.GetGraphic(20))
            {
                currentQrImage = BitmapToImageSource(bitmap);
                QrImage.Source = currentQrImage;
            }
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        private void QrImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && currentQrImage != null)
            {
                var saveDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "PNG изображение|*.png",
                    FileName = "qrcode.png"
                };
                if (saveDialog.ShowDialog() == true)
                {
                    using (var fileStream = new FileStream(saveDialog.FileName, FileMode.Create))
                    {
                        BitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(currentQrImage));
                        encoder.Save(fileStream);
                    }
                    MessageBox.Show("QR-код сохранён!");
                }
            }
        }
    }
}
