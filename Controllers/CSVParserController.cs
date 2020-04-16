using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchingGame2.Controllers
{
    [Route("api/parser/csv/parse")]
    [ApiController]
    public class CSVParserController : ControllerBase
    {
     
        // POST: api/CSVParser
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }
    }
}
