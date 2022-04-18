using QuerySystem.Managers;
using QuerySystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuerySystem.SystemAdmin
{
    public partial class AnswerList : System.Web.UI.Page
    {
        private static QuestionnaireMgr _mgr = new QuestionnaireMgr();
        private static Guid _questionnaireID;
        private static List<PersonModel> _personList;
        protected void Page_Load(object sender, EventArgs e)
        {
            string IDstring = Request.QueryString["ID"];
            if (Guid.TryParse(IDstring, out _questionnaireID))
            {
                _personList = _mgr.GetPersonList(_questionnaireID);
                this.rptList.DataSource = _personList;
                this.rptList.DataBind();
                int i = _personList.Count;
                foreach (RepeaterItem item in this.rptList.Items)
                {
                    Label lblNumber = item.FindControl("lblNumber") as Label;
                    lblNumber.Text = i.ToString();
                    i--;
                }
            }
            else
                Response.Redirect("List.aspx");
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            QuestionnaireModel questionnaire = _mgr.GetQuestionnaire(_questionnaireID);
            string FileName = questionnaire.QueryName + ".csv";
            string fd = "C:\\Csharpclass\\QuerySystem\\DownLoad";
            if (!Directory.Exists(fd))
            {
                Directory.CreateDirectory(fd);
            }            
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";

                #region Export CSV

                Response.ContentType = "application/octet-stream";
                Response.ContentEncoding = Encoding.UTF8;
                //Response.ContentType = "application/vnd.ms-csv";
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    //StreamWriter csvFileStreamWriter = new StreamWriter(MyMemoryStream);
                    StreamWriter csvFileStreamWriter = new StreamWriter(MyMemoryStream, Encoding.UTF8);
                    //string csvContent = "";
                    StringBuilder csvContent = new StringBuilder();

                    List<QuestionModel> questionList = _mgr.GetQuestionList(_questionnaireID);
                    string navString = "";
                    foreach (QuestionModel question in questionList)
                    {
                        navString += $",{question.QuestionNo}. {question.QuestionVal}";
                        if (question.Necessary)
                            navString += "(*必填)";
                    }
                    csvContent.Append($"姓名,手機,Email,年齡,填寫時間{navString}\r\n");

                    string tableString = "";
                    foreach (PersonModel person in _personList)
                    {
                        tableString += $"{person.Name},{person.Mobile},{person.Email},{person.Age},{person.CreateTime}";
                        List<AnswerModel> answerList = _mgr.GetAnswerList(person.PersonID);
                        foreach (QuestionModel question in questionList)
                        {
                            switch (question.Type)
                            {
                                case QuestionType.單選方塊:
                                    tableString += ",";
                                    AnswerModel rdb = answerList.Find(x => x.QuestionNo == question.QuestionNo);
                                    if (rdb != null)
                                    {
                                        string[] arrSelection = question.Selection.Split(';');
                                        string selection = arrSelection[Convert.ToInt32(rdb.Answer)];
                                        tableString += selection;
                                    }
                                    break;

                                case QuestionType.複選方塊:
                                    tableString += ",";
                                    List<AnswerModel> ckb = answerList.FindAll(x => x.QuestionNo == question.QuestionNo);
                                    if (ckb != null)
                                    {
                                        string[] arrSelection = question.Selection.Split(';');
                                        for (int i = 0; i < ckb.Count; i++)
                                        {
                                            if (i != 0)
                                                tableString += ";";
                                            tableString += arrSelection[Convert.ToInt32(ckb[i].Answer)];
                                        }
                                    }
                                    break;

                                case QuestionType.文字:
                                    tableString += ",";
                                    AnswerModel txt = answerList.Find(x => x.QuestionNo == question.QuestionNo);
                                    if (txt != null)
                                        tableString += txt.Answer;
                                    break;

                            }
                        }
                        tableString += "\r\n";

                    }
                    csvContent.Append(tableString);

                    csvFileStreamWriter.Write(csvContent);
                    csvFileStreamWriter.Flush();
                    MyMemoryStream.Position = 0;
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
                #endregion Export CSV
            }
            catch (Exception ex)
            {

            }
        }
    }
}