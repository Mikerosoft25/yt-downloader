using System;
using System.Linq;
using System.Threading.Tasks;
using YoutubeExplode;

namespace YTDownloader
{
    class VideoMetaData
    {
        public string Id { get; private set; }
        public string Title { get; private set; }
        public string ThumbnailUrl { get; private set; }
        public StreamMetaData MaxAudio { get; private set; }
        public StreamMetaData MediumAudio { get; private set; }
        public StreamMetaData MinAudio { get; private set; }
        
        public VideoMetaData(string id, string title, string thumbnailUrl, StreamMetaData min, StreamMetaData medium, StreamMetaData max)
        {
            Id = id;
            Title = title;
            ThumbnailUrl = thumbnailUrl;
            MaxAudio = max;
            MediumAudio = medium;
            MinAudio = min;
        }

        public static async Task<VideoMetaData> GetMetaData(string url)
        {
            if (!YoutubeClient.TryParseVideoId(url, out string videoId))
            {
                throw new Exception("Could not parse video id");
            }

            var i = await StateManager.Instance.Client.GetVideoInfoAsync(videoId);

            var streams = i.AudioStreams.OrderBy((s) =>
            {
                return s.Bitrate;
            });

            return new VideoMetaData(i.Id, i.Title, i.ImageMediumResUrl, new StreamMetaData(streams.First()), new StreamMetaData(streams.First()), new StreamMetaData(streams.Last()));
        }
    }
}
