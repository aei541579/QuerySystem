﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace QuerySystem.Helpers
{
    /// <summary>
    /// 取得config內的參數
    /// </summary>
    public class ConfigHelper
    {
        private const string _mainDBName = "MainDB";
        public static string GetConnectionString()
        {
            return GetConnectionString(_mainDBName);
        }
        /// <summary>
        /// 取得SQL的連線字串
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetConnectionString(string name)
        {
            string connString = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            return connString;
        }
        /// <summary>
        /// 取得列表頁的url
        /// </summary>
        /// <returns></returns>
        public static string ListPage()
        {
            return ConfigurationManager.AppSettings["ListPage"];
        }
        /// <summary>
        /// 取得作答頁的url
        /// </summary>
        /// <returns></returns>
        public static string FormPage()
        {
            return ConfigurationManager.AppSettings["FormPage"];
        }
    }
}