using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Devices
{
    public class DeviceProcess
    {
       
        public TB_Device MapTable { get; private set; }

       
        public DAO Bdd { get; private set; }
        
     
        public string Request { get; private set; }

      
        public DataSet Datas { get; private set; }

     
        public DeviceProcess()
        {
            this.MapTable = new TB_Device();
            this.Datas = new DataSet();
            this.Bdd = DAO.getInstance();
        }

        
        private Device CreateDevice(DataRow row)
        {
            Device device = new Device();
            device.Name = row["Nom"].ToString();
            device.Quantity = int.Parse(row["Quantite"].ToString());
            device.Id = int.Parse(row["Id"].ToString());
            device.Capacity = int.Parse(row["Capacite"].ToString());
            return device;
        }
        
        public List<Device> ListAll()
        {
            this.Datas.Clear();
            this.Request = this.MapTable.GetAll();
            this.Datas = this.Bdd.getRows(this.Request, "Devices");

            List<Device> results = new List<Device>();

            foreach (DataRow item in this.Datas.Tables["Devices"].Rows)
            {
                Device device = this.CreateDevice(item);

                results.Add(device);
            }

            return results;
        }

        /// <summary>
        /// Get a specific devices list for a step
        /// </summary>
        /// <param name="id">id of the step</param>
        /// <returns>A specific devices list</returns>
        public List<Device> ListAllByStep(int id)
        {
            this.Datas.Clear();
            this.Request = this.MapTable.GetAllByStep(id);
            this.Datas = this.Bdd.getRows(this.Request, "Device");

            List<Device> results = new List<Device>();

            foreach (DataRow item in this.Datas.Tables["Device"].Rows)
            {
                Device device = this.CreateDevice(item);
                results.Add(device);
            }

            return results;
        }

      
        public Device GetOne(int id)
        {
            this.Datas.Clear();
            this.MapTable.CurrentDevice = new Device();
            this.MapTable.CurrentDevice.Id = id;
            this.Request = this.MapTable.GetById();
            this.Datas = this.Bdd.getRows(this.Request, "Devices");

            Device result = this.CreateDevice(this.Datas.Tables["Devices"].Rows[0]);

            return result;
        }
        
        public Device GetOne(string name)
        {
            this.Datas.Clear();
            this.MapTable.CurrentDevice = new Device();
            this.MapTable.CurrentDevice.Name = name;
            this.Request = this.MapTable.GetByName();
            this.Datas = this.Bdd.getRows(this.Request, "Devices");

            Device result = this.CreateDevice(this.Datas.Tables["Devices"].Rows[0]);

            return result;
        }
    }
}
