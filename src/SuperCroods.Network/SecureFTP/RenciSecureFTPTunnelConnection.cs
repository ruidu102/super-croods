using Renci.SshNet;
using Renci.SshNet.Common;
using System;

namespace SuperCroods.Network.SecureFTP
{
    public class RenciSecureFTPTunnelConnection : ISecureFTPConnection
    {
        private readonly string password;
        private readonly string localhost = "127.0.0.1";

        private SecureFTPInfo tunnelConnectionInfo = null;
        private SshClient defaultClient;

        private RenciSecureFTPTunnelConnection(SecureFTPInfo arg)
        {
            password = arg.Credential.Password;
            Host = arg.Host;
            Path = arg.Path;
            DefaultClientConnect(AuthentiactionMethod(arg));
        }

        public static RenciSecureFTPTunnelConnection CreateConnection(SecureFTPInfo arg)
        {
            return new RenciSecureFTPTunnelConnection(arg);
        }

        public RenciSecureFTPTunnelConnection TunnelConnection(SecureFTPInfo arg)
        {
            tunnelConnectionInfo = arg;
            return this;
        }

        public string Host { get; }
        public string Path { get; }
        public SftpClient Client { get; set; }

        public bool IsDefaultConnected => defaultClient.IsConnected ? true : false;

        public bool IsConnected => Client.IsConnected ? true : false;

        public void Connect()
        {
            var port = ForwardedPort;
            defaultClient.AddForwardedPort(port);
            port.Start();

            Client = new SftpClient(
                AuthentiactionMethod(
                    new SecureFTPInfo
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
