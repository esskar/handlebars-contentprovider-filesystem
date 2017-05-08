using System.Text;

namespace Handlebars.ContentProvider.FileSystem
{
    public class FileSystemConfiguration
    {
        public FileSystemConfiguration(string targetPath = null)
        {
            TargetPath = targetPath;
            FileNameExtension = ".hbs";
        }

        public string TargetPath { get; }

        public string FileNameExtension { get; set; }

        public string PartialsSubPath { get; set; }

        public Encoding FileEncoding { get; set; }
    }
}
