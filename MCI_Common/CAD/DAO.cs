using System;
using System.Data;
using System.Data.SqlClient;

namespace MCI_Common
{
    public class DAO
    {
        private static DAO daoObj;

        private string cnx;

        private SqlDataAdapter adapter;
        
        private SqlConnection bddConnexion;

        private SqlCommand cmd;
        
        private DataSet data;
        
        private DAO()
        {
            this.cnx = "Server=localhost;Database=projetSysteme;Trusted_Connection=True;";
            this.bddConnexion = new SqlConnection(this.cnx);
            this.adapter = new SqlDataAdapter();
            this.cmd = new SqlCommand();
            this.data = new DataSet();
        }

        public static DAO getInstance()
        {
            if (daoObj == null)
            {
                daoObj = new DAO();
            }
            return daoObj;
        }

        public void actionRows(string request)
        {
            this.cmd.CommandText = request;
            int lines = 0;
            try
            {
                this.bddConnexion.Open();
                lines = this.cmd.ExecuteNonQuery();
                Console.WriteLine("{0} lines updated", lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query insert/update/delete not executed, stack trace\n" + ex.Message);
            }
            finally
            {
                this.bddConnexion.Close();
            }
        }

        public DataSet getRows(string request, string dataTableName)
        {
            this.data.Clear();
            this.cmd.CommandText = request;
            adapter.SelectCommand = this.cmd;
            adapter.SelectCommand.Connection = this.bddConnexion;

            try
            {
                adapter.Fill(this.data, dataTableName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query select not executed, stack trace\n" + ex.Message);
            }
            return this.data;
        }

    }
}
