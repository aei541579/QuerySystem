using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuerySystem.Models
{
    /// <summary>
    /// 題號/答案/答案數量
    /// </summary>
    public class StasticModel
    {
        public int QuestionNo { get; set; }
        public string Answer { get; set; }
        public int AnsCount { get; set; }
    }
}