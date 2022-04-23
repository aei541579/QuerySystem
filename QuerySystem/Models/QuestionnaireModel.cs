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
        public ActiveType IsActive { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
    public enum StateType
    {
        投票中 = 0,
        尚未開放 = 1,
        已結束 = 2
    }
    public enum ActiveType
    {
        已關閉 = 0,
        開放 = 1
    }
}