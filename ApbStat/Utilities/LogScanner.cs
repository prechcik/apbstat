namespace Prechcik.ApbStat.Utilities
{
    using System;

    public class LogScanner : IDisposable
    {
        public event KillsAssistsStunsOrArrestsEventHandler OnNewKillsAssistsStunsOrArrests;

        public event LogRestartedEventHandler OnLogRestarted;
        public void Dispose()
        {
        }
        }
    }
}
