using QuerySystem.Managers;
using QuerySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuerySystem.API
{
    /// <summary>
    /// StasticHandler 的摘要描述
    /// </summary>
    public class StasticHandler : IHttpHandler
    {
        private static QuestionnaireMgr _mgr = new QuestionnaireMgr();

        public void ProcessRequest(HttpContext context)
        {
            string IDstring = context.Request.QueryString["ID"];
            if (Guid.TryParse(IDstring, out Guid questionnaireID) &&
                string.Compare("GET", context.Request.HttpMethod, true) == 0)
            {
                //找這份問卷的題目和統計清單
                List<QuestionModel> questionList = _mgr.GetQuestionList(questionnaireID);
                List<StasticModel> stasticList = _mgr.GetStasticList(questionnaireID);

                //找此問題的題目字串和此題目的統計清單
                string questionNo = context.Request.QueryString["que"];
                QuestionModel question = questionList.Find(x => x.QuestionNo.ToString() == questionNo);
                List<StasticModel> NoList = stasticList.FindAll(x => x.QuestionNo == question.QuestionNo);

                string[] arrQue = question.Selection.Split(';');
                PieChart[] charList = new PieChart[arrQue.Length];
                for (int i = 0; i < arrQue.Length; i++)
                {
                    int ansCount = 0;
                    StasticModel stastic = NoList.Find(x => x.Answer == i.ToString());
                    if (stastic != null)
                        ansCount = stastic.AnsCount;

                    //string stasticString = $"label: {arrQue[i]}, data: {ansCount}";
                    PieChart chartData = new PieChart()
                    {
                        label = $"{arrQue[i]} ({ansCount})",
                        data = ansCount
                    };
                    charList[i] = chartData;
                }

                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(charList);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
                return;
            }

        }
        public class PieChart
        {
            public string label { get; set; }
            public int data { get; set; }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}