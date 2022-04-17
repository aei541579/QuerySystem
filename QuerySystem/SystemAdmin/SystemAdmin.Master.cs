using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuerySystem.SystemAdmin
{
    public partial class SystemAdmin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var session = HttpContext.Current.Session["ID"];
            if(session!= null && Guid.TryParse(session.ToString(), out Guid questionnaireID))
            {
                this.Qdesign.HRef = "QuestionDesign.aspx?ID=" + questionnaireID;
                this.Qdetail.HRef = "QuestionDetail.aspx?ID=" + questionnaireID;
                this.Alist.HRef = "AnswerList.aspx?ID=" + questionnaireID;
                this.Astastic.HRef = "AnswerStastic.aspx?ID=" + questionnaireID;
            }

        }
    }
}