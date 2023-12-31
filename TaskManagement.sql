USE [TaskManagement]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 12/30/2023 3:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[TaskID] [uniqueidentifier] NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Description] [varchar](500) NULL,
	[Assignee] [varchar](50) NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[Priority] [varchar](50) NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[Progress] [int] NOT NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/30/2023 3:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [uniqueidentifier] NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Fullname] [varchar](1000) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Division] [varchar](150) NOT NULL,
	[LastLogin] [datetime] NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
	[Password] [char](500) NOT NULL,
	[Salt] [varchar](500) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_UserId]  DEFAULT (newid()) FOR [UserId]
GO
/****** Object:  StoredProcedure [dbo].[Task_Create]    Script Date: 12/30/2023 3:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Task_Create]
	-- Add the parameters for the stored procedure here
	@Title VARCHAR(100),
    @Description VARCHAR(500),
    @Assignee VARCHAR(50),
	@DueDate DATETIME,
	@Priority VARCHAR(50),
	@Status VARCHAR(150),
	@CreatedBy VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[Tasks]
           ([TaskID]
           ,[Title]
           ,[Description]
           ,[Assignee]
           ,[DueDate]
           ,[Priority]
           ,[Status]
           ,[CreatedBy]
           ,[CreatedDate]
           
           ,[Progress])
     VALUES
           (NEWID()
           ,@Title
           ,@Description
           ,@Assignee
           ,@DueDate
           ,@Priority
           ,@Status
           ,@CreatedBy
           ,GETDATE()
          ,0)
END
GO
/****** Object:  StoredProcedure [dbo].[Task_Delete]    Script Date: 12/30/2023 3:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Task_Delete] 
	-- Add the parameters for the stored procedure here
	@TaskId UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM Tasks WHERE TaskId = @TaskId
END
GO
/****** Object:  StoredProcedure [dbo].[Task_FindById]    Script Date: 12/30/2023 3:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Task_FindById]
	-- Add the parameters for the stored procedure here
	@TaskId UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Tasks WHERE TaskId = @TaskId
END
GO
/****** Object:  StoredProcedure [dbo].[Task_GetAll]    Script Date: 12/30/2023 3:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Task_GetAll]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Tasks
END
GO
/****** Object:  StoredProcedure [dbo].[Task_Update]    Script Date: 12/30/2023 3:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Task_Update]
	-- Add the parameters for the stored procedure here
	@TaskId UNIQUEIDENTIFIER,
	@Title VARCHAR(100),
    @Description VARCHAR(500),
    @Assignee VARCHAR(50),
	@DueDate DATETIME,
	@Priority VARCHAR(50),
	@Status VARCHAR(150),
	@Progress int,
	@UpdatedBy VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE [dbo].[Tasks]
		SET [Title] = @Title
		  ,[Description] = @Description
		  ,[Assignee] = @Assignee
		  ,[DueDate] = @DueDate
		  ,[Priority] = @Priority
		  ,[Status] =@Status
		  ,[UpdatedBy] = @UpdatedBy
		  ,[UpdatedDate] = GETDATE()
		  ,[Progress] = @Progress
	WHERE TaskId = @TaskId
END
GO
/****** Object:  StoredProcedure [dbo].[User_Create]    Script Date: 12/30/2023 3:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[User_Create]
	@Username VARCHAR(50),
    @Password NVARCHAR(255),
	@FullName VARCHAR(MAX),
	@Email VARCHAR(50),
	@Division VARCHAR(150),
	@CreatedBy VARCHAR(50),
	@Salt VARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 IF NOT EXISTS (SELECT 1 FROM Users WHERE Username = @Username)
    BEGIN
		INSERT INTO [dbo].[Users]
		       ([UserId]
		       ,[Username]
		       ,[Fullname]
		       ,[Email]
		       ,[Division]
		       ,[CreatedBy]
		       ,[CreatedAt]
		       ,[Password]
		       ,[Salt])
		 VALUES
		       (NEWID()
		       ,@Username
		       ,@FullName
		       ,@Email
		       ,@Division
		       ,@CreatedBy
		       ,GETDATE()
		       ,@Password
		       ,@Salt)

        SELECT 'User created successfully.' AS Result;
    END
    ELSE
    BEGIN
        -- User with the given username already exists
        SELECT 'Username already exists. Choose a different username.' AS Result;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[User_GetByUsername]    Script Date: 12/30/2023 3:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[User_GetByUsername]
	@Username VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 SELECT TOP(1) * FROM dbo.Users WHERE Username = @Username
END
GO
