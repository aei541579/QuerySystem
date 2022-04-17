using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuerySystem.Models;
using QuerySystem.Managers;

namespace QuerySystem.SystemAdmin
{
    public partial class QusetionDesign : System.Web.UI.Page
    {
        private static QuestionnaireMgr _mgr = new QuestionnaireMgr();
        private static Guid _questionnaireID;
        private bool isCreateMode;
        protected void Page_Load(object sender, EventArgs e)
        {
            string IDstring = Request.QueryString["ID"];
            if (string.IsNullOrWhiteSpace(IDstring))
            {
                isCreateMode = true;
                HttpContext.Current.Session.Remove("ID");
            }
            else if (Guid.TryParse(IDstring, out _questionnaireID))
            {
                isCreateMode = false;
                initEditMode(_questionnaireID);
                HttpContext.Current.Session["ID"] = _questionnaireID;
            }
            else
                Response.Redirect("List.aspx");

        }
        private void initEditMode(Guid QuestionnaireID)
        {
            QuestionnaireModel questionnaire = _mgr.GetQuestionnaire(QuestionnaireID);
            this.txtTitle.Text = questionnaire.QueryName;
            this.txtContent.Text = questionnaire.QueryContent;
            this.txtStartTime.Text = questionnaire.StartTime.ToString();
            this.txtEndTime.Text = questionnaire.EndTime.ToString();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            QuestionnaireModel questionnaire = new QuestionnaireModel()
            {
                QuestionnaireID = Guid.NewGuid(),
                QueryName = this.txtTitle.Text.Trim(),
                QueryContent = this.txtContent.Text.Trim(),
                StartTime = Convert.ToDateTime(this.txtStartTime.Text),
                EndTime = Convert.ToDateTime(this.txtEndTime.Text)
            };

            if (isCreateMode)
            {
                questionnaire.QuestionnaireID = Guid.NewGuid();
                _mgr.CreateQuestionnaire(questionnaire);
            }
            else
            {
                questionnaire.QuestionnaireID = _questionnaireID;
                _mgr.UpdateQuestionnaire(questionnaire);
            }
            HttpContext.Current.Session["ID"] = questionnaire.QuestionnaireID;
            Response.Redirect("QuestionDetail.aspx?ID=" + questionnaire.QuestionnaireID);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }
    }
}