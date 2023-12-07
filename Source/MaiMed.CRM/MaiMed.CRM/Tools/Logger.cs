using Sagede.Core.Logging;
using System;

namespace MaiMed.CRM.Tools
{
    internal static class Logger
    {
        private static ILogger _logger;
        private static readonly object LockObject = new object();

        /// <summary>
        /// Liefert die Instanz des Loggers.
        /// </summary>
        private static ILogger SageLogger
        {
            get
            {
                lock (LockObject)
                {
                    return _logger ?? (_logger = LogManager.GetLogger("MaiMed.CRM", "Tracelogger"));
                }
            }
        }


        public static void LogError(string source, Exception ex)
        {
            LogException(source, ex);

            using (System.IO.StreamWriter file =
                   new System.IO.StreamWriter(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location) + "_Error.txt"), true))
            {
                file.WriteLine(DateTime.Now.ToString() + " " + source + "\r\n" + ex.ToString());
            }
        }

        /// <summary>
        /// Loggt Debug-Informationen im TraceLogger_Template1-Manager
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void TracelogVerbose(string message, params object[] args)
        {
            //Logger.LogVerboseFormat(message, args);
            SageLogger.Verbose(string.Format(message, args));
        }

        /// <summary>
        /// Loggt Debug-Informationen im TraceLogger_Template1-Manager
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void TracelogWarning(string message, params object[] args)
        {
            //Logger.LogVerboseFormat(message, args);
            SageLogger.Warning(string.Format(message, args));
        }

        /// <summary>
        /// Loggt Debug-Informationen im TraceLogger_Template1-Manager
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void TracelogInfo(string message, params object[] args)
        {
            //Logger.LogVerboseFormat(message, args);
            SageLogger.Information(string.Format(message, args));
        }

        /// <summary>
        /// Loggt Exceptions im Tracelog-Manager
        /// </summary>
        /// <param name="ex"></param>
        public static void LogException(Exception ex)
        {
            SageLogger.Error(String.Format("{0},{1},{2}", ex.Message, Environment.NewLine, ex.StackTrace));
        }

        private static void LogException(string source, Exception ex)
        {
            SageLogger.Error($"{source},{ex.Message},{Environment.NewLine},{ex.StackTrace}");
        }
    }
}