namespace Wacton.Tovarisch.Logging
{
    using System.Threading;

    using Wacton.Tovarisch.Enum;

    public class LogLevel : Enumeration
    {
        public static readonly LogLevel Trace = new LogLevel("Trace", NLog.LogLevel.Trace);
        public static readonly LogLevel Debug = new LogLevel("Debug", NLog.LogLevel.Debug);
        public static readonly LogLevel Info = new LogLevel("Info", NLog.LogLevel.Info);
        public static readonly LogLevel Warn = new LogLevel("Warn", NLog.LogLevel.Warn);
        public static readonly LogLevel Error = new LogLevel("Error", NLog.LogLevel.Error);
        public static readonly LogLevel Fatal = new LogLevel("Fatal", NLog.LogLevel.Fatal);

        private static int value;

        public NLog.LogLevel NLogLevel { get; }

        private LogLevel(string friendlyString, NLog.LogLevel nLogLevel)
            : base(Interlocked.Increment(ref value), friendlyString)
        {
            this.NLogLevel = nLogLevel;
        }
    }
}