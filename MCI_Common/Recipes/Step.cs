using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCI_Common.Devices;
using MCI_Common.Ingredients;
using MCI_Common.Tools;

namespace MCI_Common.Recipes
{
    public class Step
    {

        public int Id { get; set; }


        public int Order { get; set; }

  
        public float Time { get; set; }

  
        public string Description { get; set; }


        public List<Tool> Tools { get; set; }


        public List<Device> Devices { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}
