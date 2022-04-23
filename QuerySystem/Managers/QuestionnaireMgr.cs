using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using QuerySystem.Helpers;
using QuerySystem.Models;

namespace QuerySystem.Managers
{
    public class QuestionnaireMgr
    {
        public QuestionnaireModel GetQuestionnaire(Guid QuestionnaireID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Questionnaires]
                     WHERE ID = @ID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@ID", QuestionnaireID);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        QuestionnaireModel Questionnaire = new QuestionnaireModel();
                        if (reader.Read())
                        {
                            Questionnaire = BuildQuestionnaire(reader);
                        }
                        return Questionnaire;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.GetQuestionnaire", ex);
                throw;
            }
        }
        /// <summary>
        /// 取得所有問卷(非常用問卷)的清單
        /// </summary>
        /// <returns></returns>
        public List<QuestionnaireModel> GetQuestionnaireList()
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Questionnaires]
                     WHERE IsExample = 0
                     ORDER BY CreateTime DESC ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<QuestionnaireModel> questionnaireList = new List<QuestionnaireModel>();
                        while (reader.Read())
                        {
                            QuestionnaireModel questionnaire = BuildQuestionnaire(reader);
                            if (questionnaire.StartTime > DateTime.Now)
                                questionnaire.State = StateType.尚未開放;
                            else if (questionnaire.EndTime < DateTime.Now)
                                questionnaire.State = StateType.已結束;
                            else
                                questionnaire.State = StateType.投票中;
                            questionnaireList.Add(questionnaire);
                        }
                        return questionnaireList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.GetQuestionnaire", ex);
                throw;
            }
        }
        /// <summary>
        /// 取得所有問卷(非常用)搜尋結果清單
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<QuestionnaireModel> GetQuestionnaireList(string keyword)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Questionnaires]
                     WHERE QueryName Like '%'+@keyword+'%' AND IsExample = 0
                     ORDER BY CreateTime DESC ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@keyword", keyword);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<QuestionnaireModel> questionnaireList = new List<QuestionnaireModel>();
                        while (reader.Read())
                        {
                            QuestionnaireModel questionnaire = BuildQuestionnaire(reader);
                            if (questionnaire.StartTime > DateTime.Now)
                                questionnaire.State = StateType.尚未開放;
                            else if (questionnaire.EndTime < DateTime.Now)
                                questionnaire.State = StateType.已結束;
                            else
                                questionnaire.State = StateType.投票中;
                            questionnaireList.Add(questionnaire);
                        }
                        return questionnaireList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.GetQuestionnaire", ex);
                throw;
            }
        }
        /// <summary>
        /// 取得該分頁的清單
        /// </summary>
        /// <returns></returns>
        public List<QuestionnaireModel> GetIndexList(int pageIndex, int pageSize, List<QuestionnaireModel> list)
        {
            int skip = pageSize * (pageIndex - 1);  //計算跳頁數
            if (skip < 0)
                skip = 0;

            return list.Skip(skip).Take(pageSize).ToList();

        }       
        public void CreateQuestionnaire(QuestionnaireModel questionnaire)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  INSERT INTO [Questionnaires]
                        (ID, QueryName, QueryContent, StartTime, EndTime, IsExample, IsActive)
                     VALUES 
                        (@ID, @QueryName, @QueryContent, @StartTime, @EndTime, @IsExample, @IsActive) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        conn.Open();
                        command.Parameters.AddWithValue("@ID", questionnaire.QuestionnaireID);
                        command.Parameters.AddWithValue("@QueryName", questionnaire.QueryName);
                        command.Parameters.AddWithValue("@QueryContent", questionnaire.QueryContent);
                        command.Parameters.AddWithValue("@StartTime", questionnaire.StartTime);
                        command.Parameters.AddWithValue("@EndTime", questionnaire.EndTime);
                        command.Parameters.AddWithValue("@IsExample", 0);
                        command.Parameters.AddWithValue("@IsActive", questionnaire.IsActive);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.CreateQuestionnaire", ex);
                throw;
            }
        }
        public void UpdateQuestionnaire(QuestionnaireModel questionnaire)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  UPDATE [Questionnaires]
                     SET QueryName = @QueryName,
                         QueryContent = @QueryContent,
                         StartTime = @StartTime,
                         EndTime = @EndTime,
                         IsActive = @IsActive
                     WHERE ID = @ID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        conn.Open();
                        command.Parameters.AddWithValue("@ID", questionnaire.QuestionnaireID);
                        command.Parameters.AddWithValue("@QueryName", questionnaire.QueryName);
                        command.Parameters.AddWithValue("@QueryContent", questionnaire.QueryContent);
                        command.Parameters.AddWithValue("@StartTime", questionnaire.StartTime);
                        command.Parameters.AddWithValue("@EndTime", questionnaire.EndTime);
                        command.Parameters.AddWithValue("@IsActive", questionnaire.IsActive);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.UpdateQuestionnaire", ex);
                throw;
            }
        }
        public void DeleteQuestionnaire(Guid questionnaireID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  DELETE FROM [Questionnaires]                     
                     WHERE ID = @ID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        DeleteQuestion(questionnaireID);
                        DeleteAnswer(questionnaireID);

                        conn.Open();
                        command.Parameters.AddWithValue("@ID", questionnaireID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.DeleteQuestionnaire", ex);
                throw;
            }
        }
        public void DeleteQuestion(Guid questionnaireID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  DELETE FROM [Questions]                     
                     WHERE QuestionnaireID = @QuestionnaireID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        conn.Open();
                        command.Parameters.AddWithValue("@QuestionnaireID", questionnaireID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.DeleteQuestion", ex);
                throw;
            }
        }
        public void DeleteAnswer(Guid questionnaireID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  DELETE FROM [Answers]                     
                     WHERE QuestionnaireID = @QuestionnaireID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        conn.Open();
                        command.Parameters.AddWithValue("@QuestionnaireID", questionnaireID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.DeleteAnswer", ex);
                throw;
            }
        }


        private QuestionnaireModel BuildQuestionnaire(SqlDataReader reader)
        {
            return new QuestionnaireModel()
            {
                QuestionnaireID = (Guid)reader["ID"],
                QueryName = reader["QueryName"] as string,
                QueryContent = reader["QueryContent"] as string,
                CreateTime = (DateTime)reader["CreateTime"],
                StartTime = (DateTime)reader["StartTime"],
                EndTime = (DateTime)reader["EndTime"],
                IsActive = (bool)reader["IsActive"] ? ActiveType.開放 : ActiveType.已關閉
            };
        }

        public List<QuestionModel> GetQuestionList(Guid QuestionnaireID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Questions]
                     WHERE QuestionnaireID = @QuestionnaireID 
                     ORDER BY QuestionNo ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QuestionnaireID", QuestionnaireID);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<QuestionModel> questionList = new List<QuestionModel>();
                        while (reader.Read())
                        {
                            QuestionModel question = BuildQuestionModel(reader);
                            questionList.Add(question);
                        }
                        return questionList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.GetQuestionnaire", ex);
                throw;
            }
        }
        public void CreateQuestion(QuestionModel question)
        {
            string connStr = ConfigHelper.GetConnectionString();

            string commandText =
                    $@" INSERT INTO [Questions]
                            (ID, QuestionnaireID, Type, QuestionNo, QuestionVal, Selection, Necessary)
                        VALUES 
                            (@ID, @QuestionnaireID, @Type, @QuestionNo, @QuestionVal, @Selection, @Necessary) ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        conn.Open();
                        command.Parameters.AddWithValue("@ID", question.QuestionID);
                        command.Parameters.AddWithValue("@QuestionnaireID", question.QuestionnaireID);
                        command.Parameters.AddWithValue("@Type", question.Type);
                        command.Parameters.AddWithValue("@QuestionNo", question.QuestionNo);
                        command.Parameters.AddWithValue("@QuestionVal", question.QuestionVal);
                        command.Parameters.AddWithValue("@Selection", question.Selection);
                        command.Parameters.AddWithValue("@Necessary", question.Necessary);
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.CreateQuestion", ex);
                throw;
            }
        }

        private QuestionModel BuildQuestionModel(SqlDataReader reader)
        {
            return new QuestionModel()
            {
                QuestionID = (Guid)reader["ID"],
                QuestionnaireID = (Guid)reader["QuestionnaireID"],
                Type = (QuestionType)reader["Type"],
                QuestionNo = (int)reader["QuestionNo"],
                QuestionVal = reader["QuestionVal"] as string,
                Selection = reader["Selection"] as string,
                Necessary = (bool)reader["Necessary"]

            };
        }

        public void CreatePerson(PersonModel person)
        {
            string connStr = ConfigHelper.GetConnectionString();

            string commandText =
                    $@" INSERT INTO [Persons]
                            (ID, Name, Mobile, Email, Age, QuestionnaireID)
                        VALUES 
                            (@ID, @Name, @Mobile, @Email, @Age, @QuestionnaireID) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        conn.Open();
                        command.Parameters.AddWithValue("@ID", person.PersonID);
                        command.Parameters.AddWithValue("@Name", person.Name);
                        command.Parameters.AddWithValue("@Mobile", person.Mobile);
                        command.Parameters.AddWithValue("@Email", person.Email);
                        command.Parameters.AddWithValue("@Age", person.Age);
                        command.Parameters.AddWithValue("@QuestionnaireID", person.QuestionnaireID);
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.CreatePerson", ex);
                throw;
            }
        }
        public void CreateAnswer(AnswerModel answer)
        {
            string connStr = ConfigHelper.GetConnectionString();

            string commandText =
                    $@" INSERT INTO [Answers]
                            (ID, QuestionnaireID, PersonID, QuestionNo, Answer)
                        VALUES 
                            (@ID, @QuestionnaireID, @PersonID, @QuestionNo, @Answer) ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@ID", Guid.NewGuid());
                        command.Parameters.AddWithValue("@QuestionnaireID", answer.QuestionnaireID);
                        command.Parameters.AddWithValue("@PersonID", answer.PersonID);
                        command.Parameters.AddWithValue("@QuestionNo", answer.QuestionNo);
                        command.Parameters.AddWithValue("@Answer", answer.Answer);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.CreateAnswer", ex);
                throw;
            }
        }
        public List<PersonModel> GetPersonList(Guid QuestionnaireID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Persons]
                     WHERE QuestionnaireID = @QuestionnaireID 
                     ORDER BY CreateTime DESC";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QuestionnaireID", QuestionnaireID);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<PersonModel> personList = new List<PersonModel>();
                        while (reader.Read())
                        {
                            PersonModel person = BuildPersonModel(reader);
                            personList.Add(person);
                        }
                        return personList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.GetPersonList", ex);
                throw;
            }
        }

        public PersonModel GetPerson(Guid personID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Persons]
                     WHERE ID = @ID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@ID", personID);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        PersonModel person = new PersonModel();
                        if (reader.Read())
                        {
                            person = BuildPersonModel(reader);
                        }
                        return person;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.GetPersonList", ex);
                throw;
            }

        }
        private PersonModel BuildPersonModel(SqlDataReader reader)
        {
            return new PersonModel()
            {
                PersonID = (Guid)reader["ID"],
                Name = reader["Name"] as string,
                Mobile = reader["Mobile"] as string,
                Email = reader["Email"] as string,
                Age = reader["Age"] as string,
                CreateTime = (DateTime)reader["CreateTime"],
                QuestionnaireID = (Guid)reader["QuestionnaireID"]
            };
        }

        public List<AnswerModel> GetAnswerList(Guid personID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Answers]
                     WHERE PersonID = @PersonID 
                     ORDER BY QuestionNo, Answer ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@PersonID", personID);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<AnswerModel> answerList = new List<AnswerModel>();
                        while (reader.Read())
                        {
                            AnswerModel answer = new AnswerModel()
                            {
                                QuestionNo = Convert.ToInt32(reader["QuestionNo"]),
                                Answer = reader["Answer"] as string
                            };
                            answerList.Add(answer);
                        }
                        return answerList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.GetAnswerList", ex);
                throw;
            }

        }

        public List<StasticModel> GetStasticList(Guid QuestionnaireID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT QuestionNo, Answer, COUNT(QuestionnaireID) AS AnsCount
                     FROM [Answers]
                     WHERE QuestionnaireID = @QuestionnaireID
                     GROUP BY QuestionNo, Answer, QuestionnaireID
                     ORDER BY QuestionNo, Answer ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QuestionnaireID", QuestionnaireID);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<StasticModel> stasticList = new List<StasticModel>();
                        while (reader.Read())
                        {
                            StasticModel stastic = new StasticModel()
                            {
                                QuestionNo = (int)reader["QuestionNo"],
                                Answer = reader["Answer"] as string,
                                AnsCount = (int)reader["AnsCount"]
                            };
                            stasticList.Add(stastic);
                        }
                        return stasticList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.GetStasticList", ex);
                throw;
            }

        }

        public void CreateExample(Guid questionnaireID, string queryName)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  INSERT INTO [Questionnaires]
                        (ID, QueryName, IsExample)
                     VALUES 
                        (@ID, @QueryName, @IsExample) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        conn.Open();
                        command.Parameters.AddWithValue("@ID", questionnaireID);
                        command.Parameters.AddWithValue("@QueryName", queryName);
                        command.Parameters.AddWithValue("@IsExample", 1);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.CreateExample", ex);
                throw;
            }
        }
        public void UpdateExample(Guid questionnaireID, string queryName)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  UPDATE [Questionnaires]
                     SET QueryName = @QueryName
                     WHERE ID = @ID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        conn.Open();
                        command.Parameters.AddWithValue("@ID", questionnaireID);
                        command.Parameters.AddWithValue("@QueryName", queryName);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.UpdateExample", ex);
                throw;
            }
        }

        public QuestionnaireModel GetExample(Guid QuestionnaireID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Questionnaires]
                     WHERE ID = @ID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@ID", QuestionnaireID);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        QuestionnaireModel questionnaire = new QuestionnaireModel();
                        if (reader.Read())
                        {
                            questionnaire.QuestionnaireID = (Guid)reader["ID"];
                            questionnaire.QueryName = reader["QueryName"] as string;
                            questionnaire.CreateTime = (DateTime)reader["CreateTime"];
                        }
                        return questionnaire;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.GetExample", ex);
                throw;
            }
        }
        public List<QuestionnaireModel> GetExampleList()
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Questionnaires]
                     WHERE IsExample = 1
                     ORDER BY CreateTime DESC ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<QuestionnaireModel> questionnaireList = new List<QuestionnaireModel>();
                        while (reader.Read())
                        {
                            QuestionnaireModel questionnaire = new QuestionnaireModel()
                            {
                                QuestionnaireID = (Guid)reader["ID"],
                                QueryName = reader["QueryName"] as string,
                                CreateTime = (DateTime)reader["CreateTime"]
                            };
                            questionnaireList.Add(questionnaire);
                        }
                        return questionnaireList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.GetExampleList", ex);
                throw;
            }
        }
        public List<QuestionnaireModel> GetExampleList(string keyword)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Questionnaires]
                     WHERE QueryName Like '%'+@keyword+'%' AND IsExample = 1
                     ORDER BY CreateTime DESC ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@keyword", keyword);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<QuestionnaireModel> questionnaireList = new List<QuestionnaireModel>();
                        while (reader.Read())
                        {
                            QuestionnaireModel questionnaire = new QuestionnaireModel()
                            {
                                QuestionnaireID = (Guid)reader["ID"],
                                QueryName = reader["QueryName"] as string,
                                CreateTime = (DateTime)reader["CreateTime"]
                            };
                            questionnaireList.Add(questionnaire);
                        }
                        return questionnaireList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("QuestionnairMgr.GetExampleList", ex);
                throw;
            }
        }

        //public List<T> GetIndexList(int pageIndex, int pageSize, List<T> list)
        //{
        //    int skip = pageSize * (pageIndex - 1);  //計算跳頁數
        //    if (skip < 0)
        //        skip = 0;

        //    return list.Skip(skip).Take(pageSize).ToList();

        //}

    }
    //public interface IEnumerable<T> : System.Collections.IEnumerable
    //{ }


}