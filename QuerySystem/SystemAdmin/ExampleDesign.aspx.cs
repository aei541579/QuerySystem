using QuerySystem.Managers;
using QuerySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuerySystem.SystemAdmin
{
    public partial class ExampleDesign : System.Web.UI.Page
    {
        private static QuestionnaireMgr _mgr = new QuestionnaireMgr();
        private static Guid _questionnaireID;
        private static List<QuestionModel> _questionSession;
        protected void Page_Load(object sender, EventArgs e)
        {
            _questionSession = HttpContext.Current.Session["qusetionModel"] as List<QuestionModel>;
            //InitRpt(_questionSession);
            if (!IsPostBack)
            {
                string IDstring = Request.QueryString["ID"];
                if (Guid.TryParse(IDstring, out _questionnaireID))
                {
                    List<QuestionModel> questionList = _mgr.GetQuestionList(_questionnaireID);
                    this.txtTitle.Text = _mgr.GetExample(_questionnaireID).QueryName;
                    InitRpt(questionList);
                    HttpContext.Current.Session["qusetionModel"] = questionList;
                }
                else
                    Response.Redirect("ExampleList.aspx");
            }

        }
        private void InitRpt(List<QuestionModel> questionList)
        {
            if (questionList != null || questionList.Count > 0)
            {
                int i = 1;
                this.rptQuestion.Visible = true;
                this.rptQuestion.DataSource = questionList;
                this.rptQuestion.DataBind();
                foreach (RepeaterItem item in this.rptQuestion.Items)
                {
                    Label lblNumber = item.FindControl("lblNumber") as Label;
                    lblNumber.Text = i.ToString();
                    i++;
                }
            }
            else
                this.rptQuestion.Visible = false;
        }
        private void InitTextbox()
        {
            this.txtQuestion.Text = "";
            this.ddlQuestionType.SelectedIndex = 0;
            this.ckbNecessary.Checked = false;
            this.txtSelection.Text = "";
        }


        protected void btnAddQuestion_Click(object sender, EventArgs e)
        {
            //_questionSession = HttpContext.Current.Session["qusetionModel"] as List<QuestionModel>;
            QuestionModel question = new QuestionModel();
            question.QuestionID = Guid.NewGuid();
            question.QuestionnaireID = _questionnaireID;
            question.QuestionVal = this.txtQuestion.Text.Trim();
            question.Selection = this.txtSelection.Text.Trim();
            question.Type = (QuestionType)Convert.ToInt32(this.ddlQuestionType.SelectedValue);
            question.Necessary = this.ckbNecessary.Checked;
            _questionSession.Add(question);
            HttpContext.Current.Session["qusetionModel"] = _questionSession;
            InitRpt(_questionSession);
            InitTextbox();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<QuestionModel> needList = new List<QuestionModel>();
            foreach (RepeaterItem item in this.rptQuestion.Items)
            {
                HiddenField hfQuestionID = item.FindControl("hfQuestionID") as HiddenField;
                CheckBox ckbDelete = item.FindControl("ckbDel") as CheckBox;
                if (!ckbDelete.Checked && Guid.TryParse(hfQuestionID.Value, out Guid questionID))
                {
                    QuestionModel question = _questionSession.Find(x => x.QuestionID == questionID);
                    needList.Add(question);
                }
            }
            InitRpt(needList);
            HttpContext.Current.Session["qusetionModel"] = needList;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.RemoveAll();
            Response.Redirect("ExampleList.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //_questionSession = HttpContext.Current.Session["qusetionModel"] as List<QuestionModel>;
            if (string.Compare(_mgr.GetExample(_questionnaireID).QueryName, this.txtTitle.Text.Trim()) != 0)
                _mgr.UpdateExample(_questionnaireID, this.txtTitle.Text.Trim());

            int questionNo = 1;
            foreach (QuestionModel question in _questionSession)
            {
                question.QuestionNo = questionNo;
                _mgr.CreateQuestion(question);

                questionNo++;
            }
            Response.Redirect("ExampleList.aspx");
        }

        protected void rptQuestion_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "lkbEdit" && Guid.TryParse(e.CommandArgument.ToString(), out Guid questionID))
                {
                    QuestionModel question = _questionSession.Find(x => x.QuestionID == questionID);
                    this.txtQuestion.Text = question.QuestionVal;
                    this.ddlQuestionType.SelectedIndex = (int)question.Type;
                    this.ckbNecessary.Checked = question.Necessary;
                    this.txtSelection.Text = question.Selection;
                }
            }
        }
    }
}