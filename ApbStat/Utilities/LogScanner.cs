namespace Prechcik.ApbStat.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;

    public class LogScanner : IDisposable
    {
        private const int SleepTimeInMillis = 60000;

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
                        Thread.Sleep(SleepTimeInMillis);
                        continue;
                    }

                    ProcessLogStartDate(logLines);

                    var newLogLines = GetNewLogContent(logLines);

                    if (!newLogLines.Any())
                    {
                        Thread.Sleep(SleepTimeInMillis);
                        continue;
                    }

                    var kills = CountNumberOfOccurences(newLogLines, "Kill Reward");
                    var assists = CountNumberOfOccurences(newLogLines, "Assist Reward");
                    var stuns = CountNumberOfOccurences(newLogLines, "Stun Reward");
                    var arrests = CountNumberOfOccurences(newLogLines, "Arrest Reward");
                    var medals = CountNumberOfOccurences(newLogLines, "Medail Awarded");

                    PublishNewKillsAssistsStunsOrArrests(kills, assists, stuns, arrests, medals);

                    CurrentLineIndex = logLines.Length;
                    Thread.Sleep(SleepTimeInMillis);
                }
            }
        }

        public void EndLogScanning()
        {
            IsRunning = false;
        }

        public void Dispose()
        {
            EndLogScanning();
        }

        private static int CountNumberOfOccurences(IEnumerable<string> logLines, string kill)
        {
            return logLines.Count(x => x.StartsWith(string.Format("Log: [System]:  {0}", kill)));
        }

        private void PublishNewKillsAssistsStunsOrArrests(int kills, int assists, int stuns, int arrests, int medals)
        {
            if (kills <= 0 && assists <= 0 && stuns <= 0 && arrests <= 0 && medals <= 0)
            {
                return;
            }

            if (OnNewKillsAssistsStunsOrArrests != null)
            {
                OnNewKillsAssistsStunsOrArrests(
                    this,
                    new KillsAssistsStunsOrArrestsEventArgs
                    {
                        StartDate = StartOfLog,
                        Timestamp = DateTime.Now,
                        Kills = kills,
                        Assists = assists,
                        Stuns = stuns,
                        Arrests = arrests,
                        Medals = medals
                    });
            }
        }

        private string[] GetNewLogContent(string[] logLines)
        {
            int subArrayStartIndex;
            int subArrayLength;
            if (CurrentLineIndex <= 0)
            {
                subArrayStartIndex = 0;
                subArrayLength = logLines.Length;
            }
            else
            {
                subArrayStartIndex = CurrentLineIndex - 1;
                subArrayLength = logLines.Length - CurrentLineIndex;
            }

            var newLogLines = logLines.SubArray(subArrayStartIndex, subArrayLength);
            return newLogLines;
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
