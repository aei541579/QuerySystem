using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuerySystem.Models;
using QuerySystem.Managers;
using QuerySystem.Helpers;
using System.Web.UI.HtmlControls;

namespace QuerySystem.SystemAdmin
{
    public partial class QuestionDetail : System.Web.UI.Page
    {
        private static QuestionnaireMgr _mgr = new QuestionnaireMgr();
        private static Guid _questionnaireID;
        private static List<QuestionModel> _questionSession;
        private static QuestionnaireModel _questionnaire;
        /// <summary>
        /// 此問卷是否尚未被寫入資料庫
        /// </summary>
        private static bool _isCreateMode;
        protected void Page_Load(object sender, EventArgs e)
        {
            _questionSession = HttpContext.Current.Session["qusetionModel"] as List<QuestionModel>;

            //若session有值，代表為新增模式
            _questionnaire = HttpContext.Current.Session["QuestionnaireSession"] as QuestionnaireModel;
            _isCreateMode = (_questionnaire != null) ? true : false;

            if (!IsPostBack)
            {
                string IDstring = Request.QueryString["ID"];
                if (Guid.TryParse(IDstring, out _questionnaireID))
                {
                    //若questionSession為null代表 1.剛剛沒有編輯到一半亂跳頁 2.為新增模式 =>從資料庫叫問題(新增模式傳回0筆也沒問題)
                    List<QuestionModel> questionList = _questionSession == null
                        ? _mgr.GetQuestionList(_questionnaireID)
                        : _questionSession;
                    InitRpt(questionList);
                    InitDdl();
                    InitTextbox();
                    HttpContext.Current.Session["qusetionModel"] = questionList;

                    if (_isCreateMode)
                    {
                        //隱藏後面2個頁籤
                        HtmlAnchor linkAlist = Master.FindControl("Alist") as HtmlAnchor;
                        HtmlAnchor linkAstastic = Master.FindControl("Astastic") as HtmlAnchor;
                        linkAlist.Visible = false;
                        linkAstastic.Visible = false;
                    }

                    if (_mgr.GetPersonList(_questionnaireID).Count > 0)
                        InitDisabledInput();
                }
                else
                    Response.Redirect(ConfigHelper.ListPage());
            }

        }
        /// <summary>
        /// 初始化常用問題(dropdownlist)
        /// </summary>
        private void InitDdl()
        {
            List<QuestionnaireModel> exampleList = _mgr.GetExampleList();
            foreach (QuestionnaireModel example in exampleList)
            {
                ListItem ddlItem = new ListItem();
                ddlItem.Text = example.QueryName;
                ddlItem.Value = example.QuestionnaireID.ToString();
                this.ddlTemplate.Items.Add(ddlItem);
            }
        }
        /// <summary>
        /// 建立問題列表
        /// </summary>
        /// <param name="questionList"></param>
        private void InitRpt(List<QuestionModel> questionList)
        {
            if (questionList != null || questionList.Count > 0)
            {
                int i = 1;
                this.rptQuestion.Visible = true;
                this.rptQuestion.DataSource = questionList;
                this.rptQuestion.DataBind();
                foreach (RepeaterItem item in this.rptQuestion.Items)
                {
                    Label lblNumber = item.FindControl("lblNumber") as Label;
                    lblNumber.Text = i.ToString();
                    i++;
                }
            }
            else
                this.rptQuestion.Visible = false;
        }
        /// <summary>
        /// 初始化(清空)欄位
        /// </summary>
        private void InitTextbox()
        {
            this.txtQuestion.Text = "";
            this.ddlQuestionType.SelectedIndex = 0;
            this.ckbNecessary.Checked = false;
            this.txtSelection.Text = "";
            this.btnAddQuestion.Visible = true;
            this.btnEditQuestion.Visible = false;
        }
        /// <summary>
        /// 加入問題的button事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddQuestion_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["addByButton"] != null)
            {
                QuestionModel question = new QuestionModel();
                FillSelectionContent(question, out bool errorInput);
                if (errorInput)
                    return;
                question.QuestionID = Guid.NewGuid();

                _questionSession.Add(question);
                HttpContext.Current.Session.Remove("addByButton");
            }

