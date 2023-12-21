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

namespace Room.Model.Restaurant
{
    class Restaurant
    {
        /// <summary>
        /// List of the tables in the room
        /// </summary>
        List<Table> ListTables { get; set; }

        /// <summary>
        /// Staff manager instance
        /// </summary>
        public StaffManager Staff { get; set; }

        /// <summary>
        /// Clients group list
        /// </summary>
        public ClientPool CltPool { get; set; }

        /// <summary>
        /// Instantiate a restaurant
        /// </summary>
        public Restaurant()
        {
            //this.Squares = new SquareProcess().GetAll();

            Staff = new StaffManager();
            Console.WriteLine("Staff created");

            CreateClientPool();
            Console.WriteLine("Clients created");
            this.ListTables = new List<Table>();
            this.GenerateTable();
        }

        /// <summary>
        /// Generate the client ppol
        /// </summary>
        void CreateClientPool()
        {
            while (this.Staff.Counter.ready == false) { }
            this.CltPool = new ClientPool();
        }

        /// <summary>
        /// Generate the table
        /// </summary>
        public void GenerateTable()
        {
            // List all the tables
            foreach (var square in new SquareProcess().GetAll())
            {
                foreach (var table in square.Tables)
                {
                    ListTables.Add(table);
                }
            }
            int i = 0;
            int y = 0;
            foreach (var table in ListTables)
            {
                table.TableLocation.posX = 48+i;
                table.TableLocation.posY = 384+y;
                i += 48;
                if (i == 336)
                {
                    y = 80;
                }
            }
        }

        /// <summary>
        /// Apply user configuration
        /// </summary>
        /// <param name="Config"></param>
        void ApplyConfig(IController Config)
        {
            
        }
        
    }
}
