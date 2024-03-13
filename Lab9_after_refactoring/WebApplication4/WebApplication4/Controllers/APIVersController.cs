using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ClosedXML;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [Route("api/v-{version:apiVersion}/[controller]")]
    public class APIVersController : ControllerBase
    {
        private readonly IAPIVersService _service;

        public APIVersController(IAPIVersService service)
        {
            _service = service;
        }

        [HttpGet("v1")]
        [MapToApiVersion("1.0")]
        [Obsolete("This version is deprecated", true)]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetVersionOne()
        {
            int result = _service.GetRandomInteger();
            return Ok(result);
        }

        [HttpGet("v2")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetVersionTwo()
        {
            Console.WriteLine(_service);
            string result = _service.GetRandomText();
            if (result == null)
            {
                Console.WriteLine("Result is NULL");
            }
            return Ok(result);
        }

        [HttpGet("v3")]
        [MapToApiVersion("3.0")]
        [ProducesResponseType(typeof(byte[]), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetVersionThree()
        {
            byte[] excelFile = _service.GenerateExcelFile();
            return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
        }
    }

}
