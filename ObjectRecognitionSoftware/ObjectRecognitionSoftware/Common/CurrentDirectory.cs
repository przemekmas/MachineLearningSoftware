using System;
using System.IO;

namespace ObjectRecognitionSoftware.Common
{
    public static class CurrentDirectory
    {
        public static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        public static string GetPythonDirectory()
        {
            var currentPath = Directory.GetCurrentDirectory();
            var newPath = Path.GetFullPath(Path.Combine(currentPath, @"..\..\Assets\PythonAssets"));
            return newPath;
        }

        public static string GetCurrentAppDataDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
    }
}
