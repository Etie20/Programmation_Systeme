using Room.Model.Client;
using MCI_Common.RoomMaterials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Model.Staff
{
    public class RoomMaster : Staff
    {
        public List<Table> Tables { get; set; }

        public RoomMaster()
        {
            Tables = new List<Table>();
        }

        /// <summary>
        /// Check the reservation of a group of clients
        /// </summary>
        /// <param name="groupClient"></param>
        public void CheckReservation(ClientGroup groupClient)
        {
            
        }

        /// <summary>
        /// Assign a table to a group of clients
        /// </summary>
        /// <param name="groupClient"></param>
        public Table AssignTable(ClientGroup groupClient)
        {
            // Count of the number of clients in the group
            int WaitingClients = groupClient.ClientList.Count();

            // Initiate a new list of tables
            List<Table> AvailableTables = this.Tables.Where(t => t.Capacity >= WaitingClients).ToList();

            foreach (var table in AvailableTables)
            {
                AvailableTables.Add(table);
            }

            Table AssignedTable = AvailableTables.First();
            AssignedTable.ClientGroupIdAssigned = groupClient.Id;
            AssignedTable.IsAvailable = false;

            return AssignedTable;
        }

        public void CltPayment(ClientGroup grp)
        {
            Console.WriteLine("Le client {0} a payé", grp.Id);
        }


        public override void WhoAmI()
        {
            Console.WriteLine("I'm a RoomMaster");
        }
    }
}
