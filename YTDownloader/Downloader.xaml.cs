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

namespace YTDownloader
{
    /// <summary>
    /// Interaktionslogik für Downloader.xaml
    /// </summary>
    public partial class Downloader : UserControl
    {
        public event EventHandler OpenSettings;

        public Downloader()
        {
            InitializeComponent();
        }

        private void OnSettingsClick(object sender, RoutedEventArgs e)
        {
            OpenSettings?.Invoke(this, new EventArgs());
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(urlInput.Text) || urlInput.Text == "Enter video or playlist URL") return;

            videoList.Children.Add(new VideoView(urlInput.Text));

            urlInput.Text = "Enter video or playlist URL";
            urlInput.Foreground = Brushes.DimGray;
            urlInput.FontWeight = FontWeights.Normal;
        }

        private void UrlInput_Enter(object sender, KeyboardFocusChangedEventArgs e)
        {
            if(urlInput.Text == "Enter video or playlist URL")
            {
                urlInput.Text = "";

                urlInput.Foreground = Brushes.Black;
                urlInput.FontWeight = FontWeights.Bold;
            }
        }

        private void UrlInput_Leave(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (urlInput.Text == "")
            {
                urlInput.Text = "Enter video or playlist URL";

                urlInput.Foreground = Brushes.DimGray;
                urlInput.FontWeight = FontWeights.Normal;
            }
        }

        private void PressedEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrEmpty(urlInput.Text) || urlInput.Text == "Enter video or playlist URL") return;

                videoList.Children.Add(new VideoView(urlInput.Text));

                urlInput.Text = "";
            }
        }
    }
}
