using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCI_Common.Behaviour;

namespace MCI_Common.RoomMaterials
{
    public class Table
    {
        
        public int Id { get; set; }

        
        public int Numero { get; set; }

        
        public int Capacity { get; set; }

       
        public bool IsAvailable { get; set; }

        
        public int ClientGroupIdAssigned { get; set; }
  
        
        public int BreadBasket { get; set; }

        
        public int Water { get; set; }

        
        public Position TableLocation { get; set; }

        public Table()
        {
            TableLocation = new Position(0,0);
            this.IsAvailable = true;
        }
        
    }
}
