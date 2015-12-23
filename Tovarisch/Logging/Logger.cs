namespace Wacton.Tovarisch.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using NLog;

    public class Logger
    {
        public static readonly Logger Default = new Logger(LogManager.GetLogger("DefaultLogger"));
        private static readonly Dictionary<string, Logger> Loggers = new Dictionary<string, Logger>();

        private const int FlushTimeoutMilliseconds = 1000;

        private readonly NLog.Logger actualLogger;
        private Logger(NLog.Logger actualLogger)
        {
            this.actualLogger = actualLogger;
        }

        public static Logger Get(string name)
        {
            if (!Loggers.ContainsKey(name))
            {
                Loggers.Add(name, new Logger(LogManager.GetLogger(name)));
            }

            return Loggers[name];
        }

        /// <summary> Logs a trace-level message (trace is level 6, lowest level) </summary>
        public void Trace(string message)
        {
            this.actualLogger.Trace(ThreadPrefix(message));
        }

        /// <summary> Logs a trace-level exception (trace is level 6, lowest level) </summary>
        public void Trace(Exception exception, string message)
        {
            this.actualLogger.Trace(exception, ThreadPrefix(message));
        }

        /// <summary> Logs a debug-level message (debug is level 5) </summary>
        public void Debug(string message)
        {
            this.actualLogger.Debug(ThreadPrefix(message));
        }

        /// <summary> Logs a debug-level exception (debug is level 5) </summary>
        public void Debug(Exception exception, string message)
        {
            this.actualLogger.Debug(exception, ThreadPrefix(message));
        }

        /// <summary> Logs an info-level message (info is level 4) </summary>
        public void Info(string message)
        {
            this.actualLogger.Info(message);
        }

        /// <summary> Logs an info-level exception (info is level 4) </summary>
        public void Info(Exception exception, string message)
        {
            this.actualLogger.Info(exception, ThreadPrefix(message));
        }

        /// <summary> Logs a warn-level message (warn is level 3) </summary>
        public void Warn(string message)
        {
            this.actualLogger.Warn(message);
        }

        /// <summary> Logs a warn-level exception (warn is level 3) </summary>
        public void Warn(Exception exception, string message)
        {
            this.actualLogger.Warn(exception, ThreadPrefix(message));
        }

        /// <summary> Logs a error-level message (error is level 2) </summary>
        public void Error(string message)
        {
            this.actualLogger.Error(ThreadPrefix(message));
        }

        /// <summary> Logs an error-level exception (error is level 2) </summary>
        public void Error(Exception exception, string message)
        {
            this.actualLogger.Error(exception, ThreadPrefix(message));
        }

        /// <summary> Logs a fatal-level message (fatal is level 1, highest level) </summary>
        public void Fatal(string message)
        {
            this.actualLogger.Fatal(ThreadPrefix(message));
        }

        /// <summary> Logs a fatal-level exception (fatal is level 1, highest level) </summary>
        public void Fatal(Exception exception, string message)
        {
            this.actualLogger.Fatal(exception, ThreadPrefix(message));
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
