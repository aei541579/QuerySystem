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

        public void ProcessRequest(HttpContext context)
        {
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
                Guid.TryParse(context.Request.QueryString["ID"], out Guid questionnaireID))
            {
                PersonModel person = new PersonModel()
                {
                    PersonID = Guid.NewGuid(),
                    Name = context.Request.Form["Name"],
                    Mobile = context.Request.Form["Mobile"],
                    Email = context.Request.Form["Email"],
                    Age = context.Request.Form["Age"],
                    QuestionnaireID = questionnaireID
                };
                HttpContext.Current.Session["personModel"] = person;
                string answerString = context.Request.Form["Answer"];
                if(string.IsNullOrWhiteSpace(answerString))
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("noAnswer");
                    return;
                }

                string[] ansArr = answerString.Trim().Split(' ');
                List<AnswerModel> answerList = new List<AnswerModel>();
                foreach (string item in ansArr)
                {
                    string[] ans = item.Split('_');

                    AnswerModel answer = new AnswerModel()
                    {
                        PersonID = person.PersonID,
                        QuestionnaireID = questionnaireID,
                        QuestionNo = Convert.ToInt32(ans[0].Replace('Q', '0')),
                        Answer = ans[1]
                    };
                    answerList.Add(answer);
                }
                HttpContext.Current.Session["answerModel"] = answerList;
                context.Response.ContentType = "text/plain";
                context.Response.Write("success");

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