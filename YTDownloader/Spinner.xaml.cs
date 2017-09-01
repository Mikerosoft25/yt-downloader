using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace YTDownloader
{
    /// <summary>
    /// Interaktionslogik für Spinner.xaml
    /// </summary>
    public partial class Spinner : UserControl
    {
        private int strokeThickness = 6;
        private Brush spinnerForeground = Brushes.Black;
        private Brush spinnerBackground = new SolidColorBrush(Color.FromArgb(0x66, 0, 0, 0));
        
        public int StrokeThickness
        {
            get { return strokeThickness; }
            set { strokeThickness = value; }
        }

        public Brush SpinnerForeground
        {
            get { return spinnerForeground; }
            set { spinnerForeground = value; }
        }

        public Brush SpinnerBackground
        {
            get { return spinnerBackground; }
            set { spinnerBackground = value; }
        }

        public Spinner()
        {
            InitializeComponent();
        }

        private void OnResize(object sender, SizeChangedEventArgs e)
        {
            arc.Data = Geometry.Parse($"M { (Width / 2).ToString(CultureInfo.InvariantCulture) },{ (StrokeThickness / 2).ToString(CultureInfo.InvariantCulture) } A { (Width / 2 - StrokeThickness / 2).ToString(CultureInfo.InvariantCulture) },{ (Width / 2 - StrokeThickness / 2).ToString(CultureInfo.InvariantCulture) } 0 0 1 { (Width - StrokeThickness / 2).ToString(CultureInfo.InvariantCulture) }, { (Height / 2).ToString(CultureInfo.InvariantCulture) }");
        }
    }
}
