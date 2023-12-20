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

namespace Room.Model.Staff
{
    public class RankChief : Staff
    {
        /// <summary>
        /// Square in the room
        /// </summary>
        //private Square mySquare;

        /// <summary>
        /// Sprite of the rank chief
        /// </summary>
        private string Sprite;

        public RankChief()
        {
            
            Console.WriteLine("Rank chief created");
        }


        /// <summary>
        /// Prepare a table
        /// </summary>
        /// <param name="Table"></param>
        public void PrepareTable(Table tableToPrep)
        {
            
        }
        

        /// <summary>
        /// Clean a table
        /// </summary>
        /// <param name="Table"></param>
        public void ClearTable(Table tableToClr)
        {
            //Puts away napkins and nappe

            PrepareTable(tableToClr);
        }
        
        

        /// <summary>
        /// Sets position of clients around the table according to avalaible spots
        /// </summary>
        /// <param name="cltGr"></param>
        public void SeatClients(ClientGroup cltGr)
        {
            int i = 0;

            foreach (Client.Client clt in cltGr.ClientList)
            {
                //clt.Position.SetPosition(cltGr.TableSit.posAroundTable[i]);
            }
        }

        /// <summary>
        /// Take the client's order
        /// </summary>
        /// <param name="clients"></param>
        public void TakeOrderTable(ClientGroup clients)
        {
            int i;

            // if(clients.MealProgression == RecipeType.MAIN)
            // {
            //     //Takes dessert orders for those who order in two times
            //     foreach (Client.Client clt in clients.ClientList)
            //         if(clt.OrderMethod.Method() == "two")
            //         {
            //             Dish dish = new Dish(clients.tableOrder);
            //             dish.Recipe = clt.Order[2];
            //             clients.tableOrder.Dishes.Add(dish);
            //         }
            //
            // }
            // else
            // {
            //     foreach (Client.Client clt in clients.ClientList)
            //     {
            //         i = 0;
            //
            //         while (clt.Order[i] != null)
            //         {
            //             Dish dish = new Dish(clients.tableOrder);
            //             dish.Recipe = clt.Order[i];
            //             clients.tableOrder.Dishes.Add(dish);
            //             i++;
            //         }
            //     }
            //
            //     
            // }

            Console.WriteLine("Commande du groupe {0} passée", clients.Id);

            StaffManager.Instance.Counter.SendOrder(clients.tableOrder);

        }

        public override void WhoAmI()
        {
            Console.WriteLine("I'm a RankChief");
        }
    }
}
