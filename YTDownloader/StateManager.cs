using YoutubeExplode;

namespace YTDownloader
{
    public sealed class StateManager
    {
        private static readonly StateManager instance = new StateManager();

        public string OutputDirectory { get; set; }
        public YoutubeClient Client { get; set; }

        private StateManager()
        {
            Client = new YoutubeClient();
        }

        public static StateManager Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
