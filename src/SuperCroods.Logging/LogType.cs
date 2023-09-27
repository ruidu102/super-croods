using System;

namespace SuperCroods.Logging
{
    public class LogType
    {
        private string name;

        private LogType(string name)
        {
            this.name = name;
        }

        public string Name => name;

        public override string ToString()
        {
            return name;
        }

        public static LogType Info = new LogType("Info");
        public static LogType Warn = new LogType("Warn");
        public static LogType Error = new LogType("Error");
        public static LogType Off = new LogType("Off");

        public static LogType GetSetting(string arg)
        {
            if (arg.Equals("Info", StringComparison.OrdinalIgnoreCase))
                return Info;
            if (arg.Equals("Warn", StringComparison.OrdinalIgnoreCase))
                return Warn;
            if (arg.Equals("Error", StringComparison.OrdinalIgnoreCase))
                return Error;
            if (arg.Equals("None", StringComparison.OrdinalIgnoreCase))
                return Off;
            if (arg.Equals("Off", StringComparison.OrdinalIgnoreCase))
                return Off;

            throw new ArgumentException($"Unknown log type: {arg}", nameof(arg));
        }
    }
}
