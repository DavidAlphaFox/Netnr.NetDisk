﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Netnr.NetDisk.Controllers.API
{
    /// <summary>
    /// 文件接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [FilterConfigs.AuthFilter]
    public class FileController : ControllerBase
    {
        // GET: api/File
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5   
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}