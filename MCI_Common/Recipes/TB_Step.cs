using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Recipes
{
    public class TB_Step
    {
 
        public Step CurrentStep { get; set; }

        private static string DataTable = "Etape";

   
        public string GetById()
        {
            return "SELECT * FROM " + DataTable + " WHERE Id = " + CurrentStep.Id + ";";
        }


        public string GetAllByRecipe(int id)
        {
            return "SELECT * FROM Etape WHERE Recetteid = " + id + ";";
        }
    }
}
