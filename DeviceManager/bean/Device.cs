using System;
using System.Collections.Generic;

using DeviceManager.resources;

namespace DeviceManager.bean
{
    class Device
    {
        private string name;
        private string deviceID;
        private bool status;
        private string guid;
        private string manufactire;
        private string[] hardvareID;
        private List<SystemFile> sysFile;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public String DeviceID
        {
            get { return deviceID; }
            set { deviceID = value; }
        }
        public string GUID
        {
            get { return guid; }
            set { guid = value; }
        }
        public string[] HardwareID
        {
            get { return hardvareID; }
            set { hardvareID = value; }
        }
        public string Manufacturer
        {
            get { return manufactire; }
            set { manufactire = value; }
        }
        public List<SystemFile> SystemFiles
        {
            get { return sysFile; }
            set { sysFile = value; }
        }
        public bool Status
        {
            get { return status; }
            set { status = value; }
        }

        public Device(string Name, string DeviceID, string GUID, string[] HardwareID, string Manufacturer,
             List<SystemFile> SystemFiles, bool Status)
        {
            this.Name = Name;
            this.DeviceID = DeviceID;
            this.GUID = GUID;
            this.HardwareID = HardwareID;
            this.Manufacturer = Manufacturer;
            this.SystemFiles = SystemFiles;
            this.Status = Status;
        }
    }
}
