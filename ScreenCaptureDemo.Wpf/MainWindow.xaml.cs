using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ScreenCapture.Direct3D;

namespace ScreenCaptureDemo.Wpf
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public BitmapSource ImageSource { get; private set; }
        public Direct3DCapture ScreenCap = new Direct3DCapture();

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            Task.Factory.StartNew(Run);
        }

        public void Run()
        {
            while (true)
            {
                var array = ScreenCap.GetScreenBuffer();
                ImageSource = BitmapSource.Create(ScreenCap.Width, ScreenCap.Height, 300, 300, PixelFormats.Bgra32, BitmapPalettes.Halftone8, array, 4);
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ImageSource)));
            }
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            
            image.Freeze();
            return image;
        }
    }


}
