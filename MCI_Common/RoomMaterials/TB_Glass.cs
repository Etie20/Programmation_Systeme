using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.RoomMaterials
{
    public class TB_Glass : TB_RoomMaterial
    {

        public TB_Glass()
        {
            this.DataTable = "GlassView";
        }

 
        public override string GetByType(int idType)
        {
            return "SELECT * FROM " + this.DataTable + " WHERE TypeVerreid = " + idType + ";";
        }
    }
}
