namespace Wacton.Tovarisch.Logging
{
    using System;
    using System.Threading;

    using NLog;

    public static class Logger
    {
        private static readonly NLog.Logger NLogger = LogManager.GetLogger("Logger");
        private const int FlushTimeoutMilliseconds = 1000;

        public static LoggerConfiguration Configuration { get; } = new LoggerConfiguration();

        static Logger()
        {
            Configuration
                .AddDebuggerTarget(LogLevel.Trace)
                .AddDefaultTraceFileTarget()
                .AddDefaultErrorFileTarget();
        }

        /// <summary> Logs a trace-level message (trace is level 6, lowest level) </summary>
        public static void Trace(string message)
        {
            NLogger.Trace(ThreadPrefix(message));
        }

        /// <summary> Logs a trace-level exception (trace is level 6, lowest level) </summary>
        public static void Trace(Exception exception, string message)
        {
            NLogger.Trace(exception, ThreadPrefix(message));
        }

        /// <summary> Logs a debug-level message (debug is level 5) </summary>
        public static void Debug(string message)
        {
            NLogger.Debug(ThreadPrefix(message));
        }

        /// <summary> Logs a debug-level exception (debug is level 5) </summary>
        public static void Debug(Exception exception, string message)
        {
            NLogger.Debug(exception, ThreadPrefix(message));
        }

        /// <summary> Logs an info-level message (info is level 4) </summary>
        public static void Info(string message)
        {
            NLogger.Info(ThreadPrefix(message));
        }

        /// <summary> Logs an info-level exception (info is level 4) </summary>
        public static void Info(Exception exception, string message)
        {
            NLogger.Info(exception, ThreadPrefix(message));
        }

        /// <summary> Logs a warn-level message (warn is level 3) </summary>
        public static void Warn(string message)
        {
            NLogger.Warn(ThreadPrefix(message));
        }

        /// <summary> Logs a warn-level exception (warn is level 3) </summary>
        public static void Warn(Exception exception, string message)
        {
            NLogger.Warn(exception, ThreadPrefix(message));
        }

        /// <summary> Logs a error-level message (error is level 2) </summary>
        public static void Error(string message)
        {
            NLogger.Error(ThreadPrefix(message));
        }

        /// <summary> Logs an error-level exception (error is level 2) </summary>
        public static void Error(Exception exception, string message)
        {
            NLogger.Error(exception, ThreadPrefix(message));
        }

        /// <summary> Logs a fatal-level message (fatal is level 1, highest level) </summary>
        public static void Fatal(string message)
        {
            NLogger.Fatal(ThreadPrefix(message));
        }

        /// <summary> Logs a fatal-level exception (fatal is level 1, highest level) </summary>
        public static void Fatal(Exception exception, string message)
        {
            NLogger.Fatal(exception, ThreadPrefix(message));
        }

        /// <summary> Flush any pending log messages </summary>
        public static void Flush()
        {
            LogManager.Flush(FlushTimeoutMilliseconds);
        }

        // assuming static class initialised from main thread...
        private static readonly int MainThreadId = Thread.CurrentThread.ManagedThreadId;
        private static string ThreadPrefix(string message)
        {
            if (!string.IsNullOrWhiteSpace(Thread.CurrentThread.Name))
            {
                return $"[{Thread.CurrentThread.Name}] :: {message}";
            }

            if (Thread.CurrentThread.ManagedThreadId == MainThreadId)
            {
                return $"[Main Thread] :: {message}";
            }

            return $"[Thread {Thread.CurrentThread.ManagedThreadId}] :: {message}";
        }
    }
}
