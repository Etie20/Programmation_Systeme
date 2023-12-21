using MCI_Common.Devices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationKitchen.Model
{
    public class DevicesManager
    {
        private static DevicesManager Instance = null;

        public Hashtable LeasedDevices { get; set; }
        public Hashtable AvailableDevices { get; set; }

        private readonly object LockLeasing = new object();
        private readonly object LockReleasing = new object();

        private DevicesManager()
        {
            this.LeasedDevices = new Hashtable();
            this.AvailableDevices = new Hashtable();
            this.InitDevicesManager();
        }

        private void InitDevicesManager()
        {
            List<Device> allDevices = new DeviceProcess().ListAll();

            foreach (var item in allDevices)
            {
                AvailableDevices.Add(item.Name, item.Quantity);
                LeasedDevices.Add(item.Name, 0);
            }

        }

        public static DevicesManager GetInstance()
        {
            if (Instance == null) Instance = new DevicesManager();
            return Instance;
        }

        public bool LeaseDevice(Device device)
        {
            lock (LockLeasing)
            {
                if ((int)this.LeasedDevices[device.Name] < (int)this.AvailableDevices[device.Name])
                {
                    this.LeasedDevices[device.Name] = (int)this.LeasedDevices[device.Name] + 1;
                    return true;
                }
                return false;
            }
        }

        public void ReleaseDevice(Device device)
        {
            lock (LockReleasing)
            {
                this.LeasedDevices[device.Name] = (int)this.LeasedDevices[device.Name] - 1;
            }
        }

        public void ReleaseDevices(List<Device> devices)
        {
            lock (LockReleasing)
            {
                foreach (var device in devices)
                {
                    this.LeasedDevices[device.Name] = (int)this.LeasedDevices[device.Name] - 1;
                }
            }
        }
    }
}
