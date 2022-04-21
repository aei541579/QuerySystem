using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuerySystem.ShareControls
{
    public partial class ucPager : System.Web.UI.UserControl
    {
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 5;
        public int totalRows { get; set; } = 0;
        private string _url = null;
        public string Url
        {
            get
            {
                if (this._url == null)
                    return Request.Url.LocalPath;
                else
                    return this._url;
            }
            set
            {
                this._url = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Bind()
        {
            NameValueCollection collection = new NameValueCollection();
            Bind(collection);
        }
        public void Bind(string[] paramKey, string[] paramValues)
        {
            NameValueCollection collection = new NameValueCollection();
            for (int i = 0; i < paramKey.Length; i++)
            {
                collection.Add(paramKey[i], paramValues[i]);
            }
            Bind(collection);
        }

        public void Bind(NameValueCollection collection)
        {
            int pageCount = (totalRows / pageSize);
            if (totalRows % pageSize > 0)
                pageCount += 1;

            //LocalPath: MapList.aspx            
            string url = Request.Url.LocalPath;
            string paramKeyword = BuildQueryString(collection);

            this.aLinkFirst.HRef = url + "?Page=1" + paramKeyword;

            this.aLinkPrev.HRef = url + "?Page=" + (pageIndex - 1) + paramKeyword;
            if (pageIndex <= 1)
                this.aLinkPrev.Visible = false;

            this.aLinkNext.HRef = url + "?Page=" + (pageIndex + 1) + paramKeyword;
            if (pageIndex + 1 > pageCount)
                this.aLinkNext.Visible = false;

            this.aLinkPage1.HRef = url + "?Page=" + (pageIndex - 2) + paramKeyword;
            this.aLinkPage1.InnerText = (pageIndex - 2).ToString();
            if (pageIndex <= 2)
                this.aLinkPage1.Visible = false;

            this.aLinkPage2.HRef = url + "?Page=" + (pageIndex - 1) + paramKeyword;
            this.aLinkPage2.InnerText = (pageIndex - 1).ToString();
            if (pageIndex <= 1)
                this.aLinkPage2.Visible = false;

            this.aLinkPage3.HRef = "";
            this.aLinkPage3.InnerText = pageIndex.ToString();

            this.aLinkPage4.HRef = url + "?Page=" + (pageIndex + 1) + paramKeyword;
            this.aLinkPage4.InnerText = (pageIndex + 1).ToString();
            if (pageIndex + 1 > pageCount)
                this.aLinkPage4.Visible = false;

            this.aLinkPage5.HRef = url + "?Page=" + (pageIndex + 2) + paramKeyword;
            this.aLinkPage5.InnerText = (pageIndex + 2).ToString();
            if (pageIndex + 2 > pageCount)
                this.aLinkPage5.Visible = false;

            this.aLinkLast.HRef = url + "?Page=" + pageCount + paramKeyword;

        }
        private string BuildQueryString(NameValueCollection collection)
        {
            List<string> paramList = new List<string>();
            foreach (string key in collection.AllKeys)
            {
                if (collection.GetValues(key) == null)
                    continue;
                foreach (string val in collection.GetValues(key))
                {
                    paramList.Add($"&{key}={val}");
                }
            }
            string result = string.Join("", paramList);
            return result;
        }
    }
}