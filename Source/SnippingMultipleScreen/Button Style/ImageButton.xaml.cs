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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace SnippingMultipleScreen
{
    /// <summary>
    /// Interaction logic for ImageButton.xaml
    /// </summary>
    public partial class ImageButton : UserControl
    {
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance"), Description("Gets or sets the width of the System.Windows.Shapes.Shape outline.")]
        [DisplayName("Thickness")]
        public double Tickness
        {
            get { return this.Rect.StrokeThickness; }
            set
            {
                this.Rect.StrokeThickness = value;
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Common"), Description("Gets or sets the System.Windows.Media.ImageSource for the image.")]
        [DisplayName("BackgroundImage")]
        public ImageSource ImageSource
        {
            get { return this.BackgroundImage.Source; }
            set
            {
                this.BackgroundImage.Source = value;
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Common")]
        [Description("Gets or sets a value that describes how an System.Windows.Controls.Image\n\r" +
                                           "should be stretched to fill the destination rectangle.")]
        [DisplayName("ImageStretch")]
        public Stretch ImageStretch
        {
            get { return this.BackgroundImage.Stretch; }
            set
            {
                this.BackgroundImage.Stretch = value;
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Occurs when a ImageButton is clicked.")]
        [Category("Behavior")]
        [DisplayName("MouseClick")]
        public event RoutedEventHandler Click = delegate { };
        

        public ImageButton()
        {
            this.InitializeComponent();

            //
            // Create Mouse Click event's
            //
            bool MouseDown = false;
            this.MouseLeftButtonDown += (s, e) => { MouseDown = true; };
            this.MouseLeftButtonUp += (s, e) => { if (MouseDown) { MouseDown = false; Click(this, new RoutedEventArgs()); } };
            this.MouseLeave += (s, e) => { MouseDown = false; };
        }
    }
}