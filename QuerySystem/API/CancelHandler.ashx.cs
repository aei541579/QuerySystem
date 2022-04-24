using QuerySystem.Managers;
using QuerySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuerySystem.API
{
    /// <summary>
    /// CancelHandler 的摘要描述
    /// </summary>
    public class CancelHandler : IHttpHandler
    {
        private static QuestionnaireMgr _mgr = new QuestionnaireMgr();

        public void ProcessRequest(HttpContext context)
        {
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
                string.Compare("Question", context.Request.QueryString["Type"], true) == 0)
            {
                string IDtext = context.Request.Form["ID"];
                if (Guid.TryParse(IDtext, out Guid questionnaireID))
                {
                    List<QuestionModel> question = _mgr.GetQuestionList(questionnaireID);
                    if (question.Count == 0)
                        _mgr.DeleteQuestionnaire(questionnaireID);

                    context.Response.ContentType = "text/plain";
                    context.Response.Write("success");

                }

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