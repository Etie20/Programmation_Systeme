using Room.Model.Client;
using Room.Model.Factory;
using MCI_Common.Communication;
using Room.Model.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCI_Common.Dishes;

namespace Room.Model.Staff
{
    public class StaffManager
    {
        /// <summary>
        /// List of rank chiefs
        /// </summary>
        public List<RankChief> Rankchiefs { get; private set; }

        /// <summary>
        /// The room master
        /// </summary>
        public RoomMaster Master { get; private set; }

        /// <summary>
        /// List of servers
        /// </summary>
        public List<Server> Servers { get; private set; }

        /// <summary>
        /// Counter to exchange information with kitchen
        /// </summary>
        public ReadyCounter Counter { get; internal set; }

        /// <summary>
        /// Staff Manager thread safe Single instance
        /// </summary>
        private static StaffManager instance = null;
        private static readonly object padlock = new object();

        /// <summary>
        /// Instantiate a Staff Manager
        /// </summary>
        public StaffManager()
        {
            Counter = new ReadyCounter();
            Rankchiefs = new List<RankChief>();
            Servers = new List<Server>();

            for (int i = 0; i < Global_Settings.nbRankChief; i++)
                Rankchiefs.Add(new RankChief());
            for (int i = 0; i < Global_Settings.nbServers; i++)
                Servers.Add(new Server());
            Master = new RoomMaster();

            StaffManager.Instance.Counter.socket.OrderReadyReception += this.OnOrderReadyToServe;
        }

        /// <summary>
        /// Method for Singelton Management
        /// </summary>
        public static StaffManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new StaffManager();
                    }
                    return instance;
                }
            }
        }

        public void OnOrderReadyToServe(object source, EventArgs args)
        {
            MCI_Common.Dishes.Order order = (MCI_Common.Dishes.Order)((ObjectEventArgs)args).receiveObject;
            //this.Servers.Where(server => server.IsAvailable).First().ServeMeal();
        }

        /// <summary>
        /// Assign a rank chief to take the order
        /// </summary>
        /// <param name="clients"></param>
        public void OnReadyToOrder(object source, OrderEventArgs args)
        {
            int i = 0;

            Console.WriteLine("Le client {0} est prêt à commander", args.cltGroup.Id);

            Rankchiefs[i].TakeOrderTable(args.cltGroup);

        }

        /// <summary>
        /// Assign a server to clear and serve the meal when event raised
        /// </summary>
        /// <param name="source"></param>
        /// <param name="Id"></param>
        public void OnDishFinished(object source, OrderEventArgs args)
        {
            int i = 0;

            Console.WriteLine("Le client {0} a fini", args.cltGroup.Id);

            Servers[i].ClearMeal(args.cltGroup);
        }

        /// <summary>
        /// Assign a room master to make payment
        /// </summary>
        /// <param name="clients"></param>
        public void OnReadyToPay (object source, OrderEventArgs args)
        {
            Console.WriteLine("Le client {0} est prêt à payer", args.cltGroup.Id);

            //Prepare payment
            Master.CltPayment(args.cltGroup);
        }
    }
}