            HttpContext.Current.Session["qusetionModel"] = _questionSession;
            InitRpt(_questionSession);
            InitTextbox();
        }
        /// <summary>
        /// 依輸入的值建立問題model
        /// </summary>
        /// <param name="question"></param>
        /// <param name="errorInput"></param>
        private void FillSelectionContent(QuestionModel question, out bool errorInput)
        {
            errorInput = false;
            question.QuestionnaireID = _questionnaireID;
            question.Type = (QuestionType)Convert.ToInt32(this.ddlQuestionType.SelectedValue);
            if (NoInput(this.txtQuestion.Text.Trim()))
                errorInput = true;
            question.QuestionVal = this.txtQuestion.Text.Trim();
            if (question.Type != QuestionType.文字 && ErrorInput(this.txtSelection.Text.Trim()))
                errorInput = true;
            question.Selection = this.txtSelection.Text.Trim();
            question.Necessary = this.ckbNecessary.Checked;
        }
        /// <summary>
        /// 判斷是否無輸入問題標題
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool NoInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                this.ltlAlert.Visible = true;
                this.ltlAlert.Text = "**問題尚未輸入完畢**";
                return true;
            }
            else
            {
                this.ltlAlert.Visible = false;
                return false;
            }
        }
        /// <summary>
        /// 判斷回答設計是否不合理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool ErrorInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                this.ltlAlert.Visible = true;
                this.ltlAlert.Text = "**選項尚未輸入完畢**";
                return true;
            }
            try
            {
                string[] arrSelection = input.Split(';');
                if (arrSelection.Length <= 1)
                {
                    this.ltlAlert.Visible = true;
                    this.ltlAlert.Text = "**請輸入至少兩個選項**";
                    return true;
                }
                else
                {
                    foreach (var item in arrSelection)
                    {
                        if (string.IsNullOrWhiteSpace(item))
                        {
                            this.ltlAlert.Visible = true;
                            this.ltlAlert.Text = "**請正確輸入選項**";
                            return true;
                        }
                    }
                    this.ltlAlert.Visible = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.ltlAlert.Visible = true;
                this.ltlAlert.Text = "**選項輸入有誤**";
                return true;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<QuestionModel> needList = new List<QuestionModel>();
            foreach (RepeaterItem item in this.rptQuestion.Items)
            {
                HiddenField hfQuestionID = item.FindControl("hfQuestionID") as HiddenField;
                CheckBox ckbDelete = item.FindControl("ckbDel") as CheckBox;
                if (!ckbDelete.Checked && Guid.TryParse(hfQuestionID.Value, out Guid questionID))
                {
                    QuestionModel question = _questionSession.Find(x => x.QuestionID == questionID);
                    needList.Add(question);
                }
            }
            InitRpt(needList);
            InitTextbox();
            HttpContext.Current.Session["qusetionModel"] = needList;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Remove("qusetionModel");
            Response.Redirect(ConfigHelper.ListPage());
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (_questionSession.Count == 0)
            {
                this.ltlAlert.Visible = true;
                this.ltlAlert.Text = "**請至少建立一道題目**";
                return;
            }

            //若session有值，代表為新增狀態
            if (_isCreateMode)
                _mgr.CreateQuestionnaire(_questionnaire);

            //若資料庫已存在問題，一併先刪除舊問題，再寫入更新後問題
            if (_mgr.GetQuestionList(_questionnaireID).Count != 0)
                _mgr.DeleteQuestion(_questionnaireID);

            int questionNo = 1;
            foreach (QuestionModel question in _questionSession)
            {
                question.QuestionID = Guid.NewGuid();
                question.QuestionnaireID = _questionnaireID;
                question.QuestionNo = questionNo;
                _mgr.CreateQuestion(question);

                questionNo++;
            }
            HttpContext.Current.Session.Remove("qusetionModel");
            HttpContext.Current.Session.Remove("QuestionnaireSession");
            Response.Redirect(ConfigHelper.ListPage());
        }

        protected void rptQuestion_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "lkbEdit" && Guid.TryParse(e.CommandArgument.ToString(), out Guid questionID))
                {
                    QuestionModel question = _questionSession.Find(x => x.QuestionID == questionID);
                    this.hfEditQID.Value = question.QuestionID.ToString();
                    this.txtQuestion.Text = question.QuestionVal;
                    this.ddlQuestionType.SelectedIndex = (int)question.Type;
                    this.ckbNecessary.Checked = question.Necessary;
                    this.txtSelection.Text = question.Selection;
                    if (question.Type == QuestionType.文字)
                        this.txtSelection.Attributes.Add("disabled", "disable");
                    else
                        this.txtSelection.Attributes.Remove("disabled");
                    this.btnAddQuestion.Visible = false;
                    this.btnEditQuestion.Visible = true;
                }
            }
        }

        protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Guid.TryParse(this.ddlTemplate.SelectedValue, out Guid exampleID))
            {
                List<QuestionModel> question = _mgr.GetQuestionList(exampleID);
                HttpContext.Current.Session["qusetionModel"] = question;
                InitRpt(question);
            }
            else
            {
                InitRpt(new List<QuestionModel>());
            }
            InitTextbox();
        }

        protected void btnEditQuestion_Click(object sender, EventArgs e)
        {
            if (Guid.TryParse(this.hfEditQID.Value, out Guid editQID))
            {
                int index = _questionSession.FindIndex(x => x.QuestionID == editQID);
                QuestionModel question = _questionSession.Find(x => x.QuestionID == editQID);
                FillSelectionContent(question, out bool noInput);
                if (noInput)
                    return;
                _questionSession.RemoveAt(index);
                _questionSession.Insert(index, question);
                InitRpt(_questionSession);
                InitTextbox();
            }

        }
        /// <summary>
        /// 若已有作答，則將欄位enable設定為false
        /// </summary>
        private void InitDisabledInput()
        {
            this.ltlAlert.Visible = true;
            this.ltlAlert.Text = "**已經有人作答了，問題不能再修改了喔 **";
            this.ddlTemplate.Enabled = false;
            this.txtQuestion.Attributes.Add("disabled", "disable");
            this.ddlQuestionType.Enabled = false;
            this.ckbNecessary.Enabled = false;
            this.txtSelection.Attributes.Add("disabled", "disable");
            this.btnAddQuestion.Enabled = false;
            this.btnEditQuestion.Enabled = false;
            this.btnDelete.Enabled = false;
            foreach (RepeaterItem item in this.rptQuestion.Items)
            {
                LinkButton lkbEdit = item.FindControl("lkbEdit") as LinkButton;
                lkbEdit.Enabled = false;
            }
            this.divBtn.Visible = false;
        }
    }
}