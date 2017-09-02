using System;
using System.Text.RegularExpressions;

namespace YTDownloader.Convert
{
    internal static class FFmpegHelper
    {
        internal static Regex TotalDurationRegex = new Regex(@"Duration: (\d{2}:\d{2}:\d{2}\.\d{2})");
        internal static Regex ProgressTimeRegex = new Regex(@"time=(\d{2}:\d{2}:\d{2}\.\d{2})");
        internal static Regex ErrorRegex = new Regex(@"Invalid argument|HTTP error \d+|Failed to send close message|Failed to read handshake response|Failed to send close message|No such file or directory");

        /// <summary>
        /// Convert a time duration string to the total amount of seconds
        /// </summary>
        /// <param name="time">A time string of the format: hh:mm:ss.ff</param>
        /// <returns>Total seconds</returns>
        internal static float StringToSeconds(string time)
        {
            string[] fragments = time.Split(':', '.');

            float seconds = float.Parse("0," + fragments[3]);
            seconds += System.Convert.ToSingle(fragments[2]);
            seconds += System.Convert.ToSingle(fragments[1]) * 60;
            seconds += System.Convert.ToSingle(fragments[0]) * 3600;

            return seconds;
        }

        /// <summary>
        /// Convert a TimeSpan object to a valid duration string
        /// </summary>
        /// <returns>String in the format [-]S+[.m...]</returns>
        internal static string TimeSpanToString(TimeSpan span)
        {
            return span.TotalSeconds.ToString().Replace(',', '.');
        }
    }
}
