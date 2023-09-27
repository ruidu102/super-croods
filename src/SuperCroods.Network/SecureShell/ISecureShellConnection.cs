namespace SuperCroods.Network.SecureShell
{
    public interface ISecureShellConnection : IConnection
    {
        bool IsConnected { get; }
    }
}
