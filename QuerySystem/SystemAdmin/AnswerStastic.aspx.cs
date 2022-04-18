using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
                            ltlSelection.Text = "尚無資料<br/>";
                        else
                        {
                            string selection = "<table>";
                            string[] arrQue = question.Selection.Split(';');
                            for (int i = 0; i < arrQue.Length; i++)
                            {
                                int ansCount = 0;
                                StasticModel stastic = NoList.Find(x => x.Answer == i.ToString());
                                if (stastic != null)
                                    ansCount = stastic.AnsCount;
                                selection +=
                                    $@"<tr>
                                     <td>{arrQue[i]}</td>
                                     <td>{ansCount * 100 / total}%</td>
                                     <td>({ansCount})</td>
                                 </tr>";
                            }
                            selection += "</table>";
                            ltlSelection.Text = selection;
                        }
                        this.plcDynamic.Controls.Add(ltlSelection);
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