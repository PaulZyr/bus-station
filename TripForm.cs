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
    public partial class TripForm : Form
    {
        public TripForm(BusTrip busTrip)
        {
            InitializeComponent();
            BusTrip = busTrip;
        }
        public BusTrip BusTrip { get; set; }

        private void TripForm_Load(object sender, EventArgs e)
        {
            busNumberTextBox.Text = BusTrip.BusNumber;
            destinationTextBox.Text = BusTrip.Destination;
            depDateTimePicker.Value = BusTrip.Departure;
            depHourNumericUpDown.Value = BusTrip.Departure.Hour;
            depMinNumericUpDown.Value = BusTrip.Departure.Minute;
            arrDateTimePicker.Value = BusTrip.Arrivel;
            arrHourNumericUpDown.Value = BusTrip.Arrivel.Hour;
            arrMinNumericUpDown.Value = BusTrip.Arrivel.Minute;
            busTypeComboBox.SelectedIndex = (int)BusTrip.BusType;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if(CheckInputs())
            {
                BusTrip.BusNumber = busNumberTextBox.Text;
                BusTrip.Destination = destinationTextBox.Text;
                BusTrip.Departure = new DateTime(depDateTimePicker.Value.Year, depDateTimePicker.Value.Month,
                    depDateTimePicker.Value.Day, (int)depHourNumericUpDown.Value, (int)depMinNumericUpDown.Value, 0);
                BusTrip.Arrivel = new DateTime(arrDateTimePicker.Value.Year, arrDateTimePicker.Value.Month,
                    arrDateTimePicker.Value.Day, (int)arrHourNumericUpDown.Value, (int)arrMinNumericUpDown.Value, 0);
                BusTrip.Created = DateTime.Now;
                if (busTypeComboBox.SelectedIndex == 0) BusTrip.BusType = BusType.Big;
                else if (busTypeComboBox.SelectedIndex == 1) BusTrip.BusType = BusType.Medium;
                else BusTrip.BusType = BusType.Small;
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
            if (busNumberTextBox.Text == "")
            {
                MessageBox.Show("Check Bus Number");
                return false;
            }
            if (destinationTextBox.Text == "")
            {
                MessageBox.Show("Check Destination");
                return false;
            }
            if (depDateTimePicker.Value < DateTime.Now)
            {
                MessageBox.Show("Check Departure Date");
                return false;
            }
            if (arrDateTimePicker.Value < DateTime.Now || arrDateTimePicker.Value < depDateTimePicker.Value)
            {
                MessageBox.Show("Check Arrivel Date");
                return false;
            }

            return true;
        }

        
    }
}
