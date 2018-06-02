using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ObjectRecognitionSoftware.Common
{
    public static class Python
    {
        private static string _windowsRegistryInstall = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
        private static string _python = "Python";

        public static bool IsPythonInstalled()
        {
            if (IsPythonInstalledForUserOrLocally(Registry.CurrentUser.OpenSubKey(_windowsRegistryInstall)))
            {
                return true;
            }
            else if (IsPythonInstalledForUserOrLocally(Registry.LocalMachine.OpenSubKey(_windowsRegistryInstall)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static StringBuilder GetCurrentPythonVersion()
        {
            var parentKey = Registry.LocalMachine.OpenSubKey(_windowsRegistryInstall);
            var nameList = parentKey.GetSubKeyNames();
            var pythonInstallations = new StringBuilder();

            for (int i = 0; i < nameList.Length; i++)
            {
                var regKey = parentKey.OpenSubKey(nameList[i]);
                try
                {
                    var registryDisplayName = (string)regKey.GetValue("DisplayName");
                    var installationPath = (string)regKey.GetValue("InstallSource");
                    var version = (string)regKey.GetValue("DisplayVersion");

                    if (!string.IsNullOrEmpty(registryDisplayName) && registryDisplayName.Contains(_python))
                    {
                        pythonInstallations.AppendLine(registryDisplayName);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLogging.LogException(ex.ToString());
                }
            }
            return pythonInstallations;
        }

        public static void InstallUpdateTensorFlow()
        {
            new Thread(() =>
            {
                ExecuteCMDCommands.RunMultipleCommands(
                new List<string>() { "pip install tensorflow", "pip install --upgrade tensorflow" }, true, true);
            }).Start();
        }

        public static string GetLatestPythonScriptsDirectory()
        {
            var versionStart = 2;
            var pythonDirectory = string.Empty;

            for (int a = 0; a < 2; a++)
            {
                for (int j = 0; j < 10; j++)
                {
                    string pythonVersion = string.Format("Python{0}{1}", versionStart, j);
                    string pythonPath = string.Format("{0}\\Programs\\Python\\{1}", CurrentDirectory.GetCurrentAppDataDirectory(), pythonVersion);
                    if (CurrentDirectory.DirectoryExists(pythonPath))
                    {
                        pythonDirectory = string.Format(@"{0}\{1}", pythonPath.Replace("\\", @"\"), "Scripts");
                    }
                }
                versionStart++;
            }
            return pythonDirectory;
        }
        
        private static bool IsPythonInstalledForUserOrLocally(RegistryKey parentKey)
        {
            var nameList = parentKey.GetSubKeyNames();

            for (int i = 0; i < nameList.Length; i++)
            {
                var regKey = parentKey.OpenSubKey(nameList[i]);
                try
                {
                    var registryDisplayName = (string)regKey.GetValue("DisplayName");
                    var installationPath = (string)regKey.GetValue("InstallSource");
                    var version = (string)regKey.GetValue("DisplayVersion");

                    if (!string.IsNullOrEmpty(registryDisplayName) && registryDisplayName.Contains(_python))
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLogging.LogException(ex.ToString());
                }
            }
            return false;
        }
    }
}
