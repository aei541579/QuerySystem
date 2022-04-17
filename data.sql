USE [QuerySystem]
GO
INSERT [dbo].[Questionnaires] ([ID], [QueryName], [QueryContent], [CreateTime], [StartTime], [EndTime]) VALUES (N'f9a1f4f6-9d4a-4c35-86ee-1331e285103c', N'測試2', N'測試內容', CAST(N'2022-04-14T22:31:09.583' AS DateTime), CAST(N'2022-04-11T00:00:00.000' AS DateTime), CAST(N'2022-05-11T00:00:00.000' AS DateTime))
INSERT [dbo].[Questionnaires] ([ID], [QueryName], [QueryContent], [CreateTime], [StartTime], [EndTime]) VALUES (N'59a2e983-60e9-4bf1-9273-202a44d4a06f', N'test2', N'test', CAST(N'2022-04-15T17:08:00.030' AS DateTime), CAST(N'2022-04-11T00:00:00.000' AS DateTime), CAST(N'2022-05-11T00:00:00.000' AS DateTime))
INSERT [dbo].[Questionnaires] ([ID], [QueryName], [QueryContent], [CreateTime], [StartTime], [EndTime]) VALUES (N'0da3ed6a-9634-49f3-880d-6ab5b3e088a8', N'newVote', N'voting', CAST(N'2022-04-17T20:59:06.100' AS DateTime), CAST(N'2022-04-11T00:00:00.000' AS DateTime), CAST(N'2022-05-11T00:00:00.000' AS DateTime))
INSERT [dbo].[Questionnaires] ([ID], [QueryName], [QueryContent], [CreateTime], [StartTime], [EndTime]) VALUES (N'a4ef3cbd-4037-4748-93b3-d5328f1217e0', N'測試問卷', N'測試內容', CAST(N'2022-04-11T15:15:13.533' AS DateTime), CAST(N'2022-04-11T00:00:00.000' AS DateTime), CAST(N'2022-05-11T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Questions] ([ID], [QuestionnaireID], [Type], [QuestionNo], [QuestionVal], [Selection], [Necessary]) VALUES (N'bd273a52-91fa-423a-a002-02c6a740764f', N'f9a1f4f6-9d4a-4c35-86ee-1331e285103c', 0, 2, N'test', N'男;女', 0)
INSERT [dbo].[Questions] ([ID], [QuestionnaireID], [Type], [QuestionNo], [QuestionVal], [Selection], [Necessary]) VALUES (N'b1d68aae-1ccb-44cc-8e4d-0682088aeac6', N'f9a1f4f6-9d4a-4c35-86ee-1331e285103c', 0, 1, N'gender', N'male;female', 1)
INSERT [dbo].[Questions] ([ID], [QuestionnaireID], [Type], [QuestionNo], [QuestionVal], [Selection], [Necessary]) VALUES (N'040a5704-8937-411e-9df5-0c3b90d643c2', N'a4ef3cbd-4037-4748-93b3-d5328f1217e0', 2, 4, N'name', N'', 1)
INSERT [dbo].[Questions] ([ID], [QuestionnaireID], [Type], [QuestionNo], [QuestionVal], [Selection], [Necessary]) VALUES (N'a8799cbd-08cd-4b0e-84aa-2fa274cb44a5', N'a4ef3cbd-4037-4748-93b3-d5328f1217e0', 0, 2, N'test', N'1;2;3;4', 0)
INSERT [dbo].[Questions] ([ID], [QuestionnaireID], [Type], [QuestionNo], [QuestionVal], [Selection], [Necessary]) VALUES (N'b2ad6e63-8f61-4184-9f0d-318ab6c8b3b2', N'f9a1f4f6-9d4a-4c35-86ee-1331e285103c', 2, 4, N'age', N'', 1)
INSERT [dbo].[Questions] ([ID], [QuestionnaireID], [Type], [QuestionNo], [QuestionVal], [Selection], [Necessary]) VALUES (N'907d131a-7527-4b38-8d48-39349eefb20e', N'59a2e983-60e9-4bf1-9273-202a44d4a06f', 2, 3, N'文字', N'', 0)
INSERT [dbo].[Questions] ([ID], [QuestionnaireID], [Type], [QuestionNo], [QuestionVal], [Selection], [Necessary]) VALUES (N'3c2f0cf6-3e50-4c24-bbae-489ad674e0ab', N'a4ef3cbd-4037-4748-93b3-d5328f1217e0', 0, 1, N'gender', N'male;female', 1)
INSERT [dbo].[Questions] ([ID], [QuestionnaireID], [Type], [QuestionNo], [QuestionVal], [Selection], [Necessary]) VALUES (N'e89f33dc-9ead-4144-a174-89510a84869e', N'0da3ed6a-9634-49f3-880d-6ab5b3e088a8', 0, 1, N'season', N'春;夏;秋;冬', 1)
INSERT [dbo].[Questions] ([ID], [QuestionnaireID], [Type], [QuestionNo], [QuestionVal], [Selection], [Necessary]) VALUES (N'c1c78a99-bf91-4ed3-a755-922a47b77bf0', N'f9a1f4f6-9d4a-4c35-86ee-1331e285103c', 2, 3, N'name', N'', 0)
INSERT [dbo].[Questions] ([ID], [QuestionnaireID], [Type], [QuestionNo], [QuestionVal], [Selection], [Necessary]) VALUES (N'4f46c25f-4468-4e55-a4f0-b0474a66790b', N'59a2e983-60e9-4bf1-9273-202a44d4a06f', 0, 1, N'單選', N'a;b;c;d', 0)
INSERT [dbo].[Questions] ([ID], [QuestionnaireID], [Type], [QuestionNo], [QuestionVal], [Selection], [Necessary]) VALUES (N'c9d04173-f4ba-4b4a-be02-d82e62d21266', N'59a2e983-60e9-4bf1-9273-202a44d4a06f', 1, 2, N'複選', N'a;b;c;d', 0)
INSERT [dbo].[Questions] ([ID], [QuestionnaireID], [Type], [QuestionNo], [QuestionVal], [Selection], [Necessary]) VALUES (N'9478e3e3-102d-4396-842a-eccbaa921864', N'a4ef3cbd-4037-4748-93b3-d5328f1217e0', 2, 3, N'age', N'', 0)
GO
INSERT [dbo].[Persons] ([ID], [Name], [Mobile], [Email], [Age], [CreateTime], [QuestionnaireID]) VALUES (N'1360bd34-1c22-4b2e-9965-02e3006c6fcc', N'QOO', N'0909      ', N'aa@aa', N'12', CAST(N'2022-04-18T02:37:34.693' AS DateTime), N'0da3ed6a-9634-49f3-880d-6ab5b3e088a8')
INSERT [dbo].[Persons] ([ID], [Name], [Mobile], [Email], [Age], [CreateTime], [QuestionnaireID]) VALUES (N'fd261a9b-db02-4a1b-a987-2a4e5a34ba3d', N'bill', N'123       ', N'aa@aa', N'12', CAST(N'2022-04-17T21:00:09.220' AS DateTime), N'0da3ed6a-9634-49f3-880d-6ab5b3e088a8')
INSERT [dbo].[Persons] ([ID], [Name], [Mobile], [Email], [Age], [CreateTime], [QuestionnaireID]) VALUES (N'f3cb82c4-dd31-4b1a-9fe3-2c5c30cd95bd', N'bill', N'123       ', N'aa@aa', N'12', CAST(N'2022-04-17T19:13:13.230' AS DateTime), N'f9a1f4f6-9d4a-4c35-86ee-1331e285103c')
INSERT [dbo].[Persons] ([ID], [Name], [Mobile], [Email], [Age], [CreateTime], [QuestionnaireID]) VALUES (N'ea76a537-68e6-4a1b-bb56-42fffe3e2e76', N'NINI', N'0909      ', N'aa@aa', N'33', CAST(N'2022-04-18T02:46:16.743' AS DateTime), N'0da3ed6a-9634-49f3-880d-6ab5b3e088a8')
INSERT [dbo].[Persons] ([ID], [Name], [Mobile], [Email], [Age], [CreateTime], [QuestionnaireID]) VALUES (N'c489b770-365f-43c0-bd23-81ad8a42b9f4', N'may', N'123       ', N'aa@aa', N'22', CAST(N'2022-04-17T19:13:34.623' AS DateTime), N'f9a1f4f6-9d4a-4c35-86ee-1331e285103c')
INSERT [dbo].[Persons] ([ID], [Name], [Mobile], [Email], [Age], [CreateTime], [QuestionnaireID]) VALUES (N'6c7e9647-001b-430a-bdbe-889f46b1c481', N'amy', N'12        ', N'aa@aa', N'12', CAST(N'2022-04-17T18:59:14.640' AS DateTime), N'59a2e983-60e9-4bf1-9273-202a44d4a06f')
INSERT [dbo].[Persons] ([ID], [Name], [Mobile], [Email], [Age], [CreateTime], [QuestionnaireID]) VALUES (N'b7a2cabc-39ed-4c20-add3-a099730e9071', N'bill', N'123       ', N'aa@aa', N'12', CAST(N'2022-04-17T19:02:14.807' AS DateTime), N'59a2e983-60e9-4bf1-9273-202a44d4a06f')
INSERT [dbo].[Persons] ([ID], [Name], [Mobile], [Email], [Age], [CreateTime], [QuestionnaireID]) VALUES (N'bc8b7c0f-dcc3-4899-b566-a3724deed378', N'harry', N'123       ', N'aa@aa', N'12', CAST(N'2022-04-17T19:02:34.780' AS DateTime), N'59a2e983-60e9-4bf1-9273-202a44d4a06f')
INSERT [dbo].[Persons] ([ID], [Name], [Mobile], [Email], [Age], [CreateTime], [QuestionnaireID]) VALUES (N'9aeef4cd-b126-4ea2-bf3f-d2c00c6570c8', N'你好', N'09999     ', N'aa@aa', N'12', CAST(N'2022-04-18T01:47:02.970' AS DateTime), N'0da3ed6a-9634-49f3-880d-6ab5b3e088a8')
INSERT [dbo].[Persons] ([ID], [Name], [Mobile], [Email], [Age], [CreateTime], [QuestionnaireID]) VALUES (N'33aa710c-c04a-4d99-bd0b-ebf348cd0429', N'peter', N'123       ', N'aa@aa', N'33', CAST(N'2022-04-17T19:13:57.217' AS DateTime), N'a4ef3cbd-4037-4748-93b3-d5328f1217e0')
GO
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'd7ac1b40-db70-4c27-a7bf-08583e8bb302', N'59a2e983-60e9-4bf1-9273-202a44d4a06f', N'b7a2cabc-39ed-4c20-add3-a099730e9071', 2, N'1')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'674dbd90-2b27-4b1b-a4dd-0cdf0a3843b7', N'59a2e983-60e9-4bf1-9273-202a44d4a06f', N'bc8b7c0f-dcc3-4899-b566-a3724deed378', 2, N'2')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'867c5736-e4f3-4d8f-b7bf-1c457375d038', N'f9a1f4f6-9d4a-4c35-86ee-1331e285103c', N'c489b770-365f-43c0-bd23-81ad8a42b9f4', 2, N'1')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'db5a26a5-0809-42c7-80ec-2770dd891bff', N'59a2e983-60e9-4bf1-9273-202a44d4a06f', N'b7a2cabc-39ed-4c20-add3-a099730e9071', 2, N'2')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'375c884d-563b-4c3f-b946-2a7e420c95cd', N'59a2e983-60e9-4bf1-9273-202a44d4a06f', N'6c7e9647-001b-430a-bdbe-889f46b1c481', 1, N'0')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'531ffbfe-5de2-4d9d-a17a-2c5d0d7ca605', N'0da3ed6a-9634-49f3-880d-6ab5b3e088a8', N'fd261a9b-db02-4a1b-a987-2a4e5a34ba3d', 1, N'1')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'bcad068c-7fb1-4c54-858b-32598855ad96', N'f9a1f4f6-9d4a-4c35-86ee-1331e285103c', N'c489b770-365f-43c0-bd23-81ad8a42b9f4', 3, N'a')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'46b7897b-5b76-4f6e-beca-3882488a18a1', N'59a2e983-60e9-4bf1-9273-202a44d4a06f', N'bc8b7c0f-dcc3-4899-b566-a3724deed378', 2, N'0')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'c5c08094-5a95-469e-8e4d-3dd075c076dd', N'59a2e983-60e9-4bf1-9273-202a44d4a06f', N'b7a2cabc-39ed-4c20-add3-a099730e9071', 3, N'qq')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'382446de-77fb-4b6d-8b7c-432f8b6461fc', N'f9a1f4f6-9d4a-4c35-86ee-1331e285103c', N'f3cb82c4-dd31-4b1a-9fe3-2c5c30cd95bd', 2, N'0')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'f94467bc-5e61-4d01-8822-46161a5d4b6f', N'0da3ed6a-9634-49f3-880d-6ab5b3e088a8', N'9aeef4cd-b126-4ea2-bf3f-d2c00c6570c8', 1, N'0')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'02480616-bda5-4d6d-848d-4833b6867ad8', N'59a2e983-60e9-4bf1-9273-202a44d4a06f', N'6c7e9647-001b-430a-bdbe-889f46b1c481', 3, N'a')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'b5d3bf06-27a2-4b54-bb05-5a86302359bc', N'59a2e983-60e9-4bf1-9273-202a44d4a06f', N'bc8b7c0f-dcc3-4899-b566-a3724deed378', 2, N'1')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'b399f026-8864-4260-b45a-5f62c525c755', N'59a2e983-60e9-4bf1-9273-202a44d4a06f', N'6c7e9647-001b-430a-bdbe-889f46b1c481', 2, N'1')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'11546f0d-8855-40ab-a4f3-6a03a02eb308', N'f9a1f4f6-9d4a-4c35-86ee-1331e285103c', N'f3cb82c4-dd31-4b1a-9fe3-2c5c30cd95bd', 1, N'0')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'b369a542-e5d7-40c9-acaf-6acb9318554f', N'f9a1f4f6-9d4a-4c35-86ee-1331e285103c', N'f3cb82c4-dd31-4b1a-9fe3-2c5c30cd95bd', 3, N'bill')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'def552bc-99f5-46be-8514-73091c89e97a', N'f9a1f4f6-9d4a-4c35-86ee-1331e285103c', N'f3cb82c4-dd31-4b1a-9fe3-2c5c30cd95bd', 4, N'12')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'9775f979-daf8-4b29-9c79-7455262d34b3', N'f9a1f4f6-9d4a-4c35-86ee-1331e285103c', N'c489b770-365f-43c0-bd23-81ad8a42b9f4', 1, N'1')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'd817a653-f7b5-41b9-a984-82b16d579135', N'59a2e983-60e9-4bf1-9273-202a44d4a06f', N'bc8b7c0f-dcc3-4899-b566-a3724deed378', 2, N'3')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'70e54b57-670c-4764-b563-9c6a063bb2e3', N'0da3ed6a-9634-49f3-880d-6ab5b3e088a8', N'ea76a537-68e6-4a1b-bb56-42fffe3e2e76', 1, N'2')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'd1673a93-7f15-4e5f-846d-9e2c36f10656', N'59a2e983-60e9-4bf1-9273-202a44d4a06f', N'bc8b7c0f-dcc3-4899-b566-a3724deed378', 1, N'2')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'55245e39-edfb-4194-817a-aad21596710c', N'f9a1f4f6-9d4a-4c35-86ee-1331e285103c', N'c489b770-365f-43c0-bd23-81ad8a42b9f4', 4, N'qq')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'8a6fbe6f-39d3-4e05-866f-bdc8011d29a7', N'59a2e983-60e9-4bf1-9273-202a44d4a06f', N'b7a2cabc-39ed-4c20-add3-a099730e9071', 1, N'1')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'95713d7e-88a9-4220-be26-c3fe667d8644', N'59a2e983-60e9-4bf1-9273-202a44d4a06f', N'bc8b7c0f-dcc3-4899-b566-a3724deed378', 3, N'qq')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'd6ed8d9c-f642-471f-b8ea-ca250c3799aa', N'a4ef3cbd-4037-4748-93b3-d5328f1217e0', N'33aa710c-c04a-4d99-bd0b-ebf348cd0429', 2, N'0')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'd9a488d4-9adf-4194-b311-cc4150d670f4', N'59a2e983-60e9-4bf1-9273-202a44d4a06f', N'6c7e9647-001b-430a-bdbe-889f46b1c481', 2, N'0')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'786a632f-4c6a-493a-9148-ce25837371b1', N'a4ef3cbd-4037-4748-93b3-d5328f1217e0', N'33aa710c-c04a-4d99-bd0b-ebf348cd0429', 1, N'0')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'2d9397f8-9961-4b07-8f8a-eb09946e8e2e', N'a4ef3cbd-4037-4748-93b3-d5328f1217e0', N'33aa710c-c04a-4d99-bd0b-ebf348cd0429', 3, N'123')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'4b82ccd3-f52b-4bcd-99a4-eea9d63f22d3', N'a4ef3cbd-4037-4748-93b3-d5328f1217e0', N'33aa710c-c04a-4d99-bd0b-ebf348cd0429', 4, N'123')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'e65f5aa9-17a4-4976-b8bf-f173bf4f8031', N'59a2e983-60e9-4bf1-9273-202a44d4a06f', N'b7a2cabc-39ed-4c20-add3-a099730e9071', 2, N'3')
INSERT [dbo].[Answers] ([ID], [QuestionnaireID], [PersonID], [QuestionNo], [Answer]) VALUES (N'5837e3a5-c738-4b00-8bc6-fd064f8de4c2', N'0da3ed6a-9634-49f3-880d-6ab5b3e088a8', N'1360bd34-1c22-4b2e-9965-02e3006c6fcc', 1, N'0')
GO
