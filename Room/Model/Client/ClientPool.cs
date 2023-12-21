using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Room.Model.Staff;
using MCI_Common.Communication;

namespace Room.Model.Client
{
    public class ClientPool
    {
        /// <summary>
        /// Number of clients since start
        /// </summary>
        public int NbCltSinceStart { get; set; }

        /// <summary>
        /// Number of client groups
        /// </summary>
        public int NbCltGp { get; set; }

        /// <summary>
        /// List the client group
        /// </summary>
        public List<ClientGroup> ClientGroups { get; set; }

        /// <summary>
        /// Instantiate a clientpool
        /// </summary>
        public ClientPool()
        {
            this.NbCltSinceStart = 0;
            this.NbCltGp = 0;

            this.ClientGroups = new List<ClientGroup>();

            //Creates client groups
            /*   UNCOMMENT FOR PRODUCTION   */
            this.SpawnClientsGroup();
            
        }

        /// <summary>
        /// Create a clients pool periodically
        /// </summary>
        public void SpawnClientsGroup()
        {
            while(this.NbCltSinceStart < Global_Settings.nbCltPerShift)
            {
                ClientGroup group = GenerateGroup();
                ThreadPool.QueueUserWorkItem(group.Start);
                //Wait 5 Sim min
                Thread.Sleep(5* MCI_Common.Timer.Clock.Instance.Period);
            }
        }

        /// <summary>
        /// Generates the client group from created clients
        /// </summary>
        private ClientGroup GenerateGroup()
        {
            // List for storing generated clients
            List<Client> cltList = new List<Client>();

            // Randomizer
            Random number = new Random();

            // Random number used for deciding group size
            int rdNb = number.Next(1, 11);

            // Increment number of clients since start
            this.NbCltSinceStart += rdNb;

            //Increment group number
            this.NbCltGp++;

            // Generates between 1 and 10 clients 
            cltList = GenerateClients(rdNb);

            // ClientGroup created
            
            ClientGroup group = new ClientGroup(this.NbCltGp, cltList);
            this.ClientGroups.Add(group);

            return group;
        }

        /// <summary>
        /// Generates the clients
        /// </summary>
        /// <param name="nb"></param>
        /// <returns></returns>
        private List<Client> GenerateClients(int nb)
        {
            List<Client> cltList = new List<Client>();
            Client clt;

            for (int i = 0; i < nb; i++)
            {
                clt = new Client();
                cltList.Add(clt);
            }
            return cltList;
        }

    }
}
