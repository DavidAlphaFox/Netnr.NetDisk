using Microsoft.AspNetCore.Mvc.Filters;
using Netnr.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Netnr.NetDisk
{
    /// <summary>
    /// 过滤器
    /// </summary>
    public class FilterConfigs
    {
        /// <summary>
        /// 全局错误处理
        /// </summary>
        public class ErrorActionFilter : IExceptionFilter
        {
            public void OnException(ExceptionContext context)
            {
                Core.ConsoleTo.Log(context.Exception);
            }
        }

        private static Dictionary<string, string> _dicDescription;

        public static Dictionary<string, string> DicDescription
        {
            get
            {
                if (_dicDescription == null)
                {
                    var ass = System.Reflection.Assembly.GetExecutingAssembly();
                    var listController = ass.ExportedTypes.Where(x => x.BaseType.FullName == "Microsoft.AspNetCore.Mvc.Controller").ToList();

                    var dic = new Dictionary<string, string>();
                    foreach (var conll in listController)
                    {
                        var methods = conll.GetMethods();
                        foreach (var item in methods)
                        {
                            if (item.DeclaringType == conll)
                            {
                                string remark = "未备注说明";

                                var desc = item.CustomAttributes.Where(x => x.AttributeType == typeof(DescriptionAttribute)).FirstOrDefault();
                                if (desc != null)
                                {
                                    remark = desc.ConstructorArguments.FirstOrDefault().Value.ToString();
                                }
                                var action = (conll.Name.Replace("Controller", "/") + item.Name).ToLower();
                                if (!dic.ContainsKey(action))
                                {
                                    dic.Add(action, remark);
                                }
                            }
                        }
                    }
                    _dicDescription = dic;
                }

                return _dicDescription;
            }
            set
            {
                _dicDescription = value;
            }
        }

        /// <summary>
        /// 全局过滤器
        /// </summary>
        public class GlobalFilter : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                var hc = context.HttpContext;

                //支持跨域
                if (GlobalTo.GetValue<bool>("safe:apicors"))
                {
                    hc.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                }

                //日志记录
                try
                {
                    string controller = context.RouteData.Values["controller"].ToString().ToLower();
                    string action = context.RouteData.Values["action"].ToString().ToLower();
                    string url = hc.Request.Path.ToString() + hc.Request.QueryString.Value;
                    var referer = hc.Request.Headers["referer"].ToString();
                    var requestid = Core.UniqueTo.LongId().ToString();
                    hc.Response.Headers.Add("_qid", requestid);

                    //客户端信息
                    var ct = new Core.ClientTo(hc);

                    //日志保存
                    var mo = new Func.Logs.LogsVM()
                    {
                        LogRequestId = requestid,
                        LogAction = controller + "/" + action,
                        LogUrl = url,
                        LogIp = ct.IPv4,
                        LogReferer = referer,
                        LogCreateTime = DateTime.Now,
                        LogBrowserName = ct.BrowserName,
                        LogSystemName = ct.SystemName,
                        LogGroup = 1,
                        LogLevel = "info"
                    };
                    mo.LogContent = DicDescription[mo.LogAction.ToLower()];

                    Func.Logs.Insert(mo);
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// 授权访问
        /// </summary>
        public class AuthFilter : Attribute, IActionFilter
        {
            public void OnActionExecuted(ActionExecutedContext context)
            {

            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                var isAuth = false;

                var hc = context.HttpContext;

                try
                {
                    if (hc.User.Identity.IsAuthenticated)
                    {
                        isAuth = true;
                    }
                    else
                    {
                        var token = hc.Request.Headers["token"].ToString();

                        //token验证
                        if (!string.IsNullOrWhiteSpace(token))
                        {
                            using (var db = new ContextBase())
                            {
                                var ut = db.UserToken.Where(x => x.UtToken == token).FirstOrDefault();
                                if ((DateTime.Now - ut?.UtTokenTime)?.TotalMinutes <= GlobalTo.GetValue<int>("safe:tokenexpiredate"))
                                {
                                    isAuth = true;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Core.ConsoleTo.Log(ex);
                }

                if (!isAuth)
                {
                    hc.Response.StatusCode = 401;

                    var msg = System.Text.Encoding.Default.GetBytes("Unauthorized");
                    hc.Response.Body.Write(msg, 0, msg.Length);
                }
            }
        }
    }
}
