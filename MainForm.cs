using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Home0326_7_1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        List<BusTrip> _busTrips = new List<BusTrip>();
        List<BusTrip> _copied = new List<BusTrip>();
        private string _path = "tripBase.json";

        #region Load Show
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(_path))
            {
                try
                {
                    string str = File.ReadAllText(_path);
                    List<BusTrip> tmpStrips = new List<BusTrip>();
                    tmpStrips = JsonConvert.DeserializeObject<List<BusTrip>>(str);
                    if (tmpStrips.Count > 0)
                    {
                        _busTrips = tmpStrips;
                        ShowTrips();
                        return;
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            else
            {
                try
                {
                    File.Create(_path);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        public void ShowTrips()
        {
            mainTripsListView.Items.Clear();
            foreach(var trip in _busTrips)
            {
                AddTripListView(trip);
            }
            
        }
        private void showAllToolStripButton_Click(object sender, EventArgs e)
        {
            ShowTrips();
        }
        private void showAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTrips();
        }
        private void AddTripListView(BusTrip trip)
        {
            ListViewItem item = new ListViewItem(new string[]
            {
                trip.BusNumber, trip.Destination,
                $"{trip.Departure.Year}.{trip.Departure.Month:00}.{trip.Departure.Day:00}",
                $"{trip.Departure.Hour:00}:{trip.Departure.Minute:00}",
                $"{trip.Arrivel.Year}.{trip.Arrivel.Month:00}.{trip.Arrivel.Day:00}",
                $"{trip.Arrivel.Hour:00}:{trip.Arrivel.Minute:00}", trip.BusType.ToString()
            });
            item.Tag = trip.TripCode;
            mainTripsListView.Items.Add(item);
        }
        private BusTrip GetTripByTripCode(string str)
        {
            foreach(var trip in _busTrips)
            {
                if (str == trip.TripCode.ToString())
                {
                    return trip;
                }
            }
            return null;
        }

        #endregion

        #region New Edit Delete

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            CreateNewTrip();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewTrip();
        }
        private void newContext_Click(object sender, EventArgs e)
        {
            CreateNewTrip();
        }

        private void CreateNewTrip()
        {
            TripForm form = new TripForm(new BusTrip());
            var res = form.ShowDialog();
            if (res == DialogResult.OK)
            {
                _busTrips.Add(form.BusTrip);
            }
            ShowTrips();
        }

        private void editTripToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EditTrip();
        }

        private void editToolStripButton_Click(object sender, EventArgs e)
        {
            EditTrip();
        }
        private void editContext_Click(object sender, EventArgs e)
        {
            EditTrip();
        }

        private void EditTrip()
        {
            if (mainTripsListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Choose line on Bus Number to edit Trip");
            }
            else if (mainTripsListView.SelectedItems.Count > 1)
            {
                MessageBox.Show("Choose ONE only trip to edit");
            }
            else
            {
                BusTrip trip = GetTripByTripCode(mainTripsListView.SelectedItems[0].Tag.ToString());
                TripForm form = new TripForm(trip);
                var res = form.ShowDialog();
                if(res == DialogResult.OK)
                {
                    trip = form.BusTrip;
                    ShowTrips();
                }
            }
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void deleteContext_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void Delete()
        {
            CopyTrip();
            var result = MessageBox.Show($"Delete {_copied.Count} trips?", "Deleting", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Remove();
            }
        }
        private void Remove()
        {
            foreach (var trip in _copied)
            {
                _busTrips.Remove(trip);
            }
        }

        private void newListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newTripList();
        }
        private void newTripList()
        {
            List<BusTrip> newList = new List<BusTrip>();
            
            string str = JsonConvert.SerializeObject(newList);
            string path = SaveAs(str);
            if (path != "")
            {
                _busTrips = newList;
                _path = path;
                ShowTrips();
            }
        }
        #endregion

        #region Copy Cut Paste
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyTrip();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            CopyTrip();
        }

        private void copyContext_Click(object sender, EventArgs e)
        {
            CopyTrip();
        }

        private void CopyTrip()
        {
            _copied.Clear();
            foreach (ListViewItem item in mainTripsListView.SelectedItems)
            {
                _copied.Add(GetTripByTripCode(item.Tag.ToString()));
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutTrips();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            CutTrips();
        }

        private void cutContext_Click(object sender, EventArgs e)
        {
            CutTrips();
        }
        private void CutTrips()
        {
            CopyTrip();
            Remove();
            ShowTrips();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            PasteTrips();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteTrips();
        }

        private void pasteContext_Click(object sender, EventArgs e)
        {
            PasteTrips();
        }
        private void PasteTrips()
        {
            foreach (var trip in _copied)
            {
                _busTrips.Add(BusTrip.DeepCopy(trip));
            }
            ShowTrips();
        }
        #endregion

        #region Open Save
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenTripsFile();
        }
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenTripsFile();
        }

        private void loadAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenTripsFile();
        }
        private void OpenTripsFile()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Json Files(*.json)|*.json|All files(*.*)|*.*";
            string str;
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    str = File.ReadAllText(open.FileName);
                    List<BusTrip> tmpStrips = new List<BusTrip>();
                    tmpStrips = JsonConvert.DeserializeObject<List<BusTrip>>(str);
                    if (tmpStrips.Count > 0)
                    {
                        _busTrips = tmpStrips;
                        ShowTrips();
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }
        private void addToolStripButton_Click(object sender, EventArgs e)
        {
            AddTripsFromFile();
        }

        private void loadContext_Click(object sender, EventArgs e)
        {
            AddTripsFromFile();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTripsFromFile();
        }
        private void AddTripsFromFile()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Json Files(*.json)|*.json|All files(*.*)|*.*";
            string str;
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    str = File.ReadAllText(open.FileName);
                    List<BusTrip> tmpStrips = new List<BusTrip>();
                    tmpStrips = JsonConvert.DeserializeObject<List<BusTrip>>(str);
                    if (tmpStrips.Count > 0)
                    {
                        _busTrips.AddRange(tmpStrips);
                        ShowTrips();
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveContext_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void Save()
        {
            string str = JsonConvert.SerializeObject(_busTrips); 
            File.WriteAllText(_path, str);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsAll();
        }
        private void saveAsContext_Click(object sender, EventArgs e)
        {
            SaveAsAll();
        }

        private void SaveAsAll()
        {
            string str = JsonConvert.SerializeObject(_busTrips);
            SaveAs(str);
        }
        private void saveSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsSelected();
        }
        private void saveSelectedContext_Click(object sender, EventArgs e)
        {
            SaveAsSelected();
        }

        private void SaveAsSelected()
        {
            CopyTrip();
            string str = JsonConvert.SerializeObject(_copied);
            SaveAs(str);
        }

        private string SaveAs(string str)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Json Files(*.json)|*.json|All files(*.*)|*.*";
            save.FileName = "newTripBase.json";
            if (save.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(save.FileName, str);
                return save.FileName;
            }
            return "";
        }

        #endregion

        #region Hot Keys
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyValue == 'S')
            {
                CreateNewTrip();
            }
            else if (e.Control && e.KeyValue == 'S')
            {
                Save();
            }
            else if (e.Control && e.KeyValue == 'N')
            {
                CreateNewTrip();
            }
            else if (e.Control && e.KeyValue == 'P')
            {
                PrintTrips();
            }
            else if (e.Control && e.KeyValue == 'C')
            {
                CopyTrip();
            }
            else if (e.Control && e.KeyValue == 'V')
            {
                PasteTrips();
            }
            else if (e.Control && e.KeyValue == 'X')
            {
                CutTrips();
            }
            else if (e.Control && e.KeyValue == 'F')
            {
                FindTrips();
            }
            else if (e.Control && e.KeyValue == 'W')
            {
                ShowTrips();
            }
            else if (e.Control && e.KeyValue == 'A')
            {
                SelectAll();
            }
        }
        #endregion

        #region Find
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAll();
        }
        private void SelectAll()
        {
            var n = mainTripsListView.Items.Count;
            for (var i = 0; i < n; ++i)
            {
                mainTripsListView.Items[i].Selected = true;
                mainTripsListView.Items[i].Focused = true;
            }
        }
        private void findTripsToolStripButton_Click(object sender, EventArgs e)
        {
            FindTrips();
        }
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindTrips();
        }

        private void FindTrips()
        {
            FindBusesForm form = new FindBusesForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                SeekTrips(form._seekDate, form._seekDestination);
            }
        }

        private void SeekTrips(DateTime seekDate, string destination)
        {
            mainTripsListView.Items.Clear();
            foreach (var trip in _busTrips)
            {
                TimeSpan spArrivel = seekDate - trip.Arrivel;
                TimeSpan spDeparture = trip.Departure - DateTime.Now;
                if ((destination == "all" || destination == trip.Destination)
                    && spArrivel.TotalMinutes >= 0 && spDeparture.TotalMinutes >= 0)
                {
                    AddTripListView(trip);
                }
            }
        }
        private void findByBusNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindBusNumberForm form = new FindBusNumberForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                string num = form.BusNumber;
                mainTripsListView.Items.Clear();
                foreach (var trip in _busTrips)
                {
                    if(trip.BusNumber == num)
                    {
                        AddTripListView(trip);
                    }
                }
            }
        }
        private void showAllForNextWeekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainTripsListView.Items.Clear();
            foreach (var trip in _busTrips)
            {
                TimeSpan sp = trip.Departure - DateTime.Now;
                if (sp.TotalDays <= 7)
                {
                    AddTripListView(trip);
                }
            }
            
        }
        
        #endregion

        #region About Print Exit

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bus Station Info App, v.1.0\nHomework in 'STEP' University\n" +
                "Group: PE911\nStudent: Zyrianov Pavlo");
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            PrintTrips();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintTrips();
        }
        private void PrintTrips()
        {
            PrintDocument print = new PrintDocument();
            print.PrintPage += new PrintPageEventHandler(print_PrintPage);
            print.Print();
        }

        private void print_PrintPage(System.Object sender,
           System.Drawing.Printing.PrintPageEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, mainTripsListView.Width, mainTripsListView.Height);
            Bitmap bitmap = new Bitmap(mainTripsListView.Width, mainTripsListView.Height);
            mainTripsListView.DrawToBitmap(bitmap, rect);
            e.Graphics.DrawImage(bitmap, new Point(0, 0));
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitApp();
        }
        private void exitContext_Click(object sender, EventArgs e)
        {
            ExitApp();
        }
        private void ExitApp()
        {
            var result = MessageBox.Show("Save All to File before closing?", "Checking Saving Dialog", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Save();
            }
            Close();
        }





        #endregion

        #region Sort

        private ListViewColumnSorter lvwColumnSorter = new ListViewColumnSorter();

        private void mainTripsListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortColumn(e.Column);
        }

        private void SortColumn(int n)
        {
            mainTripsListView.ListViewItemSorter = lvwColumnSorter;

            if (n == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = n;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            mainTripsListView.Sort();
        }




        #endregion

        
    }
}
