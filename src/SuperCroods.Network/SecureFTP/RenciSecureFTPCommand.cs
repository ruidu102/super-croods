using Renci.SshNet;

namespace SuperCroods.Network.SecureFTP
{
    public class RenciSecureFTPCommand : ISecureFTPCommand
    {
        private SftpClient client;

        private RenciSecureFTPCommand(SecureFTPInfo arg)
        {

        }

        public RenciSecureFTPCommand GetCommand(SecureFTPInfo arg)
        {
            return new RenciSecureFTPCommand(arg);
        }

        public ISecureFTPConnection Connection { get; set; }

        public void Execute() { }
    }
}
