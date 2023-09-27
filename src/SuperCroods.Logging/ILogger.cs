namespace SuperCroods.Logging
{
    public interface ILogger
    {
        void Log(object message);
        void Log(LogType type, object message);
    }
}
