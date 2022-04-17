using QuerySystem.Managers;
using QuerySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuerySystem
{
    public partial class ConfirmPage : System.Web.UI.Page
    {
        private static QuestionnaireMgr _mgr = new QuestionnaireMgr();
        private static Guid _questionnaireID;
        private static List<AnswerModel> _answerList;
        private static PersonModel _person;
        protected void Page_Load(object sender, EventArgs e)
        {
            _answerList = HttpContext.Current.Session["answerModel"] as List<AnswerModel>;
            string IDstring = Request.QueryString["ID"];
            if (Guid.TryParse(IDstring, out _questionnaireID))
            {
                QuestionnaireModel questionnaire = _mgr.GetQuestionnaire(_questionnaireID);
                this.hfID.Value = _questionnaireID.ToString();
                this.ltlTitle.Text = questionnaire.QueryName;
                this.ltlContent.Text = questionnaire.QueryContent;

                _person = HttpContext.Current.Session["personModel"] as PersonModel;
                this.txtName.Text = _person.Name;
                this.txtMobile.Text = _person.Mobile;
                this.txtMail.Text = _person.Email;
                this.txtAge.Text = _person.Age;

                List<QuestionModel> questionList = _mgr.GetQuestionList(_questionnaireID);
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
                if (Convert.ToInt32(rdb.Answer) == i)
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
                if (ckbList.Find(x => x.Answer == i.ToString()) != null)
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
            textBox.Text = txt.Answer;
            this.plcDynamic.Controls.Add(textBox);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Form.aspx?ID=" + _questionnaireID);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            _mgr.CreatePerson(_person);
            foreach (AnswerModel answer in _answerList)
            {
                _mgr.CreateAnswer(answer);
            }
            HttpContext.Current.Session.RemoveAll();
            Response.Redirect("List.aspx");
        }
    }
}