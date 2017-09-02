using System;
using System.Text;

namespace YTDownloader.Convert
{
    public class InputBuilder
    {
        private string url;

        private bool useCodec = false;
        private bool useFormat = false;
        private bool useDuration = false;
        private bool useStartTime = false;
        private bool useSamplingRate = false;
        private bool useCustomArguments = false;

        private string codec;
        private string format;
        private TimeSpan duration;
        private TimeSpan startTime;
        private int samplingRate;
        private string customArguments;

        /// <summary>
        /// Create a new input builder
        /// </summary>
        /// <param name="input">url or file path</param>
        public InputBuilder(string input)
        {
            url = input;
        }

        /// <summary>
        /// Select a decoder 
        /// </summary>
        /// <param name="codec">Decoder</param>
        public InputBuilder WithCodec(string codec)
        {
            useCodec = true;
            this.codec = codec;

            return this;
        }

        /// <summary>
        /// Force file format. The format is normally auto detected
        /// </summary>
        /// <param name="format">Format</param>
        public InputBuilder WithFormat(string format)
        {
            useFormat = true;
            this.format = format;

            return this;
        }

        /// <summary>
        /// Limit the duration of data read from the input file
        /// </summary>
        /// <param name="duration">Limit</param>
        public InputBuilder WithDuration(TimeSpan duration)
        {
            useDuration = true;
            this.duration = duration;

            return this;
        }

        /// <summary>
        /// Change start point
        /// </summary>
        /// <param name="startTime"><see cref="TimeSpan"/> from the begining</param>
        public InputBuilder WithStartTime(TimeSpan startTime)
        {
            useStartTime = true;
            this.startTime = startTime;

            return this;
        }

        /// <summary>
        /// Set the audio sampling frequency
        /// </summary>
        /// <param name="samplingRate">Frequency</param>
        public InputBuilder WithSamplingRate(int samplingRate)
        {
            useSamplingRate = true;
            this.samplingRate = samplingRate;

            return this;
        }

        public InputBuilder WithCustomArguments(string arguments)
        {
            useCustomArguments = true;
            customArguments += arguments;

            return this;
        }
        
        internal string GetInput(int index)
        {
            StringBuilder b = new StringBuilder();

            if (useCodec) b.Append(" -codec " + codec);
            if (useFormat) b.Append(" -f " + format);
            if (useDuration) b.Append(" -t " + FFmpegHelper.TimeSpanToString(duration));
            if (useStartTime) b.Append(" -ss " + FFmpegHelper.TimeSpanToString(startTime));
            if (useSamplingRate) b.Append(" -ar " + samplingRate);
            if (useCustomArguments) b.Append(' ' + customArguments + ' ');

            b.Append(" -i " + url);

            return b.ToString();
        }
    }
}
