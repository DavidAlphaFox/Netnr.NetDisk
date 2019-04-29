using System;

namespace Netnr.Domain
{
    /// <summary>
    /// 用户
    /// </summary>
    public partial class UserInfo
    {
        /// <summary>
        /// 自增
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string UserPwd { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// 用户组
        /// </summary>
        public string UserGroup { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string UserPhoto { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int? UserSex { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? UserBirthday { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string UserPhone { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string UserMail { get; set; }
        /// <summary>
        /// 网站
        /// </summary>
        public string UserUrl { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string UserSay { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? UserAddTime { get; set; }
        /// <summary>
        /// 登录限制
        /// </summary>
        public int? LoginLimit { get; set; }
        /// <summary>
        /// 登录标识
        /// </summary>
        public string UserSign { get; set; }
        /// <summary>
        /// 第三方登录
        /// </summary>
        public string OpenId1 { get; set; }
        public string OpenId2 { get; set; }
        public string OpenId3 { get; set; }
        public string OpenId4 { get; set; }
        public string OpenId5 { get; set; }
        public string OpenId6 { get; set; }
        public string OpenId7 { get; set; }
        public string OpenId8 { get; set; }
        public string OpenId9 { get; set; }
    }
}
