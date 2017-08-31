using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTDownloader
{
    class VideoMetaData
    {
        public string Id { get; private set; }
        public string Title { get; private set; }
        public string ThumbnailUrl { get; private set; }
        public IEnumerable<StreamMetaData> Streams { get; private set; }
        
        public VideoMetaData(string id, string title, string thumbnailUrl, IEnumerable<StreamMetaData> audioStreams)
        {
            Id = id;
            Title = title;
            ThumbnailUrl = thumbnailUrl;
            Streams = audioStreams;
        }
    }
}
