using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationRestaurant.Model
{
    public class LogWriter
    {
        private static LogWriter Instance = null;

        public List<string> Logs { get; private set; }

        private static readonly object LockLog = new object();

        public event EventHandler NewLogEvent;

        private LogWriter() { this.Logs = new List<string>(); }

        public static LogWriter GetInstance()
        {
            if (Instance == null) Instance = new LogWriter();
            return Instance;
        }

        public void Write(string log)
        {
            lock (LockLog)
            {
                Logs.Add(log);
                OnNewLogEvent(EventArgs.Empty);
            }
        }

        protected virtual void OnNewLogEvent(EventArgs e)
        {
            NewLogEvent?.Invoke(this, e);
        }
    }
}
