using APISolution3;
using IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Loginservice : Ilogin
    {
        private readonly IDAL _Dal;
        private readonly IConfiguration _config;


        public Loginservice(IDAL dal, IConfiguration config)
        {
            _Dal = dal;
            _config = config;
        }

        public async Task<List<MstUser>> Validateuser(string username, string password)
        {
           List<MstUser> users = new List<MstUser>();
            SqlCommand cmd= new SqlCommand("Getuserdetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@loginID", username);
            cmd.Parameters.AddWithValue("@password", password);
            DataTable dt=_Dal.GetData(cmd);
            users = dt.AsEnumerable().Select(row => new MstUser
            {
                LoginId = row.Table.Columns.Contains("LoginId") && !row.IsNull("LoginId") ? row.Field<string>("LoginId") : "",
                Password = row.Table.Columns.Contains("Password") && !row.IsNull("Password") ? row.Field<string>("Password") : "",
                Name = row.Table.Columns.Contains("Name") && !row.IsNull("Name") ? row.Field<string>("Name") : "",
                Phone = row.Table.Columns.Contains("Phone") && !row.IsNull("Phone") ? row.Field<string>("Phone") : "",
                EmailId = row.Table.Columns.Contains("EmailId") && !row.IsNull("EmailId") ? row.Field<string>("EmailId") : "",

            }).ToList();

            return users;
        }
    }
}
