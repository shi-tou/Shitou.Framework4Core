using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shitou.Framework.Demo.DataContract;
using Shitou.Framework.Demo.Model.Books;
using Shitou.Framework.Demo.Service;

namespace Shitou.Framework.Demo.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Books")]
    public class BooksController : Controller
    {
        private IBaseService BaseService;
        public BooksController(IBaseService baseService)
        {
            BaseService = baseService;
        }

        // GET: api/Books
        [HttpGet]
        public IEnumerable<BookInfo> Get()
        {
            return BaseService.GetPageList<BookInfo>(new PageRequest
            {
                PageIndex = 1,
                PageSize = 10,
                OrderBy = "ID desc"
            });
        
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Books
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Books/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            BaseService.Update(new BookInfo(), new { ID = 1 });
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }
    }
}
