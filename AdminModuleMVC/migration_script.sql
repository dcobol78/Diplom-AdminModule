IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [Attendances] (
        [Id] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Attendances] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [Images] (
        [Id] nvarchar(450) NOT NULL,
        [ImageTitle] nvarchar(max) NULL,
        [MimeType] nvarchar(max) NULL,
        [ImageData] varbinary(max) NOT NULL,
        CONSTRAINT [PK_Images] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [Tests] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [ParentType] nvarchar(max) NOT NULL,
        [ParentId] nvarchar(max) NOT NULL,
        [StartTime] datetime2 NOT NULL,
        [CloseTime] datetime2 NOT NULL,
        [Duration] int NOT NULL,
        [AttemptsAlowed] int NOT NULL,
        [Cost] int NOT NULL,
        [Exp] real NULL,
        CONSTRAINT [PK_Tests] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [Teachers] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NULL,
        [Surname] nvarchar(max) NULL,
        [Patronymic] nvarchar(max) NULL,
        [DOB] date NULL,
        [Email] nvarchar(max) NULL,
        [UserId] nvarchar(max) NULL,
        [AvatarId] nvarchar(450) NULL,
        CONSTRAINT [PK_Teachers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Teachers_Images_AvatarId] FOREIGN KEY ([AvatarId]) REFERENCES [Images] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [Questions] (
        [Id] nvarchar(450) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [Type] nvarchar(max) NOT NULL,
        [Number] int NOT NULL,
        [Cost] int NOT NULL,
        [TestId] nvarchar(450) NULL,
        CONSTRAINT [PK_Questions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Questions_Tests_TestId] FOREIGN KEY ([TestId]) REFERENCES [Tests] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [TestSessions] (
        [Id] nvarchar(450) NOT NULL,
        [TestId] nvarchar(450) NULL,
        CONSTRAINT [PK_TestSessions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TestSessions_Tests_TestId] FOREIGN KEY ([TestId]) REFERENCES [Tests] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [Answers] (
        [Id] nvarchar(450) NOT NULL,
        [IsCorrect] bit NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [Number] int NULL,
        [QuestionId] nvarchar(450) NULL,
        [TestSessionId] nvarchar(450) NULL,
        CONSTRAINT [PK_Answers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Answers_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]),
        CONSTRAINT [FK_Answers_TestSessions_TestSessionId] FOREIGN KEY ([TestSessionId]) REFERENCES [TestSessions] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [Achivements] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ExpThreshold] real NULL,
        [CourseId] nvarchar(450) NOT NULL,
        [ImageId] nvarchar(450) NULL,
        [StudentId] nvarchar(450) NULL,
        CONSTRAINT [PK_Achivements] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Achivements_Images_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Images] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [CourseChats] (
        [Id] nvarchar(450) NOT NULL,
        [CourseName] nvarchar(max) NOT NULL,
        [CourseId] nvarchar(450) NOT NULL,
        [LastInteraction] datetime2 NULL,
        CONSTRAINT [PK_CourseChats] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [CourseChatTeacher] (
        [CourseChatsId] nvarchar(450) NOT NULL,
        [TeachersId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_CourseChatTeacher] PRIMARY KEY ([CourseChatsId], [TeachersId]),
        CONSTRAINT [FK_CourseChatTeacher_CourseChats_CourseChatsId] FOREIGN KEY ([CourseChatsId]) REFERENCES [CourseChats] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_CourseChatTeacher_Teachers_TeachersId] FOREIGN KEY ([TeachersId]) REFERENCES [Teachers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [CourseChatStudent] (
        [CourseChatsId] nvarchar(450) NOT NULL,
        [StudentsId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_CourseChatStudent] PRIMARY KEY ([CourseChatsId], [StudentsId]),
        CONSTRAINT [FK_CourseChatStudent_CourseChats_CourseChatsId] FOREIGN KEY ([CourseChatsId]) REFERENCES [CourseChats] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [CourseCodes] (
        [Id] nvarchar(450) NOT NULL,
        [CourseId] nvarchar(450) NULL,
        [UsesLeft] int NOT NULL,
        [Code] nvarchar(max) NULL,
        CONSTRAINT [PK_CourseCodes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [CourseFiles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ParentId] nvarchar(max) NULL,
        [ParentType] nvarchar(max) NULL,
        [Path] nvarchar(max) NULL,
        [AuthorId] nvarchar(450) NULL,
        [CourseId] nvarchar(450) NULL,
        [SectorId] nvarchar(450) NULL,
        [ThemeId] nvarchar(450) NULL,
        CONSTRAINT [PK_CourseFiles] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CourseFiles_Teachers_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Teachers] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [Homeworks] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NULL,
        [ParentId] nvarchar(max) NULL,
        [Duration] int NULL,
        [Description] nvarchar(max) NULL,
        [HomeworkFileId] nvarchar(450) NULL,
        [Cost] int NULL,
        [Exp] real NULL,
        [ParentType] nvarchar(max) NULL,
        [AllowLateUpload] bit NULL,
        [StartDate] datetime2 NULL,
        [EndDate] datetime2 NULL,
        CONSTRAINT [PK_Homeworks] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Homeworks_CourseFiles_HomeworkFileId] FOREIGN KEY ([HomeworkFileId]) REFERENCES [CourseFiles] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [Courses] (
        [Id] nvarchar(450) NOT NULL,
        [AutorId] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [AutorName] nvarchar(max) NULL,
        [Duration] int NULL,
        [CreationDate] datetime2 NOT NULL,
        [IsPublic] bit NOT NULL,
        [IsCoherent] bit NOT NULL,
        [HomeworkId] nvarchar(450) NULL,
        [TestId] nvarchar(450) NULL,
        [Exp] real NULL,
        [CourseImageId] nvarchar(450) NULL,
        [StartingDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Courses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Courses_Homeworks_HomeworkId] FOREIGN KEY ([HomeworkId]) REFERENCES [Homeworks] ([Id]),
        CONSTRAINT [FK_Courses_Images_CourseImageId] FOREIGN KEY ([CourseImageId]) REFERENCES [Images] ([Id]),
        CONSTRAINT [FK_Courses_Tests_TestId] FOREIGN KEY ([TestId]) REFERENCES [Tests] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [CourseTeacher] (
        [CoursesId] nvarchar(450) NOT NULL,
        [TeachersId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_CourseTeacher] PRIMARY KEY ([CoursesId], [TeachersId]),
        CONSTRAINT [FK_CourseTeacher_Courses_CoursesId] FOREIGN KEY ([CoursesId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_CourseTeacher_Teachers_TeachersId] FOREIGN KEY ([TeachersId]) REFERENCES [Teachers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [Events] (
        [Id] nvarchar(450) NOT NULL,
        [CourseId] nvarchar(450) NULL,
        [Name] nvarchar(max) NULL,
        [StartTime] datetime2 NULL,
        [Type] int NOT NULL,
        [Content] nvarchar(max) NULL,
        [AttendanceId] nvarchar(450) NULL,
        CONSTRAINT [PK_Events] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Events_Attendances_AttendanceId] FOREIGN KEY ([AttendanceId]) REFERENCES [Attendances] ([Id]),
        CONSTRAINT [FK_Events_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [Sections] (
        [Id] nvarchar(450) NOT NULL,
        [IdCourse] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [Number] int NOT NULL,
        [Duration] int NULL,
        [HomeworkId] nvarchar(450) NULL,
        [Exp] real NULL,
        [TestId] nvarchar(450) NULL,
        [CourseId] nvarchar(450) NULL,
        CONSTRAINT [PK_Sections] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Sections_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]),
        CONSTRAINT [FK_Sections_Homeworks_HomeworkId] FOREIGN KEY ([HomeworkId]) REFERENCES [Homeworks] ([Id]),
        CONSTRAINT [FK_Sections_Tests_TestId] FOREIGN KEY ([TestId]) REFERENCES [Tests] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [TeacherPermissions] (
        [Id] nvarchar(450) NOT NULL,
        [TeacherId] nvarchar(450) NOT NULL,
        [CourseId] nvarchar(450) NOT NULL,
        [CanEditCourse] bit NOT NULL,
        [CanAddStudents] bit NOT NULL,
        [CanRemoveStudents] bit NOT NULL,
        [CanManageContent] bit NOT NULL,
        [CanViewGrades] bit NOT NULL,
        [CanEditGrades] bit NOT NULL,
        CONSTRAINT [PK_TeacherPermissions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TeacherPermissions_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TeacherPermissions_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [Students] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NULL,
        [Surname] nvarchar(max) NULL,
        [Patronymic] nvarchar(max) NULL,
        [DOB] date NULL,
        [Email] nvarchar(max) NULL,
        [Title] nvarchar(max) NULL,
        [AvatarId] nvarchar(450) NULL,
        [UserId] nvarchar(max) NULL,
        [AttendanceId] nvarchar(450) NULL,
        [EventId] nvarchar(450) NULL,
        CONSTRAINT [PK_Students] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Students_Attendances_AttendanceId] FOREIGN KEY ([AttendanceId]) REFERENCES [Attendances] ([Id]),
        CONSTRAINT [FK_Students_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [Events] ([Id]),
        CONSTRAINT [FK_Students_Images_AvatarId] FOREIGN KEY ([AvatarId]) REFERENCES [Images] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [Themes] (
        [Id] nvarchar(450) NOT NULL,
        [IdSection] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [Number] int NOT NULL,
        [Duration] int NULL,
        [HomeworkId] nvarchar(450) NULL,
        [Exp] real NULL,
        [TestId] nvarchar(450) NULL,
        [SectorId] nvarchar(450) NULL,
        CONSTRAINT [PK_Themes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Themes_Homeworks_HomeworkId] FOREIGN KEY ([HomeworkId]) REFERENCES [Homeworks] ([Id]),
        CONSTRAINT [FK_Themes_Sections_SectorId] FOREIGN KEY ([SectorId]) REFERENCES [Sections] ([Id]),
        CONSTRAINT [FK_Themes_Tests_TestId] FOREIGN KEY ([TestId]) REFERENCES [Tests] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [CourseStudent] (
        [CoursesId] nvarchar(450) NOT NULL,
        [StudentsId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_CourseStudent] PRIMARY KEY ([CoursesId], [StudentsId]),
        CONSTRAINT [FK_CourseStudent_Courses_CoursesId] FOREIGN KEY ([CoursesId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_CourseStudent_Students_StudentsId] FOREIGN KEY ([StudentsId]) REFERENCES [Students] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [CourseStudentExps] (
        [Id] nvarchar(450) NOT NULL,
        [StudentId] nvarchar(450) NULL,
        [CourseId] nvarchar(450) NULL,
        [Exp] real NOT NULL,
        CONSTRAINT [PK_CourseStudentExps] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CourseStudentExps_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]),
        CONSTRAINT [FK_CourseStudentExps_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [HomeworkFiles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Path] nvarchar(max) NULL,
        [AutorId] nvarchar(450) NULL,
        [HomeworkId] nvarchar(450) NULL,
        CONSTRAINT [PK_HomeworkFiles] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_HomeworkFiles_Homeworks_HomeworkId] FOREIGN KEY ([HomeworkId]) REFERENCES [Homeworks] ([Id]),
        CONSTRAINT [FK_HomeworkFiles_Students_AutorId] FOREIGN KEY ([AutorId]) REFERENCES [Students] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [Items] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Type] nvarchar(max) NULL,
        [AchivementId] nvarchar(450) NOT NULL,
        [ImageId] nvarchar(450) NULL,
        [Title] nvarchar(max) NULL,
        [StudentId] nvarchar(450) NULL,
        CONSTRAINT [PK_Items] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Items_Achivements_AchivementId] FOREIGN KEY ([AchivementId]) REFERENCES [Achivements] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Items_Images_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Images] ([Id]),
        CONSTRAINT [FK_Items_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [Notifications] (
        [Id] nvarchar(450) NOT NULL,
        [Title] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ReciverEmail] nvarchar(max) NULL,
        [Seen] bit NOT NULL,
        [UserId] nvarchar(max) NULL,
        [CreationTime] datetime2 NOT NULL,
        [StudentId] nvarchar(450) NULL,
        [TeacherId] nvarchar(450) NULL,
        CONSTRAINT [PK_Notifications] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Notifications_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]),
        CONSTRAINT [FK_Notifications_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [TestGrades] (
        [Id] nvarchar(450) NOT NULL,
        [TestSessionId] nvarchar(450) NULL,
        [TestId] nvarchar(450) NULL,
        [Grade] real NULL,
        [MaxGrade] int NULL,
        [StudentId] nvarchar(450) NULL,
        CONSTRAINT [PK_TestGrades] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TestGrades_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]),
        CONSTRAINT [FK_TestGrades_TestSessions_TestSessionId] FOREIGN KEY ([TestSessionId]) REFERENCES [TestSessions] ([Id]),
        CONSTRAINT [FK_TestGrades_Tests_TestId] FOREIGN KEY ([TestId]) REFERENCES [Tests] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [TestInstances] (
        [Id] nvarchar(450) NOT NULL,
        [TestId] nvarchar(450) NOT NULL,
        [StudentId] nvarchar(450) NOT NULL,
        [StartTime] datetime2 NOT NULL,
        [EndTime] datetime2 NOT NULL,
        CONSTRAINT [PK_TestInstances] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TestInstances_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TestInstances_Tests_TestId] FOREIGN KEY ([TestId]) REFERENCES [Tests] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [UserChats] (
        [Id] nvarchar(450) NOT NULL,
        [FirstUserName] nvarchar(max) NULL,
        [FirstUserId] nvarchar(max) NULL,
        [SecondUserName] nvarchar(max) NULL,
        [SecondUserId] nvarchar(max) NULL,
        [LastInteraction] datetime2 NULL,
        [StudentId] nvarchar(450) NULL,
        [TeacherId] nvarchar(450) NULL,
        CONSTRAINT [PK_UserChats] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserChats_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]),
        CONSTRAINT [FK_UserChats_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [UserFiles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Path] nvarchar(max) NULL,
        [AuthorId] nvarchar(450) NULL,
        CONSTRAINT [PK_UserFiles] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserFiles_Students_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Students] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [TestAnswers] (
        [Id] nvarchar(450) NOT NULL,
        [TestInstanceId] nvarchar(450) NOT NULL,
        [QuestionId] nvarchar(450) NOT NULL,
        [AnswerId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_TestAnswers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TestAnswers_Answers_AnswerId] FOREIGN KEY ([AnswerId]) REFERENCES [Answers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TestAnswers_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TestAnswers_TestInstances_TestInstanceId] FOREIGN KEY ([TestInstanceId]) REFERENCES [TestInstances] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [Messages] (
        [Id] nvarchar(450) NOT NULL,
        [Content] nvarchar(max) NULL,
        [CreationTime] datetime2 NULL,
        [ChatId] nvarchar(max) NULL,
        [UserId] nvarchar(max) NULL,
        [UserName] nvarchar(max) NULL,
        [ImageId] nvarchar(max) NULL,
        [CourseChatId] nvarchar(450) NULL,
        [UserChatId] nvarchar(450) NULL,
        CONSTRAINT [PK_Messages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Messages_CourseChats_CourseChatId] FOREIGN KEY ([CourseChatId]) REFERENCES [CourseChats] ([Id]),
        CONSTRAINT [FK_Messages_UserChats_UserChatId] FOREIGN KEY ([UserChatId]) REFERENCES [UserChats] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE TABLE [HomeworkGrades] (
        [Id] nvarchar(450) NOT NULL,
        [Grade] real NOT NULL,
        [Exp] real NOT NULL,
        [HomeworkId] nvarchar(450) NULL,
        [UserFileId] nvarchar(450) NULL,
        [MaxGrade] int NOT NULL,
        [StudentId] nvarchar(450) NULL,
        CONSTRAINT [PK_HomeworkGrades] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_HomeworkGrades_Homeworks_HomeworkId] FOREIGN KEY ([HomeworkId]) REFERENCES [Homeworks] ([Id]),
        CONSTRAINT [FK_HomeworkGrades_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]),
        CONSTRAINT [FK_HomeworkGrades_UserFiles_UserFileId] FOREIGN KEY ([UserFileId]) REFERENCES [UserFiles] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Achivements_CourseId] ON [Achivements] ([CourseId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Achivements_ImageId] ON [Achivements] ([ImageId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Achivements_StudentId] ON [Achivements] ([StudentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Answers_QuestionId] ON [Answers] ([QuestionId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Answers_TestSessionId] ON [Answers] ([TestSessionId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE UNIQUE INDEX [IX_CourseChats_CourseId] ON [CourseChats] ([CourseId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_CourseChatStudent_StudentsId] ON [CourseChatStudent] ([StudentsId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_CourseChatTeacher_TeachersId] ON [CourseChatTeacher] ([TeachersId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_CourseCodes_CourseId] ON [CourseCodes] ([CourseId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_CourseFiles_AuthorId] ON [CourseFiles] ([AuthorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_CourseFiles_CourseId] ON [CourseFiles] ([CourseId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_CourseFiles_SectorId] ON [CourseFiles] ([SectorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_CourseFiles_ThemeId] ON [CourseFiles] ([ThemeId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Courses_CourseImageId] ON [Courses] ([CourseImageId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Courses_HomeworkId] ON [Courses] ([HomeworkId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Courses_TestId] ON [Courses] ([TestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_CourseStudent_StudentsId] ON [CourseStudent] ([StudentsId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_CourseStudentExps_CourseId] ON [CourseStudentExps] ([CourseId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_CourseStudentExps_StudentId] ON [CourseStudentExps] ([StudentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_CourseTeacher_TeachersId] ON [CourseTeacher] ([TeachersId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Events_AttendanceId] ON [Events] ([AttendanceId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Events_CourseId] ON [Events] ([CourseId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_HomeworkFiles_AutorId] ON [HomeworkFiles] ([AutorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_HomeworkFiles_HomeworkId] ON [HomeworkFiles] ([HomeworkId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_HomeworkGrades_HomeworkId] ON [HomeworkGrades] ([HomeworkId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_HomeworkGrades_StudentId] ON [HomeworkGrades] ([StudentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_HomeworkGrades_UserFileId] ON [HomeworkGrades] ([UserFileId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Homeworks_HomeworkFileId] ON [Homeworks] ([HomeworkFileId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Items_AchivementId] ON [Items] ([AchivementId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Items_ImageId] ON [Items] ([ImageId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Items_StudentId] ON [Items] ([StudentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Messages_CourseChatId] ON [Messages] ([CourseChatId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Messages_UserChatId] ON [Messages] ([UserChatId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Notifications_StudentId] ON [Notifications] ([StudentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Notifications_TeacherId] ON [Notifications] ([TeacherId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Questions_TestId] ON [Questions] ([TestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Sections_CourseId] ON [Sections] ([CourseId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Sections_HomeworkId] ON [Sections] ([HomeworkId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Sections_TestId] ON [Sections] ([TestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Students_AttendanceId] ON [Students] ([AttendanceId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Students_AvatarId] ON [Students] ([AvatarId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Students_EventId] ON [Students] ([EventId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_TeacherPermissions_CourseId] ON [TeacherPermissions] ([CourseId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_TeacherPermissions_TeacherId] ON [TeacherPermissions] ([TeacherId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Teachers_AvatarId] ON [Teachers] ([AvatarId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_TestAnswers_AnswerId] ON [TestAnswers] ([AnswerId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_TestAnswers_QuestionId] ON [TestAnswers] ([QuestionId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_TestAnswers_TestInstanceId] ON [TestAnswers] ([TestInstanceId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_TestGrades_StudentId] ON [TestGrades] ([StudentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_TestGrades_TestId] ON [TestGrades] ([TestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_TestGrades_TestSessionId] ON [TestGrades] ([TestSessionId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_TestInstances_StudentId] ON [TestInstances] ([StudentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_TestInstances_TestId] ON [TestInstances] ([TestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_TestSessions_TestId] ON [TestSessions] ([TestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Themes_HomeworkId] ON [Themes] ([HomeworkId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Themes_SectorId] ON [Themes] ([SectorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_Themes_TestId] ON [Themes] ([TestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_UserChats_StudentId] ON [UserChats] ([StudentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_UserChats_TeacherId] ON [UserChats] ([TeacherId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    CREATE INDEX [IX_UserFiles_AuthorId] ON [UserFiles] ([AuthorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    ALTER TABLE [Achivements] ADD CONSTRAINT [FK_Achivements_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    ALTER TABLE [Achivements] ADD CONSTRAINT [FK_Achivements_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    ALTER TABLE [CourseChats] ADD CONSTRAINT [FK_CourseChats_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    ALTER TABLE [CourseChatStudent] ADD CONSTRAINT [FK_CourseChatStudent_Students_StudentsId] FOREIGN KEY ([StudentsId]) REFERENCES [Students] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    ALTER TABLE [CourseCodes] ADD CONSTRAINT [FK_CourseCodes_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    ALTER TABLE [CourseFiles] ADD CONSTRAINT [FK_CourseFiles_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    ALTER TABLE [CourseFiles] ADD CONSTRAINT [FK_CourseFiles_Sections_SectorId] FOREIGN KEY ([SectorId]) REFERENCES [Sections] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    ALTER TABLE [CourseFiles] ADD CONSTRAINT [FK_CourseFiles_Themes_ThemeId] FOREIGN KEY ([ThemeId]) REFERENCES [Themes] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240609213446_InitialCourseMigration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240609213446_InitialCourseMigration', N'8.0.4');
END;
GO

COMMIT;
GO

