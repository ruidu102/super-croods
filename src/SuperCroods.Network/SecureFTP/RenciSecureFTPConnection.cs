using Renci.SshNet;
using Renci.SshNet.Common;
using System;

namespace SuperCroods.Network.SecureFTP
{
    public class RenciSecureFTPConnection : ISecureFTPConnection
    {
        private readonly string password;

        private RenciSecureFTPConnection(SecureFTPInfo arg)
        {
            password = arg.Credential.Password;
            Host = arg.Host;
            Path = arg.Path;
            Client = new SftpClient(AuthentiactionMethod(arg));
        }

        public static RenciSecureFTPConnection CreateConnection(SecureFTPInfo arg)
        {
            return new RenciSecureFTPConnection(arg);
        }

        public bool IsConnected => Client.IsConnected ? true : false;

        public void Connect()
        {
            Client.Connect();
        }

        public void Disconnect()
        {
            if (Client != null)
            {
                Client.Disconnect();
                Client.Dispose();
            }
        }

        public string Host { get; }
        public string Path { get; }
        public SftpClient Client { get; }

        private ConnectionInfo AuthentiactionMethod(SecureFTPInfo arg)
        {
            KeyboardInteractiveAuthenticationMethod kauth =
                new KeyboardInteractiveAuthenticationMethod(arg.Credential.UserName);
            PasswordAuthenticationMethod pauth =
                new PasswordAuthenticationMethod(
                    arg.Credential.UserName, arg.Credential.Password);
            kauth.AuthenticationPrompt +=
                new EventHandler<AuthenticationPromptEventArgs>(HandleKeyEvent);

            return new ConnectionInfo(
                arg.Host,
                arg.Port,
                arg.Credential.UserName,
                pauth,
                kauth
                );
        }

        private void HandleKeyEvent(object sender, AuthenticationPromptEventArgs e)
        {
            foreach (AuthenticationPrompt prompt in e.Prompts)
            {
                if (prompt.Request.IndexOf(
                    "Password:", StringComparison.InvariantCultureIgnoreCase) != -1)
                    prompt.Response = password;
            }
        }
    }
}
