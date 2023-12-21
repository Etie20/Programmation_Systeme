using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Ingredients
{
    [Serializable]
    public class Ingredient
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public int Quantity { get; set; }

        public ConservationType TypeConserv { get; set; }
    }
}
