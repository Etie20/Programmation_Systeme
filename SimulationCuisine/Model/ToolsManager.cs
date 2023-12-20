using MCI_Common.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationKitchen.Model
{
    public class ToolsManager
    {
        private static ToolsManager Instance = null;

        public Hashtable LeasedTools { get; set; }
        public Hashtable AvailableTools { get; set; }

        private readonly object LockLeasing = new object();
        private readonly object LockReleasing = new object();

        private ToolsManager()
        {
            this.LeasedTools = new Hashtable();
            this.AvailableTools = new Hashtable();
            this.InitToolsManager();
        }

        private void InitToolsManager()
        {
            List<Tool> allTools = new ToolProcess().ListAll();
            foreach (var item in allTools)
            {
                AvailableTools.Add(item.Id,item.Quantity);
                LeasedTools.Add(item.Id, 0);
            }

        }

        public static ToolsManager GetInstance()
        {
            if (Instance == null) Instance = new ToolsManager();
            return Instance;
        }

        public bool LeaseTool(Tool tool)
        {
            lock (LockLeasing)
            {
                Console.WriteLine(tool.Name);
                if ((int)this.LeasedTools[tool.Id] < (int)this.AvailableTools[tool.Id])
                {
                    this.LeasedTools[tool.Id] = (int)this.LeasedTools[tool.Id] + 1;
                    return true;
                }
                return false;
            }
        }

        public void ReleaseTool(Tool tool)
        {
            lock (LockReleasing)
            {
                this.LeasedTools[tool.Id] = (int)this.LeasedTools[tool.Id] - 1;
            }
        }
    }
}
