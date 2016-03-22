using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Epam.JDI.Core.Logging
{
    public class JDILogger : ILogger
    {
        public Func<string> LogFileFormat = () => "{0}_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".log";
        private static readonly ConcurrentDictionary<string, object> LogFileSyncRoots = new ConcurrentDictionary<string, object>();
        private static readonly string LogRecordTemplate = Environment.NewLine + "[{0}] {1}: {2}" + Environment.NewLine;
        public Func<string> LogDirectoryRoot = () => "/../.Logs/";
        public bool CreateFoldersForLogTypes = true;

        private static string GetLogRecord(string typeName, string msg)
        {
            return String.Format(LogRecordTemplate, typeName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), msg);
        }

        public JDILogger()
        {
            var logRoot = GetValidUrl(ConfigurationSettings.AppSettings["VILogPath"]);
            if (!String.IsNullOrEmpty(logRoot))
                LogDirectoryRoot = () => logRoot;
        }

        public JDILogger(string path)
        {
            LogDirectoryRoot = () => path;
        }

        public static string GetValidUrl(string logPath)
        {
            if (String.IsNullOrEmpty(logPath))
                return "";
            var result = logPath.Replace("/", "\\");
            if (result[1] != ':' && result.Substring(0, 3) != "..\\")
                result = (result[0] == '\\')
                    ? ".." + result
                    : "..\\" + result;
            return (result.Last() == '\\')
                ? result
                : result + "\\";
        }

        private void InLog(String fileName, String typeName, String msg)
        {
            var logDirectory = GetValidUrl(LogDirectoryRoot()) + (CreateFoldersForLogTypes ? fileName + "s\\" : "");
            CreateDirectory(logDirectory);
            var logFileName = logDirectory + String.Format(LogFileFormat(), fileName);

            var logFileSyncRoot = LogFileSyncRoots.GetOrAdd(logFileName, s => s);
            lock (logFileSyncRoot)
            {
                File.AppendAllText(logFileName, GetLogRecord(typeName, msg));
            }
        }

        public static void CreateDirectory(String directoryName)
        {
            if (!File.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
        }

        public void Trace(String msg)
        {
            InLog("Event", "Trace", msg);
        }
        public void Debug(String msg)
        {
            InLog("Event", "Debug", msg);
        }
        public void Info(String msg)
        {
            InLog("Event", "Info", msg);
        }

        public void Error(String msg)
        {
            InLog("Error", "Error", msg);
            InLog("Event", "Error", msg);
        }
        public void Step(String msg)
        {
            InLog("Event", "Step", msg);
        }
        public void TestDescription(String msg)
        {
            InLog("Event", "Test", msg);
        }
        public void TestSuit(String msg)
        {
            InLog("Event", "Suit", msg);
        }
    }
}

