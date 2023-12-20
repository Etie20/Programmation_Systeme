using MCI_Common.Recipes;
using Room.Model.Behaviour;
using MCI_Common.Behaviour;
using MCI_Common.Timer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Model.Client
{
    // Imovable ???
    public class Client : Movable
    {
        /// <summary>
        /// If the client received its dish, false when
        /// the waiter clears the table
        /// </summary>
        public bool Served { get; set; }

        /// <summary>
        /// List of the dishes ordered by the client
        /// </summary>
        public Recipe[] Order = new Recipe[3];

        /// <summary>
        /// The client orders in one or two times
        /// </summary>
        public OrderBehaviour OrderMethod;

        /// <summary>
        /// Instantiate a client
        /// </summary>
        public Client()
        {
            if(Randomizer.Instance.R.Next(0,2) == 0)
                OrderMethod = new OrderAllOne();
            else
                OrderMethod = new OrderTwoStep();

            Console.WriteLine("Client created, order method: {0}", OrderMethod);
        }
        
        /// <summary>
        /// Order a meal
        /// </summary>
        public void OrderMeal()
        {
            OrderMethod.OrderMeal(this);
        }        

       
    }
}

