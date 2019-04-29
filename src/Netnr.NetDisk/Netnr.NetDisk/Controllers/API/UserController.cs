using Microsoft.AspNetCore.Mvc;
using Netnr.Data;
using System;
using System.Collections.Generic;
using static Netnr.Func.ApiCommon;

namespace Netnr.NetDisk.Controllers.API
{
    /// <summary>
    /// 用户接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// 用户接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return "用户接口";
        }

        /// <summary>
        /// 获取用户ID的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public OutputModel Get(int id)
        {
            var om = new OutputModel();
            try
            {
                using (var db = new ContextBase())
                {
                    om.Code = 200;
                    om.Data = db.UserInfo.Find(id);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionHandler(ref om);
            }
            return om;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public OutputModel Post([FromBody]Domain.UserInfo model)
        {
            var om = new OutputModel();
            try
            {
                using (var db = new ContextBase())
                {
                    om.Code = 200;
                    om.Data = db.UserInfo.Add(model);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionHandler(ref om);
            }
            return om;
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        [HttpPut("{id}")]
        public OutputModel Put(int id, [FromBody]Domain.UserInfo model)
        {
            var om = new OutputModel();
            try
            {
                using (var db = new ContextBase())
                {
                    var cm = db.UserInfo.Find(id);
                    var cmpis = cm.GetType().GetProperties();
                    var modelgt = model.GetType();
                    var fixedItem = new List<string>
                    {
                        "UserId",
                        "UserName"
                    };
                    foreach (var pi in cmpis)
                    {
                        if (!fixedItem.Contains(pi.Name))
                        {
                            var cv = modelgt.GetProperty(pi.Name).GetValue(model);
                            if (cv != null)
                            {
                                //var val = GlobalTo.ConvertValue(pi.PropertyType, cv.ToString());
                                pi.SetValue(cm, cv);
                            }
                        }
                    }

                    om.Code = 200;
                    om.Data = db.UserInfo.Update(cm);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionHandler(ref om);
            }
            return om;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public OutputModel Delete(int id)
        {
            var om = new OutputModel();
            try
            {
                using (var db = new ContextBase())
                {
                    om.Code = 200;
                    om.Data = db.UserInfo.Remove(db.UserInfo.Find(id));
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionHandler(ref om);
            }
            return om;
        }
    }
}