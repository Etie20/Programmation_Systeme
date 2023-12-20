using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.RoomMaterials
{
    public class TB_Table
    {

        public Table CurrentTable { get; set; }

        private static string DataTable = "tables";

        public string GetAll(int square)
        {
            return "SELECT * FROM " + DataTable + " WHERE rangs_id = "+square+";";
        }
    }
}
