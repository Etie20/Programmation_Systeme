using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MCI_Common.RoomMaterials;
using MCI_Common.Recipes;
using MCI_Common.Communication;
using MCI_Common.Dishes;
using MCI_Common.Behaviour;
using Room.Model.Staff;
using MCI_Common.Timer;

namespace Room.Model.Client
{
    public class ClientGroup : Movable
    {
        /// <summary>
        /// Delegate for ReadyToOrder event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        public delegate void ReadyToOrderEventHandler(object source, OrderEventArgs args);

        /// <summary>
        /// Delegate for DishFinished event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        public delegate void DishFinishedEventHandler(object source, OrderEventArgs args);

        /// <summary>
        /// Delegate for ReadyToPay event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        public delegate void ReadyToPayEventHandler(object source, OrderEventArgs args);

        // TODO ajouter les envent handler au staff + abonnement

        /// <summary>
        /// Id of the clients group
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// List of clients in the group
        /// </summary>
        public List<Client> ClientList { get; set; }

        /// <summary>
        /// Order of the group containing dishes for each client
        /// </summary>
        public MCI_Common.Dishes.Order tableOrder { get; set; }

        /// <summary>
        /// Group's table
        /// </summary>
        public Table TableSit { get; set; }

        public RecipeType MealProgression = RecipeType.UNKNOWN;

        private bool Reserved;
        private bool IsHurry;
        private bool Ready = false;

        // Events
        private event ReadyToOrderEventHandler ReadyToOrder;
        private event DishFinishedEventHandler DishFinished;
        private event ReadyToPayEventHandler ReadyToPay;
        
        private string Sprite;
        
        public ClientGroup(int id, List<Client> list)
        {
            Id = id;
            ClientList = list;

            //adding subscriptions to events
            DishFinished += StaffManager.Instance.OnDishFinished;
            ReadyToOrder += StaffManager.Instance.OnReadyToOrder;
            ReadyToPay += StaffManager.Instance.OnReadyToPay;

            
            


            

            //Gets menus, reflexion moment
            Order();
            //Meal Orders are done

            //(Wine order)


            //Wait for food, then eats
            

            //this.Eat();  //for test purpose


            //Meal finished, ready to pay

        }

        public void Start(object data)
        {
            Console.WriteLine("New client group ({1} pax), thread ID : {0}", Thread.CurrentThread.ManagedThreadId, this.ClientList.Count());
            //Spawns at inital position
            MoveTo(192, 608);
            //Go to room master's counter
            MoveTo(10, 10);
            StaffManager.Instance.Master.AssignTable(this);
            //Follows the room master (or leaves)


            //Gets seated -> set individual clients around table

        }

        /// <summary>
        /// Group order -> each client orders (fills individual order table)
        /// </summary>
        public void Order()
        {
            Console.WriteLine("Group {0} ordering...", Id);

            foreach (Client clt in ClientList)
            {
                clt.OrderMeal();
            }

            //5min for decision making
            Thread.Sleep(5*Clock.Instance.Period);

            //Ready to order
            OnReadyToOrder(this);
            WaitMeal();
            
        }

        /// <summary>
        /// Order dessert (those who didn't)
        /// </summary>
        public void OrderDessert()
        {
            foreach (Client clt in ClientList)
                if (clt.Order[2] == null)
                    clt.OrderMethod.OrderDessert(clt);

            OnReadyToOrder(this);

        }

        /// <summary>
        /// Wait until every client of the group is served
        /// </summary>
        private void WaitMeal()
        {
            Console.WriteLine("Group {0} waiting for it's meal", Id);

            while (Ready != true)
            {
                Ready = true;

                foreach(Client clt in ClientList)
                {
                    if (clt.Served == false)
                        Ready = false;
                }
            }
            Eat();
        }

        /// <summary>
        /// Eat for given time, depending on meal eaten
        /// </summary>
        /// <param name="CurrentDish"></param>
        private void Eat()
        {
            int delay = 0;
            

            // Delay according to the dish eaten
            if (MealProgression == RecipeType.STARTER)
                delay = Global_Settings.timeStarter;
            else if (MealProgression == RecipeType.MAIN)
                delay = Global_Settings.timeMain;
            else if(MealProgression == RecipeType.DESSERT)
                delay = Global_Settings.timeDessert;
            else if(MealProgression == RecipeType.UNKNOWN)
                Console.WriteLine("Meal hasn't started");
            else if (MealProgression == RecipeType.FINISHED)
                Console.WriteLine("Meal has finished");
            else
                Console.WriteLine("Eat error");


            // If the client group is in a hurry, they stay twice less time
            if (IsHurry == true)
                delay /= 2;

            // Wait for the group to finish eating, delay multiplied to get minutes
            Console.WriteLine("Eating...");
            Thread.Sleep(delay* MCI_Common.Timer.Clock.Instance.Period);

            OnDishFinished(this);

            if (MealProgression == RecipeType.DESSERT)
                OnReadyToPay(this);

        }
        
        
        
        
        //methode manger    -> appeler dishfinished
        //                  -> appeler readytopay quand dessert fini

        //en fonction du type de commande, lorsque les clients ont rempli leur choix -> appeler readytoorder



        // RAISING EVENTS

        /// <summary>
        /// Executed when clients have decided
        /// </summary>
        /// <param name="GroupId"></param>
        protected virtual void OnReadyToOrder(ClientGroup group)
        {
            ReadyToOrder?.Invoke(this, new OrderEventArgs(group));
        }

        /// <summary>
        /// Executed when every client of a group has finished eating
        /// </summary>
        /// <param name="GroupId"></param>
        protected virtual void OnDishFinished(ClientGroup group)
        {
            DishFinished?.Invoke(this, new OrderEventArgs(group));
        }

        /// <summary>
        /// Executed when client is ready to pay (after finishing it's dessert)
        /// </summary>
        /// <param name="GroupId"></param>
        protected virtual void OnReadyToPay(ClientGroup group)
        {
            ReadyToPay?.Invoke(this, new OrderEventArgs(group));
        }


    }

    /// <summary>
    /// Class for passing the id of the client group to the event handler (waiter)
    /// </summary>
    public class OrderEventArgs : EventArgs
    {
        public ClientGroup cltGroup;

        public OrderEventArgs(ClientGroup group) { cltGroup = group; }

    }
}
