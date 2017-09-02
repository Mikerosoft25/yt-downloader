using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using YoutubeExplode;

namespace YTDownloader
{
    /// <summary>
    /// Interaktionslogik für VideoView.xaml
    /// </summary>
    public partial class VideoView : UserControl
    {
        private VideoMetaData video;
        private YoutubeClient client = new YoutubeClient();

        public VideoView(string url)
        {
            InitializeComponent();

            title.Content = url;
            LoadVideoInfo(url);
        }
        
        private async void LoadVideoInfo(string url)
        {
            video = await VideoMetaData.GetMetaData(url);

            title.Content = video.Title;
            title.FontStyle = FontStyles.Normal;
            title.Foreground = Brushes.Black;

            spinner.Visibility = Visibility.Collapsed;
            thumbnail.Visibility = Visibility.Visible;

            BitmapImage thumbnailImage = new BitmapImage();
            thumbnailImage.BeginInit();
            thumbnailImage.UriSource = new Uri(video.ThumbnailUrl, UriKind.Absolute);
            thumbnailImage.EndInit();

            thumbnail.Source = thumbnailImage;
        }
    }
}
