using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace QuerySystem.Helpers
{
    public class Logger
    {
        private const string _savePath = "C:\\log\\log.log";
        public static void WriteLog(string moduleName, Exception ex)
        {
            string content = $"---------\r\n{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}\r\n{moduleName}\r\n{ex.ToString()}\r\n---------\r\n{Environment.NewLine}";

            if (!Directory.Exists("C:\\log"))
                Directory.CreateDirectory("C:\\log");
            if (!File.Exists(_savePath))
                File.Create(_savePath).Close();
            
                File.AppendAllText(_savePath, content);
            

        }
    }
}