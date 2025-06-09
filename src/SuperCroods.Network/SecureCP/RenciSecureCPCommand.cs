using Renci.SshNet;

namespace SuperCroods.Network.SecureCP
{
    public class RenciSecureCPCommand : ISecureCPCommand
    {
        private ScpClient client;

        private RenciSecureCPCommand(SecureCPInfo arg) { }

        public RenciSecureCPCommand GetCommand(SecureCPInfo arg)
        {
            return new RenciSecureCPCommand(arg);
        }

        public ISecureCPConnection Connection { get; set; }

        public void Execute() { }
    }
}