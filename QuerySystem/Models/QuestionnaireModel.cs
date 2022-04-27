using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuerySystem.Models
{
    /// <summary>
    /// *問卷型別*
    /// questionnaireID/問卷標題/問卷副標/開放狀態/啟用狀態/建立時間/開始時間/結束時間
    /// </summary>
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
    /// <summary>
    /// 投票中/尚未開放/已結束
    /// </summary>

    public enum StateType
    {
        投票中 = 0,
        尚未開放 = 1,
        已結束 = 2
    }
    /// <summary>
    /// 已關閉/開放
    /// </summary>

    public enum ActiveType
    {
        已關閉 = 0,
        開放 = 1
    }
}