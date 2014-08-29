namespace Prechcik.ApbStat.Utilities
{
    using System;

    public class KillsAssistsStunsOrArrestsEventArgs : EventArgs
    {
        public int Kills { get; set; }

        public int Assists { get; set; }

        public int Stuns { get; set; }

        public int Arrests { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime Timestamp { get; set; }

        public int Medals { get; set; }
    }
}
