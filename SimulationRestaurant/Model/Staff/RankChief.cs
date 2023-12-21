using MCI_Common.RoomMaterials;
using MCI_Common.Dishes;
using MCI_Common.Recipes;
using Room.Model.Staff;
using Room.Model.Client;
using Room.Model.Behaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCI_Common.Communication;
using Room.Model.Restaurant;
using MCI_Common.Behaviour;
using SimulationRestaurant.Model;

namespace Room.Model.Staff
{
    public class RankChief : Staff
    {
        /// <summary>
        /// Availability of a rank chiefS
        /// </summary>
        public bool IsAvailable { get; set; }

        public RankChief()
        {
            this.Position = new Position(48,192);
            this.IsAvailable = true;
            
            //adding subscriptions to events
            this.MoveEvent += Restaurant.Restaurant.Instance.UpdateMove;
        }

        public void PrepareTable(Table tableToPrep)
        {
            
        }
        

        public void ClearTable(Table tableToClr)
        {

            PrepareTable(tableToClr);
        }
        
        

        public void SeatClients(ClientGroup cltGr)
        {
            int i = 0;

            foreach (Client.Client clt in cltGr.ClientList)
            {
                //clt.Position.SetPosition(cltGr.TableSit.posAroundTable[i]);
            }
        }

   
        public void TakeOrderTable(ClientGroup clients)
        {
            this.IsAvailable = false;
            Position nextClient = new Position(clients.Position.posX - 20, clients.Position.posY);
            MoveTo(nextClient);

            if (clients.MealProgression == RecipeType.MAIN)
            {
                //Takes dessert orders for those who order in two times
                foreach (Client.Client clt in clients.ClientList)
                    if(clt.OrderMethod.Name == "two")
                    {
                        Dish dish = new Dish();
                        dish.Recipe = clt.Order[2];
                        clients.tableOrder.Dishes.Add(dish);
                    }

            }
            else
            {
                foreach (Client.Client clt in clients.ClientList)
                {
                    for (int i = 0; i < clt.Order.Length; i++)
                    {
                        Dish dish = new Dish();
                        if(clt.Order[i] != null)
                        {
                            dish.Recipe = clt.Order[i];
                            clients.tableOrder.Dishes.Add(dish);
                        }
                        
                    }
                }

                
            }

            LogWriter.GetInstance().Write("Commande du groupe " + clients.Id + " passée");

            StaffManager.Instance().Counter.SendOrder(clients.tableOrder);
            this.IsAvailable = true;
        }

        public override void WhoAmI()
        {
            Console.WriteLine("I'm a RankChief");
        }
    }
}
