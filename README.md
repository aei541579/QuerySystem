Query System
-----
#### 軟體基本功能
1. 前台可搜尋問卷、作答、觀看統計數據
2. 後台可設計問卷、管理問卷(新增/編輯/刪除)，觀看統計數據
3. 後台可查看所有作答詳細資料，並匯出表單
4. 後台可建立/管理常用問題，並引用至問卷設計
</br>

#### 環境依賴
asp.net framework4.8 
</br></br>

#### 資料庫建置(MSSQL)
1. 結構與描述 `structure.sql`
2. 資料 `data.sql`
3. 備份 `QuerySystem.bak`
</br>

#### 目錄結構描述
(in QuerySystem.sln)
>**List.aspx** `前台起始頁面(列表頁)`</br>
>Form.aspx `作答頁`</br>
>ConfirmPage.aspx `確認頁`</br>
>Stastic.aspx `前台統計頁`</br>
>
>**[SystemAdmin]** 
>>**List.aspx** `後台起始頁面(列表頁)`</br>
>>QuestionDesign.aspx `問卷設計頁`</br>
>>QuestionDetail.aspx `問題設計頁`</br>
>>AnswerList.aspx `作答列表頁`</br>
>>AnswerDetail.aspx `作答詳細資料頁`</br>
>>AnswerStastic.aspx `後台統計頁`</br></br>
>>ExampleList.aspx `常用問題列表頁`</br>
>>ExampleDesign.aspx `常用問題設計頁`
>>
>**[Models]**
>>QuestionnaireModel.cs `問卷model`</br>
>>QuestionModel.cs `問題model`</br>
>>AnswerModel.cs `答案model`</br>
>>PersonModel.cs `作答者model`</br>
>>StasticModel.cs `統計model`
>>
>**[Managers]**
>>QuestionnaireMgr.cs `與MSSQL溝通的所有方法`
>>
>**[ShareControls]**
>>ucJSScript.ascx `引用js/css`</br>
>>ucLeftColumn.ascx `後台左側表單控制項`</br>
>>ucPager.ascx `列表頁分頁控制項`
>>
>**[API]**
>>AddQuestionHandler.ashx `用於後台建立問題`</br>
>>AnswerHandler.ashx `用於前台取得作答結果`</br>
>>CancelHandler.ashx `用於後台取消問卷建立`</br>
>>StasticHandler.ashx `用於統計頁`
>>
>**[Helpers]**
>>ConfigHelper.cs `讀取web.config相關參數`</br>
>>Logger.cs `寫入錯誤訊息`

