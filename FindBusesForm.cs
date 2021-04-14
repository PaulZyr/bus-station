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
    public partial class FindBusesForm : Form
    {
        public FindBusesForm()
        {
            InitializeComponent();
        }
        public DateTime _seekDate;
        public string _seekDestination;
        private void okButton_Click(object sender, EventArgs e)
        {
            if (checkInputs())
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else MessageBox.Show("Wrong Date and Time");
        }
        private bool checkInputs()
        {
            if (destTextBox.Text == "") return false;
            _seekDestination = destTextBox.Text;
            _seekDate = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month,
                dateTimePicker.Value.Day, (int)hourNumericUpDown.Value, (int)minNumericUpDown.Value, 0);
            return true;
        }

        private void FindBusesForm_Load(object sender, EventArgs e)
        {
            dateTimePicker.Value = DateTime.Now;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
