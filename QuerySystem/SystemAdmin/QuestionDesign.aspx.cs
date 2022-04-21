using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuerySystem.Models;
using QuerySystem.Managers;
using System.Web.UI.HtmlControls;

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
                if (!IsPostBack)
                    initCreateMode();
                HttpContext.Current.Session.Remove("ID");
            }
            else if (Guid.TryParse(IDstring, out _questionnaireID))
            {
                isCreateMode = false;
                if (!IsPostBack)
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
            this.txtStartTime.Text = questionnaire.StartTime.ToString("yyyy-MM-dd");
            this.txtEndTime.Text = questionnaire.EndTime.ToString("yyyy-MM-dd");
        }
        private void initCreateMode()
        {
            HtmlAnchor linkQdesign = Master.FindControl("Qdetail") as HtmlAnchor;
            HtmlAnchor linkAlist = Master.FindControl("Alist") as HtmlAnchor;
            HtmlAnchor linkAstastic = Master.FindControl("Astastic") as HtmlAnchor;
            linkQdesign.Visible = false;
            linkAlist.Visible = false;
            linkAstastic.Visible = false;
            this.txtStartTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (InputError(out string errorMsg))
            {
                this.ltlAlert.Text = errorMsg;
                return;
            }

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
        private bool InputError(out string errorMsg)
        {
            errorMsg = string.Empty;
            if (string.IsNullOrWhiteSpace(this.txtTitle.Text.Trim()))
                errorMsg += "**必須輸入問卷標題**<br/>";
            if (string.IsNullOrWhiteSpace(this.txtStartTime.Text))
                errorMsg += "**必須輸入起始日期**<br/>";
            else if (Convert.ToDateTime(this.txtStartTime.Text) < DateTime.Today && isCreateMode)
                errorMsg += "**起始日期不可早於今天**<br/>";
            else if (string.IsNullOrWhiteSpace(this.txtEndTime.Text))
                errorMsg += "**必須輸入結束日期**<br/>";
            else if (Convert.ToDateTime(this.txtStartTime.Text) > Convert.ToDateTime(this.txtEndTime.Text))
                errorMsg += "**起始日期不可晚於結束日期**<br/>";

            if (string.IsNullOrEmpty(errorMsg))
                return false;
            return true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.RemoveAll();
            Response.Redirect("List.aspx");
        }
    }
}