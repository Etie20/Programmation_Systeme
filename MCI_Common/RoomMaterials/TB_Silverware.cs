using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.RoomMaterials
{
    public class TB_Silverware : TB_RoomMaterial
    {
        
        public TB_Silverware()
        {
            this.DataTable = "CouvertsView";
        }


        public override string GetByType(int idType)
        {
            return "SELECT * FROM " + this.DataTable + " WHERE TypeCouvertid = " + idType + ";";
        }
    }
}
