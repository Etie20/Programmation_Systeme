using MCI_Common.Communication;
using MCI_Common.Dishes;
using MCI_Common.Recipes;
using Room.Model.Client;
using Room.Model.Staff;
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
        private bool _ready = false;
        public bool ready
        {
            get { return this._ready; }
            set { _ready = value; }
        }

        public List<Recipe>[] Menu { get; set; }

        public ReadyCounter()
        {

            Menu = new List<Recipe>[3];

            Menu[0] = new List<Recipe>();
            Menu[1] = new List<Recipe>();
            Menu[2] = new List<Recipe>();

            s = "10.162.128.230";
            p = 11000;
            socket = new SocketCom(s, p);
            
            Console.WriteLine("Socket created");
            
            socket.MenuReception += UpdateMenu;

            //while(StaffManager.Instance.Counter.Menu[0][0] == null)
                socket.Send("<MENU>");
        }

        public void SendOrder(MCI_Common.Dishes.Order order)
        {
            string msg = Serialization.SerializeAnObject(order);
            msg += "<ORDER>";
            socket.Send(msg);
        }

        public void GetOrder(MCI_Common.Dishes.Order order)
        {
            socket.Send("<ORDER>");
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
        }
    }
}
