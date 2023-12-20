using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Dishes
{
    public class Order
    {
        /// <summary>
        /// Id of the command
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// List of dishes ordered
        /// </summary>
        public List<Dish> Dishes { get; set; }

        /// <summary>
        /// The order is readay or not
        /// </summary>
        public bool Ready { get; set; }

        public Order()
        {
            this.Dishes = new List<Dish>();
            this.Ready = false;
        }
    }
}
