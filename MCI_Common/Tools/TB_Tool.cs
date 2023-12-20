using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Tools
{
    public class TB_Tool
    {
        /// <summary>
        /// Tool to use
        /// </summary>
        public Tool CurrentTool { get; set; }

        /// <summary>
        /// Name of the table in database
        /// </summary>
        private static string DataTable = "Ustensile";

        /// <summary>
        /// Get all rows in table
        /// </summary>
        /// <returns>SQL request</returns>
        public string GetAll()
        {
            return "SELECT * FROM " + DataTable + ";";
        }

        /// <summary>
        /// Get all tools for a given step
        /// </summary>
        /// <returns>SQL request</returns>
        public string GetAllByStep(int id)
        {
            return "SELECT * FROM UstensileParEtape WHERE Etapesid = "+id+";";
        }

        /// <summary>
        /// Get a tool by Id
        /// </summary>
        /// <returns>SQL request</returns>
        public string GetById()
        {
            return "SELECT * FROM " + DataTable + " WHERE Id = " + CurrentTool.Id + ";";
        }

        /// <summary>
        /// Get a tool by Name
        /// </summary>
        /// <returns>SQL request</returns>
        public string GetByName()
        {
            return "SELECT * FROM " + DataTable + " WHERE Nom = '" + CurrentTool.Name + "';";
        }
    }
}
