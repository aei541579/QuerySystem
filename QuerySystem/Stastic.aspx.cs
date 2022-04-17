using QuerySystem.Managers;
using QuerySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace QuerySystem
{
    public partial class Stastic : System.Web.UI.Page
    {
        private static QuestionnaireMgr _mgr = new QuestionnaireMgr();
        private static Guid _questionnaireID;
        protected void Page_Load(object sender, EventArgs e)
        {
            string IDstring = Request.QueryString["ID"];
            if (Guid.TryParse(IDstring, out _questionnaireID))
            {
                QuestionnaireModel questionnaire = _mgr.GetQuestionnaire(_questionnaireID);
                this.ltlTitle.Text = questionnaire.QueryName;
                this.ltlContent.Text = questionnaire.QueryContent;

                List<QuestionModel> questionList = _mgr.GetQuestionList(_questionnaireID);
                List<StasticModel> stasticList = _mgr.GetStasticList(_questionnaireID);
                foreach (QuestionModel question in questionList)
                {
                    string q = $"<br/>{question.QuestionNo}. {question.QuestionVal}";
                    if (question.Necessary)
                        q += "(*必填)";
                    Literal ltlQuestion = new Literal();
                    ltlQuestion.Text = q + "<br/>";
                    this.plcDynamic.Controls.Add(ltlQuestion);
                    if (question.Type != QuestionType.文字)
                    {
                        List<StasticModel> NoList = stasticList.FindAll(x => x.QuestionNo == question.QuestionNo);
                        int total = 0;
                        foreach (StasticModel item in NoList)
                        {
                            total += item.AnsCount;
                        }

                        string[] arrQue = question.Selection.Split(';');
                        for (int i = 0; i < arrQue.Length; i++)
                        {
                            int ansCount = 0;
                            StasticModel stastic = NoList.Find(x => x.Answer == i.ToString());
                            if (stastic != null)
                                ansCount = stastic.AnsCount;

                            Literal ltlSelection = new Literal();
                            ltlSelection.Text = $"{arrQue[i]} : {ansCount * 100 / total}% ({ansCount})";
                            this.plcDynamic.Controls.Add(ltlSelection);

                            HtmlGenericControl outterDiv = new HtmlGenericControl("div");
                            outterDiv.Style.Value = "width:50%;height:20px;border:1px solid black;";
                            this.plcDynamic.Controls.Add(outterDiv);
                            HtmlGenericControl innerDiv = new HtmlGenericControl("div");
                            innerDiv.Style.Value = $"width:{ansCount * 100 / total}%;height:20px;background-color:gray;color:white;font-weight:bold;";
                            outterDiv.Controls.Add(innerDiv);                            
                        }
                    }
                    else
                    {
                        Literal ltlSelection = new Literal();
                        ltlSelection.Text = "-<br/>";
                        this.plcDynamic.Controls.Add(ltlSelection);
                    }
                }
            }
            else
                Response.Redirect("List.aspx");

        }

    }
}