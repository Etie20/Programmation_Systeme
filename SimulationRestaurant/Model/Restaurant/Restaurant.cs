using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCI_Common.RoomMaterials;
using Room.Model.Factory;
using Room.Model.Client;
using Room.Model.Staff;
using Room.Interfaces;
using SimulationRestaurant.Interfaces;

namespace Room.Model.Restaurant
{
    public class Restaurant : IModel
    {
        public List<Table> ListTables { get; private set; }
        
        public StaffManager Staff { get; set; }
        
        public List<ClientGroup> Clients { get; set; }
        
        public List<Server> Servers { get; set; }
        
        public List<RankChief> Rankchiefs { get; set; }

        public RoomMaster Master { get; set; }

        public event EventHandler Change;

        public static Restaurant Instance = null;
        
        public Restaurant()
        {
            Instance = this;
        }

        public void UpdateMove(object sender, EventArgs e)
        {
            this.OnChange(EventArgs.Empty);
        }

        public void Start()
        {
            this.ListTables = new List<Table>();
            this.GenerateTable();
            Staff = StaffManager.Instance(ListTables);
            Console.WriteLine("Staff created");
            this.Master = StaffManager.Instance().Master;
            this.Rankchiefs = StaffManager.Instance().Rankchiefs;
            this.Servers = StaffManager.Instance().Servers;

            CreateClientPool();
            Console.WriteLine("Clients created");
        }
        
        void CreateClientPool()
        {
            //while (this.Staff.Counter.ready == false) { }
            Console.WriteLine("Create client pool");
            ClientPool CltPool = new ClientPool();
            Clients = CltPool.ClientGroups;
            //StaffManager.Instance().ClientPool = CltPool;
        }
        
        public void GenerateTable()
        {
            // List all the tables
            foreach (var square in new SquareProcess().GetAll())
            {
                foreach (var table in square.Tables)
                {
                    Console.WriteLine("New table");
                    ListTables.Add(table);
                }
            }
            int i = 0;
            int y = 0;
            foreach (var table in ListTables)
            {
                table.TableLocation.posX = 48+i;
                table.TableLocation.posY = 64+y;
                i += 64;
                if (i == 384)
                {
                    y = 64;
                    i = 0;
                }
            }
        }

        protected virtual void OnChange(EventArgs e)
        {
            Change?.Invoke(this, e);
        }

    }
}
