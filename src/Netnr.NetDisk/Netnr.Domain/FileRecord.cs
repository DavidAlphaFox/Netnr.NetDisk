using System;

namespace Netnr.Domain
{
    /// <summary>
    /// 文件记录
    /// </summary>
    public partial class FileRecord
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        public string FileId { get; set; }
        /// <summary>
        /// 所属用户
        /// </summary>
        public int? Uid { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime FileCreateTime { get; set; }
        /// <summary>
        /// 文件大小，B
        /// </summary>
        public int FileSize { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }
        /// <summary>
        /// 文件Hash
        /// </summary>
        public string FileHash { get; set; }
        /// <summary>
        /// 文件标签
        /// </summary>
        public string FileTag { get; set; }
        /// <summary>
        /// 文件备注
        /// </summary>
        public string FileRmeark { get; set; }
    }
}
