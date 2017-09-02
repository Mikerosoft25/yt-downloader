using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using YTDownloader.Convert;

namespace YTDownloader
{
    /// <summary>
    /// Interaktionslogik für VideoView.xaml
    /// </summary>
    public partial class VideoView : UserControl
    {
        private VideoMetaData video;
        private bool videoLoaded = false;

        public VideoView(string url)
        {
            InitializeComponent();

            title.Content = url;
            LoadVideoInfo(url);
        }
        
        private async void LoadVideoInfo(string url)
        {
            video = await VideoMetaData.GetMetaData(url);
            videoLoaded = true;

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

        private void DownloadAudio()
        {
            if (!videoLoaded) return;

            StreamMetaData selectedStream = video.MaxAudio;

            progress.Visibility = Visibility.Visible;

            try
            {
                bool error = false;
                FFmpegInstance instance = new FFmpeg()
                    .AddInput(selectedStream.Url)
                    .AddOutput('"' + StateManager.Instance.OutputDirectory + "\\" + video.Title + ".mp3\"")
                    .Run();

                instance.ConversionError += (s, msg) =>
                {
                    error = true;
                    progress.Dispatcher.Invoke(() =>
                    {
                        progress.Foreground = Brushes.Red;
                        buttonImage.Source = new BitmapImage(new Uri("./Icons/refresh.png", UriKind.Relative));
                    });
                };

                instance.ConvertionFinished += (s, e) =>
                {
                    if (error) return;

                    buttonImage.Dispatcher.Invoke(() =>
                    {
                        progress.Value = 100;
                        buttonImage.Source = new BitmapImage(new Uri("./Icons/done.png", UriKind.Relative));
                    });
                };

                instance.ProgressChanged += (s, p) =>
                {
                    progress.Dispatcher.Invoke(() =>
                    {
                        progress.Value = p;
                    });
                };
            }
            catch
            {
                throw;
            }
        }

        private void ButtonEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            button.Background = new SolidColorBrush(Color.FromRgb(0xBA, 0xE3, 0xFB));
        }

        private void ButtonLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            button.Background = Brushes.Transparent;
        }
        
        private void ButtonMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DownloadAudio();
        }
    }
}
