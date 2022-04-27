using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuerySystem.Managers;
using QuerySystem.Models;

namespace QuerySystem.API
{
    /// <summary>
    /// AnswerHandler 的摘要描述
    /// </summary>
    public class AnswerHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        private static QuestionnaireMgr _mgr = new QuestionnaireMgr();
        public void ProcessRequest(HttpContext context)
        {
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
                Guid.TryParse(context.Request.QueryString["ID"], out Guid questionnaireID) &&
                string.Compare("Confirm", context.Request.QueryString["Action"], true) == 0)
            {
                string profileString = context.Request.Form["Profile"];
                string[] proArr = profileString.Split(';');
                //判斷基本資料填寫格式
                if (!int.TryParse(proArr[1], out int phone) || !proArr[2].Contains('@') || !int.TryParse(proArr[3], out int age) || age > 200)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("errorInput");
                    return;
                }
                PersonModel person = new PersonModel()
                {
                    PersonID = Guid.NewGuid(),
                    Name = proArr[0],
                    Mobile = proArr[1],
                    Email = proArr[2],
                    Age = proArr[3],
                    QuestionnaireID = questionnaireID
                };
                HttpContext.Current.Session["personModel"] = person;

                string answerString = context.Request.Form["Answer"];
                //完全沒填寫
                if (string.IsNullOrWhiteSpace(answerString))
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("noAnswer");
                    return;
                }

                string[] ansArr = answerString.Trim().Split(';');
                List<AnswerModel> answerList = new List<AnswerModel>();
                foreach (string item in ansArr)
                {
                    //切出的字串為空白則跳過
                    if (string.IsNullOrWhiteSpace(item))
                        continue;

                    string[] ans = item.Split('_');
                    AnswerModel answer = new AnswerModel();
                    answer.PersonID = person.PersonID;
                    answer.QuestionnaireID = questionnaireID;
                    answer.QuestionNo = Convert.ToInt32(ans[0].Replace('Q', '0'));
                    answer.Answer = ans[1];

                    answerList.Add(answer);
                }

                HttpContext.Current.Session["answerModel"] = answerList;
                context.Response.ContentType = "text/plain";
                context.Response.Write("success");
                return;
            }

            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
               string.Compare("Submit", context.Request.QueryString["Action"], true) == 0)
            {
                List<AnswerModel> answerList = HttpContext.Current.Session["answerModel"] as List<AnswerModel>;
                PersonModel person = HttpContext.Current.Session["personModel"] as PersonModel;

                _mgr.CreatePerson(person);
                foreach (AnswerModel answer in answerList)
                {
                    _mgr.CreateAnswer(answer);
                }
                HttpContext.Current.Session.RemoveAll();
                context.Response.ContentType = "text/plain";
                context.Response.Write("success");
                return;
            }

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