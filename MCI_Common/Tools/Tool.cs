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
        /// <summary>
        /// Name of the tool
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tool id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Quantity available in stock
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Availability of the tool
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Time to wash the tool
        /// </summary>
        public float WashingTime { get; set; }

        public Tool()
        {
            this.IsAvailable = false;
        }
    }
}
