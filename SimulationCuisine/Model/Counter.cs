using MCI_Common.Communication;
using MCI_Common.Dishes;
using MCI_Common.Recipes;
using SimulationKitchen.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationKitchen.Model
{
    public class Counter
    {
        /// <summary>
        /// Instance of the counter
        /// </summary>
        private static Counter Instance = null;

        /// <summary>
        /// Socket client to connect to room
        /// </summary>
        public SocketCom RoomCommunication { get; private set; }

        /// <summary>
        /// List of clients's orders
        /// </summary>
        public List<Order> Orders { get; set; }

        /// <summary>
        /// List of the recipe available in menu
        /// </summary>
        public List<Recipe> Menu { get; private set; }

        /// <summary>
        /// Instantiate the counter
        /// </summary>
        private Counter()
        {
            this.RoomCommunication = new SocketCom(Configuration.IP_KITCHEN, Configuration.PORT_KITCHEN);
        }

        /// <summary>
        /// Send all the orders that are currently ready
        /// </summary>
        public void SendOrderReady(Order order)
        {
            string msg = Serialization.SerializeAnObject(order);
            msg += "<ORDER_READY>";
            this.RoomCommunication.Send(msg);
            this.Orders.Remove(order);
        }

        public static Counter GetInstance()
        {
            if(Counter.Instance == null)
            {
                Counter.Instance = new Counter();
            }
            return Counter.Instance;
        }
    }
}
