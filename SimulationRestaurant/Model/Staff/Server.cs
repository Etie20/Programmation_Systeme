using Room.Model.Client;
using MCI_Common.Recipes;
using MCI_Common.Dishes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCI_Common.Behaviour;

namespace Room.Model.Staff
{
    public class Server : Staff
    {
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Instantiate a server
        /// </summary>
        public Server()
        {
            this.IsAvailable = true;
            this.Position = new Position(320, 32);
        }

        /// <summary>
        /// Serve the client's meal
        /// </summary>
        /// <param name="clients"></param>
        public void ServeMeal(ClientGroup clts, MCI_Common.Dishes.Order order)
        {
            this.IsAvailable = false;
            //Gives the meals
            foreach (var clt in clts.ClientList)
            {
                clt.Served = true;
            }

            //Update meal progression
            if(clts.MealProgression == RecipeType.UNKNOWN)
                clts.MealProgression = RecipeType.STARTER;
            else if (clts.MealProgression == RecipeType.STARTER)
                clts.MealProgression = RecipeType.MAIN;
            else if (clts.MealProgression == RecipeType.MAIN)
                clts.MealProgression = RecipeType.DESSERT;
            else if (clts.MealProgression == RecipeType.DESSERT)
                clts.MealProgression = RecipeType.FINISHED;

            this.IsAvailable = true;
        }

        /// <summary>
        /// Clean the client's meal
        /// </summary>
        /// <param name="clients"></param>
        public void ClearMeal(ClientGroup clients)
        {
            this.IsAvailable = false;
            Console.WriteLine("Clearing meal of group {0}", clients.Id);

            foreach (Client.Client clt in clients.ClientList)
                clt.Served = false;

            if (clients.MealProgression == RecipeType.MAIN)
                clients.OrderDessert();

            this.IsAvailable = true;
        }

        public override void WhoAmI()
        {
            Console.WriteLine("I'm a Server");
        }
    }
}
