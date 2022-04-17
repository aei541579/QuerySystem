using QuerySystem.Managers;
using QuerySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuerySystem.SystemAdmin
{
    public partial class AnswerList : System.Web.UI.Page
    {
        private static QuestionnaireMgr _mgr = new QuestionnaireMgr();
        private static Guid _questionnaireID;
        protected void Page_Load(object sender, EventArgs e)
        {
            string IDstring = Request.QueryString["ID"];
            if (Guid.TryParse(IDstring, out _questionnaireID))
            {
                List<PersonModel> personList = _mgr.GetPersonList(_questionnaireID);
                this.rptList.DataSource = personList;
                this.rptList.DataBind();
                int i = personList.Count;
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
    }
}