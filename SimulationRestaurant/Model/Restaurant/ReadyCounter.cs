using MCI_Common.Communication;
using MCI_Common.Dishes;
using MCI_Common.Recipes;
using Room.Model.Client;
using Room.Model.Staff;
using SimulationRestaurant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Model.Restaurant
{
    public class ReadyCounter
    {
        public SocketCom socket;
        public int p;
        public string s;
        public bool ready { get; set; } = false;

        public List<Recipe>[] Menu { get; set; }

        public ReadyCounter()
        {

            this.Menu = new List<Recipe>[3];

            this.Menu[0] = new RecipeProcess().GetByRecipeType(1);
            this.Menu[1] = new RecipeProcess().GetByRecipeType(2);
            this.Menu[2] = new RecipeProcess().GetByRecipeType(3);

            s = "127.0.0.1";
            p = 5000;
            socket = new SocketCom(s, p);


            
            Console.WriteLine("Socket created");
            
            socket.MenuReception += UpdateMenu;

           /* while(StaffManager.Instance.Counter.Menu[0][0] == null)*/
            socket.Send("<MENU>");
        }

        public void SendOrder(MCI_Common.Dishes.Order order)
        {
            LogWriter.GetInstance().Write("Send order");
            string msg = Serialization.SerializeAnObject(order);
            msg += "<ORDER>";
            socket.Send(msg);
        }

        public void UpdateMenu(object send, EventArgs e)
        {
            Console.WriteLine("Menu Update");
            ObjectEventArgs oe = (ObjectEventArgs)e;

            List<Recipe> AllAvailableRecipe = (List<Recipe>) oe.receiveObject;

            List<Recipe> Starter = AllAvailableRecipe.Where(o => o.Type == RecipeType.STARTER).ToList();
            List<Recipe> Main = AllAvailableRecipe.Where(o => o.Type == RecipeType.MAIN).ToList();
            List<Recipe> Dessert = AllAvailableRecipe.Where(o => o.Type == RecipeType.DESSERT).ToList();
            this.Menu[0] = Starter;
            this.Menu[1] = Main;
            this.Menu[2] = Dessert;

            this.ready = true;
        }
    }
}
