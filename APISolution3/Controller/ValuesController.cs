using IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISolution3.Controller
{
    [Authorize]
    [Route("api/[controller]")]

    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IRes _res;

        public ValuesController(IConfiguration configuration, IRes res)
        {
            _configuration = configuration;
            _res = res;
        }

        [HttpGet("getcity")]
        public async Task<IActionResult> getcity()
        {
            var cities=await _res.GetCity();
            return Ok(cities);
        }


        [HttpGet("getuser")]
        public async Task<IActionResult> getUser()
        {
            var cities = await _res.getUser();
            return Ok(cities);
        }


        [HttpPost("setuser")]
        public async Task<IActionResult> Setuser(MstUser user)
        {
           await _res.setuser(user);
            return Ok();
        }
    }
}
