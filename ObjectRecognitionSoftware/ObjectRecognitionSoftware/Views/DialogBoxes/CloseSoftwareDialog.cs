using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjectRecognitionSoftware.Views.DialogBoxes
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
            this.Dispose();
            this.Close();
        }
    }
}
