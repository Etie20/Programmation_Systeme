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
using SimulationRestaurant.Model;

namespace Room.Model.Client
{
    public class ClientGroup : Movable
    {

        public delegate void ReadyToOrderEventHandler(object source, OrderEventArgs args);
        
        public delegate void DishFinishedEventHandler(object source, OrderEventArgs args);
      
        public delegate void ReadyToPayEventHandler(object source, OrderEventArgs args);

        public delegate void ReadyToSitEventHandler(object source, OrderEventArgs args);

        // TODO ajouter les envent handler au staff + abonnement
        
        public int Id { get; set; }


        public List<Client> ClientList { get; set; }


        public MCI_Common.Dishes.Order tableOrder { get; set; }

     
        public Table TableSit { get; set; }

        public RecipeType MealProgression = RecipeType.UNKNOWN;

        private bool Reserved;
        private bool IsHurry;
        private bool Ready = false;

        // Events
        private event ReadyToOrderEventHandler ReadyToOrder;
        private event DishFinishedEventHandler DishFinished;
        private event ReadyToPayEventHandler ReadyToPay;
        private event ReadyToSitEventHandler ReadyToSit;
        
        
        public ClientGroup(int id, List<Client> list)
        {
            Id = id;
            ClientList = list;
            this.Position = new Position(80, 288);
            this.tableOrder = new MCI_Common.Dishes.Order();
            this.tableOrder.Id = this.Id;

            //adding subscriptions to events
            DishFinished += StaffManager.Instance().OnDishFinished;
            ReadyToOrder += StaffManager.Instance().OnReadyToOrder;
            ReadyToPay += StaffManager.Instance().OnReadyToPay;
            //ReadyToSit += StaffManager.Instance().OnReadyToSit;

            this.MoveEvent += Restaurant.Restaurant.Instance.UpdateMove;
            


            

            //Gets menus, reflexion moment
            
            //Meal Orders are done

            //(Wine order)


            //Wait for food, then eats
            

            //this.Eat();  //for test purpose


            //Meal finished, ready to pay

        }

        public void Start(object data)
        {
            LogWriter.GetInstance().Write("New client group "+this.ClientList.Count()+" pax), thread ID : " + Thread.CurrentThread.ManagedThreadId);
            
            MoveTo(new Position(64, 240));
            Table table = StaffManager.Instance().Master.AssignTable(this);

            if (table == null)
            {
                LogWriter.GetInstance().Write("Pas de table disponible");
                MoveTo(new Position(80, 400));
                return;
            }
            else
            {
                OnReadyToSit(this);
                MoveTo(table.TableLocation);
                Order();
                WaitMeal();
            }
        }

        public void Order()
        {
            LogWriter.GetInstance().Write("Group "+Id+" ordering...");

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
        

        protected virtual void OnReadyToOrder(ClientGroup group)
        {
            ReadyToOrder?.Invoke(this, new OrderEventArgs(group));
        }
        
        protected virtual void OnDishFinished(ClientGroup group)
        {
            DishFinished?.Invoke(this, new OrderEventArgs(group));
        }

      
        protected virtual void OnReadyToPay(ClientGroup group)
        {
            ReadyToPay?.Invoke(this, new OrderEventArgs(group));
        }

        protected virtual void OnReadyToSit(ClientGroup group)
        {
            ReadyToSit?.Invoke(this, new OrderEventArgs(group));
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
