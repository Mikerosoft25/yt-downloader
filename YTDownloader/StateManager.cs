using YoutubeExplode;
using System;
using System.IO;

namespace YTDownloader
{
    public sealed class StateManager
    {
        private static readonly StateManager instance = new StateManager();

        private const string settingsFile = "./settings.ini";

        public string SoundQuality { get; set; }
        public string OutputDirectory { get; set; }
        public YoutubeClient Client { get; set; }

        private StateManager()
        {
            Client = new YoutubeClient();
            LoadSetting();
        }

        public static StateManager Instance
        {
            get
            {
                return instance;
            }
        }

        public void LoadSetting()
        {
            if (!File.Exists(settingsFile))
            {
                StreamWriter sw = File.CreateText(settingsFile);
                sw.WriteLine("outputDirectory=" + Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%\\Downloads"));
                sw.WriteLine("soundQuality=Maximum");
                sw.Close();
            }

            string[] lines = File.ReadAllLines(settingsFile);

            foreach (string line in lines)
            {
                if (line[0] == '#') continue;

                string[] temp = line.Split(new char[] { '=' }, 2);

                if (temp[0] == "outputDirectory")
                {
                    OutputDirectory = temp[1];
                }

                if (temp[0] == "soundQuality"){
                    SoundQuality = temp[1];
                }
            }
        }

        public void SaveSettings()
        {
            StreamWriter sw = File.CreateText(settingsFile);
            sw.WriteLine("outputDirectory="+OutputDirectory);
            sw.WriteLine("soundQuality="+SoundQuality);
            sw.Close();
        }
    }
}
