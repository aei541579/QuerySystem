using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuerySystem.Models
{
    public class AnswerModel
    {
        public Guid AnswerID { get; set; }
        public Guid QuestionnaireID { get; set; }
        public Guid PersonID { get; set; }
        public int QuestionNo { get; set; }
        public string Answer { get; set; }
        
    }
}