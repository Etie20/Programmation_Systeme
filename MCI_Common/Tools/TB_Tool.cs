using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Tools
{
    public class TB_Tool
    {

        public Tool CurrentTool { get; set; }


        private static string DataTable = "Ustensile";

  
        public string GetAll()
        {
            return "SELECT * FROM " + DataTable + ";";
        }


        public string GetAllByStep(int id)
        {
            return "SELECT * FROM EtapesUstensiles WHERE Etapesid = "+id+";";
        }


        public string GetById()
        {
            return "SELECT * FROM " + DataTable + " WHERE Id = " + CurrentTool.Id + ";";
        }

        public string GetByName()
        {
            return "SELECT * FROM " + DataTable + " WHERE Nom = '" + CurrentTool.Name + "';";
        }
    }
}
