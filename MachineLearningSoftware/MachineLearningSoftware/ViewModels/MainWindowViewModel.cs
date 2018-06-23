using MachineLearningSoftware.Common;
using MachineLearningSoftware.Entities;
using MachineLearningSoftware.Views.DialogBoxes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Input;

namespace MachineLearningSoftware.ViewModels
{
    [Export]
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields

        private HelpDialogBox _helpDialog;
        private CloseSoftwareDialog _closeDialog;
        private MainWindowFunctions _mainWindowFunctions;

        #endregion

        #region Properties

        public ICollection<IResourceItemEntity> ResourceItems { get; }

        public ICommand DisplayHelpDialogCommand
        {
            get { return new CommandDelegate(DisplayHelpDialog, CanExecute); }
        }

        public ICommand DisplayExitDialogCommand
        {
            get { return new CommandDelegate(DisplayExitDialog, CanExecute); }
        }
        
        public ICommand DisplayTensorFlowWebsiteCommand
        {
            get { return new CommandDelegate(NavigateToTensorFlow, CanExecute); }
        }

        public ICommand DisplayPythonWebsiteCommand
        {
            get { return new CommandDelegate(NavigateToPython, CanExecute); }
        }

        #endregion

        #region Constructor

        [ImportingConstructor]
        public MainWindowViewModel(MainWindowFunctions mainWindowFunctions)
        {
            ResourceItems = new ObservableCollection<IResourceItemEntity>();
            _mainWindowFunctions = mainWindowFunctions;
        }

        #endregion

        #region Public Methods

        public void SetTabControl(TabControl tabControl)
        {
            _mainWindowFunctions.TabControl = tabControl;
        }

        public void SetMainMenu(ListBox mainMenu)
        {
            _mainWindowFunctions.LoadPanels(mainMenu);
        }

        public void OpenPage(string page)
        {
            _mainWindowFunctions.OpenPage(page);
        }

        #endregion

        #region Private Methods

        private void DisplayHelpDialog(object context)
        {
            _helpDialog = new HelpDialogBox();
            _helpDialog.ShowDialog();
        }

        private void DisplayExitDialog(object context)
        {
            _closeDialog = new CloseSoftwareDialog();
            _closeDialog.ShowDialog();
        }

        private void NavigateToTensorFlow(object context)
        {
            HyperlinkNavigation.NavigateTo("https://www.tensorflow.org/");
        }
        
        private void NavigateToPython(object context)
        {
            HyperlinkNavigation.NavigateTo("https://www.python.org/");
        }

        #endregion
    }
}
