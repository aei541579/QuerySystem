using QuerySystem.Models;
using QuerySystem.Managers;
using QuerySystem.Helpers;
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
        private const int _pageSize = 10;
        private static int _pageIndex;
        private static int _totalRows;
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Session.RemoveAll();
            string pageIndexText = this.Request.QueryString["Page"];
            _pageIndex = (string.IsNullOrWhiteSpace(pageIndexText)) ? 1 : Convert.ToInt32(pageIndexText);

            if (!IsPostBack)
            {
                string keyword = this.Request.QueryString["keyword"];
                List<QuestionnaireModel> questionnaireList = _mgr.GetExampleList();
                if (!string.IsNullOrWhiteSpace(keyword))
                    questionnaireList = questionnaireList.FindAll(x => x.QueryName.Contains(keyword));

                List<QuestionnaireModel> resultList = _mgr.GetIndexList(_pageIndex, _pageSize, questionnaireList);
                this.txtTitle.Text = keyword;
                _totalRows = resultList.Count;
                this.ucPager.totalRows = _totalRows;
                this.ucPager.pageIndex = _pageIndex;
                string[] paramKey = { "keyword" };
                string[] paramValues = { keyword };
                this.ucPager.Bind(paramKey, paramValues);
                InitRpt(resultList);
            }
        }
        /// <summary>
        /// 建立清單列表
        /// </summary>
        /// <param name="questionnaireList"></param>
        private void InitRpt(List<QuestionnaireModel> questionnaireList)
        {
            this.rptTable.DataSource = questionnaireList;
            this.rptTable.DataBind();
            int i = _totalRows - (_pageIndex - 1) * _pageSize;
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
            Response.Redirect(this.Request.Url.LocalPath + "?Page=1");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string redirectUrl = this.Request.Url.LocalPath + "?Page=1";
            if (!string.IsNullOrWhiteSpace(this.txtTitle.Text.Trim()))
                redirectUrl += "&keyword=" + this.txtTitle.Text.Trim();
            Response.Redirect(redirectUrl);
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {           
            Response.Redirect(ConfigHelper.ExampleDesignPage() + Guid.NewGuid());
        }
    }
}