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
        private static QuestionnaireModel _exampleModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            _questionSession = HttpContext.Current.Session["qusetionModel"] as List<QuestionModel>;
            _exampleModel = HttpContext.Current.Session["ExampleModel"] as QuestionnaireModel;

            //InitRpt(_questionSession);
            if (!IsPostBack)
            {
                string IDstring = Request.QueryString["ID"];
                if (Guid.TryParse(IDstring, out _questionnaireID))
                {
                    List<QuestionModel> questionList = _mgr.GetQuestionList(_questionnaireID);
                    this.txtTitle.Text = (_exampleModel != null)
                        ? _exampleModel.QueryName
                        : _mgr.GetExample(_questionnaireID).QueryName;
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
            this.btnAddQuestion.Visible = true;
            this.btnEditQuestion.Visible = false;
        }


        protected void btnAddQuestion_Click(object sender, EventArgs e)
        {
            QuestionModel question = new QuestionModel();
            FillSelectionContent(question, out bool errorInput);
            if (errorInput)
                return;
            question.QuestionID = Guid.NewGuid();

            _questionSession.Add(question);
            HttpContext.Current.Session["qusetionModel"] = _questionSession;
            InitRpt(_questionSession);
            InitTextbox();
        }
        private void FillSelectionContent(QuestionModel question, out bool errorInput)
        {
            errorInput = false;
            question.QuestionnaireID = _questionnaireID;
            question.Type = (QuestionType)Convert.ToInt32(this.ddlQuestionType.SelectedValue);
            if (NoInput(this.txtQuestion.Text.Trim()))
                errorInput = true;
            question.QuestionVal = this.txtQuestion.Text.Trim();
            if (question.Type != QuestionType.文字 && ErrorInput(this.txtSelection.Text.Trim()))
                errorInput = true;
            question.Selection = this.txtSelection.Text.Trim();
            question.Necessary = this.ckbNecessary.Checked;
        }
        private bool NoInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                this.ltlAlert.Visible = true;
                this.ltlAlert.Text = "**問題尚未輸入完畢**";
                return true;
            }
            else
            {
                this.ltlAlert.Visible = false;
                return false;
            }
        }
        private bool ErrorInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                this.ltlAlert.Visible = true;
                this.ltlAlert.Text = "**選項尚未輸入完畢**";
                return true;
            }
            try
            {
                string[] arrSelection = input.Split(';');
                if (arrSelection.Length <= 1)
                {
                    this.ltlAlert.Visible = true;
                    this.ltlAlert.Text = "**請輸入至少兩個選項**";
                    return true;
                }
                else
                {
                    foreach (var item in arrSelection)
                    {
                        if (string.IsNullOrWhiteSpace(item))
                        {
                            this.ltlAlert.Visible = true;
                            this.ltlAlert.Text = "**請正確輸入選項**";
                            return true;
                        }
                    }
                    this.ltlAlert.Visible = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.ltlAlert.Visible = true;
                this.ltlAlert.Text = "**選項輸入有誤**";
                return true;
            }
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
            //若session有example，代表為新增狀態
            if (_exampleModel != null)
                _mgr.CreateExample(_exampleModel.QuestionnaireID, _exampleModel.QueryName);

            if (_mgr.GetQuestionList(_questionnaireID) != null)
                _mgr.DeleteQuestion(_questionnaireID);

            int questionNo = 1;
            foreach (QuestionModel question in _questionSession)
            {
                question.QuestionNo = questionNo;
                _mgr.CreateQuestion(question);

                questionNo++;
            }
            HttpContext.Current.Session.Remove("qusetionModel");
            HttpContext.Current.Session.Remove("ExampleModel");
            Response.Redirect("ExampleList.aspx");
        }

        protected void rptQuestion_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "lkbEdit" && Guid.TryParse(e.CommandArgument.ToString(), out Guid questionID))
                {
                    QuestionModel question = _questionSession.Find(x => x.QuestionID == questionID);
                    this.hfEditQID.Value = question.QuestionID.ToString();
                    this.txtQuestion.Text = question.QuestionVal;
                    this.ddlQuestionType.SelectedIndex = (int)question.Type;
                    this.ckbNecessary.Checked = question.Necessary;
                    this.txtSelection.Text = question.Selection;
                    if (question.Type == QuestionType.文字)
                        this.txtSelection.Attributes.Add("disabled", "disable");
                    else
                        this.txtSelection.Attributes.Remove("disabled");
                    this.btnAddQuestion.Visible = false;
                    this.btnEditQuestion.Visible = true;
                }
            }
        }

        protected void btnEditQuestion_Click(object sender, EventArgs e)
        {
            if (Guid.TryParse(this.hfEditQID.Value, out Guid editQID))
            {
                int index = _questionSession.FindIndex(x => x.QuestionID == editQID);
                QuestionModel question = _questionSession.Find(x => x.QuestionID == editQID);
                FillSelectionContent(question, out bool noInput);
                if (noInput)
                    return;
                _questionSession.RemoveAt(index);
                _questionSession.Insert(index, question);
                InitRpt(_questionSession);
                InitTextbox();
            }
        }
    }
}