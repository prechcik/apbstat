namespace Prechcik.ApbStat.Utilities
{
    using System;

    public class LogRestartedEventArgs : EventArgs
    {
        public DateTime OldStartOfLog { get; set; }

        public DateTime NewStartOfLog { get; set; }
    }
}
