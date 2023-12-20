using MCI_Common.Behaviour;
using MCI_Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimulationKitchen.Model
{
    public class Washer : Movable
    {
        /// <summary>
        /// List of the tools to wash
        /// </summary>
        public List<Tool> ToolsToWash { get; private set; }

        /// <summary>
        /// Lock for the tools to wash list
        /// </summary>
        private object LockToolsToWashList = new object();

        public Washer()
        {
            this.ToolsToWash = new List<Tool>();
        }

        private void WashTool(Tool tool)
        {
            //MoveTo(304, 80);
            LogWriter.GetInstance().Write("Washer start washing tool " + tool.Name);
            Thread.Sleep((int)Math.Round(tool.WashingTime * 6000));
            LogWriter.GetInstance().Write("Washer finished washing tool " + tool.Name);
            this.ToolsToWash.Remove(tool);
            ToolsManager.GetInstance().ReleaseTool(tool);
        }

        public Thread StartWorking()
        {
            return new Thread(() =>
            {
                LogWriter.GetInstance().Write("Washer start working");
                while (true)
                {
                    lock (LockToolsToWashList)
                    {
                        List<Tool> tools = new List<Tool>(this.ToolsToWash);

                        if (tools.Count() > 0)
                        {
                            foreach (var item in tools)
                            {
                                this.WashTool(item);
                            }
                        }
                    }
                    Thread.Sleep(500);
                }
            });
        }

        public void AddToolsToWash(List<Tool> tools)
        {
            lock (LockToolsToWashList)
            {
                foreach (var item in tools)
                {
                    this.ToolsToWash.Add(item);
                }
            }
            
        }
    }
}
