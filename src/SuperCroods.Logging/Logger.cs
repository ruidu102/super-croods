using System;
using static SuperCroods.Skeleton.IO.CustomDirectory;
using static SuperCroods.Skeleton.IO.CustomFile;

namespace SuperCroods.Logging
{
    public class Logger : ILogger
    {
        private LogType logType;
        private string name;

        private Logger() { }

        public static Logger Create() => new Logger();

        public Logger FileName(string arg)
        {
            name = arg;
            return this;
        }

        public Logger Type(string arg)
        {
            logType = LogType.GetSetting(arg);
            return this;
        }

        public void Log(object message)
        {
            Write(message);
        }

        public void Log(LogType type, object message)
        {
            Write(type, message);
        }

        private void Write(object message)
        {
            CreateDirectoryIfNeeded(name);
            WriteTo(name,
                $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] " +
                $"| {message.ToString()} {Environment.NewLine}");
        }

        private void Write(LogType type, object message)
        {
            if (type == LogType.Off)
                return;

            CreateDirectoryIfNeeded(name);
            WriteTo(name,
                $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] " +
                $"| {type.Name.ToUpper()} " +
                $"| {message.ToString()} {Environment.NewLine}");
        }

        private void CreateDirectoryIfNeeded(string path)
        {
            CreateDirectoryFor(
                GetDirectoryFrom(path));
        }
    }
}
