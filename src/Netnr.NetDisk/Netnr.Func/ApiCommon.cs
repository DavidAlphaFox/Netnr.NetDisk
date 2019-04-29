using System;
using System.Collections.Generic;
using System.Text;

namespace Netnr.Func
{
    public static class ApiCommon
    {
        public class OutputModel
        {
            public int Code { get; set; }
            public string Message { get; set; } = "success";
            public object Data { get; set; }
        }

        public static void ExceptionHandler(this Exception ex, ref OutputModel om)
        {
            om.Code = -1;
            om.Message = "ERROR：" + ex.Message;
        }
    }
}
