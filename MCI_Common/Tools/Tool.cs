using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MCI_Common.Tools
{
    public class Tool
    {
 
        public string Name { get; set; }

        public int Id { get; set; }
        
        public int Quantity { get; set; }


        public bool IsAvailable { get; set; }


        public float WashingTime { get; set; }

        public Tool()
        {
            this.IsAvailable = false;
        }
    }
}
