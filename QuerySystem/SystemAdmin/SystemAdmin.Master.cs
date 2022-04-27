using QuerySystem.Models;
using QuerySystem.Managers;
using QuerySystem.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuerySystem.SystemAdmin
{
    public partial class SystemAdmin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var session = HttpContext.Current.Session["ID"];
            QuestionnaireModel questionnaire = HttpContext.Current.Session["QuestionnaireSession"] as QuestionnaireModel;

            if (session != null && Guid.TryParse(session.ToString(), out Guid questionnaireID))
            {
                if (new QuestionnaireMgr().GetQuestionnaire(questionnaireID) != null)
                {
                    //若資料庫有問卷
                    this.Qdesign.HRef = ConfigHelper.QuestionDesignPage() + "?ID=" + questionnaireID;
                    this.Qdetail.HRef = ConfigHelper.QuestionDetailPage() + questionnaireID;
                    this.Alist.HRef = ConfigHelper.AnswerListPage() + questionnaireID;
                    this.Astastic.HRef = "AnswerStastic.aspx?ID=" + questionnaireID;
                }
                else if(questionnaire != null && questionnaire.QuestionnaireID == questionnaireID)
                {
                    //若資料庫無問卷，但session有，表示為新增問卷模式
                    //判斷session的ID與querystring是否相等
                    this.Qdesign.HRef = ConfigHelper.QuestionDesignPage() + "?ID=" + questionnaireID;
                    this.Qdetail.HRef = ConfigHelper.QuestionDetailPage() + questionnaireID;
                }
                else
                    Response.Redirect(ConfigHelper.ListPage());
            }          

        }
    }
}