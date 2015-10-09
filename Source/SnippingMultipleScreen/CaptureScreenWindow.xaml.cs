using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SnippingMultipleScreen
{
    /// <summary>
    /// Interaction logic for CaptureScreen.xaml
    /// </summary>
    public partial class CaptureScreenWindow : Window
    {
        private bool _mouseDown = false;
        private Rectangle _current;
        private Point _initialPoint;

        public event EventHandler Captured = delegate { };

        public ImageSource SetImageSource
        {
            set { this.imgScreenShot.Source = value; }
        }

        protected BitmapSource CropImage(BitmapSource image, Rectangle ClippingRectangle)
        {
            // Create a CroppedBitmap based off of a xaml defined resource.
            CroppedBitmap cb = new CroppedBitmap(
                image,
                //select region rect
                new Int32Rect(
                    (int)Canvas.GetLeft(ClippingRectangle),
                    (int)Canvas.GetTop(ClippingRectangle),
                    (int)ClippingRectangle.Width,
                    (int)ClippingRectangle.Height));

            return cb;
        }

        public CaptureScreenWindow()
        {
            this.InitializeComponent();

            //
            // Initialize Rectangle
            //
            _current = new Rectangle();
            _current.Width = 1;
            _current.Height = 1;
            // Create a SolidColorBrush and use it to paint the rectangle.
            SolidColorBrush myBrush = new SolidColorBrush();
            myBrush.Color = Color.FromArgb(50, 0, 0, 100);
            _current.Stroke = Brushes.Red;
            _current.StrokeThickness = 2;
            _current.Fill = myBrush;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            _mouseDown = (e.ButtonState == MouseButtonState.Pressed) && (e.ChangedButton == MouseButton.Left);

            if (!_mouseDown)
                return;

            _initialPoint = e.MouseDevice.GetPosition(MyCanvas);

            MyCanvas.Children.Add(_current);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_mouseDown)
            {
                Point position = e.MouseDevice.GetPosition(MyCanvas);
                _current.SetValue(Canvas.LeftProperty, Math.Min(position.X, _initialPoint.X));
                _current.SetValue(Canvas.TopProperty, Math.Min(position.Y, _initialPoint.Y));
                _current.Width = Math.Abs(position.X - _initialPoint.X);
                _current.Height = Math.Abs(position.Y - _initialPoint.Y);
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.ChangedButton == MouseButton.Left)
            {
                _mouseDown = false;

                this.Captured(CropImage((BitmapSource)this.imgScreenShot.Source, _current), new EventArgs());

                this.Close();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.Escape) this.Close();
        }
    }
}