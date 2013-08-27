///<summary>
///Copyright (C) Shandong SARON Intelligent Technology Co., Ltd all rights reserved
///文件名称：ConditionFilter.cs
///功能概述：正则表达式，条件过滤
///    作者：朱建刚
///创建日期：2013-04-19
///</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saron.Common.PubFun
{
    /// <summary>
    /// 正则表达式，条件过滤
    /// </summary>
    class ConditionFilter
    {
        /// <summary>
        /// 验证身份证号
        /// </summary>
        /// <param name="str_idcard">输入身份证号</param>
        /// <returns></returns>
        public static bool IsIDcard(string str_idcard)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_idcard, @"(^\d{18}$)|(^\d{15}$)");
        }

        /// <summary>
        /// 验证数字
        /// </summary>
        /// <param name="str_number">输入数字</param>
        /// <returns></returns>
        public static bool IsNumber(string str_number)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_number, @"^[0-9]*$");
        }
        ///<summary>
        ///编码验证(以字母开头的)
        /// </summary>
        public static bool IsCode(string str_number)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_number,@"^[a-zA-Z]\w{1,40}");
        }
        /// <summary>
        /// 验证用户名（用户名一般就是4-12位，只能是字母（大小写敏感），数字，下划线，不能以下划线开头和结尾）
        /// </summary>
        /// <param name="str_userName">用户名字符串</param>
        /// <returns></returns>
        public static bool IsUserName(string str_userName)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_userName, @"^[a-zA-Z]\w{3,11}$");
        }

        /// <summary>
        /// 验证密码（以字母开头，长度在6~18之间，只能包含字符、数字和下划线）
        /// </summary>
        /// <param name="str_passWord">密码字符串</param>
        /// <returns></returns>
        public static bool IsPassWord(string str_passWord)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_passWord, @"^[a-zA-Z]+[0-9]{1,}[a-zA-Z]{1,}$|[a-zA-Z]{1,}[0-9]{1,}$");
        }

        /// <summary>
        /// 验证密码（大写字母+小写字母+数字的任意组合，而且必须包含这3种数据）
        /// </summary>
        /// <param name="str_passWord">密码字符串</param>
        /// <returns></returns>
        public static bool IsPassWordTwo(string str_passWord)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_passWord, @"^[0-9a-zA-Z]|[a-zA-Z0-9]{5,20}+$");
        }
        /// <summary>
        /// 验证邮件的格式（数字、字母、符号的组合@数字/字母.com/cn）
        /// </summary>
        /// <param name="str_passWord">邮件地址</param>
        /// <returns></returns>
        public static bool IsEmail(string str_email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_email, @"^\s*([A-Za-z0-9_-]+(\.\w+)*@([\w-]+\.)+\w{2,3})\s*$");
        }
        /// <summary>
        /// 验证手机号码的格式（11位的数字）
        /// </summary>
        /// <param name="str_passWord">邮件地址</param>
        /// <returns></returns>
        public static bool IsMobilePhone(string str_mobile)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_mobile, @"\b(86)?0?\d{11}\b");
        }
        //判断字符串中只能是字母、数字、下划线、中文
        public static bool IsValidString(string str_String)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_String,@"^[a-zA-Z0-9_\u4e00-\u9fa5]+$");
        }
        //判断是否是有效的URL
        public static bool IsValidURL(string str_URL)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_URL, @"[a-zA-z]+://[^\s]{1,1000}");

        }
    }
}
