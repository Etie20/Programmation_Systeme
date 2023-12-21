using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Tools
{
    public class ToolProcess
    {

        public TB_Tool MapTable { get; private set; }

        public DAO Bdd { get; private set; }


        public string Request { get; private set; }


        public DataSet Datas { get; private set; }

        public ToolProcess()
        {
            this.MapTable = new TB_Tool();
            this.Datas = new DataSet();
            this.Bdd = DAO.getInstance();
        }

        private Tool CreateTool(DataRow row)
        {
            Tool tool = new Tool();
            tool.Name = row["Nom"].ToString();
            tool.Quantity = int.Parse(row["Quantite"].ToString());
            tool.Id = int.Parse(row["Id"].ToString());
            tool.WashingTime = float.Parse(row["TempsNettoyage"].ToString());

            return tool;
        }

        public List<Tool> ListAll()
        {
            this.Datas.Clear();
            this.Request = this.MapTable.GetAll();
            this.Datas = this.Bdd.getRows(this.Request, "Tools");

            List<Tool> results = new List<Tool>();

            foreach (DataRow item in this.Datas.Tables["Tools"].Rows)
            {
                Tool tool = this.CreateTool(item);
                results.Add(tool);
                
            }
            return results;
        }

       
        public List<Tool> ListAllByStep(int id)
        {
            this.Datas.Clear();
            this.Request = this.MapTable.GetAllByStep(id);
            this.Datas = this.Bdd.getRows(this.Request, "Tools");

            List<Tool> results = new List<Tool>();

            foreach (DataRow item in this.Datas.Tables["Tools"].Rows)
            {
                Console.WriteLine(item[1]);
                Tool tool = this.CreateTool(item);
                results.Add(tool);
            }

            return results;
        }


        public Tool GetOne(string name)
        {
            this.Datas.Clear();
            this.MapTable.CurrentTool = new Tool();
            this.MapTable.CurrentTool.Name = name;
            this.Request = this.MapTable.GetByName();
            this.Datas = this.Bdd.getRows(this.Request, "Tools");

            Tool result = this.CreateTool(this.Datas.Tables["Tools"].Rows[0]);

            return result;
        }
    }
}
