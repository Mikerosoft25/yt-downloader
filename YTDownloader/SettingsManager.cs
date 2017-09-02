using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTDownloader
{
    public sealed class SettingsManager
    {
        private static readonly SettingsManager instance = new SettingsManager();

        public string OutputDirectory { get; set; }

        private SettingsManager()
        {
                                                  
        }

        public static SettingsManager Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
