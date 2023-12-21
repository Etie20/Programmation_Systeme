using Room.Model.Restaurant;
using SimulationRestaurant.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace SimulationRestaurant
{
    static class Program
    {
        
        [STAThread]
        static void Main()
        {
            IModel model = new Restaurant();
            model.Start();
            using (var game = new Game1(model))
                game.Run();
        }
        

        public static void Stop()
        {
           
        }
    }
}