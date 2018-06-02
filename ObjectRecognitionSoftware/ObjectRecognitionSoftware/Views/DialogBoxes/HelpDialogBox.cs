using System.Windows.Forms;
using System.Reflection;

namespace ObjectRecognitionSoftware.Views.DialogBoxes
{
    public partial class HelpDialogBox: Form
    {
        public HelpDialogBox()
        {
            InitializeComponent();
            SetSoftwareVersion();
        }

        private void SetSoftwareVersion()
        {
            VersionLabel1.Text = string.Format("Software version {0}", GetSoftwareVersion());
        }

        private string GetSoftwareVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void OkButton1_Click(object sender, System.EventArgs e)
        {
            Dispose();
            Close();
        }
    }
}
