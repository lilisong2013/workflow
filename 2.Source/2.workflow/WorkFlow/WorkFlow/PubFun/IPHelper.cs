using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
namespace Saron.Common.PubFun
{
    public class IPHelper
    {
        public static string GetIpAddress()
        {
            IPAddress[] addressList = Dns.GetHostByName(Dns.GetHostName()).AddressList;
            string nativeIP = string.Empty;
            string serverIP = string.Empty;
            if (addressList.Length > 1)
            {
                nativeIP = addressList[0].ToString();
                serverIP = addressList[1].ToString();
            }
            else
            {
                nativeIP = addressList[0].ToString();
                serverIP = "Break the line...";
            }

            return nativeIP;
        }

        public static string GetClientIP()
        {
            // 穿过代理服务器取远程用户真实IP地址
            string Ip = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
                {
                    if (HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] != null)
                        Ip = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"].ToString();
                    else
                        if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                            Ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                        else
                            Ip = "202.96.134.133";
                }
                else
                    Ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
            {
                Ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            else
            {
                Ip = "202.96.134.133";
            }
            return Ip;
        }
    }
}
