using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.RoomMaterials
{
    public class TB_Square
    {
        /// <summary>
        /// Ingredient use to interact with database (get and set)
        /// </summary>
        public Square CurrentSquare{ get; set; }

        /// <summary>
        /// Name of the table in database
        /// </summary>
        private static string DataTable = "carres";

        /// <summary>
        /// Get all rows in table
        /// </summary>
        /// <returns>SQL request</returns>
        public string GetAll()
        {
            return "SELECT * FROM " + DataTable + ";";
        }

    }
}
