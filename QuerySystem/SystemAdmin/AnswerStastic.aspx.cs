using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using QuerySystem.Managers;
using QuerySystem.Models;

namespace QuerySystem.SystemAdmin
{
    public partial class AnswerStastic : System.Web.UI.Page
    {
        private static QuestionnaireMgr _mgr = new QuestionnaireMgr();
        protected void Page_Load(object sender, EventArgs e)
        {
            string IDstring = Request.QueryString["ID"];
            if (Guid.TryParse(IDstring, out Guid questionnaireID))
            {
                HttpContext.Current.Session["ID"] = questionnaireID;
                List<QuestionModel> questionList = _mgr.GetQuestionList(questionnaireID);
                List<StasticModel> stasticList = _mgr.GetStasticList(questionnaireID);
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

                        Literal ltlSelection = new Literal();
                        if (total == 0)
                        {
                            Literal ltlNoAns = new Literal();
                            ltlNoAns.Text = "尚無資料<br/>";
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