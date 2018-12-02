using MachineLearningSoftware.Common;
using MachineLearningSoftware.Controls.Entities;
using MachineLearningSoftware.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MachineLearningSoftware.Views.ViewModels
{
    [Export]
    public class TensorBoardViewModel : BaseViewModel
    {
        private Action _loadStart;
        private Action _loadEnd;
        private string _modelDirectory;
        private string _address;

        public ICommand NavigateToPageCommand
        {
            get { return new CommandDelegate(OnNavigateToPage, CanExecute); }
        }

        public ICommand StartTensorBoardCommand
        {
            get { return new CommandDelegate(OnStartTensorBoard, CanExecute); }
        }

        public ICommand StopTensorBoardCommand
        {
            get { return new CommandDelegate(OnStopTensorBoard, CanExecute); }
        }

        public string ModelDirectory
        {
            get { return _modelDirectory; }
            set
            {
                if (_modelDirectory != value)
                {
                    _modelDirectory = value;
                    OnPropertyChanged(nameof(ModelDirectory));
                }
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged(nameof(Address));
                }
            }
        }

        public Action LoadStart
        {
            get { return _loadStart; }
            set
            {
                if (_loadStart != value)
                {
                    _loadStart = value;
                    OnPropertyChanged(nameof(LoadStart));
                }
            }
        }

        public Action LoadEnd
        {
            get { return _loadEnd; }
            set
            {
                if (_loadEnd != value)
                {
                    _loadEnd = value;
                    OnPropertyChanged(nameof(LoadEnd));
                }
            }
        }

        public TensorBoardViewModel()
        {
            Address = "http://localhost:6006/";
            LoadStart = StartAction;
            LoadEnd = EndAction;
        }

        private void EndAction()
        {
            IsModalVisible = false;
        }

        private void StartAction()
        {
            IsModalVisible = true;
        }

        private void OnStartTensorBoard(object parameter)
        {
            if (!string.IsNullOrEmpty(ModelDirectory))
            {
                Task.Run(() =>
                {
                    OnStopTensorBoard(null);
                    var startCommand = $"tensorboard --logdir=\"{ModelDirectory}";
                    ExecuteCMDCommands.RunMultipleCommands(new List<string>() { startCommand, "echo" });
                });
            }
        }

        private void OnStopTensorBoard(object parameter)
        {
            foreach (var process in Process.GetProcessesByName("Tensorboard"))
            {
                process.Kill();
            }
        }

        private void OnNavigateToPage(object parameter)
        {
            if (parameter is Action<string> loadPage)
            {
                loadPage.Invoke(Address);
            }
        }
    }
}
