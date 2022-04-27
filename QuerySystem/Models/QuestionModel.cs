using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuerySystem.Models
{
    /// <summary>
    /// *問題型別*
    /// questionID/questionnaireID/題目種類/題號/問題內容/回答選項/必填
    /// </summary>
    public class QuestionModel
    {
        public Guid QuestionID { get; set; }
        public Guid QuestionnaireID { get; set; }
        public QuestionType Type { get; set; }
        public int QuestionNo { get; set; }
        public string QuestionVal { get; set; }       
        public string Selection { get; set; }
        public bool Necessary { get; set; }
        
    }
    // <summary>
    /// 單選/複選/文字
    /// </summary>
    public enum QuestionType
    {
        單選方塊 = 0,
        複選方塊 = 1,
        文字 = 2
    }
}