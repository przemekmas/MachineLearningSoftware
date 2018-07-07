using MachineLearningSoftware.Common;
using MachineLearningSoftware.Entities;
using MachineLearningSoftware.ViewModels;
using MachineLearningSoftware.Views.DialogBoxes;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace MachineLearningSoftware.Views.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RetrainInceptionModelViewModel : BaseViewModel
    {
        #region Fields

        private StringBuilder _textLogBuilder = new StringBuilder();
        private string _textBoxLog;
        public string imageDirectory;
        
        #endregion
        
        #region Properties

        public string TextBoxLog
        {
            get { return _textBoxLog; }
            set
            {
                _textBoxLog = value;
                OnPropertyChanged(nameof(TextBoxLog));
            }
        }
        
        public ICommand OpenHelpDialog
        {
            get { return new CommandDelegate(DisplayHelpDialog, CanExecute); }
        }

        public bool IsPythonInstalled
        {
            get { return Python.IsPythonInstalled(); }
        }

        #endregion

        #region Constructor

        public RetrainInceptionModelViewModel()
        {
            ConfigureHeaderControl(true, true, Properties.RetrainInceptionModelResource.Title,
                true, Properties.RetrainInceptionModelResource.MainInformation);
            DisplayPythonVersion();
            ExecuteCMDCommands.outputHandler = OutputHandler;
        }
        
        #endregion

        #region Public Methods

        public void InstallRequiredPackages()
        {       
            Python.InstallUpdateTensorFlow();
        }

        public void RetrainInceptionModel()
        {
            if (!string.IsNullOrEmpty(imageDirectory))
            {
                new Thread(() =>
                {
                    var assetsFolder = CurrentDirectory.GetPythonAssetsDirectory("RetrainInceptionModel");
                    var commands = new List<string>() { string.Format("cd {0}", assetsFolder),
                        string.Format(@"python Retrain.py --output_graph=retrained_graph.pb --output_labels=retrained_labels.txt --image_dir={0}", imageDirectory) };

                    ExecuteCMDCommands.RunMultipleCommands(commands, redirectOutput: true);
                }).Start();
            }
        }

        #endregion

        #region Private Methods
             
        private void OutputHandler(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
            {
                TextBoxLog += string.Format("{0} \n", e.Data);
            }
        }

        private void DisplayPythonVersion()
        {
            TextBoxLog += Python.GetCurrentPythonVersion();
        }

        private void DisplayHelpDialog(object obj)
        {
            new InceptionModelHelpDialog().ShowDialog();
        }

        #endregion
    }
}
