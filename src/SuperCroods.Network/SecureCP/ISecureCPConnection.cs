namespace SuperCroods.Network.SecureCP
{
    public interface ISecureCPConnection : IConnection
    {
        bool IsConnected { get; }
    }
}
