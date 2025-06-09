namespace SuperCroods.Network.SecureCP
{
    public interface ISecureCPCommand : ICommand
    {
        ISecureCPConnection Connection { get; set; }
    }
}
