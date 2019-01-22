using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shitou.Framework.Demo.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public ApiResponse<string[]> Get()
        {
            ApiResponse<string[]> response = new ApiResponse<string[]>();
            response.MsgCode = "10000";
            response.MsgContent = "suceess";
            response.ResponseData = new string[] { "value1", "value2" };
            return response;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ApiResponse<string> Get(int id)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            response.MsgCode = "10000";
            response.MsgContent = "suceess";
            response.ResponseData = "value";
            return response;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string test)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string test)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
