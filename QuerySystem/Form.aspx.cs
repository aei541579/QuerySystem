using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuerySystem.Managers;
using QuerySystem.Models;

namespace QuerySystem
{
    public partial class Form : System.Web.UI.Page
    {
        private static QuestionnaireMgr _mgr = new QuestionnaireMgr();
        private static Guid _questionnaireID;
        private static List<AnswerModel> _answerList;
        protected void Page_Load(object sender, EventArgs e)
        {
            string IDstring = Request.QueryString["ID"];
            if (Guid.TryParse(IDstring, out _questionnaireID))
            {
                _answerList = HttpContext.Current.Session["answerModel"] as List<AnswerModel>;
                bool isEditMode = _answerList == null ? false : true;

                QuestionnaireModel questionnaire = _mgr.GetQuestionnaire(_questionnaireID);
                this.hfID.Value = _questionnaireID.ToString();
                this.ltlTitle.Text = questionnaire.QueryName;
                this.ltlContent.Text = questionnaire.QueryContent;

                List<QuestionModel> questionList = _mgr.GetQuestionList(_questionnaireID);
                foreach (QuestionModel question in questionList)
                {
                    string q = $"<br/>{question.QuestionNo}. {question.QuestionVal}";
                    if (question.Necessary)
                        q += "(*必填)";
                    Literal ltlQuestion = new Literal();
                    ltlQuestion.Text = q + "<br/>";
                    this.plcDynamic.Controls.Add(ltlQuestion);
                    if (isEditMode)
                    {
                        switch (question.Type)
                        {
                            case QuestionType.單選方塊:
                                EditRdb(question);
                                break;
                            case QuestionType.複選方塊:
                                EditCkb(question);
                                break;
                            case QuestionType.文字:
                                EditTxt(question);
                                break;
                        }
                    }
                    else
                    {
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
                if (isEditMode)
                {
                    PersonModel person = HttpContext.Current.Session["personModel"] as PersonModel;
                    this.txtName.Text = person.Name;
                    this.txtMobile.Text = person.Mobile;
                    this.txtMail.Text = person.Email;
                    this.txtAge.Text = person.Age;
                }
            }
            else
                Response.Redirect("List.aspx");

            
        }
        private void CreateRdb(QuestionModel question)
        {
            RadioButtonList radioButtonList = new RadioButtonList();
            radioButtonList.ID = "Q" + question.QuestionNo;
            this.plcDynamic.Controls.Add(radioButtonList);
            string[] arrQue = question.Selection.Split(';');
            for (int i = 0; i < arrQue.Length; i++)
            {
                ListItem item = new ListItem(arrQue[i], i.ToString());
                radioButtonList.Items.Add(item);
            }
        }
        private void CreateCkb(QuestionModel question)
        {
            CheckBoxList checkBoxList = new CheckBoxList();
            checkBoxList.ID = "Q" + question.QuestionNo;
            this.plcDynamic.Controls.Add(checkBoxList);
            string[] arrQue = question.Selection.Split(';');
            for (int i = 0; i < arrQue.Length; i++)
            {
                ListItem item = new ListItem(arrQue[i], i.ToString());
                checkBoxList.Items.Add(item);
            }
        }
        private void CreateTxt(QuestionModel question)
        {
            TextBox textBox = new TextBox();
            textBox.ID = "Q" + question.QuestionNo;
            this.plcDynamic.Controls.Add(textBox);
        }
        private void EditRdb(QuestionModel question)
        {
            AnswerModel rdb = _answerList.Find(x => x.QuestionNo == question.QuestionNo);
            RadioButtonList radioButtonList = new RadioButtonList();
            radioButtonList.ID = "Q" + question.QuestionNo;
            this.plcDynamic.Controls.Add(radioButtonList);
            string[] arrQue = question.Selection.Split(';');
            for (int i = 0; i < arrQue.Length; i++)
            {
                ListItem item = new ListItem(arrQue[i], i.ToString());
                if (Convert.ToInt32(rdb.Answer) == i)
                    item.Selected = true;
                radioButtonList.Items.Add(item);
            }
        }
        private void EditCkb(QuestionModel question)
        {
            List<AnswerModel> ckbList = _answerList.FindAll(x => x.QuestionNo == question.QuestionNo);
            CheckBoxList checkBoxList = new CheckBoxList();
            checkBoxList.ID = "Q" + question.QuestionNo;
            this.plcDynamic.Controls.Add(checkBoxList);
            string[] arrQue = question.Selection.Split(';');
            for (int i = 0; i < arrQue.Length; i++)
            {
                ListItem item = new ListItem(arrQue[i], i.ToString());
                if (ckbList.Find(x => x.Answer == i.ToString()) != null)
                    item.Selected = true;
                checkBoxList.Items.Add(item);
            }
        }
        private void EditTxt(QuestionModel question)
        {
            AnswerModel txt = _answerList.Find(x => x.QuestionNo == question.QuestionNo);
            TextBox textBox = new TextBox();
            textBox.ID = "Q" + question.QuestionNo;
            textBox.Text = txt.Answer;
            this.plcDynamic.Controls.Add(textBox);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }

    }
}