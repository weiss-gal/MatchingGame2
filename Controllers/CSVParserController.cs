using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatchingGame2.lib.parsing.CVSParser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchingGame2.Controllers
{
    [Route("api/parser/csv/parse")]
    [ApiController]
    public class CSVParserController : ControllerBase
    {
        CSVParser _csvParser;
     
        public CSVParserController(CSVParser p)
        {
            _csvParser = p;
        }

        // POST: api/CSVParser
        [HttpPost]
        public void Post([FromBody] string value)
        {
            
            var parsedLines = _csvParser.parse(Request.Body, "HTTP Request body", )
        }
    }
}
