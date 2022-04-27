using QuerySystem.Managers;
using QuerySystem.Models;
using QuerySystem.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace QuerySystem
{
    public partial class List : System.Web.UI.Page
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
                GetSearchResult();
            }
        }
        private void GetSearchResult()
        {
            List<QuestionnaireModel> searchList = new List<QuestionnaireModel>();
            string keyword = this.Request.QueryString["keyword"];
            string start = this.Request.QueryString["start"];
            string end = this.Request.QueryString["end"];
            List<QuestionnaireModel> dataList = _mgr.GetQuestionnaireList().FindAll(x => x.IsActive == ActiveType.開放);
            if (!string.IsNullOrWhiteSpace(keyword))
               dataList = dataList.FindAll(x => x.QueryName.Contains(keyword));

            this.txtTitle.Text = keyword;
            if (DateTime.TryParse(start, out DateTime startTime))
                this.txtStartTime.Text = startTime.ToString("yyyy-MM-dd");
            if (DateTime.TryParse(end, out DateTime endTime))
                this.txtEndTime.Text = endTime.ToString("yyyy-MM-dd");

            foreach (QuestionnaireModel result in dataList)
            {
                if (start != null && result.StartTime < startTime)
                    continue;
                else if (end != null && result.EndTime > endTime)
                    continue;
                searchList.Add(result);
            }
            List<QuestionnaireModel> resultList = _mgr.GetIndexList(_pageIndex, _pageSize, searchList);
            _totalRows = searchList.Count;
            this.ucPager.totalRows = _totalRows;
            this.ucPager.pageIndex = _pageIndex;
            string[] paramKey = { "keyword", "start", "end" };
            string[] paramValues = { keyword, start, end };
            this.ucPager.Bind(paramKey, paramValues);
            InitRpt(resultList);
        }

        /// <summary>
        /// 初始化清單列表
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
                HiddenField hfID = item.FindControl("hfID") as HiddenField;
                HtmlAnchor aQueLink = item.FindControl("aQueLink") as HtmlAnchor;
                Label lblState = item.FindControl("lblState") as Label;
                if (lblState.Text != "投票中")
                    aQueLink.HRef = "";
                else
                    aQueLink.HRef = ConfigHelper.FormPage() + "?ID=" + hfID.Value;

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string redirectUrl = this.Request.Url.LocalPath + "?Page=1";
            if (!string.IsNullOrWhiteSpace(this.txtTitle.Text.Trim()))
                redirectUrl += "&keyword=" + this.txtTitle.Text.Trim();
            if (DateTime.TryParse(this.txtStartTime.Text, out DateTime startTime))
                redirectUrl += "&start=" + startTime.ToShortDateString();
            if (DateTime.TryParse(this.txtEndTime.Text, out DateTime endTime))
                redirectUrl += "&end=" + endTime.ToShortDateString();
            Response.Redirect(redirectUrl);
        }

    }
}