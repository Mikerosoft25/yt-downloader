using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace YTDownloader.Convert
{
    public class FFmpeg
    {
        private string ffmpegExecutable;
        private bool hideBanner = true;
        private bool exitOnError = true;
        private bool overrideFiles = true;
        private List<InputBuilder> inputs = new List<InputBuilder>();
        private List<OutputBuilder> outputs = new List<OutputBuilder>();

        /// <summary>
        /// Create a new FFmpeg Builder with the ffmpeg executable in same directory
        /// </summary>
        public FFmpeg()
            : this("./ffmpeg.exe")
        { }
        
        /// <summary>
        /// Create a new FFmpeg Builder
        /// </summary>
        /// <param name="ffmpegExecutable">Path to the FFmpeg executable</param>
        public FFmpeg(string ffmpegExecutable)
        {
            this.ffmpegExecutable = ffmpegExecutable;
        }

        /// <summary>
        /// Overide existing files
        /// </summary>
        public FFmpeg SetOverride(bool overide)
        {
            overrideFiles = overide;
            return this;
        }

        /// <summary>
        /// Changes the path to the FFmpeg executable
        /// </summary>
        public FFmpeg SetFFmpegPath(string path)
        {
            ffmpegExecutable = path;
            return this;
        }
        
        /// <summary>
        /// Add an input
        /// </summary>
        /// <param name="input">url or file path</param>
        public FFmpeg AddInput(string input)
        {
            inputs.Add(new InputBuilder(input));
            return this;
        }
        
        /// <summary>
        /// Add an input
        /// </summary>
        /// <param name="input">Custom input</param>
        public FFmpeg AddInput(InputBuilder input)
        {
            inputs.Add(input);
            return this;
        }
        
        /// <summary>
        /// Add an output
        /// </summary>
        /// <param name="input">url or file path</param>
        public FFmpeg AddOutput(string output)
        {
            outputs.Add(new OutputBuilder(output));
            return this;
        }
        
        /// <summary>
        /// Add an output
        /// </summary>
        /// <param name="input">Custom output</param>
        public FFmpeg AddOutput(OutputBuilder output)
        {
            outputs.Add(output);
            return this;
        }

        /// <summary>
        /// Run the current configuration
        /// </summary>
        /// <returns>The started instance</returns>
        public FFmpegInstance Run()
        {
            string arguments = (overrideFiles)? "-y " : "-n ";
            if (hideBanner) arguments += "-hide_banner ";
            if (exitOnError) arguments += "-xerror ";

            for (int i = 0; i < inputs.Count; i++)
                arguments += inputs[i].GetInput(i) + ' ';

            for (int i = 0; i < outputs.Count; i++)
                arguments += outputs[i].GetOutput(i) + ' ';

#if DEBUG
            Console.WriteLine(arguments);
#endif

            Process ffmpeg = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ffmpegExecutable,
                    Arguments = arguments,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    WorkingDirectory = Environment.CurrentDirectory,

                    RedirectStandardError = true,
                    StandardErrorEncoding = Encoding.UTF8
                }
            };

            return new FFmpegInstance(ffmpeg);
        }
    }
}
