namespace SuperCroods.Network.SecureFTP
{
    public interface ISecureFTPCommand : ICommand
    {
        ISecureFTPConnection Connection { get; set; }
    }
}
