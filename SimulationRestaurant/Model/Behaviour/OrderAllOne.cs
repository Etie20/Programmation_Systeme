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
    class OrderAllOne : OrderBehaviour
    {
        
        public string Name { get; set; }
        
        public OrderAllOne()
        {
            this.Name = "One";
        }
        
        public void OrderDessert(Client.Client clt)
        {
            Console.WriteLine("yoyoyoyoyoyo");
            Console.WriteLine(StaffManager.Instance().Counter.Menu[2].Count);
            if(clt.Order[2] == null)
                clt.Order[2] = StaffManager.Instance().Counter.Menu[2][Randomizer.Instance.R.Next(0, StaffManager.Instance().Counter.Menu[2].Count)];
        }

        public void OrderMain(Client.Client clt)
        {
            Console.WriteLine("yoyoyoyoyoyo");
            Console.WriteLine(StaffManager.Instance().Counter.Menu[1].Count);
            clt.Order[1] = StaffManager.Instance().Counter.Menu[1][Randomizer.Instance.R.Next(0, StaffManager.Instance().Counter.Menu[1].Count)];
        }
        
        public void OrderStarter(Client.Client clt)
        {
            Console.WriteLine("yoyoyoyoyoyo");
            Console.WriteLine(StaffManager.Instance().Counter.Menu[0].Count);
            clt.Order[0] = StaffManager.Instance().Counter.Menu[0][Randomizer.Instance.R.Next(0, StaffManager.Instance().Counter.Menu[0].Count)];
            Console.WriteLine(clt.Order[0].Name);
        }
        
        public void OrderWine(Client.Client clt)
        {
            
        }

        /// <summary>
        /// Order a meal method
        /// </summary>
        /// <param name="clt"></param>
        public void OrderMeal(Client.Client clt)
        {
            OrderStarter(clt);
            OrderMain(clt);
            OrderDessert(clt);
        }

    }
}
