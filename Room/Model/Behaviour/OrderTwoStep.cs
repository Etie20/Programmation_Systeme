using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCI_Common.Recipes;
using MCI_Common.Timer;
using Room.Model.Client;
using Room.Model.Staff;

namespace Room.Model.Behaviour
{
    class OrderTwoStep : OrderBehaviour
    {
        /// <summary>
        /// Name of the behaviour
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Instantiate a new order method : order in two times
        /// </summary>
        public OrderTwoStep()
        {
            this.Name = "TwoStep";
        }

        /// <summary>
        /// Order dessert method
        /// </summary>
        /// <param name="clt"></param>
        public void OrderDessert(Client.Client clt)
        {
            clt.Order[2] = StaffManager.Instance.Counter.Menu[2][Randomizer.Instance.R.Next(0, StaffManager.Instance.Counter.Menu[2].Count)];
        }

        /// <summary>
        /// Order main method
        /// </summary>
        /// <param name="clt"></param>
        public void OrderMain(Client.Client clt)
        {
            clt.Order[1] = StaffManager.Instance.Counter.Menu[1][Randomizer.Instance.R.Next(0, StaffManager.Instance.Counter.Menu[1].Count)];
        }

        /// <summary>
        /// Order starter method
        /// </summary>
        /// <param name="clt"></param>
        public void OrderStarter(Client.Client clt)
        {
            Console.WriteLine(StaffManager.Instance.Counter.Menu[0][0]);
            clt.Order[0] = StaffManager.Instance.Counter.Menu[0][Randomizer.Instance.R.Next(0, StaffManager.Instance.Counter.Menu[0].Count)];

        }

        /// <summary>
        /// Order a wine bottle
        /// </summary>
        /// <param name="clt"></param>
        public void OrderWine(Client.Client clt)
        {

        }

        /// <summary>
        /// Order a meal
        /// </summary>
        /// <param name="clt"></param>
        public void OrderMeal(Client.Client clt)
        {
            OrderStarter(clt);
            OrderMain(clt);
        }
    }
}
