using System;
using System.Windows.Forms;

namespace ObjectRecognitionSoftware.Views.DialogBoxes
{
    public partial class InceptionModelHelpDialog : Form
    {
        public InceptionModelHelpDialog()
        {
            InitializeComponent();
        }

        private void CloseButton1_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }
    }
}
