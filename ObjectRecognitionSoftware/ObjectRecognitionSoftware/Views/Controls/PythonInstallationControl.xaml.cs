using ObjectRecognitionSoftware.Views.Controls.ViewModels;
using System.Windows.Controls;

namespace ObjectRecognitionSoftware.Views.Controls
{
    /// <summary>
    /// Interaction logic for PythonInstallationControl.xaml
    /// </summary>
    public partial class PythonInstallationControl : UserControl
    {
        public PythonInstallationControl()
        {
            InitializeComponent();
            this.DataContext = new PythonInstallationViewModel();
        }
    }
}
