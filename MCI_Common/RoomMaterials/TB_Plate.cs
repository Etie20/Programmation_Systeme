using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.RoomMaterials
{
    public class TB_Plate : TB_RoomMaterial
    {
       
        public TB_Plate()
        {
            this.DataTable = "Assiettes";
        }

   
        public override string GetByType(int idType)
        {
            return "SELECT * FROM "+this.DataTable+" WHERE TypeAssietteid = "+idType+";";
        }
    }
}
