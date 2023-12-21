using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCI_Common.Recipes;

namespace Room.Model.Behaviour
{
    public interface OrderBehaviour
    {
        string Name { get; set; }

        /// <summary>
        /// Order meal according to order method
        /// </summary>
        /// <param name="clt"></param>
        void OrderMeal(Client.Client clt);

        /// <summary>
        /// Order dessert
        /// </summary>
        /// <param name="clt"></param>
        void OrderDessert(Client.Client clt);

        /// <summary>
        /// Order Starter
        /// </summary>
        /// <param name="clt"></param>
        void OrderStarter(Client.Client clt);

        /// <summary>
        /// Order main meal
        /// </summary>
        /// <param name="clt"></param>
        void OrderMain(Client.Client clt);

        /// <summary>
        /// Order wine
        /// </summary>
        /// <param name="clt"></param>
        void OrderWine(Client.Client clt);
    }
}
