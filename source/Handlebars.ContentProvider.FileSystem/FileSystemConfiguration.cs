using System.Text;

namespace Handlebars.ContentProvider.FileSystem
{
    public class FileSystemConfiguration
    {
        private string _partialsSubPath;

        public FileSystemConfiguration(string targetPath = null)
        {
            TargetPath = FileSystemUtil.NormalizePath(targetPath);
            FileNameExtension = ".hbs";
        }

        public string TargetPath { get; }

        public string FileNameExtension { get; set; }

        public string PartialsSubPath
        {
            get { return _partialsSubPath; }
            set { _partialsSubPath = FileSystemUtil.NormalizePath(value); }
        }

        public Encoding FileEncoding { get; set; }
    }
}
