using IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DAL
{
    public class ClsDAL:IDAL
    {
        private readonly IConfiguration configuration;

        public ClsDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string getconnectionstring()
        {
            return configuration.GetConnectionString("DefaultConnection");
        }

        public bool Execute(SqlCommand cmd)
        {
            bool result = false;
            string connectionstring=getconnectionstring();
            SqlConnection con=new SqlConnection(connectionstring);
            try {
                if(ConnectionState.Open !=con.State)
                {
                    con.Open();
                }
                cmd.Connection=con;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            
            }
            catch (Exception ex) { 
            
            }
            finally {
                cmd.Dispose();
                if (ConnectionState.Open != con.State)
                {

                    con.Close();
                }

            }

            return result;

        }

       

        public DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            string connecttionstring=getconnectionstring();
            SqlConnection conn = new SqlConnection(connecttionstring);

            try
            {
                if(ConnectionState.Open != conn.State) {

                    conn.Open();
                }
                cmd.Connection = conn;
                SqlDataReader rdr = cmd.ExecuteReader();
                if(rdr.HasRows) {

                    dt.Load(rdr);
                }
               

            }catch(Exception ex) {
            
            }
            finally
            {
                cmd.Dispose();
                if (ConnectionState.Open != conn.State)
                {

                    conn.Close();
                }
            }
            return dt;
        }
    }
}
