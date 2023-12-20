using SimulationKitchen.Contract;
using SimulationKitchen.Controller;
using SimulationKitchen.Model;
using System;
namespace SimulationKitchen
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IModel model = new Kitchen(2);
            model.Start();

            using (var game = new Game1(model))
                game.Run();
            // Application.EnableVisualStyles();
            // Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new Form1());
            
        }

        [STAThread]
        public static void Launch()
        {
            IModel model = new Kitchen(Configuration.CookersNumber);
            model.Start();

            using (var game = new Game1(model))
                game.Run();
        }

        public static void Stop()
        {
            IModel model = new Kitchen(Configuration.CookersNumber);
            model.Start();

            using (var game = new Game1(model))
            game.Exit();
        }
    }
}
