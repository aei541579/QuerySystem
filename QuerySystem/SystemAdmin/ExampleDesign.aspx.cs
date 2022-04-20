﻿using QuerySystem.Managers;
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
            this.btnAddQuestion.Visible = true;
            this.btnEditQuestion.Visible = false;
        }


        protected void btnAddQuestion_Click(object sender, EventArgs e)
        {            
            QuestionModel question = new QuestionModel();
            FillSelectionContent(question, out bool noInput);
            if (noInput)
                return;
            question.QuestionID = Guid.NewGuid();

            _questionSession.Add(question);
            HttpContext.Current.Session["qusetionModel"] = _questionSession;
            InitRpt(_questionSession);
            InitTextbox();
        }
        private void FillSelectionContent(QuestionModel question, out bool noInput)
        {
            noInput = false;
            question.QuestionnaireID = _questionnaireID;
            question.Type = (QuestionType)Convert.ToInt32(this.ddlQuestionType.SelectedValue);
            if (NoInput(this.txtQuestion.Text.Trim()))
                noInput = true;
            question.QuestionVal = this.txtQuestion.Text.Trim();
            if (question.Type != QuestionType.文字 && NoInput(this.txtSelection.Text.Trim()))
                noInput = true;
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
            //若標題有被修改，則寫入新標題
            if (string.Compare(_mgr.GetExample(_questionnaireID).QueryName, this.txtTitle.Text.Trim()) != 0)
                _mgr.UpdateExample(_questionnaireID, this.txtTitle.Text.Trim());

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