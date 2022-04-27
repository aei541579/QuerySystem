using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuerySystem.Managers;
using QuerySystem.Models;
using QuerySystem.Helpers;

namespace QuerySystem
{
    public partial class Form : System.Web.UI.Page
    {
        private static QuestionnaireMgr _mgr = new QuestionnaireMgr();
        private static Guid _questionnaireID;
        private static List<AnswerModel> _answerList;
        private bool _isEditMode;
        protected void Page_Load(object sender, EventArgs e)
        {
            string IDstring = Request.QueryString["ID"];
            if (Guid.TryParse(IDstring, out _questionnaireID))
            {
                _answerList = HttpContext.Current.Session["answerModel"] as List<AnswerModel>;
                _isEditMode = _answerList == null ? false : true;

                QuestionnaireModel questionnaire = _mgr.GetQuestionnaire(_questionnaireID);
                if (questionnaire == null)
                    Response.Redirect(ConfigHelper.ListPage());
                this.hfID.Value = _questionnaireID.ToString();
                this.ltlTitle.Text = questionnaire.QueryName;
                this.ltlContent.Text = questionnaire.QueryContent;
                if (_isEditMode)
                {
                    PersonModel person = HttpContext.Current.Session["personModel"] as PersonModel;
                    this.txtName.Text = person.Name;
                    this.txtMobile.Text = person.Mobile;
                    this.txtMail.Text = person.Email;
                    this.txtAge.Text = person.Age;
                }

                List<QuestionModel> questionList = _mgr.GetQuestionList(_questionnaireID);
                foreach (QuestionModel question in questionList)
                {
                    string qText = $"<br/>{question.QuestionNo}. {question.QuestionVal}";
                    if (question.Necessary)
                        qText += "(*必填)";
                    Literal ltlQuestion = new Literal();
                    ltlQuestion.Text = qText + "<br/>";
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
                Response.Redirect(ConfigHelper.ListPage());


        }
        /// <summary>
        /// 建立單選方塊
        /// </summary>
        /// <param name="question"></param>
        private void CreateRdb(QuestionModel question)
        {
            AnswerModel rdb = _isEditMode
                ? _answerList.Find(x => x.QuestionNo == question.QuestionNo)
                : new AnswerModel();
            RadioButtonList radioButtonList = new RadioButtonList();
            radioButtonList.ID = "Q" + question.QuestionNo;
            if (question.Necessary)
                radioButtonList.CssClass = "Necessary";
            this.plcDynamic.Controls.Add(radioButtonList);
            string[] arrQue = question.Selection.Split(';');
            for (int i = 0; i < arrQue.Length; i++)
            {
                ListItem item = new ListItem(arrQue[i], i.ToString());
                if (rdb != null && rdb.Answer == i.ToString())
                    item.Selected = true;
                radioButtonList.Items.Add(item);
            }
        }
        /// <summary>
        /// 建立複選方塊
        /// </summary>
        /// <param name="question"></param>
        private void CreateCkb(QuestionModel question)
        {
            List<AnswerModel> ckbList = _isEditMode
                ? _answerList.FindAll(x => x.QuestionNo == question.QuestionNo)
                : new List<AnswerModel>();
            CheckBoxList checkBoxList = new CheckBoxList();
            checkBoxList.ID = "Q" + question.QuestionNo; 
            if (question.Necessary)
                checkBoxList.CssClass = "Necessary";
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
        /// <summary>
        /// 建立文字方塊
        /// </summary>
        /// <param name="question"></param>
        private void CreateTxt(QuestionModel question)
        {
            AnswerModel txt = _isEditMode
                ? _answerList.Find(x => x.QuestionNo == question.QuestionNo)
                : new AnswerModel();
            TextBox textBox = new TextBox();
            textBox.ID = "Q" + question.QuestionNo;
            if (question.Necessary)
                textBox.CssClass = "Necessary";
            if (_isEditMode && txt != null)
                textBox.Text = txt.Answer;
            this.plcDynamic.Controls.Add(textBox);
        }

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    HttpContext.Current.Session.RemoveAll();
        //    Response.Redirect(ConfigHelper.ListPage());
        //}

    }
}