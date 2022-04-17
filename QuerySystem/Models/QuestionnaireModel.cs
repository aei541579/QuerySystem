using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuerySystem.Models
{
    public class QuestionnaireModel
    {
        public Guid QuestionnaireID { get; set; }
        public string QueryName { get; set; }
        public string QueryContent { get; set; }
        public StateType State { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
    public enum StateType
    {
       已關閉 = 0,
       開放 = 1
    }
}