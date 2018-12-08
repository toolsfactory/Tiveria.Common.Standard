using System.IO;
using System.Security.Principal;
using System.Security.AccessControl;

namespace Tiveria.Common
{
    public class DirectoryUtils
    {
        public static bool IsDirectory(string path)
        {
            FileAttributes attr = File.GetAttributes(path);
            return ((attr & FileAttributes.Directory) == FileAttributes.Directory);
        }

        public static void CleanFolder(string path)
        {
            try
            {
                ForcedDeleteDirectory(path);
                System.IO.Directory.CreateDirectory(path);
            }
            catch { }
        }

        public static void ForcedDeleteDirectory(string path, bool recursive = true)
        {
            if (!System.IO.Directory.Exists(path))
                return;

            var dir = new System.IO.DirectoryInfo(path);
            RemoveReadOnlyFlags(dir, recursive);
            dir.Delete(recursive);
        }

        private static void RemoveReadOnlyFlags(System.IO.DirectoryInfo dir, bool recursive = true)
        {
            foreach (var file in dir.GetFiles())
                if (file.IsReadOnly)
                    file.IsReadOnly = false;

            if (recursive)
                RemoveReadOnlyFlagsInSubDirectories(dir, recursive);
        }

        private static void RemoveReadOnlyFlagsInSubDirectories(System.IO.DirectoryInfo dir, bool recursive)
        {
            foreach (var subdir in dir.GetDirectories())
                RemoveReadOnlyFlags(subdir, recursive);
        }   
    }
}
