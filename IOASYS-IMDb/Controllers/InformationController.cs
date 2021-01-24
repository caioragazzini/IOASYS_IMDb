using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOASYS_IMDb.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [Produces("application/json")]
    [Route("api/{v:apiVersion}/Information")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content("<html><body><h2> Versão - V 1.0 </h2></body></html>", "text/html");
        }




    }
}
