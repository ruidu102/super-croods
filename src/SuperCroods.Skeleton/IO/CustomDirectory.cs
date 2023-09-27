using System;
using System.IO;
using System.Threading;

namespace SuperCroods.Skeleton.IO
{
    public static class CustomDirectory
    {
        // Refactoring
        public static bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public static void CreateDirectoryFor(string path)
        {
            if (!Exists(path))
                Directory.CreateDirectory(path);
        }

        public static void Delete(string path)
        {
            try
            {
                if (Exists(path))
                    Directory.Delete(path);
            }
            catch (IOException)
            {
                Directory.Delete(path);
            }
            catch (UnauthorizedAccessException)
            {
                Thread.Sleep(0);
                Directory.Delete(path);
            }
        }

        public static void DeleteAll(string path)
        {
            try
            {
                if (Exists(path))
                    Directory.Delete(path, true);
            }
            catch (IOException)
            {
                Directory.Delete(path, true);
            }
            catch (UnauthorizedAccessException)
            {
                Thread.Sleep(10);
                Directory.Delete(path, true);
            }
        }

        public static string GetDirectoryFrom(string path)
        {
            return Path.GetDirectoryName(path);
        }

        public static string GetAssemblyDirectory =>
            Path.GetDirectoryName(
                System.Reflection
                .Assembly
                .GetEntryAssembly()
                .Location);
    }
}
