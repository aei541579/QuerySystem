using QuerySystem.Models;
using QuerySystem.Managers;
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
            if (session != null && Guid.TryParse(session.ToString(), out Guid questionnaireID))
            {
                this.Qdesign.HRef = "QuestionDesign.aspx?ID=" + questionnaireID;
                this.Qdetail.HRef = "QuestionDetail.aspx?ID=" + questionnaireID;
                this.Alist.HRef = "AnswerList.aspx?ID=" + questionnaireID;
                this.Astastic.HRef = "AnswerStastic.aspx?ID=" + questionnaireID;
            }

            //若questionnaireSession為null，則表示為編輯模式
            QuestionnaireModel questionnaire = HttpContext.Current.Session["QuestionnaireSession"] as QuestionnaireModel;
            string queryStringID = Request.QueryString["ID"] as string;
            Guid id;
            if (Guid.TryParse(queryStringID, out id) && questionnaire == null)
            {
                //若有人手動輸入不存在於資料庫的id=>在design頁會被視為編輯模式，回傳一個new questionnaire
                //若找問題清單數量為0，表示問卷確實不存在，且在編輯模式下
                List<QuestionModel> questionList = new QuestionnaireMgr().GetQuestionList(id);
                if (questionList.Count == 0)
                    Response.Redirect("List.aspx");
            }
            else if (Guid.TryParse(queryStringID, out id) && questionnaire != null)
            {
                //若在新增模式下被偷改ID，則比較兩者ID是否一致
                if (id != questionnaire.QuestionnaireID)
                    Response.Redirect("List.aspx");
            }


        }
    }
}