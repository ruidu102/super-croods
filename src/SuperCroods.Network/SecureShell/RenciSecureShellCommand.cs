using Renci.SshNet;

namespace SuperCroods.Network.SecureShell
{
    public class RenciSecureShellCommand : ISecureShellCommand
    {
        private SshClient client;
        private string command;

        private RenciSecureShellCommand(SshClient arg)
        {
            client = arg;
        }

        public static RenciSecureShellCommand Create(SshClient arg)
        {
            return new RenciSecureShellCommand(arg);
        }

        public void Execute()
        {
            Result = client.RunCommand(command).Execute();
        }

        public ISecureShellCommand CommandText(string arg)
        {
            command = arg;
            return this;
        }

        public string Result { get; private set; }
    }
}
