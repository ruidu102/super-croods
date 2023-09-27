using System;
using System.IO;
using System.Security.Cryptography;

namespace SuperCroods.Skeleton.IO
{
    public static class CustomFile
    {
        // Refactoring
        public static bool Exists(string path)
        {
            return File.Exists(path);
        }

        public static void CreateFor(string path)
        {
            if (!Exists(path))
            {
                var f = File.Create(path);
                f.Close();
            }
        }

        public static void CopyTo(string src, string det, bool overwrite = true)
        {
            File.Copy(src, det, overwrite);
        }

        public static void WriteTo(string path, string content, bool isCleanUp = false)
        {
            if (isCleanUp)
            {
                File.WriteAllText(path, String.Empty);
                File.WriteAllText(path, content);
            }
            else
                File.AppendAllText(path, content);
        }

        public static void WriteTo(string path, Stream content)
        {
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                //content.Seek(0, SeekOrigin.End);
                content.Position = 0;
                content.CopyTo(stream);
            }
        }

        public static string ReadFrom(string path)
        {
            return File.ReadAllText(path);
        }

        public static string CalculateMd5Checksum(string path)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(path))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "");
                }
            }
        }
    }
}
