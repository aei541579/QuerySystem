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
    public partial class List : System.Web.UI.Page
    {
        private static QuestionnaireMgr _mgr = new QuestionnaireMgr();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<QuestionnaireModel> questionnaireList = _mgr.GetQuestionnaireList();
                InitRpt(questionnaireList);

            }
        }
        private void InitRpt(List<QuestionnaireModel> questionnaireList)
        {
            this.rptTable.DataSource = questionnaireList;
            this.rptTable.DataBind();
            int i = questionnaireList.Count;
            foreach (RepeaterItem item in this.rptTable.Items)
            {
                Label lblNumber = item.FindControl("lblNumber") as Label;
                lblNumber.Text = i.ToString();
                i--;
            }
        }

        protected void rptTable_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in this.rptTable.Items)
            {
                HiddenField hfID = item.FindControl("hfID") as HiddenField;
                CheckBox ckbDel = item.FindControl("ckbDel") as CheckBox;
                if (ckbDel.Checked && Guid.TryParse(hfID.Value, out Guid questionnaireID))
                    _mgr.DeleteQuestionnaire(questionnaireID);
            }
            List<QuestionnaireModel> questionnaireList = _mgr.GetQuestionnaireList();
            InitRpt(questionnaireList);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<QuestionnaireModel> searchList = new List<QuestionnaireModel>();
            List<QuestionnaireModel> dataList = 
                (string.IsNullOrWhiteSpace(this.txtTitle.Text)) 
                ? _mgr.GetQuestionnaireList() 
                : _mgr.GetQuestionnaireList(this.txtTitle.Text);

            foreach (QuestionnaireModel result in dataList)
            {
                if (DateTime.TryParse(this.txtStartTime.Text, out DateTime startTime) &&
                    result.StartTime < startTime)
                    return;
                else if (DateTime.TryParse(this.txtEndTime.Text, out DateTime endTime) &&
                   result.EndTime > endTime)
                    return;
                searchList.Add(result);
            }
            InitRpt(searchList);

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("QuestionDesign.aspx");
        }
    }
}