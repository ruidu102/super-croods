using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperCroods.Network.SecureShell;

namespace SuperCroods.Network.Tests.SecureShell
{
    [TestClass]
    public class RenciSecureShellTunnelConnectionTest
    {

        private static List<SecureShellInfo> clientInfoList = new List<SecureShellInfo>()
        {
            new SecureShellInfo()
            {
                Host = "10.67.154.41",
                Port = 22,
                Credential = new SecureShellCredential
                {
                    UserName = "sysadmin",
                    Password = "superuser"
                }
            },
            new SecureShellInfo()
            {
                Host = "10.67.157.125",
                Port = 22,
                Credential = new SecureShellCredential
                {
                    UserName = "sysadmin",
                    Password = "superuser"
                }
            },
        };

        private static string curl_cmd_text =
            $"curl -k 'http://192.168.31.1/redfish/v1/Systems/UBB/LogServices/EventLog/Entries'";

        //private static GoSecureShellInfo clientInfo = new GoSecureShellInfo
        //{
        //    Host = "10.67.154.41",
        //    Port = 22,
        //    Credential = new GoSecureShellCredential
        //    {
        //        UserName = "sysadmin",
        //        Password = "superuser"
        //    }

        //};

        private static SecureShellInfo clientInfo2 = new SecureShellInfo
        {
            Host = "10.67.155.140",
            Port = 22,
            Credential = new SecureShellCredential
            {
                UserName = "amdtest",
                Password = "123123"
            }

        };


        [TestMethod]
        public void ShouldTunnelConnectionSucceed()
        {
            bool isClientConnect = false;

            foreach (var info in clientInfoList)
            {
                var tunnelConnection = RenciSecureShellTunnelConnection.CreateConnection(clientInfo2);

                try
                {
                    bool isDefaultConnected = tunnelConnection.IsDefaultConnected;

                    if (isDefaultConnected)
                    {
                        tunnelConnection.TunnelConnection(info).Connect();
                        isClientConnect = tunnelConnection.IsConnected;
                        string result = tunnelConnection.Client.RunCommand(curl_cmd_text).Result;
                        Console.WriteLine("Command result: " + result);
                    }
                    tunnelConnection.Disconnect();
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
        }
    }
}
