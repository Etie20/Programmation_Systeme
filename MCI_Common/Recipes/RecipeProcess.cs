using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Recipes
{
    public class RecipeProcess
    {

        public TB_Recipe MapTable { get; private set; }


        public DAO Bdd { get; private set; }


        public string Request { get; private set; }

        
        public DataSet Datas { get; private set; }

        
        public RecipeProcess()
        {
            this.MapTable = new TB_Recipe();
            this.Datas = new DataSet();
            this.Bdd = DAO.getInstance();
        }

    
        private RecipeType DefineRecipeType(int Typeid)
        {
            switch (Typeid)
            {
                case 1:
                    return RecipeType.STARTER;
                case 2:
                    return RecipeType.MAIN;
                case 3:
                    return RecipeType.DESSERT;
            }
            return RecipeType.UNKNOWN;
        }

  
        private Recipe CreateRecipe(DataRow row)
        {
            Recipe recipe = new Recipe();
            recipe.Name = row["Nom"].ToString();
            recipe.BreakTime = int.Parse(row["Temps_Pause"].ToString());
            recipe.Id = int.Parse(row["Id"].ToString());
            recipe.Persons = int.Parse(row["Personnes"].ToString());
            recipe.PrepTime = float.Parse(row["Temps_Prep"].ToString());
            recipe.BakeTime = float.Parse(row["TempsCuisson"].ToString());
            recipe.Type = this.DefineRecipeType(int.Parse(row["TypeRecetteid"].ToString()));
            recipe.Steps = new StepProcess().ListAllByRecipe(recipe.Id);
            
            return recipe;
        }


        public List<Recipe> GetAll()
        {
            this.Datas.Clear();
            this.Request = this.MapTable.GetAll();
            this.Datas = this.Bdd.getRows(this.Request, "Recipe").Copy();

            List<Recipe> results = new List<Recipe>();

            foreach (DataRow item in this.Datas.Tables["Recipe"].Rows)
            {
                Recipe recipe = this.CreateRecipe(item);
                results.Add(recipe);
            }

            return results;
        }

        public List<Recipe> GetByRecipeType(int RecipeType)
        {
            this.Datas.Clear();
            this.Request = this.MapTable.GetByRecipeType(RecipeType);
            this.Datas = this.Bdd.getRows(this.Request, "Recipe").Copy();

            List<Recipe> results = new List<Recipe>();

            foreach (DataRow item in this.Datas.Tables["Recipe"].Rows)
            {
                Recipe recipe = this.CreateRecipe(item);
                results.Add(recipe);
            }

            return results;
        }

  
        public Recipe GetOne(int id)
        {
            this.Datas.Clear();
            this.MapTable.CurrentRecipe = new Recipe();
            this.MapTable.CurrentRecipe.Id = id;
            this.Request = this.MapTable.GetById();
            this.Datas = this.Bdd.getRows(this.Request, "Recipe");

            Recipe result = this.CreateRecipe(this.Datas.Tables["Recipe"].Rows[0]);

            return result;
        }


        public Recipe GetOne(string name)
        {
            this.Datas.Clear();
            this.MapTable.CurrentRecipe = new Recipe();
            this.MapTable.CurrentRecipe.Name = name;
            this.Request = this.MapTable.GetByName();
            this.Datas = this.Bdd.getRows(this.Request, "Recipe");

            Recipe result = this.CreateRecipe(this.Datas.Tables["Recipe"].Rows[0]);

            return result;
        }
    }
}
