using System;
using System.Text;

namespace YTDownloader.Convert
{
    public class OutputBuilder
    {
        private string url;

        private bool useCodec = false;
        private bool useFormat = false;
        private bool useEndTime = false;
        private bool useDuration = false;
        private bool useStartTime = false;
        private bool useSamplingRate = false;
        private bool useFileSizeLimit = false;
        private bool useCustomArguments = false;

        private string codec;
        private string format;
        private TimeSpan endTime;
        private TimeSpan duration;
        private TimeSpan startTime;
        private int samplingRate;
        private string fileSizeLimit;
        private string customArguments;
        
        public OutputBuilder(string output)
        {
            url = output;
        }

        /// <summary>
        /// Select an encoder
        /// </summary>
        /// <param name="codec">encoder</param>
        public OutputBuilder WithCodec(string codec)
        {
            useCodec = true;
            this.codec = codec;

            return this;
        }

        /// <summary>
        /// Force file format. The format is normally auto detected
        /// </summary>
        /// <param name="format">Format</param>
        public OutputBuilder WithFormat(string format)
        {
            useFormat = true;
            this.format = format;

            return this;
        }
        
        /// <summary>
        /// Change end point
        /// </summary>
        /// <param name="startTime"><see cref="TimeSpan"/> from the begining</param>
        public OutputBuilder WithEndTime(TimeSpan endTime)
        {
            useEndTime = true;
            this.endTime = endTime;

            return this;
        }

        /// <summary>
        /// Limit the duration of data read from the input file
        /// </summary>
        /// <param name="duration">Limit</param>
        public OutputBuilder WithDuration(TimeSpan duration)
        {
            useDuration = true;
            this.duration = duration;

            return this;
        }

        /// <summary>
        /// Change start point
        /// </summary>
        /// <param name="startTime"><see cref="TimeSpan"/> from the begining</param>
        public OutputBuilder WithStartTime(TimeSpan startTime)
        {
            useStartTime = true;
            this.startTime = startTime;

            return this;
        }

        /// <summary>
        /// Set the audio sampling frequency
        /// </summary>
        /// <param name="samplingRate">Frequency</param>
        public OutputBuilder WithSamplingRate(int samplingRate)
        {
            useSamplingRate = true;
            this.samplingRate = samplingRate;

            return this;
        }

        /// <summary>
        /// Set the file size limit
        /// </summary>
        /// <param name="fileSizeLimit">Limit in Bytes. e.g. 1/1K/1M/1G</param>
        public OutputBuilder WithFileSizeLimit(string fileSizeLimit)
        {
            useFileSizeLimit = true;
            this.fileSizeLimit = fileSizeLimit;

            return this;
        }

        public OutputBuilder WithCustomArguments(string arguments)
        {
            useCustomArguments = true;
            customArguments += arguments;

            return this;
        }

        internal string GetOutput(int index)
        {
            StringBuilder b = new StringBuilder();

            if (useCodec) b.Append(" -codec " + codec);
            if (useFormat) b.Append(" -f " + format);
            if (useEndTime) b.Append(" -to " + FFmpegHelper.TimeSpanToString(endTime));
            if (useDuration) b.Append(" -t " + FFmpegHelper.TimeSpanToString(duration));
            if (useStartTime) b.Append(" -ss " + FFmpegHelper.TimeSpanToString(startTime));
            if (useSamplingRate) b.Append(" -ar " + samplingRate);
            if (useFileSizeLimit) b.Append(" -fs " + fileSizeLimit);
            if (useCustomArguments) b.Append(' ' + customArguments + ' ');

            b.Append(' ' + url);

            return b.ToString();
        }
    }
}
