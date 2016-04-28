namespace Wacton.Tovarisch.Logging
{
    using System;
    using System.IO;

    using NLog;
    using NLog.Config;
    using NLog.Targets;

    public class LoggerConfiguration
    {
        private static readonly string DefaultDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Wacton");
        private static readonly string StandardFilePath = DefaultDirectory + "/${processname}.log";
        private static readonly string ErrorFilePath = DefaultDirectory + "/${processname}_ERRORS.log";

        public static readonly string StandardLayout = @"[${longdate}|${level}] ${message} ${onexception:${newline}${exception:format=tostring:maxInnerException=5}${newline}}";
        public static readonly string ErrorLayout = @"[${longdate}] ${message} ${onexception:${newline}${exception:format=tostring:maxInnerException=5}${newline}}";

        public LoggerConfiguration()
        {
            // can't modify it if we haven't set it yet
            var config = new LoggingConfiguration();
            LogManager.Configuration = config;
        }

        /// <summary> Logs all levels to {CommonApplicationData}\Wacton\{processName}.log </summary>
        public LoggerConfiguration AddDefaultTraceFileTarget()
        {
            var target = CreateFileTarget(StandardFilePath);
            return this.AddTarget(LogLevel.Trace, target);
        }

        /// <summary> Logs error levels and above to {CommonApplicationData}\Wacton\{processName}_ERRORS.log </summary>
        public LoggerConfiguration AddDefaultErrorFileTarget()
        {
            var target = CreateFileTarget(ErrorFilePath);
            return this.AddTarget(LogLevel.Error, target);
        }

        /// <summary> Logs specified log level and above to specified file path </summary>
        public LoggerConfiguration AddFileTarget(LogLevel minLogLevel, string filePath, string layout = null)
        {
            var target = CreateFileTarget(filePath, layout);
            return this.AddTarget(minLogLevel, target);
        }

        /// <summary> Logs specified log level and above to {fileDirectory}\{yyyy-MM-dd}_{fileSuffix}.log </summary>
        public LoggerConfiguration AddDatedFileTarget(LogLevel minLogLevel, string fileDirectory, string fileSuffix, string layout = null)
        {
            var filePath = Path.Combine(fileDirectory, "${date:format=yyyy-MM-dd}_" + fileSuffix + ".log");
            return this.AddFileTarget(minLogLevel, filePath, layout);
        }

        /// <summary> Logs all levels to attached managed debugger (e.g. Visual Studio output window) </summary>
        public LoggerConfiguration AddDebuggerTarget(LogLevel minLogLevel, string layout = null)
        {
            var target = new DebuggerTarget { Layout = layout ?? StandardLayout };
            return this.AddTarget(minLogLevel, target);
        }

        /// <summary> Logs all levels to the console </summary>
        public LoggerConfiguration AddConsoleTarget(LogLevel minLogLevel, string layout = null)
        {
            var target = new ConsoleTarget { Layout = layout ?? StandardLayout };
            return this.AddTarget(minLogLevel, target);
        }

        /// <summary> Logs all levels to the console with level-based coloring </summary>
        public LoggerConfiguration AddColoredConsoleTarget(LogLevel minLogLevel, string layout = null)
        {
            var target = new ColoredConsoleTarget { Layout = layout ?? StandardLayout };
            return this.AddTarget(minLogLevel, target);
        }

        /// <summary> Logs specified log level and above to custom target (requires knowledge of NLog target usage) </summary>
        public LoggerConfiguration AddTarget(LogLevel minLogLevel, Target target)
        {
            var loggingRule = new LoggingRule("*", minLogLevel.NLogLevel, target);
            LogManager.Configuration.LoggingRules.Add(loggingRule);
            LogManager.ReconfigExistingLoggers();
            return this;
        }

        /// <summary> Clears all log targets </summary>
        public LoggerConfiguration ClearTargets()
        {
            LogManager.Configuration.LoggingRules.Clear();
            LogManager.ReconfigExistingLoggers();
            return this;
        }

        private static FileTarget CreateFileTarget(string filePath, string layout = null)
        {
            return new FileTarget { FileName = filePath, Layout = layout ?? StandardLayout, KeepFileOpen = true, EnableFileDelete = false, AutoFlush = true };
        }
    }
}
