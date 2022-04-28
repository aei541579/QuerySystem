using System;
using System.Web;
using QuerySystem.Models;
using QuerySystem.Managers;
using QuerySystem.Helpers;
using System.Web.UI.HtmlControls;

namespace QuerySystem.SystemAdmin
{
    public partial class QusetionDesign : System.Web.UI.Page
    {
        private static QuestionnaireMgr _mgr = new QuestionnaireMgr();
        private static Guid _questionnaireID;
        /// <summary>
        /// 此問卷是否尚未被建立(寫入資料庫)
        /// </summary>
        private static bool _isNewQuestionnaire;
        protected void Page_Load(object sender, EventArgs e)
        {
            string IDstring = Request.QueryString["ID"];
            if (string.IsNullOrWhiteSpace(IDstring))
            {
                _isNewQuestionnaire = true;
                if (!IsPostBack)
                    initCreateMode();
                //為使頁籤得以跳轉，有將querystring的ID寫入session，若為新建模式則清除此session
                HttpContext.Current.Session.Remove("ID");
            }
            else if (Guid.TryParse(IDstring, out _questionnaireID))
            {
                //若session有問卷，則問卷仍未寫進資料庫(自問題編輯頁跳轉回來)
                QuestionnaireModel questionnaire = HttpContext.Current.Session["QuestionnaireSession"] as QuestionnaireModel;
                if (questionnaire != null)
                {
                    _isNewQuestionnaire = true;
                    //隱藏後面2個頁籤
                    HtmlAnchor linkAlist = Master.FindControl("Alist") as HtmlAnchor;
                    HtmlAnchor linkAstastic = Master.FindControl("Astastic") as HtmlAnchor;
                    linkAlist.Visible = false;
                    linkAstastic.Visible = false;
                }
                else
                {
                    _isNewQuestionnaire = false;
                    //session無問卷，則從資料庫叫問卷
                    questionnaire = _mgr.GetQuestionnaire(_questionnaireID);
                    //若回傳null，表示為手動輸入ID且此問卷不存在，跳轉回清單頁
                    if(questionnaire == null)
                        Response.Redirect(ConfigHelper.ListPage());
                }

                if (!IsPostBack)
                    initEditMode(questionnaire);
                //將此ID寫入session，以利頁籤跳轉
                HttpContext.Current.Session["ID"] = _questionnaireID;
            }
            else
                Response.Redirect(ConfigHelper.ListPage());

        }
        /// <summary>
        /// 初始化編輯模式(將問卷model代入欄位)
        /// </summary>
        /// <param name="questionnaire"></param>
        private void initEditMode(QuestionnaireModel questionnaire)
        {            
            this.txtTitle.Text = questionnaire.QueryName;
            this.txtContent.Text = questionnaire.QueryContent;
            this.txtStartTime.Text = questionnaire.StartTime.ToString("yyyy-MM-dd");
            this.txtEndTime.Text = questionnaire.EndTime.ToString("yyyy-MM-dd");
            this.ckbActive.Checked = (questionnaire.IsActive == ActiveType.開放);
        }
        /// <summary>
        /// 初始化新增模式(將後3個頁籤隱藏)
        /// </summary>
        private void initCreateMode()
        {
            this.txtStartTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            HtmlAnchor linkQdesign = Master.FindControl("Qdetail") as HtmlAnchor;
            HtmlAnchor linkAlist = Master.FindControl("Alist") as HtmlAnchor;
            HtmlAnchor linkAstastic = Master.FindControl("Astastic") as HtmlAnchor;
            linkQdesign.Visible = false;
            linkAlist.Visible = false;
            linkAstastic.Visible = false;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (InputError(out string errorMsg))
            {
                this.ltlAlert.Text = errorMsg;
                return;
            }

            QuestionnaireModel questionnaire = new QuestionnaireModel()
            {
                QuestionnaireID = Guid.NewGuid(),
                QueryName = this.txtTitle.Text.Trim(),
                QueryContent = this.txtContent.Text.Trim(),
                StartTime = Convert.ToDateTime(this.txtStartTime.Text),
                EndTime = Convert.ToDateTime(this.txtEndTime.Text),
                IsActive = this.ckbActive.Checked ? ActiveType.開放 : ActiveType.已關閉
            };

            if (_isNewQuestionnaire)
            {
                //若為新問卷，則先寫入session，待問題建立完成後再一併寫入資料庫
                questionnaire.QuestionnaireID = Guid.NewGuid();
                HttpContext.Current.Session["QuestionnaireSession"] = questionnaire;
            }
            else
            {
                //若不為新問卷(編輯舊問卷)，直接更新資料庫內的問卷
                questionnaire.QuestionnaireID = _questionnaireID;
                _mgr.UpdateQuestionnaire(questionnaire);
            }
            HttpContext.Current.Session["ID"] = questionnaire.QuestionnaireID;
            Response.Redirect(ConfigHelper.QuestionDetailPage() + questionnaire.QuestionnaireID);
        }
        /// <summary>
        /// 判斷問卷是否有不合理的輸入
        /// </summary>
        /// <param name="errorMsg">錯誤輸入提示訊息</param>
        /// <returns></returns>
        private bool InputError(out string errorMsg)
        {
            errorMsg = string.Empty;
            if (string.IsNullOrWhiteSpace(this.txtTitle.Text.Trim()))
                errorMsg += "**必須輸入問卷標題**<br/>";
            if (string.IsNullOrWhiteSpace(this.txtStartTime.Text))
                errorMsg += "**必須輸入起始日期**<br/>";
            else if (Convert.ToDateTime(this.txtStartTime.Text) < DateTime.Today && _isNewQuestionnaire)
                errorMsg += "**起始日期不可早於今天**<br/>";
            else if (string.IsNullOrWhiteSpace(this.txtEndTime.Text))
                errorMsg += "**必須輸入結束日期**<br/>";
            else if (Convert.ToDateTime(this.txtStartTime.Text) > Convert.ToDateTime(this.txtEndTime.Text))
                errorMsg += "**起始日期不可晚於結束日期**<br/>";

            if (string.IsNullOrEmpty(errorMsg))
                return false;
            return true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.RemoveAll();
            Response.Redirect(ConfigHelper.ListPage());
        }
    }
}