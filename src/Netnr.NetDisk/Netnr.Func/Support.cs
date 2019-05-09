using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static Netnr.Func.ApiCommon;

namespace Netnr.Func
{
    public class Support
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public OutputModel Init()
        {
            var om = new OutputModel();
            try
            {
                CreateRoot();
                CreateUserFolder("0");
            }
            catch (Exception ex)
            {
                ex.ExceptionHandler(ref om);
            }

            return om;
        }

        /// <summary>
        /// 获取根目录
        /// </summary>
        /// <returns></returns>
        public string GetRoot()
        {
            var root = GlobalTo.GetValue("path:root").Replace("~", GlobalTo.ContentRootPath);
            return root.TrimEnd('/') + "/";
        }

        /// <summary>
        /// 获取用户目录
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <returns></returns>
        public string GetRootUser(string uid)
        {
            var path = GetRoot();
            path += GlobalTo.GetValue<bool>("safe:anonymous")
                ? "anonymous" : uid;
            return path + "/";
        }

        /// <summary>
        /// 获取用户回收站目录
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <returns></returns>
        public string GetRootUserRecycleBin(string uid)
        {
            var path = GetRootUser(uid) + ".delete/";
            return path;
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path"></param>
        public void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 创建根目录
        /// </summary>
        public void CreateRoot()
        {
            var root = GetRoot();
            CreateFolder(root);
        }

        /// <summary>
        /// 创建用户目录
        /// </summary>
        /// <param name="uid">用户ID</param>
        public void CreateUserFolder(string uid)
        {
            var path = GetRootUser(uid) + ".user/";
            CreateFolder(path);
        }

        /// <summary>
        /// 创建用户回收站
        /// </summary>
        /// <param name="uid"></param>
        public void CreateUserRecycleBinFolder(string uid)
        {
            var path = GetRootUserRecycleBin(uid);
            CreateFolder(path);
        }
    }
}
