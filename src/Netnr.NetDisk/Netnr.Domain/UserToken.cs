using System;
using System.Collections.Generic;
using System.Text;

namespace Netnr.Domain
{
    /// <summary>
    /// 授权
    /// </summary>
    public class UserToken
    {
        public int Uid { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary>
        public string UtAppId { get; set; }
        /// <summary>
        /// 应用密钥
        /// </summary>
        public string UtAppKey { get; set; }
        /// <summary>
        /// 应用Token
        /// </summary>
        public string UtToken { get; set; }
        /// <summary>
        /// 应用Token的时间
        /// </summary>
        public DateTime UtTokenTime { get; set; }
        /// <summary>
        /// 应用权限
        /// </summary>
        public string UtPermission { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime UtCreateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string UtRemark { get; set; }
    }
}
