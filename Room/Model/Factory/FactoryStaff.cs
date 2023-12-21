using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Model.Factory
{
    public class FactoryStaff
    {
        private static FactoryStaff instance = null;
        private static readonly object padlock = new object();

        public 

        FactoryStaff()
        {

        }

        public static void GetFactory()
        {
            int RankChiefNumber = 1;
            int RoomMasterNumber = 1;
            int ServerNumber = 1;

            while (RoomMasterNumber != 0)
            {
                RoomMasterNumber--;
                new FactoryRoomMaster();
            }
            while (RankChiefNumber != 0)
            {
                RankChiefNumber--;
            }
            while (ServerNumber != 0)
            {
                ServerNumber--;
                new FactoryServer();
            }
            
        }
    }
}
