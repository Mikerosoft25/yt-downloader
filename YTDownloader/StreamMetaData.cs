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
    }
}