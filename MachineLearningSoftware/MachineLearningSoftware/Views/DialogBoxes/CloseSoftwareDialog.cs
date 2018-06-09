using System;
using System.Windows.Forms;

namespace MachineLearningSoftware.Views.DialogBoxes
{
    public partial class CloseSoftwareDialog : Form
    {
        public CloseSoftwareDialog()
        {
            InitializeComponent();
        }

        private void YesButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NoButton1_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }
    }
}
