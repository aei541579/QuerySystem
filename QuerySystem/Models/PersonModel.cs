using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuerySystem.Models
{
    /// <summary>
    /// *作答者型別*
    /// personID/questionnaireID/作答者基本資料
    /// </summary>
    public class PersonModel
    {
        public Guid PersonID { get; set; }
        public Guid QuestionnaireID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Age { get; set; }        
        public DateTime CreateTime { get; set; }

    }
}