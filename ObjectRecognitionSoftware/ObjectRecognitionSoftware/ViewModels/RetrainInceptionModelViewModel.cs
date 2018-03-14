using Microsoft.Win32;
using ObjectRecognitionSoftware.Common;
using ObjectRecognitionSoftware.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ObjectRecognitionSoftware.ViewModels
{
    public class RetrainInceptionModelViewModel : NotifyPropertyChanged
    {
        #region Fields

        private StringBuilder textLogBuilder = new StringBuilder();
        private string m_TextBoxLog;
        private bool m_PythonInstalled;
        private bool m_IsModalVisible;
        private string m_PythonInstallationLabel;

        #endregion

        public string imageDirectory;

        #region Properties

        public string TextBoxLog
        {
            get { return m_TextBoxLog; }
            set
            {
                m_TextBoxLog = value;
                OnPropertyChanged(nameof(TextBoxLog));
            }
        }

        public string PythonInstallationLabel
        {
            get { return m_PythonInstallationLabel; }
            set
            {
                m_PythonInstallationLabel = value;
                OnPropertyChanged(nameof(PythonInstallationLabel));
            }
        }

        public bool PythonInstalled
        {
            get { return m_PythonInstalled; }
            set
            {
                m_PythonInstalled = value;
                OnPropertyChanged(nameof(PythonInstalled)); 
            }
        }

        public bool IsModalVisible
        {
            get { return m_IsModalVisible; }
            set
            {
                m_IsModalVisible = value;
                OnPropertyChanged(nameof(IsModalVisible));
            }
        }

        #endregion

        #region Constructor

        public RetrainInceptionModelViewModel()
        {
            IsPythonInstalled();
            ExecuteCMDCommands.outputHandler = OutputHandler;
        }

        #endregion

        #region Public Methods

        public void InstallRequiredPackages()
        {
            //Need to set the path variable for python pip
            SetPythonEnvironmentPath();
            InstallUpdateTensorFlow();
        }

        public void BuildTensorFlow()
        {
            if (!string.IsNullOrEmpty(imageDirectory))
            {
                new Thread(() =>
                {
                    var assetsFolder = CurrentDirectory.GetPythonDirectory();
                    var commands = new List<string>() { string.Format("cd {0}", assetsFolder),
                        string.Format(@"python Retrain.py --output_graph=retrained_graph.pb --output_labels=retrained_labels.txt --image_dir={0}", imageDirectory) };

                    ExecuteCMDCommands.RunMultipleCommands(commands);
                }).Start();
            }
        }

        #endregion

        #region Private Methods

        private void IsPythonInstalled()
        {
            var name = "Python";
            var parentKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");
            var nameList = parentKey.GetSubKeyNames();
            
            for (int i = 0; i < nameList.Length; i++)
            {
                var regKey = parentKey.OpenSubKey(nameList[i]);
                try
                {
                    var registryDisplayName = (string)regKey.GetValue("DisplayName");
                    var installationPath = (string)regKey.GetValue("InstallSource");
                    var version = (string)regKey.GetValue("DisplayVersion");
                    
                    if (!string.IsNullOrEmpty(registryDisplayName) && registryDisplayName.Contains(name))
                    {                        
                        textLogBuilder.AppendLine(registryDisplayName);
                        PythonInstalled = true;
                    }                
                }
                catch (Exception ex)
                {
                    ExceptionLogging.LogException(ex.ToString());
                }
            }

            if (!PythonInstalled)
            {
                textLogBuilder.AppendLine("Python is not installed please install python");
                PythonInstallationLabel = "Python is not installed";
            }
            else
            {
                PythonInstallationLabel = "Python is installed";
            }

            TextBoxLog += textLogBuilder.ToString();
        }
                
        private void SetPythonEnvironmentPath()
        {
            string pythonDirectory = CurrentDirectory.GetCurrentAppDataDirectory();

        }
        
        private void InstallUpdateTensorFlow()
        {
            new Thread(() =>
            {
                ExecuteCMDCommands.RunMultipleCommands(
                new List<string>() { "pip install tensorflow", "pip install --upgrade tensorflow" });            
            }).Start();
        }
       
        private string GetCurrentWindowsDirectory()
        {
            return ExecuteCMDCommands.GetCommandOutput("echo %CD:~0,3%");
        }
        
        private void OutputHandler(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
            {
                TextBoxLog += string.Format("{0} \n", e.Data);
            }
        }

        #endregion
    }
}
