using Renci.SshNet;
using Renci.SshNet.Common;
using System;

namespace SuperCroods.Network.SecureShell
{
    public class RenciSecureShellConnection : ISecureShellConnection
    {
        private string password;
        
        private RenciSecureShellConnection(SecureShellInfo arg)
        {
            password = arg.Credential.Password;
            KeyboardInteractiveAuthenticationMethod kauth =
                new KeyboardInteractiveAuthenticationMethod(arg.Credential.UserName);
            PasswordAuthenticationMethod pauth =
                new PasswordAuthenticationMethod(
                    arg.Credential.UserName, arg.Credential.Password);
            kauth.AuthenticationPrompt +=
                new EventHandler<AuthenticationPromptEventArgs>(HandleKeyEvent);
            ConnectionInfo connectionInfo =
                new ConnectionInfo(
                    arg.Host, arg.Port,
                    arg.Credential.UserName,
                    pauth, kauth);
            Path = arg.Path;
            Client = new SshClient(connectionInfo);
        }

        public static RenciSecureShellConnection CreateConnection(SecureShellInfo arg)
        {
            return new RenciSecureShellConnection(arg);
        }

        public string Path { get; }

        public SshClient Client { get; }

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

        private void HandleKeyEvent(object sender, AuthenticationPromptEventArgs e)
        {
            foreach (AuthenticationPrompt prompt in e.Prompts)
                prompt.Response = password;
        }
    }
}
