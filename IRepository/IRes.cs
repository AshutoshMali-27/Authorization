using APISolution3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IRes
    {
        Task<List<MstCity>> GetCity();
        Task<List<MstUser>> getUser();
        Task<bool> setuser(MstUser user);
    }
}
