using System;

namespace Netnr.Domain
{
    /// <summary>
    /// 文件设置
    /// </summary>
    public partial class FileSetting
    {
        /// <summary>
        /// 设置ID
        /// </summary>
        public string FsId { get; set; }
        /// <summary>
        /// 所属用户
        /// </summary>
        public int? Uid { get; set; }
        /// <summary>
        /// 设置类型
        /// </summary>
        public string FsType { get; set; }
        /// <summary>
        /// 设置对象
        /// </summary>
        public string FsValue { get; set; }
        /// <summary>
        /// 设置权限
        /// </summary>
        public string FsPermission { get; set; }
        /// <summary>
        /// 设置时间
        /// </summary>
        public DateTime FsCreateTime { get; set; }
        /// <summary>
        /// 设置备注
        /// </summary>
        public string FsRmeark { get; set; }
    }
}
