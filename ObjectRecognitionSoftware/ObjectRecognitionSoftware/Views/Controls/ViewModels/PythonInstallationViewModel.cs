using ObjectRecognitionSoftware.Common;
using ObjectRecognitionSoftware.Entities;

namespace ObjectRecognitionSoftware.Views.Controls.ViewModels
{
    public class PythonInstallationViewModel : NotifyPropertyChanged
    {
        private bool _pythonInstalled;
        private string _pythonInstallationLabel;

        public string PythonInstallationLabel
        {
            get { return _pythonInstallationLabel; }
            set
            {
                _pythonInstallationLabel = value;
                OnPropertyChanged(nameof(PythonInstallationLabel));
            }
        }

        public bool PythonInstalled
        {
            get { return _pythonInstalled; }
            set
            {
                _pythonInstalled = value;
                OnPropertyChanged(nameof(PythonInstalled));
            }
        }

        public PythonInstallationViewModel()
        {
            PythonInstalled = Python.IsPythonInstalled();
            if (PythonInstalled)
            {
                PythonInstallationLabel = "Python is installed";
            }
            else
            {
                PythonInstallationLabel = "Python is not installed";
            }
        }
    }
}