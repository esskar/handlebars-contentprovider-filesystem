using System.IO;

namespace Handlebars.ContentProvider.FileSystem
{
    internal static class FileSystemUtil
    {
        public static string NormalizePath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            path = path.Replace(
                Path.DirectorySeparatorChar == '\\' ? '/' : '\\',
                Path.DirectorySeparatorChar);
            return path;
        }
    }
}
