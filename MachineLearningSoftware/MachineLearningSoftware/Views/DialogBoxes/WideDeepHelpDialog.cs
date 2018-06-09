using System;
using System.Windows.Forms;

namespace MachineLearningSoftware.Views.DialogBoxes
{
    public partial class WideDeepHelpDialog : Form
    {
        public WideDeepHelpDialog()
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
