using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.RoomMaterials
{
    public class PlateProcess : MaterialRoomProcess
    {
        
        public PlateProcess()
        {
            this.MapTable = new TB_Plate();
            this.Datas = new DataSet();
            this.Bdd = DAO.getInstance();
        }

    
        private PlateType DefineTypePlate(int idType)
        {
            switch (idType)
            {
                case 1:
                    return PlateType.STARTER;
                case 2:
                    return PlateType.BOWL;
                case 3:
                    return PlateType.DINNER;
                case 4:
                    return PlateType.DESSERT;
            }
            return PlateType.UNKNOW;

        }

  
        private Plate CreatePlate(DataRow row)
        {
            Plate plate = new Plate();
            plate.Id = int.Parse(row["Id"].ToString());
            plate.Name = row["Nom"].ToString();
            plate.Quantity = int.Parse(row["Quantite"].ToString());
            plate.Type = this.DefineTypePlate(int.Parse(row["TypeAssietteid"].ToString()));
            return plate;
        }

        public override List<RoomMaterial> GetAll()
        {
            this.Datas.Clear();
            this.Request = this.MapTable.GetAll();
            this.Datas = this.Bdd.getRows(this.Request, "Plates");

            List<RoomMaterial> results = new List<RoomMaterial>();

            foreach (DataRow item in this.Datas.Tables["Plates"].Rows)
            {
                Plate plate = this.CreatePlate(item);
                results.Add(plate);
            }

            return results;
        }


        public Plate GetByType(PlateType type)
        {
            int id = 0;
            switch (type)
            {
                case PlateType.STARTER:
                    id = 1;
                    break;
                case PlateType.BOWL:
                    id = 2;
                    break;
                case PlateType.DINNER:
                    id = 3;
                    break;
                case PlateType.DESSERT:
                    id = 4;
                    break;
            }

            this.Datas.Clear();
            this.Request = this.MapTable.GetByType(id);
            this.Datas = this.Bdd.getRows(this.Request, "Plates");

            Plate result = this.CreatePlate(this.Datas.Tables["Plates"].Rows[0]);

            return result;
        }
    }
}
