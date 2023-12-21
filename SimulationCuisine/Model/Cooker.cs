using MCI_Common.Behaviour;
using MCI_Common.Devices;
using MCI_Common.Dishes;
using MCI_Common.Recipes;
using MCI_Common.Tools;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SimulationKitchen.Model
{
    public class Cooker : Movable
    {

        public int Id { get; set; }


        public bool IsAvailable { get; set; }
        
        public ToolsManager ToolsStorage { get; set; }


        public DevicesManager DevicesStorage { get; set; }

        public Washer WasherEngine { get; set; }

        public Oven OvenCook { get; private set; }

        public event EventHandler OrderReady;
        
        public Cooker(int id, Washer washer, Oven oven, Position position)
        {
            this.Id = id;
            this.ToolsStorage = ToolsManager.GetInstance();
            this.DevicesStorage = DevicesManager.GetInstance();
            this.WasherEngine = washer;
            this.OvenCook = oven;
            this.Position = position;
            this.IsAvailable = true;
        }

     
        public void PrepareStep(Step step, Dish dish = null)
        {
            LogWriter.GetInstance().Write("Cooker " +this.Id+" start step "+step.Order+" : " + step.Description);
            Console.WriteLine("Cooker " +this.Id+" start step "+step.Order+" : " + step.Description);
            this.IsAvailable = false;
            Console.WriteLine("tool and device");
            Console.WriteLine(step.Tools.Count);
            Console.WriteLine(step.Devices.Count);
            // if (!this.LeaseNeededTools(step.Tools)) LogWriter.GetInstance().Write("Ustensiles insuffisants pour réaliser la tâche");
            // else if (!this.LeaseNeededDevices(step.Devices)) LogWriter.GetInstance().Write("Appareils insuffisants pour réaliser la tâche");
            // else
            // {
                LogWriter.GetInstance().Write("Cooker " + this.Id + " working ...");
                Thread.Sleep((int) Math.Round(step.Time*60000)/60);
                LogWriter.GetInstance().Write("Cooker " + this.Id + " step finished");
                this.WasherEngine.AddToolsToWash(step.Tools);
                this.DevicesStorage.ReleaseDevices(step.Devices);
                              
                if (dish != null)
                {
                    if (dish.Recipe.BakeTime == 0)
                    {
                        MoveTo(272, 272);
                        dish.Ready = true;
                        this.OnOrderReady(EventArgs.Empty);

                    }
                    else while (!this.OvenCook.PutInOven(dish)) { }
                }

                this.IsAvailable = true;
            //}
        }

        private bool LeaseNeededTools(List<Tool> tools)
        {
            Console.WriteLine("commencer a lacher ustensile");
            foreach (var item in tools)
            {
                Stopwatch s = new Stopwatch();
                s.Start();
                while (s.Elapsed < TimeSpan.FromSeconds(5))
                {
                    if (this.ToolsStorage.LeaseTool(item))
                    {
                        item.IsAvailable = true;
                        break;
                    }
                }
                s.Stop();
            }
            Console.WriteLine("FIN1");
            return tools.All(tool => tool.IsAvailable);
        }
        
        private bool LeaseNeededDevices(List<Device> devices)
        {
            Console.WriteLine("commencer a lacher Appareil");

            foreach (var item in devices)
            {
                Stopwatch s = new Stopwatch();
                s.Start();
                while (s.Elapsed < TimeSpan.FromSeconds(5))
                {
                    if (this.DevicesStorage.LeaseDevice(item))
                    {
                        item.IsAvailable = true;
                        break;
                    }
                }
                s.Stop();
            }
            Console.WriteLine("FIN2");
            return devices.All(device => device.IsAvailable);
        }

        protected virtual void OnOrderReady(EventArgs e)
        {
            OrderReady?.Invoke(this, e);
        }

    }
}
