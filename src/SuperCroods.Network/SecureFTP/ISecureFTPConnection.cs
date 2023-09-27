namespace SuperCroods.Network.SecureFTP
{
    public interface ISecureFTPConnection : IConnection
    {
        bool IsConnected { get; }
    }
}
