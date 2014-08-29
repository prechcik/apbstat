namespace Prechcik.ApbStat.Utilities
{
    using System;
    using System.IO;

    public class LogScanner : IDisposable
    {
        public event KillsAssistsStunsOrArrestsEventHandler OnNewKillsAssistsStunsOrArrests;

        public event LogRestartedEventHandler OnLogRestarted;

        public DateTime StartOfLog { get; set; }

        public string FileLocation { get; set; }

        private bool IsRunning { get; set; }

        private int CurrentLineIndex { get; set; }

        public void BeginLogScanning()
        {
            IsRunning = true;

            while (IsRunning)
            {
                using (var reader = new StreamReader(File.Open(FileLocation, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), true))
                {
                    var logContent = reader.ReadToEnd().Replace("\0", string.Empty);
                    var logLines = logContent.Split('\n');

                    if (!logLines.Any() || logLines.All(string.IsNullOrWhiteSpace) || logLines.Length == CurrentLineIndex)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }

                    ProcessLogStartDate(logLines);
                    CurrentLineIndex = logLines.Length;
                    Thread.Sleep(10000);
                }
            }
        }
        public void Dispose()
        {
            EndLogScanning();
        }
        }

        private void ProcessLogStartDate(string[] logLines)
        {
            var logStartDate = ExtractStartOfLogDateTime(logLines);

            if (logStartDate != StartOfLog)
            {
                OnLogRestarted(this, new LogRestartedEventArgs { OldStartOfLog = StartOfLog, NewStartOfLog = logStartDate });
            }

            StartOfLog = logStartDate;
        }

        private DateTime ExtractStartOfLogDateTime(string[] logLines)
        {
            var startDateLogLine = logLines[1].Replace("Log: ", string.Empty);
            return DateTime.Parse(startDateLogLine);
        }
    }
}
