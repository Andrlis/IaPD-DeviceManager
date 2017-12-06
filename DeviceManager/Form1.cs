using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DeviceManager.resources;
using DeviceManager.bean;

namespace DeviceManager
{
    public partial class Form1 : Form
    {
        private DeviceController deviceController;
        private int selectedIndex;

        public Form1()
        {
            deviceController = new DeviceController();
            deviceController.FindDevices();

            InitializeComponent();
            PaintUI();
        }

        private void PaintUI()
        {
            listView1.Scrollable = true;

            listView1.View = View.Details;
            listView1.HeaderStyle = ColumnHeaderStyle.None;

            listView2.HeaderStyle = ColumnHeaderStyle.None;

            button1.Enabled = false;

            foreach (var device in deviceController.devices)
            {
                addComponent(device);
            }
        }

        private void addComponent(Device device)
        {
            listView1.Items.Add(device.Name);
        }

        private void addDescription()
        {
            listView2.Clear();

            listView2.Items.Add("Name: " + deviceController.devices[selectedIndex].Name);
            listView2.Items.Add("DeviceID: " + deviceController.devices[selectedIndex].DeviceID);
            listView2.Items.Add("GUID: " + deviceController.devices[selectedIndex].GUID);
            listView2.Items.Add("Manufacturer: " + deviceController.devices[selectedIndex].Manufacturer);

            listView2.Items.Add("\nHardwareIDInfo: ");
            try
            {
                foreach (var hardwareID in deviceController.devices[selectedIndex].HardwareID)
                {
                    listView2.Items.Add(hardwareID);
                }
            }
            catch (NullReferenceException) { }

            if (deviceController.devices[selectedIndex].SystemFiles.Count != 0)
            {
                listView2.Items.Add("System Files: ");
                foreach (var sysFile in deviceController.devices[selectedIndex].SystemFiles)
                {
                    listView2.Items.Add("Path: " + sysFile.Path);
                    listView2.Items.Add("Description: " + sysFile.Description);
                }
            }

            listView2.Items.Add("Status: " + (deviceController.devices[selectedIndex].Status ? "on" : "off"));

            button1.Text = (deviceController.devices[selectedIndex].Status ? "Turn off" : "Turn on");
            button1.Enabled = true;
        }



        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var selected = listView1.FocusedItem;

                if (selected != null && selected.Bounds.Contains(e.Location) == true)
                {
                    selectedIndex = selected.Index;
                    addDescription();
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(deviceController.TurnOnOff(deviceController.devices[selectedIndex]) ? "Ok" : "Try again");
            addDescription();
        }
    }
}
