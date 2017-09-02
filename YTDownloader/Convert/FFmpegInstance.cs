using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YTDownloader.Convert
{
    public class FFmpegInstance
    {
        private Process process;
        private bool finished = false;
        private bool disposed = false;
        private bool errorHandled = false;

        public bool Finished { get { return finished; } }

        /// <summary>
        /// Finished event will be after every conversion or on after fatal error
        /// </summary>
        public event EventHandler ConvertionFinished;

        /// <summary>
        /// Error event will be called on error. A short message will be passed
        /// </summary>
        public event EventHandler<string> ConversionError;

        /// <summary>
        /// Progress event will be called on progress change. The Percentage in percent will be passed
        /// </summary>
        public event EventHandler<float> ProgressChanged;

        internal FFmpegInstance(Process process)
        {
            this.process = process;
            
            Task.Run(() =>
            {
                try
                {
                    float totalDuration = 0;

                    process.Exited += (sender, e) => {
                        if (!errorHandled && process.ExitCode != 0)
                            InvokeConversionError("Error #" + process.ExitCode);

                        Dispose();
                    };
                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (string.IsNullOrEmpty(e.Data)) return;

#if DEBUG
                        Console.WriteLine(e.Data);
#endif

                        Match match;

                        match = FFmpegHelper.TotalDurationRegex.Match(e.Data);
                        if (match.Success)
                            totalDuration = FFmpegHelper.StringToSeconds(match.Groups[1].Value);

                        match = FFmpegHelper.ProgressTimeRegex.Match(e.Data);
                        if (match.Success)
                            this.InvokeProgressChanged(FFmpegHelper.StringToSeconds(match.Groups[1].Value) / totalDuration * 100);
                        
                        match = FFmpegHelper.ErrorRegex.Match(e.Data);
                        if (match.Success)
                        {
                            errorHandled = true;
                            this.InvokeConversionError(match.Value);
                        }
                    };
                    
                    process.Start();
                    process.BeginErrorReadLine();
                    process.WaitForExit();
                }
                catch (Exception e)
                {
                    errorHandled = true;
                    this.InvokeConversionError(e.Message);
                }
                finally
                {
                    finished = true;
                    this.InvokeConversionFinished();
                    Cancel();
                }
            });
        }

        ~FFmpegInstance()
        {
            Dispose();
        }

        internal void InvokeConversionError(string message)
        {
            ConversionError?.Invoke(this, message);
        }

        internal void InvokeConversionFinished()
        {
            ConvertionFinished?.Invoke(this, new EventArgs());
        }
        
        internal void InvokeProgressChanged(float progess)
        {
            ProgressChanged?.Invoke(this, progess);
        }
        
        /// <summary>
        /// Cancel the conversion
        /// </summary>
        public void Cancel()
        {
            finished = true;

            if (disposed || !process.HasExited) return;

            process.Kill();
            errorHandled = true;
            InvokeConversionError("process terminated");
        }

        private void Dispose()
        {
            if (disposed) return;

            disposed = true;
            process.Dispose();
        }
    }
}
