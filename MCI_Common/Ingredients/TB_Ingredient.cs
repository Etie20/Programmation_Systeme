using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Ingredients
{
    public class TB_Ingredient
    {
       
        public Ingredient CurrentIngredient { get; set; }

        private static string DataTable = "Denree";
        
        public string GetAll()
        {
            return "SELECT * FROM " + DataTable + ";";
        }

        public string GetById()
        {
            return "SELECT * FROM " + DataTable + " WHERE Id = " + CurrentIngredient.Id + ";";
        }

        public string GetByName()
        {
            return "SELECT * FROM " + DataTable + " WHERE Nom = '" + CurrentIngredient.Name + "';";
        }

       
        public string GetAllByStep(int id)
        {
            return "SELECT * FROM DenreeParEtape WHERE Etapeid = " + id + ";";
        }

        public string AddStock()
        {
            return "UPDATE " + DataTable + "SET Quantite = Quantite + "+CurrentIngredient.Quantity+" WHERE Id = '" + CurrentIngredient.Id + "';";
        }

        public string RemoveStock()
        {
            return "UPDATE " + DataTable + "SET Quantite = Quantite - " + CurrentIngredient.Quantity + " WHERE Id = '" + CurrentIngredient.Id + "';";
        }
    }
}
