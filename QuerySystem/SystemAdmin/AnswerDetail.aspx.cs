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
    public partial class AnswerDetail : System.Web.UI.Page
    {
        private static QuestionnaireMgr _mgr = new QuestionnaireMgr();
        private static Guid _personID;
        private static List<AnswerModel> _answerList;
        protected void Page_Load(object sender, EventArgs e)
        {
            string personIDstring = Request.QueryString["PersonID"];
            if (Guid.TryParse(personIDstring, out _personID))
            {
                PersonModel person = _mgr.GetPerson(_personID);
                QuestionnaireModel questionnaire = _mgr.GetQuestionnaire(person.QuestionnaireID);
                this.ansListLink.HRef = "AnswerList.aspx?ID=" + questionnaire.QuestionnaireID.ToString();
                this.ltlTitle.Text = questionnaire.QueryName;
                this.ltlContent.Text = questionnaire.QueryContent;

                this.txtName.Text = person.Name;
                this.txtMobile.Text = person.Mobile;
                this.txtMail.Text = person.Email;
                this.txtAge.Text = person.Age;

                _answerList = _mgr.GetAnswerList(_personID);
                List<QuestionModel> questionList = _mgr.GetQuestionList(person.QuestionnaireID);
                foreach (QuestionModel question in questionList)
                {
                    string q = $"<br/>{question.QuestionNo}. {question.QuestionVal}";
                    if (question.Necessary)
                        q += "(*必填)";
                    Literal ltlQuestion = new Literal();
                    ltlQuestion.Text = q + "<br/>";
                    this.plcDynamic.Controls.Add(ltlQuestion);

                    switch (question.Type)
                    {
                        case QuestionType.單選方塊:
                            CreateRdb(question);
                            break;
                        case QuestionType.複選方塊:
                            CreateCkb(question);
                            break;
                        case QuestionType.文字:
                            CreateTxt(question);
                            break;
                    }
                }
            }
            else
                Response.Redirect("List.aspx");
        }
        private void CreateRdb(QuestionModel question)
        {
            AnswerModel rdb = _answerList.Find(x => x.QuestionNo == question.QuestionNo);
            RadioButtonList radioButtonList = new RadioButtonList();
            radioButtonList.ID = "Q" + question.QuestionNo;
            radioButtonList.Enabled = false;
            this.plcDynamic.Controls.Add(radioButtonList);
            string[] arrQue = question.Selection.Split(';');
            for (int i = 0; i < arrQue.Length; i++)
            {
                ListItem item = new ListItem(arrQue[i], i.ToString());
                if (rdb != null && Convert.ToInt32(rdb.Answer) == i)
                    item.Selected = true;
                radioButtonList.Items.Add(item);
            }
        }
        private void CreateCkb(QuestionModel question)
        {
            List<AnswerModel> ckbList = _answerList.FindAll(x => x.QuestionNo == question.QuestionNo);
            CheckBoxList checkBoxList = new CheckBoxList();
            checkBoxList.ID = "Q" + question.QuestionNo;
            checkBoxList.Enabled = false;
            this.plcDynamic.Controls.Add(checkBoxList);
            string[] arrQue = question.Selection.Split(';');
            for (int i = 0; i < arrQue.Length; i++)
            {
                ListItem item = new ListItem(arrQue[i], i.ToString());
                if (ckbList != null && ckbList.Exists(x => x.Answer == i.ToString()))
                    item.Selected = true;
                checkBoxList.Items.Add(item);
            }
        }
        private void CreateTxt(QuestionModel question)
        {
            AnswerModel txt = _answerList.Find(x => x.QuestionNo == question.QuestionNo);
            TextBox textBox = new TextBox();
            textBox.ID = "Q" + question.QuestionNo;
            textBox.Enabled = false;
            if (txt != null)
                textBox.Text = txt.Answer;
            this.plcDynamic.Controls.Add(textBox);
        }
    }
}