using Room.Model.Client;
using Room.Model.Factory;
using MCI_Common.Communication;
using Room.Model.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCI_Common.Dishes;
using MCI_Common.RoomMaterials;
using SimulationRestaurant.Model;

namespace Room.Model.Staff
{
    public class StaffManager
    {
        
        public List<RankChief> Rankchiefs { get; private set; }

        public RoomMaster Master { get; private set; }

        
        public List<Server> Servers { get; private set; }

        public ReadyCounter Counter { get; internal set; }

        public ClientPool ClientPool { get; set; }

        
        private static StaffManager instance = null;
        private static readonly object padlock = new object();

     
        private StaffManager(List<Table> tables)
        {
            Counter = new ReadyCounter();
            
            Rankchiefs = new List<RankChief>();
            Servers = new List<Server>();

            for (int i = 0; i < Global_Settings.nbRankChief; i++)
                Rankchiefs.Add(new RankChief());
            for (int i = 0; i < Global_Settings.nbServers; i++)
                Servers.Add(new Server());
            Master = new RoomMaster(tables);

            SocketCom.instance.OrderReadyReception += this.OnOrderReadyToServe;
        }

        
        public static StaffManager Instance(List<Table> tables = null)
        {
            lock(padlock)
            {
                    if (instance == null)
                    {
                        instance = new StaffManager(tables);
                }
                    return instance;
            }
            
        }

        public void OnOrderReadyToServe(object source, EventArgs args)
        {
            MCI_Common.Dishes.Order order = (MCI_Common.Dishes.Order)((ObjectEventArgs)args).receiveObject;
            ClientGroup cltgrp = this.ClientPool.ClientGroups.Where(clt => clt.Id == order.Id).First();
            this.Servers.Where(server => server.IsAvailable).First().ServeMeal(cltgrp, order);
        }

       
        public void OnReadyToOrder(object source, OrderEventArgs args)
        {
            LogWriter.GetInstance().Write("Le client " + args.cltGroup.Id + " est prêt à commander");

            RankChief chief = Rankchiefs.Where(c => c.IsAvailable).First();

            if (chief == null) LogWriter.GetInstance().Write("Pas de chef de rang disponible");
            else
            {
                chief.TakeOrderTable(args.cltGroup);
            }


        }

        // public void OnReadyToSit(object source, OrderEventArgs args)
        // {            
        //     Console.WriteLine("Le client {0} est prêt à s'asseoir", args.cltGroup.Id);
        //     RankChief rankChief = Rankchiefs.Where(c => c.IsAvailable).First();
        //     rankChief.MoveTo(82, 100);
        //     
        // }
        
        public void OnDishFinished(object source, OrderEventArgs args)
        {
            Console.WriteLine("Le client {0} a fini", args.cltGroup.Id);

            Server server = Servers.Where(c => c.IsAvailable).First();
            server.ClearMeal(args.cltGroup);
        }
        
        public void OnReadyToPay (object source, OrderEventArgs args)
        {
            Console.WriteLine("Le client {0} est prêt à payer", args.cltGroup.Id);
            
            Master.CltPayment(args.cltGroup);
        }
    }
}
