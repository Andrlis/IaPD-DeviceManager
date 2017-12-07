using System;
using System.Collections.Generic;
using System.Management;

using DeviceManager.bean;

namespace DeviceManager.resources
{
    class DeviceController
    {
        public List<Device> devices { get; }

        public DeviceController()
        {
            devices = new List<Device>();
        }

        public void FindDevices()
        {
            ManagementObjectSearcher deviceList = new ManagementObjectSearcher("Select * from Win32_PnPEntity");

            if (deviceList == null)
            {
                return;
            }

            foreach (ManagementObject device in deviceList.Get())
            {
                List<SystemFile> SystemFiles = new List<SystemFile>();

                foreach (var SystemFile in device.GetRelated("Win32_SystemDriver"))
                {
                    SystemFiles.Add(new SystemFile(SystemFile["PathName"]?.ToString(), SystemFile["Description"]?.ToString()));
                }

                devices.Add(new Device(
                    device["Name"]?.ToString(),
                    device["DeviceID"]?.ToString(),
                    device["ClassGuid"]?.ToString(),
                    device["HardwareID"] != null ? (string[])device["HardwareID"] : null,
                    device["Manufacturer"]?.ToString(),
                    SystemFiles,
                    device.GetPropertyValue("Status").ToString().Equals("OK")));
            }

        }

        public bool EnableDevice(Device device)
        {
            bool isEnabled = false;

            ManagementScope scope = new ManagementScope(@"\\" + Environment.MachineName + @"\root\CIMV2");
            SelectQuery query = new SelectQuery("Select * from Win32_PnPEntity");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope,query);
            foreach (ManagementObject dev in searcher.Get())
            {
                if (dev["DeviceID"].ToString().Equals(device.DeviceID))
                {
                    try
                    {
                        dev.InvokeMethod("Enable", new object[] { false });
                        isEnabled = true;
                        device.Status = true;
                    }
                    catch (ManagementException)
                    { }
                }
            }

            return isEnabled;
        }

        public bool DisableDevice(Device device)
        {
            bool isEnabled = false;

            ManagementScope scope = new ManagementScope(@"\\" + Environment.MachineName + @"\root\CIMV2");
            SelectQuery query = new SelectQuery("Select * from Win32_PnPEntity");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            foreach (ManagementObject dev in searcher.Get())
            {
                if (dev["DeviceID"].ToString().Equals(device.DeviceID))
                {
                    try
                    {
                        dev.InvokeMethod("Disable", new object[] { false });
                        isEnabled = true;
                        device.Status = false;
                    }
                    catch (ManagementException)
                    { }
                }
            }

            return isEnabled;
        }

        public bool Сheck(Device device)
        {
            foreach (ManagementObject dev in new ManagementObjectSearcher("Select * from Win32_PnPEntity").Get())
            {
                if (dev["DeviceID"].ToString().Equals(device.DeviceID))
                {
                    return dev["Status"].ToString().Equals("OK") == !device.Status;
                }
            }
            return false;
        }
    }
}
