using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Devices
{
    public class Device
    {
        /// <summary>
        /// Name of the device
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id of the device
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Quantity available
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Capacity of the device
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Availability of the device
        /// </summary>
        public bool IsAvailable { get; set; }

        public Device()
        {
            this.IsAvailable = false;
        }
    }
}
