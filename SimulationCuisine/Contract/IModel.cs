using MCI_Common.Recipes;
using SimulationKitchen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationKitchen.Contract
{
    public interface IModel
    {
        List<Recipe> Menu { get; }

        List<Cooker> Cookers { get; }

        void Start();

    }
}
