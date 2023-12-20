using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.RoomMaterials
{
    public abstract class RoomMaterial
    {
        
        public int Id { get; set; }

       
        public string Name { get; set; }

        
        public int Quantity { get; set; }
    }
}
