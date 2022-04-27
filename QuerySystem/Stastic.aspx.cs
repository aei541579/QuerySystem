using QuerySystem.Managers;
using QuerySystem.Models;
using QuerySystem.Helpers;
using System;
using System.Collections.Generic;
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
                if (questionnaire == null)
                    Response.Redirect(ConfigHelper.ListPage());

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
                            //計算該題回答總筆數
                            total += item.AnsCount;
                        }

                        if (total == 0)
                        {
                            Literal ltlNoAns = new Literal();
                            ltlNoAns.Text = "(尚無作答資料)<br/>";
                            this.plcDynamic.Controls.Add(ltlNoAns);
                        }
                        else
                        {
                            HtmlGenericControl legendPlaceholder = new HtmlGenericControl("div");
                            legendPlaceholder.ID = "legendPlaceholder";
                            HtmlGenericControl flotcontainer = new HtmlGenericControl("div");
                            flotcontainer.ID = "flotcontainer";
                            flotcontainer.Attributes.Add("class", question.QuestionNo.ToString());
                            
                            this.plcDynamic.Controls.Add(legendPlaceholder);
                            this.plcDynamic.Controls.Add(flotcontainer);
                        }
                    }
                    else
                    {
                        Literal ltlSelection = new Literal();
                        ltlSelection.Text = "(文字方塊不統計)<br/>";
                        this.plcDynamic.Controls.Add(ltlSelection);
                    }
                }
            }
            else
                Response.Redirect(ConfigHelper.ListPage());

        }

    }
}