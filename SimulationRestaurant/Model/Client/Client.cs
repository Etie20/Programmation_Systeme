using MCI_Common.Recipes;
using Room.Model.Behaviour;
using MCI_Common.Behaviour;
using MCI_Common.Timer;
using System;
using SimulationRestaurant.Model;

namespace Room.Model.Client
{
    public class Client : Movable
    {
        
        public bool Served { get; set; }

        
        public Recipe[] Order = new Recipe[3];

        
        public OrderBehaviour OrderMethod;

        
        public Client()
        {
            if(Randomizer.Instance.R.Next(0,2) == 0)
                OrderMethod = new OrderAllOne();
            else
                OrderMethod = new OrderTwoStep();

            Console.WriteLine("Client created, order method: {0}", OrderMethod);
            LogWriter.GetInstance().Write("Client created, order method: " + OrderMethod);
        }
        
      
        public void OrderMeal()
        {
            OrderMethod.OrderMeal(this);
        }        

       
    }
}

