using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.RoomMaterials
{
    public class SilverwareProcess : MaterialRoomProcess
    {

        public SilverwareProcess()
        {
            this.MapTable = new TB_Silverware();
            this.Datas = new DataSet();
            this.Bdd = DAO.getInstance();
        }
 
        private SilverwareType DefineTypeSilverware(int idType)
        {
            switch (idType)
            {
                case 1:
                    return SilverwareType.KNIFE;
                case 2:
                    return SilverwareType.FORK;
                case 3:
                    return SilverwareType.DESSERTSPOON;
                case 4:
                    return SilverwareType.TEASPOON;
            }
            return SilverwareType.UNKNOW;

        }


        private Silverware CreateSilverware(DataRow row)
        {
            Silverware silverware = new Silverware();
            silverware.Id = int.Parse(row["Id"].ToString());
            silverware.Name = row["Nom"].ToString();
            silverware.Quantity = int.Parse(row["Quantite"].ToString());
            silverware.Type = this.DefineTypeSilverware(int.Parse(row["TypeCouvertid"].ToString()));
            return silverware;
        }

        public override List<RoomMaterial> GetAll()
        {
            this.Datas.Clear();
            this.Request = this.MapTable.GetAll();
            this.Datas = this.Bdd.getRows(this.Request, "Silverware");

            List<RoomMaterial> results = new List<RoomMaterial>();

            foreach (DataRow item in this.Datas.Tables["Silverware"].Rows)
            {
                Silverware plate = this.CreateSilverware(item);
                results.Add(plate);
            }

            return results;
        }


        public Silverware GetByType(SilverwareType type)
        {
            int id = 0;
            switch (type)
            {
                case SilverwareType.KNIFE:
                    id = 1;
                    break;
                case SilverwareType.FORK:
                    id = 2;
                    break;
                case SilverwareType.DESSERTSPOON:
                    id = 3;
                    break;
                case SilverwareType.TEASPOON:
                    id = 4;
                    break;
            }

            this.Datas.Clear();
            this.Request = this.MapTable.GetByType(id);
            this.Datas = this.Bdd.getRows(this.Request, "Silverware");

            Silverware result = this.CreateSilverware(this.Datas.Tables["Silverware"].Rows[0]);

            return result;
        }
    }
}

