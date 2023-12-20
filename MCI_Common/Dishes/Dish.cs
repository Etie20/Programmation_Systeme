using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCI_Common.Recipes;

namespace MCI_Common.Dishes
{
    public class Dish
    {

        public int Id { get; set; }


        public Recipe Recipe { get; set; }

        public bool Ready { get; set; }


        public Dish()
        {
            this.Ready = false;
        }
    }
}
