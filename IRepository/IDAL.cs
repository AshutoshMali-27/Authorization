using Microsoft.Data.SqlClient;
using System.Data;

namespace IRepository
{
    public interface IDAL
    {

        public DataTable GetData(SqlCommand cmd);
        public bool Execute(SqlCommand cmd);
    }
}
