using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.RoomMaterials
{
    public class SquareProcess
    {
        
        public TB_Square MapTableSquare { get; set; }

        
        public TB_Table MapTableTable { get; set; }

        
        public DAO Bdd { get; set; }

        
        public string Request { get; set; }

       
        public DataSet DatasTable { get; set; }

        
        public DataSet DatasSquare { get; set; }

        
        public SquareProcess()
        {
            this.MapTableSquare = new TB_Square();
            this.MapTableTable = new TB_Table();
            this.DatasSquare = new DataSet();
            this.DatasTable = new DataSet();
            this.Bdd = DAO.getInstance();
        }

        private Table CreateTable(DataRow row)
        {
            Table table = new Table();
            table.Id = int.Parse(row["id"].ToString());
            table.Numero = int.Parse(row["numero"].ToString());
            table.Capacity = int.Parse(row["capacite"].ToString());
            return table;
        }
        
        private Square CreateSquare(DataRow row)
        {
            Square square = new Square();
            square.Id = int.Parse(row["id"].ToString());
            square.Tables = this.GetTables(square.Id);
            return square;
        }
        
        public List<Square> GetAll()
        {
            this.DatasSquare.Clear();
            this.Request = this.MapTableSquare.GetAll();
            this.DatasSquare = this.Bdd.getRows(this.Request, "carres").Copy();
            List<Square> results = new List<Square>();
            foreach (DataRow item in this.DatasSquare.Tables["carres"].Rows)
            {
                Square square = this.CreateSquare(item);
                results.Add(square);
            }
            return results;
        }

       
        private List<Table> GetTables(int idSquare)
        {
            this.DatasTable = new DataSet();
            this.Request = this.MapTableTable.GetAll(idSquare);
            this.DatasTable = this.Bdd.getRows(this.Request, "tables");

            List<Table> results = new List<Table>();

            foreach (DataRow item in this.DatasTable.Tables["tables"].Rows)
            {
                Table table = this.CreateTable(item);
                results.Add(table);
            }

            return results;
        }

    }
}
