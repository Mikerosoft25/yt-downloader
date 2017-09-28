using YoutubeExplode.Models.MediaStreams;

namespace YTDownloader
{
    public class StreamMetaData
    {
        public int Bitrate { get; private set; }
        public string Url { get; private set; }

        public StreamMetaData(int bitrate, string url)
        {
            Bitrate = bitrate;
            Url = url;
        }

        public StreamMetaData(AudioStreamInfo stream)
        {
            Bitrate = (int)stream.Bitrate;
            Url = stream.Url;
        }

        public override string ToString()
        {
            return '{' + Bitrate + ", " + Url + '}';
        }
    }
}