using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Home0326_7_1
{
    public partial class FindBusNumberForm : Form
    {
        public FindBusNumberForm()
        {
            InitializeComponent();
        }
        public string BusNumber { get; set; }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (CheckInputs())
            {
                BusNumber = numTextBox.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private bool CheckInputs()
        {
            if (numTextBox.Text == "")
            {
                MessageBox.Show("Input Bus Number");
                return false;
            }
            return true;
        }
    }
}
