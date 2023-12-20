using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.RoomMaterials
{
    public class GlassProcess : MaterialRoomProcess
    {
        /// <summary>
        /// Create a glass process class
        /// </summary>
        public GlassProcess()
        {
            this.MapTable = new TB_Glass();
            this.Datas = new DataSet();
            this.Bdd = DAO.getInstance();
        }

        /// <summary>
        /// Define the type of the glass according to database information
        /// </summary>
        /// <param name="idType">type id store in database</param>
        /// <returns>A glass type</returns>
        private GlassType DefineTypeGlass(int idType)
        {
            switch (idType)
            {
                case 1:
                    return GlassType.WATER;
                case 2:
                    return GlassType.VINE;
                case 3:
                    return GlassType.FLUTE;
            }
            return GlassType.UNKNOW;

        }

        /// <summary>
        /// Build a glass from datarow
        /// </summary>
        /// <param name="row">Datarow from request</param>
        /// <returns>a glass</returns>
        private Glass CreateGlass(DataRow row)
        {
            Glass glass = new Glass();
            glass.Id = int.Parse(row["Id"].ToString());
            glass.Name = row["Nom"].ToString();
            glass.Quantity = int.Parse(row["Quantite"].ToString());
            glass.Type = this.DefineTypeGlass(int.Parse(row["TypeVerreid"].ToString()));
            return glass;
        }

        /// <summary>
        /// Get all the glass available
        /// </summary>
        /// <returns>List of glass</returns>
        public override List<RoomMaterial> GetAll()
        {
            this.Datas.Clear();
            this.Request = this.MapTable.GetAll();
            this.Datas = this.Bdd.getRows(this.Request, "Glass");

            List<RoomMaterial> results = new List<RoomMaterial>();

            foreach (DataRow item in this.Datas.Tables["Glass"].Rows)
            {
                Glass plate = this.CreateGlass(item);
                results.Add(plate);
            }

            return results;
        }

        /// <summary>
        /// Get a specific type of glass
        /// </summary>
        /// <param name="type">a type of glass</param>
        /// <returns>a glass</returns>
        public Glass GetByType(GlassType type)
        {
            int id = 0;
            switch (type)
            {
                case GlassType.WATER:
                    id = 1;
                    break;
                case GlassType.VINE:
                    id = 2;
                    break;
                case GlassType.FLUTE:
                    id = 3;
                    break;
            }

            this.Datas.Clear();
            this.Request = this.MapTable.GetByType(id);
            this.Datas = this.Bdd.getRows(this.Request, "Glass");

            Glass result = this.CreateGlass(this.Datas.Tables["Glass"].Rows[0]);

            return result;
        }
    }
}
