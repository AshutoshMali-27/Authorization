using APISolution3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface Ilogin
    {
        Task<List<MstUser>> Validateuser(string username,string password) ;
    }
}
