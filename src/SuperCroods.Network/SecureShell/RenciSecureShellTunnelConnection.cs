using Renci.SshNet;
using Renci.SshNet.Common;
using System;

namespace SuperCroods.Network.SecureShell
{
    public class RenciSecureShellTunnelConnection : ISecureShellConnection
    {
        private readonly string password;
        private readonly string localhost = "127.0.0.1";

        private SecureShellInfo tunnelConnectionInfo = null;
        private SshClient defaultClient;

        /// <summary>
        /// By defualt is the first client connection info
        /// </summary>
        /// <param name="arg"></param>
        private RenciSecureShellTunnelConnection(SecureShellInfo arg)
        {
            password = arg.Credential.Password;
            Path = arg.Path;

            DefaultClientConnect(AuthentiactionMethod(arg));
        }

        public static RenciSecureShellTunnelConnection CreateConnection(SecureShellInfo arg)
        {
            return new RenciSecureShellTunnelConnection(arg);
        }

        public string Path { get; }

        public SshClient Client { get; set; }

        public RenciSecureShellTunnelConnection TunnelConnection(SecureShellInfo arg)
        {
            tunnelConnectionInfo = arg;
            return this;
        }

        public bool IsDefaultConnected => defaultClient.IsConnected ? true : false;

        public bool IsConnected => Client.IsConnected ? true : false;

        /// <summary>
        /// Connnect to destination host via tunnel way.
        /// </summary>
        public void Connect()
        {
            var port = ForwardedPort;
            defaultClient.AddForwardedPort(port);
            port.Start();

            Client = new SshClient(
                AuthentiactionMethod(
                    new SecureShellInfo
                    {
                        Host = port.BoundHost,
                        Port = (int)port.BoundPort,
                        Credential = tunnelConnectionInfo.Credential
                    }));
         
            Client.Connect();
        }

        public void Disconnect()
        {
            if (Client != null)
            {
                Client.Disconnect();
                Client.Dispose();
            }

            DefaultClientDisconnect();
        }

        private ForwardedPortLocal ForwardedPort
        {
            get
            {
                return new ForwardedPortLocal(
                    localhost,
                    tunnelConnectionInfo.Host,
                    uint.Parse(tunnelConnectionInfo.Port.ToString())
                    );
            }
        }

        private void DefaultClientConnect(ConnectionInfo arg)
        {
            defaultClient = new SshClient(arg);
            defaultClient.Connect();
        }

        private void DefaultClientDisconnect()
        {
            if (defaultClient != null)
            {
                defaultClient.Disconnect();
                defaultClient.Dispose();
            }
        }

        private ConnectionInfo AuthentiactionMethod(SecureShellInfo arg)
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
                prompt.Response = password;
        }

    }
}
