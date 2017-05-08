using System;
using System.IO;
using System.Linq;
using Handlebars.Core;

namespace Handlebars.ContentProvider.FileSystem
{
    public class FileSystemTemplateContentProvider : IHandlebarsTemplateContentProvider
    {
        public FileSystemTemplateContentProvider(string targetPath = null)
            : this(new FileSystemConfiguration(targetPath)) { }

        public FileSystemTemplateContentProvider(FileSystemConfiguration fileSystemConfiguration)
        {
            if (fileSystemConfiguration == null)
                throw new ArgumentNullException(nameof(fileSystemConfiguration));
            FileSystemConfiguration = fileSystemConfiguration;
        }

        public FileSystemConfiguration FileSystemConfiguration { get; }

        public string GetTemplateContent(string templateName, string parentTemplateName)
        {
            var fileName = FileSystemUtil.NormalizePath(templateName);
            if (!fileName.EndsWith(FileSystemConfiguration.FileNameExtension, StringComparison.OrdinalIgnoreCase))
                fileName += FileSystemConfiguration.FileNameExtension;

            string fullFileName;
            if (!string.IsNullOrWhiteSpace(parentTemplateName))
            {
                parentTemplateName = FileSystemUtil.NormalizePath(parentTemplateName);
                fullFileName = Closest(parentTemplateName, fileName);
                if (fullFileName == null && !string.IsNullOrWhiteSpace(FileSystemConfiguration.PartialsSubPath))
                {
                    var partialsFileName = Path.Combine(FileSystemConfiguration.PartialsSubPath, fileName);
                    fullFileName = Closest(parentTemplateName, partialsFileName);
                }
            }
            else if (!string.IsNullOrWhiteSpace(FileSystemConfiguration.TargetPath))
            {
                fullFileName = Path.Combine(FileSystemConfiguration.TargetPath, fileName);
            }
            else
            {
                fullFileName = fileName;
            }

            if (string.IsNullOrWhiteSpace(fullFileName))
                return null;
            return FileSystemConfiguration.FileEncoding != null 
                ? File.ReadAllText(fullFileName, FileSystemConfiguration.FileEncoding) 
                : File.ReadAllText(fullFileName);
        }

        private static string Closest(string fileName, string otherFileName)
        {
            var dir = GetDir(fileName);
            while (dir != null)
            {
                var fullFileName = Path.Combine(dir, otherFileName);
                if (File.Exists(fullFileName))
                    return fullFileName;
                dir = GetDir(dir);
            }
            return null;
        }

        private static string GetDir(string currentFilePath)
        {
            if (string.IsNullOrWhiteSpace(currentFilePath))
                return null;

            var parts = currentFilePath.Split(Path.DirectorySeparatorChar);
            return parts.Length == 1
                ? ""
                : string.Join(Path.DirectorySeparatorChar.ToString(), parts.Take(parts.Length - 1));
        }
    }
}