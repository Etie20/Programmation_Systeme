using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Devices
{
    public class TB_Device
    {

        public Device CurrentDevice { get; set; }


        private static string DataTable = "Appareil";

    
        public string GetAll()
        {
            return "SELECT * FROM "+DataTable+";";
        }
        public string GetById()
        {
            return "SELECT * FROM "+DataTable+" WHERE Id = "+CurrentDevice.Id+";";
        }
        
        public string GetByName()
        {
            return "SELECT * FROM " + DataTable + " WHERE Nom = '" + CurrentDevice.Name + "';";
        }
        
        public string GetAllByStep(int id)
        {
            return "SELECT * FROM Etape_Appareil WHERE Etapeid = " + id + ";";
        }

    }
}
