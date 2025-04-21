using APISolution3;
using IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClsResult:IRes
    {
        private readonly IDAL Dal;
        private readonly IConfiguration configuration;


        public ClsResult(IDAL _Dal,IConfiguration _config) {
        
            this.Dal = _Dal;
            this.configuration = _config;
        }

        public async Task<List<MstCity>> GetCity()
        {
            List<MstCity> mstCities = new List<MstCity>();
            SqlCommand cmd = new SqlCommand("SP_GetCity");
            cmd.CommandType=CommandType.StoredProcedure;
            DataTable dt = this.Dal.GetData(cmd);
            mstCities = dt.AsEnumerable().Select(row => new MstCity
            {
                Id=row.Table.Columns.Contains("Id")&& !row.IsNull("Id")? row.Field<int>("Id") :0,
                City = row.Table.Columns.Contains("city") && !row.IsNull("City") ? row.Field<string>("City") : "",
                CountryCode = row.Table.Columns.Contains("CountryCode") && !row.IsNull("CountryCode") ? row.Field<string>("CountryCode") : "",
                CityCode = row.Table.Columns.Contains("CityCode") && !row.IsNull("CityCode") ? row.Field<string>("CityCode") : ""

            }).ToList();


            return mstCities;
            
        }

        public async Task<List<MstUser>> getUser()
        {
            List<MstUser> Rsp=new List<MstUser>();
            SqlCommand cmd = new SqlCommand("getuser");
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt=this.Dal.GetData(cmd);
            Rsp = dt.AsEnumerable().Select(row => new MstUser {
            
                UserId=row.Table.Columns.Contains("UserId") && !row.IsNull("UserId") ? row.Field<int>("UserId") :0,
                Name=row.Table.Columns.Contains("Name") && !row.IsNull("Name") ? row.Field<string>("Name") :"",
                EmailId=row.Table.Columns.Contains("EmailId") && !row.IsNull("EmailId") ? row.Field<string>("EmailId"):"",
                Phone=row.Table.Columns.Contains("Phone") && !row.IsNull("Phone")? row.Field<string>("Phone"):"",
                Role=row.Table.Columns.Contains("role") && !row.IsNull("role") ? row.Field<string>("role"):""
            
            
            } 
            ).ToList();

            return Rsp;
        }

        public async Task<bool> setuser(MstUser user)
        {
            bool result=false;

            SqlCommand cmd = new SqlCommand("setuserDetails");
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LoginID", user.LoginId);
            cmd.Parameters.AddWithValue("@password", user.Name);
            cmd.Parameters.AddWithValue("@Name", user.Password);
            cmd.Parameters.AddWithValue("@phone", user.Phone);
            cmd.Parameters.AddWithValue("@EmailId", user.EmailId);
            cmd.Parameters.AddWithValue("@Role", user.Role);
            this.Dal.Execute(cmd);
            result = true;

            return result;

        }
    }
}
