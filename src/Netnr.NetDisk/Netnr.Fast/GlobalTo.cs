using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

public class GlobalTo
{
    /// <summary>
    /// 全局配置
    /// </summary>
    public static IConfiguration Configuration;

    /// <summary>
    /// 托管环境信息
    /// 
    /// //内部访问（项目根路径）
    /// HostingEnvironment.ContentRootPath
    /// 
    /// //web外部访问（wwwroot）
    /// HostingEnvironment.WebRootPath
    /// 
    /// </summary>
    public static IHostingEnvironment HostingEnvironment;

    /// <summary>
    /// 内部访问（项目根路径）
    /// </summary>
    public static string ContentRootPath
    {
        get
        {
            return HostingEnvironment.ContentRootPath;
        }
    }

    /// <summary>
    /// web外部访问（wwwroot）
    /// </summary>
    public static string WebRootPath
    {
        get
        {
            return HostingEnvironment.WebRootPath;
        }
    }

    /// <summary>
    /// json配置文件，需AppsettingsJson复制到输出目录，不然调试找到文件
    /// </summary>
    private static JObject appsettingsJson;
    public static JObject AppsettingsJson
    {
        get
        {
            if (appsettingsJson == null)
            {
                using (var sr = new StreamReader(ContentRootPath + "/appsettings.json", Encoding.Default))
                {
                    appsettingsJson = JObject.Parse(sr.ReadToEnd());
                }
            }
            return appsettingsJson;
        }
        set => appsettingsJson = value;
    }

    /// <summary>
    /// 起始路径（Windows为磁盘跟目录，linux为/）
    /// </summary>
    public static string StartPath = "/";

    /// <summary>
    /// 获取AppsettingsJson的值
    /// </summary>
    /// <param name="path">如：ConnectionStrings:SQLServerConn</param>
    /// <returns></returns>
    public static string GetValue(string path)
    {
        string result = string.Empty;

        if (!string.IsNullOrWhiteSpace(path))
        {
            var listp = path.Split(':').ToList();
            var deep = 0;
            var jo = AppsettingsJson as JToken;
            while (deep < listp.Count)
            {
                try
                {
                    jo = jo[listp[deep++]];
                }
                catch (System.Exception)
                {
                    goto output;
                }
            }
            result = jo?.ToString() ?? "";
        }
    output: return result;
    }

    public static T GetValue<T>(string path)
    {
        return (T)ConvertValue(typeof(T), GetValue(path));
    }

    public static object ConvertValue(Type type, string value)
    {
        if (type == typeof(object))
        {
            return value;
        }

        if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            return ConvertValue(Nullable.GetUnderlyingType(type), value);
        }

        var converter = TypeDescriptor.GetConverter(type);
        if (converter.CanConvertFrom(typeof(string)))
        {
            return converter.ConvertFromInvariantString(value);
        }

        return null;
    }
}