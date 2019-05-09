using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Netnr.NetDisk.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [FilterConfigs.AuthFilter]
    public class FolderController : ControllerBase
    {
        // GET: api/Folder
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Folder/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Folder
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Folder/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
