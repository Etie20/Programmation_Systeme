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

        public TB_Step MapTable { get; private set; }


        public DAO Bdd { get; private set; }


        public string Request { get; private set; }


        public DataSet Datas { get; private set; }


        public StepProcess()
        {
            this.MapTable = new TB_Step();
            this.Datas = new DataSet();
            this.Bdd = DAO.getInstance();
        }

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
