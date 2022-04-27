using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuerySystem.Models
{
    /// <summary>
    /// *統計型別*
    /// 題號/答案/答案數量
    /// </summary>
    public class StasticModel
    {
        public int QuestionNo { get; set; }
        public string Answer { get; set; }
        public int AnsCount { get; set; }
    }
}