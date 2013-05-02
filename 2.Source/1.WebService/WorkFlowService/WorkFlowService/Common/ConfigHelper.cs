using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Web.Configuration;


namespace Saron.Common
{
    public class ConfigHelper
    {

        #region AppConfig的操作（适用于Winform系统中）
        

        /// <summary>
        /// 根据键值获取配置文件appsettings节中的value
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static string AppGetConfig(string key)
        {
            ConfigurationManager.RefreshSection("appSettings");
            string val = string.Empty;

            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
                val = ConfigurationManager.AppSettings[key];
            return val;
        }

        /// <summary>
        /// 获取所有配置文件
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> AppGetConfig()
        {
            ConfigurationManager.RefreshSection("appSettings");
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (string key in ConfigurationManager.AppSettings.AllKeys)
                dict.Add(key, ConfigurationManager.AppSettings[key]);
            return dict;
        }

        /// <summary>
        /// 根据键值获取配置文件appsettings节中的value
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="defaultValue">默认值，如果value为空，则返回该默认值</param>
        /// <returns></returns>
        public static string AppGetConfig(string key, string defaultValue)
        {
            ConfigurationManager.RefreshSection("appSettings");
            string val = defaultValue;
            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
                val = ConfigurationManager.AppSettings[key];
            if (val == null)
                val = defaultValue;
            return val;
        }

        /// <summary>
        /// 写配置文件,如果节点不存在则自动创建到appsettings节中
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool AppSetConfig(string key, string value)
        {
            try
            {
                Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (!conf.AppSettings.Settings.AllKeys.Contains(key))
                {
                    conf.AppSettings.Settings.Add(key, value);
                }
                else
                {
                    conf.AppSettings.Settings[key].Value = value;
                }
                conf.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 写配置文件(用键值创建),如果节点不存在则自动创建appsettings节中
        /// </summary>
        /// <param name="dict">键值集合</param>
        /// <returns></returns>
        public static bool AppSetConfig(Dictionary<string, string> dict)
        {
            try
            {
                if (dict == null || dict.Count == 0)
                    return false;
                Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                foreach (string key in dict.Keys)
                {
                    if (!conf.AppSettings.Settings.AllKeys.Contains(key))
                        conf.AppSettings.Settings.Add(key, dict[key]);
                    else
                        conf.AppSettings.Settings[key].Value = dict[key];
                }
                conf.Save();
                return true;
            }
            catch { return false; }
        }

        #endregion


        #region WebConfig的操作（适用于web系统中）

        /// <summary>
        /// 获得webconfig文件中的连接字符串
        /// </summary>
        /// <param name="ConnectionName"></param>
        /// <returns></returns>
        public static string WebGetConnection(string ConnectionName)
        {
            return WebConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;
        }
        /// <summary>
        /// 设置webconfig文件中的连接字符串
        /// </summary>
        /// <param name="ConnectionName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool WebSetConnection(string ConnectionName, string Value)
        {
            bool result = false;
            try
            {
                Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);
                ConnectionStringsSection con = (ConnectionStringsSection)config.GetSection("connectionStrings");
                con.ConnectionStrings[ConnectionName].ConnectionString = Value;
                config.Save();
                result = true;
            }
            catch (Exception er)
            {
                throw new Exception(er.Message);
            }
            return result;
        }


        /// <summary>
        /// 设置webconfig文件appsettings中某个key 的value。(不存在则添加）。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool WebSetConfig(Dictionary<string, string> dict)
        {
            try
            {
                if (dict == null || dict.Count == 0)
                    return false;
                Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);
                foreach (string key in dict.Keys)
                {
                    if (!config.AppSettings.Settings.AllKeys.Contains(key))
                        config.AppSettings.Settings.Add(key, dict[key]);
                    else
                        config.AppSettings.Settings[key].Value = dict[key];
                }
                config.Save();
                return true;
            }
            catch (Exception er)
            {
                return false;
            }
        }

        /// <summary>
        /// 读取webconfig文件appsettings中某个key 的value，（如果不存在key，返回string.empty）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string WebGetConfig(string key)
        {
            string result = string.Empty;
            try
            {
                result = WebConfigurationManager.AppSettings[key].ToString();
            }
            catch (Exception er)
            {
                throw new Exception(er.Message);
            }
            return result;
        }
        #endregion
    }
}
