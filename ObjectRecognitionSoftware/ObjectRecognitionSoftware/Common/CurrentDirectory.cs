using System;
using System.IO;
using System.IO.Compression;

namespace ObjectRecognitionSoftware.Common
{
    public static class CurrentDirectory
    {
        public static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        public static string GetPythonAssetsDirectory(string folder)
        {
            var currentPath = Directory.GetCurrentDirectory();
            return Path.GetFullPath(Path.Combine(currentPath, string.Format(@"Assets\PythonAssets\{0}", folder)));
        }

        public static string GetPythonAssetsDirectory()
        {
            var currentPath = Directory.GetCurrentDirectory();
            return Path.GetFullPath(Path.Combine(currentPath, @"\Assets\PythonAssets"));
        }

        public static string GetCurrentAppDataDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        public static string NavigateToDirectory(string directory)
        {
            var currentPath = Directory.GetCurrentDirectory();
            var newPath = Path.GetFullPath(Path.Combine(currentPath, directory));
            return newPath;
        }

        public static void CreateNewDirectory(string directory)
        {
            Directory.CreateDirectory(directory);
        }

        public static bool DirectoryExists(string directory)
        {
            return Directory.Exists(directory);
        }

        // Extracting files only works with .NET Framework 4.5 and above
        public static void ExtractFiles(string fileDir, string extractDir)
        {
            ZipFile.ExtractToDirectory(fileDir, extractDir);
        }
    }
}
