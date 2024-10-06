using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperCroods.Network.SecureFTP;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace SuperCroods.Network.Test.SecureFTP
{
    [TestClass]
    public class RenciSecureFTPTunnelConnectionTests
    {

        private static List<SecureFTPInfo> clientInfoList = new List<SecureFTPInfo>()
        {
            new SecureFTPInfo()
            {
                Host = "10.67.154.41",
                Port = 22,
                Credential = new SecureFTPCredential
                {
                    UserName = "sysadmin",
                    Password = "superuser"
                }
            },
            new SecureFTPInfo()
            {
                Host = "10.67.157.125",
                Port = 22,
                Credential = new SecureFTPCredential
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

        private static SecureFTPInfo clientInfo2 = new SecureFTPInfo
        {
            Host = "10.67.155.140",
            Port = 22,
            Credential = new SecureFTPCredential
            {
                UserName = "amdtest",
                Password = "123123"
            }

        };

        [TestMethod]
        public void ShouldDownloadFile2JumpStation()
        {

            foreach (var info in clientInfoList)
            {
                var tunnelConnection = RenciSecureFTPTunnelConnection.CreateConnection(clientInfo2);

                if (tunnelConnection.IsDefaultConnected)
                {
                    tunnelConnection.TunnelConnection(info).Connect();

                    var files = tunnelConnection.Client.ListDirectory("/var/tmp");
                    files.ToList().ForEach(d => Console.WriteLine(d.FullName));
                    var expectedFileFullName = files.ToList().Find(f => f.Name.Contains("qsession-c6bd592cf60b2bca83KbAtU2zhjcOd.expire")).FullName;
                    var expectedFileName = files.ToList().Find(f => f.Name.Contains("qsession-c6bd592cf60b2bca83KbAtU2zhjcOd.expire")).Name;

                    byte[] content = new byte[0];
                    MemoryStream output = new MemoryStream();
                    tunnelConnection.Client.DownloadFile(expectedFileFullName, output);
                    output.Position = 0;
                    content = output.ToArray();
                    output.Dispose();

                    string filePath = Path.Combine(Path.GetTempPath(), expectedFileName);
                    var result = new MemoryStream(content);
                    using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        //content.Seek(0, SeekOrigin.End);
                        result.Position = 0;
                        result.CopyTo(stream);
                    }
                }
                tunnelConnection.Disconnect();
            }
        }
    }
}
