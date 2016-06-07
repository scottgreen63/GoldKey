using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace GoldKeyLib.Entities
{
    [Serializable]
    public sealed class ExceptionHandler
    {
        private static ExceptionHandler _instance = null;
        private static readonly object _lock = new object();
        private static string _projectname = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        private ExceptionHandler()
        {
        }

        public static ExceptionHandler Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ExceptionHandler();
                    }

                    return _instance;
                }
            }
        }

        public sealed class ErrorLogger
        {
            private ErrorLogger()
            {
            }

            private static ErrorLogger _errorlogger = null;
            private static readonly object _lock = new object();

            public ErrorLogger Logger
            {
                get
                {
                    lock (_lock)
                    {
                        if (_errorlogger == null)
                        {
                            _errorlogger = new ErrorLogger();
                        }

                        return _errorlogger;
                    }
                }
            }

            public static bool WriteToErrorLog(string msg, string errortype, string stkTrace, string title)
            {
                try
                {
                    string sDirPath = Application.StartupPath + "\\Errors\\";
                    string sFileName = "errorlog.txt";
                    string sFullFilePath = sDirPath + "\\" + sFileName;

                    FileStream fileStream;
                    StreamWriter streamWriter;

                    if (!(System.IO.Directory.Exists(sDirPath)))
                    {
                        System.IO.Directory.CreateDirectory(sDirPath);
                    }

                    if (!(System.IO.File.Exists(sFullFilePath)))
                    {
                        fileStream = new FileStream(sFullFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        //fileStream.Close();
                        //fileStream = new FileStream(sFullFilePath, FileMode.Append, FileAccess.Write);
                    }
                    else
                    {
                        fileStream = new FileStream(sFullFilePath, FileMode.Append, FileAccess.Write);
                    }

                    streamWriter = new StreamWriter(fileStream);

                    streamWriter.Write("Title: " + title + Environment.NewLine);
                    streamWriter.Write("ErrorType: " + errortype + Environment.NewLine);
                    streamWriter.Write("Message: " + msg + Environment.NewLine);
                    streamWriter.Write("StackTrace: " + stkTrace + Environment.NewLine);
                    streamWriter.Write("Date/Time: " + DateTime.Now.ToString("g", CultureInfo.CreateSpecificCulture("en-us")) + Environment.NewLine);
                    streamWriter.Write("============================================" + Environment.NewLine);

                    streamWriter.Close();
                    //fileStream.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    //Since we can't write to EventLog...Write out to Error Log (file)
                    //The opposite approach will be taken with the Error Log
                    ExceptionHandler.EventLogger.WriteToEventLog(ex.Message, _projectname, EventLogEntryType.Error, _projectname);
                    return false;
                }
            }

            public static void AddError(string Message, string stkTrace)
            {
                //Log a information to the errorLog
                WriteToErrorLog(Message, EventLogEntryType.Information.ToString(), stkTrace, _projectname);
            }

            public static void AddErrorException(string Message, string stkTrace)
            {
                //Log a exception to the errorLog
                WriteToErrorLog(Message, EventLogEntryType.Error.ToString(), stkTrace, _projectname);
            }

            public static void AddErrorWarning(string Message, string stkTrace)
            {
                //Log a warning to the errorLog
                WriteToErrorLog(Message, EventLogEntryType.Warning.ToString(), stkTrace, _projectname);
            }
        }

        public sealed class EventLogger
        {
            private EventLogger()
            {
            }

            private static EventLogger _eventlogger = null;
            private static readonly object _lock = new object();

            public static EventLogger Logger
            {
                get
                {
                    lock (_lock)
                    {
                        if (_eventlogger == null)
                        {
                            _eventlogger = new EventLogger();
                        }

                        return _eventlogger;
                    }
                }
            }

            public static bool WriteToEventLog(string entry, string appName, EventLogEntryType eventType, string logName)
            {
                EventLog objEventLog = new EventLog();

                try
                {
                    if (!(EventLog.SourceExists(appName)))
                    {
                        EventLog.CreateEventSource(appName, logName);
                    }

                    objEventLog.Source = appName;
                    objEventLog.WriteEntry(entry, eventType);
                    return true;
                }
                catch (Exception Ex)
                {
                    //Since we can't write to EventLog...Write out to Error Log (file)
                    //The opposite approach will be taken with the Error Log
                    ExceptionHandler.ErrorLogger.WriteToErrorLog(Ex.Message, EventLogEntryType.Error.ToString(), Ex.StackTrace, _projectname);
                    return false;
                }
            }

            public static void AddEvent(string Message)
            {
                //Log a information to the eventLog
                WriteToEventLog(Message, _projectname, EventLogEntryType.Information, _projectname);
            }

            public static void AddEventException(string Message)
            {
                //Log a exception to the eventLog
                WriteToEventLog(Message, _projectname, EventLogEntryType.Error, _projectname);
            }

            public static void AddEventWarning(string Message)
            {
                //Log a warning to the eventLog
                WriteToEventLog(Message, _projectname, EventLogEntryType.Warning, _projectname);
            }
        }
    }
}