using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuerySystem.Managers;
using QuerySystem.Models;

namespace QuerySystem.API
{
    /// <summary>
    /// AddQuestionHandler 的摘要描述
    /// </summary>
    public class AddQuestionHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        private static QuestionnaireMgr _mgr = new QuestionnaireMgr();

        public void ProcessRequest(HttpContext context)
        {
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 &&
                string.Compare("button", context.Request.QueryString["Action"], true) == 0 )
            {
                HttpContext.Current.Session["addByButton"] = "addByButton";

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