using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Ingredients
{
    public class IngredientProcess
    {
        /// <summary>
        /// Table mapped for build request SQL
        /// </summary>
        public TB_Ingredient MapTable { get; private set; }

        /// <summary>
        /// BDD connection object
        /// </summary>
        public DAO Bdd { get; private set; }

        /// <summary>
        /// SQL request
        /// </summary>
        public string Request { get; private set; }

        /// <summary>
        /// DataSet to get request results
        /// </summary>
        public DataSet Datas { get; private set; }

        /// <summary>
        /// Create an ingredient process class
        /// </summary>
        public IngredientProcess()
        {
            this.MapTable = new TB_Ingredient();
            this.Datas = new DataSet();
            this.Bdd = DAO.getInstance();
        }

        /// <summary>
        /// Build an ingredient from datarow
        /// </summary>
        /// <param name="row">DataRow from dataset</param>
        /// <returns>an Ingredient</returns>
        private Ingredient CreateIngredient(DataRow row)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.Name = row["Nom"].ToString();
            ingredient.Quantity = int.Parse(row["Quantite"].ToString());
            ingredient.Id = int.Parse(row["Id"].ToString());

            switch (int.Parse(row["TypeStockageid"].ToString()))
            {
                case 1:
                    ingredient.TypeConserv = ConservationType.AMBIANT;
                    break;
                case 2:
                    ingredient.TypeConserv = ConservationType.COLD;
                    break;
                case 3:
                    ingredient.TypeConserv = ConservationType.FREEZE;
                    break;
            }
            return ingredient;
        }

        /// <summary>
        /// List all tools available in database
        /// </summary>
        /// <returns>A list of all tools</returns>
        public List<Ingredient> ListAll()
        {
            this.Datas.Clear();
            this.Request = this.MapTable.GetAll();
            this.Datas = this.Bdd.getRows(this.Request, "Ingredients");

            List<Ingredient> results = new List<Ingredient>();

            foreach (DataRow item in this.Datas.Tables["Ingredients"].Rows)
            {
                Ingredient ingredient = this.CreateIngredient(item);
                results.Add(ingredient);
            }

            return results;
        }

        /// <summary>
        /// Get a specific ingredients list for a step
        /// </summary>
        /// <param name="id">id of the step</param>
        /// <returns>A specific ingredients list</returns>
        public List<Ingredient> ListAllByStep(int id)
        {
            this.Datas.Clear();
            this.Request = this.MapTable.GetAllByStep(id);
            this.Datas = this.Bdd.getRows(this.Request, "Ingredient");

            List<Ingredient> results = new List<Ingredient>();

            foreach (DataRow item in this.Datas.Tables["Ingredient"].Rows)
            {
                Ingredient device = this.CreateIngredient(item);
                results.Add(device);
            }

            return results;
        }

        /// <summary>
        /// Get a specific ingredient
        /// </summary>
        /// <param name="id">id of the ingredient to get</param>
        /// <returns>A specific ingredient</returns>
        public Ingredient GetOne(int id)
        {
            this.Datas.Clear();
            this.MapTable.CurrentIngredient = new Ingredient();
            this.MapTable.CurrentIngredient.Id = id;
            this.Request = this.MapTable.GetById();
            this.Datas = this.Bdd.getRows(this.Request, "Ingredient");

            Ingredient ingredient = this.CreateIngredient(this.Datas.Tables["Ingredient"].Rows[0]);

            return ingredient;
        }

        /// <summary>
        /// Get a specific ingredient
        /// </summary>
        /// <param name="name">Name of the ingredient to get</param>
        /// <returns>A specific ingredient</returns>
        public Ingredient GetOne(string name)
        {
            this.Datas.Clear();
            this.MapTable.CurrentIngredient = new Ingredient();
            this.MapTable.CurrentIngredient.Name = name;
            this.Request = this.MapTable.GetByName();
            this.Datas = this.Bdd.getRows(this.Request, "Ingredients");

            Ingredient ingredient = this.CreateIngredient(this.Datas.Tables["Ingredient"].Rows[0]);

            return ingredient;
        }
    }
}
