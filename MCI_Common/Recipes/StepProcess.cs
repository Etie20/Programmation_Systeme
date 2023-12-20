using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCI_Common.Devices;
using MCI_Common.Ingredients;
using MCI_Common.Tools;

namespace MCI_Common.Recipes
{
    public class StepProcess
    {
        /// <summary>
        /// Table mapped for build request SQL
        /// </summary>
        public TB_Step MapTable { get; private set; }

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
        /// Create a device process class
        /// </summary>
        public StepProcess()
        {
            this.MapTable = new TB_Step();
            this.Datas = new DataSet();
            this.Bdd = DAO.getInstance();
        }

        /// <summary>
        /// Build a Step from datarow
        /// </summary>
        /// <param name="row">Datarow from dataset</param>
        /// <returns>a step</returns>
        private Step CreateStep(DataRow row)
        {
            Step step = new Step();
            step.Order = int.Parse(row["Ordre"].ToString());
            step.Time = float.Parse(row["Temps"].ToString());
            step.Id = int.Parse(row["Id"].ToString());
            step.Description = row["Description"].ToString();
            step.Tools = new ToolProcess().ListAllByStep(step.Id);
            step.Devices = new DeviceProcess().ListAllByStep(step.Id);
            step.Ingredients = new IngredientProcess().ListAllByStep(step.Id);
            return step;
        }

        /// <summary>
        /// Get a specific step
        /// </summary>
        /// <param name="id">id of the step to get</param>
        /// <returns>A specific step</returns>
        public Step GetOne(int id)
        {
            this.Datas.Clear();
            this.MapTable.CurrentStep = new Step();
            this.MapTable.CurrentStep.Id = id;
            this.Request = this.MapTable.GetById();
            this.Datas = this.Bdd.getRows(this.Request, "Step");

            Step result = this.CreateStep(this.Datas.Tables["Step"].Rows[0]);

            return result;
        }

        /// <summary>
        /// Get a the steps list for a recipe
        /// </summary>
        /// <param name="id">id of the recipe</param>
        /// <returns>A recipe steps list</returns>
        public List<Step> ListAllByRecipe(int id)
        {
            this.Datas.Clear();
            this.Request = this.MapTable.GetAllByRecipe(id);
            this.Datas = this.Bdd.getRows(this.Request, "Steps").Copy();

            List<Step> results = new List<Step>();

            foreach (DataRow item in this.Datas.Tables["Steps"].Rows)
            {
                Step step = this.CreateStep(item);
                results.Add(step);
            }

            return results;
        }

    }
}
