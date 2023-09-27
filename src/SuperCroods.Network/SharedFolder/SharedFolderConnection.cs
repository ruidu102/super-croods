using System.Runtime.InteropServices;

namespace SuperCroods.Network.SharedFolder
{
    public class SharedFolderConnection : ISharedFolderConnection
    {
        private SharedFolderInfo folder;

        private SharedFolderConnection(SharedFolderInfo arg)
        {
            folder = arg;
        }

        public static SharedFolderConnection CreateConnection(SharedFolderInfo arg)
        {
            return new SharedFolderConnection(arg);
        }

        public void Connect()
        {
            InternalConnect();
        }

        public void Disconnect()
        {
            InternalDisconnect();
        }

        private void InternalConnect()
        {
            var netResource = new NetworkResource
            {
                Scope = ResourceScope.GlobalNetwork,
                Type = ResourceType.Disk,
                DisplayType = ResourceDisplayType.Share,
                RemoteName = folder.Path
            };

            var result = WNetAddConnection2(
                netResource,
                folder.Credential.Password,
                folder.Credential.UserName,
                0
                );
        }

        private void InternalDisconnect()
        {
            var result = WNetCancelConnection2(
                folder.Path, (int)ResourceType.Disk, true);
        }

        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(
            NetworkResource resource,
            string password, string username, int flag);

        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(
            string path, int flag, bool force);

    }
}
