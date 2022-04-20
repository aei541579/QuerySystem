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
            string content = $@"---------\r\n
                                {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}
                                {moduleName}
                                {ex.ToString()}
                                ---------\r\n
                                {Environment.NewLine}";
            if (!File.Exists(_savePath))
                File.Create(_savePath);
            File.AppendAllText(_savePath, content);
        }
    }
}