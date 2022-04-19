using QuerySystem.Models;
using QuerySystem.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuerySystem.SystemAdmin
{
    public partial class ExampleList : System.Web.UI.Page
    {
        private static QuestionnaireMgr _mgr = new QuestionnaireMgr();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<QuestionnaireModel> questionnaireList = _mgr.GetExampleList();
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
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in this.rptTable.Items)
            {
                HiddenField hfID = item.FindControl("hfID") as HiddenField;
                CheckBox ckbDel = item.FindControl("ckbDel") as CheckBox;
                if (ckbDel.Checked && Guid.TryParse(hfID.Value, out Guid questionnaireID))
                    _mgr.DeleteQuestionnaire(questionnaireID);
            }
            List<QuestionnaireModel> questionnaireList = _mgr.GetExampleList();
            InitRpt(questionnaireList);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //List<QuestionnaireModel> searchList = new List<QuestionnaireModel>();
            List<QuestionnaireModel> searchList =
                (string.IsNullOrWhiteSpace(this.txtTitle.Text))
                ? _mgr.GetExampleList()
                : _mgr.GetExampleList(this.txtTitle.Text);

            InitRpt(searchList);

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Guid newID = Guid.NewGuid();
            _mgr.CreateExample(newID, this.txtCreate.Text.Trim());
            Response.Redirect("ExampleDesign.aspx?ID=" + newID);
        }
    }
}