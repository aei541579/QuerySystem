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
        public List<QuestionnaireModel> GetQuestionnaireList()
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Questionnaires]
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
                            questionnaire.State = (questionnaire.EndTime < DateTime.Now) ? StateType.已關閉 : StateType.開放;
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
        public List<QuestionnaireModel> GetQuestionnaireList(string keyword)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Questionnaires]
                     WHERE QueryName Like '%'+@keyword+'%'
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
                            questionnaire.State = (questionnaire.EndTime < DateTime.Now) ? StateType.已關閉 : StateType.開放;
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


        public void CreateQuestionnaire(QuestionnaireModel questionnaire)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  INSERT INTO [Questionnaires]
                        (ID, QueryName, QueryContent, StartTime, EndTime)
                     VALUES 
                        (@ID, @QueryName, @QueryContent, @StartTime, @EndTime) ";
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
                     SET QueryName = QueryName,
                         QueryContent = QueryContent,
                         StartTime = StartTime,
                         EndTime = EndTime
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


        private QuestionnaireModel BuildQuestionnaire(SqlDataReader reader)
        {
            return new QuestionnaireModel()
            {
                QuestionnaireID = (Guid)reader["ID"],
                QueryName = reader["QueryName"] as string,
                QueryContent = reader["QueryContent"] as string,
                CreateTime = (DateTime)reader["CreateTime"],
                StartTime = (DateTime)reader["StartTime"],
                EndTime = (DateTime)reader["EndTime"]
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



    }
}