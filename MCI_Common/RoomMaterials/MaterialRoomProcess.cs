using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.RoomMaterials
{
    public abstract class MaterialRoomProcess
    {

        protected TB_RoomMaterial MapTable { get; set; }
        
        protected DAO Bdd { get; set; }

  
        protected string Request { get; set; }


        protected DataSet Datas { get; set; }

        public abstract List<RoomMaterial> GetAll();
    }
}
