using MCI_Common.RoomMaterials;
using Room.Model.Client;
using Room.Model.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationRestaurant.Interfaces
{
    public interface IModel
    {
        List<Table> ListTables { get; }

        List<ClientGroup> Clients { get;  }

        List<RankChief> Rankchiefs { get; }

        List<Server> Servers { get; }

        RoomMaster Master { get;  }

        event EventHandler Change;

        void Start();
    }
}
