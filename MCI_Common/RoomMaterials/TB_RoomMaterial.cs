using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.RoomMaterials
{
    public abstract class TB_RoomMaterial
    {

        public RoomMaterial CurrentRoomMaterial { get; set; }
        
        public string DataTable { get; set; }


        public string GetAll()
        {
            return "SELECT * FROM " + DataTable + ";";
        }


        abstract public string GetByType(int idType);
    }
}
