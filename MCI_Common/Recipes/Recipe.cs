using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Recipes
{
    public class Recipe 
    {
        /// <summary>
        /// ID of the recipe
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the recipe
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Number of persons can eat this recipe
        /// </summary>
        public int Persons { get; set; }

        /// <summary>
        /// Preparation time of the recipe
        /// </summary>
        public float PrepTime { get; set; }

        /// <summary>
        /// Time to bake the recipe
        /// </summary>
        public float BakeTime { get; set; }

        /// <summary>
        /// Time to wait until you can use the preparation
        /// </summary>
        public float BreakTime { get; set; }

        /// <summary>
        /// Type of the recipe
        /// </summary>  
        public RecipeType Type { get; set; }

        /// <summary>
        /// List of the recipe's steps
        /// </summary>
        public List<Step> Steps { get; set; }

        public Recipe()
        {
            this.BakeTime = 0;
        }

        /*object ICloneable.Clone()
        {
            return this.Clone();
        }

        private Recipe Clone()
        {
            Recipe clone = new Recipe();
            clone.BakeTime = this.BakeTime;
            clone.BreakTime = this.BreakTime;
            clone.Id = this.Id;

        }*/
    }
}
