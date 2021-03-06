SET NOCOUNT ON
GO

USE master
GO
if EXISTS (SELECT * FROM sysdatabases WHERE name='OrganizationData')
		DROP DATABASE OrganizationData
go

DECLARE @device_directory NVARCHAR(520)
SELECT @device_directory = SUBSTRING(filename, 1, CHARINDEX(N'master.mdf', LOWER(filename)) - 1)
FROM master.dbo.sysaltfiles WHERE dbid = 1 AND fileid = 1

EXECUTE (N'CREATE DATABASE OrganizationData
  ON PRIMARY (NAME = N''OrganizationData'', FILENAME = N''' + @device_directory + N'OrganizationData.mdf'')
  LOG ON (NAME = N''OrganizationData_log'',  FILENAME = N''' + @device_directory + N'OrganizationData.ldf'')')
go

ALTER DATABASE [OrganizationData] SET RECOVERY SIMPLE WITH NO_WAIT
GO

SET QUOTED_IDENTIFIER ON
GO

/* Set DATEFORMAT so that the date strings are interpreted correctly regardless of
   the default DATEFORMAT on the server.
*/
SET DATEFORMAT dmy
GO

USE "OrganizationData"
GO

EXEC sys.sp_db_vardecimal_storage_format N'OrganizationData', N'ON'
GO

ALTER DATABASE [OrganizationData] SET QUERY_STORE = OFF
GO

USE [OrganizationData]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 15.12.2020 6:38:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[FatherName] [nvarchar](50) NULL,
	[BirthDate] [date] NULL,
	[PasportSerial] [int] NOT NULL,
	[PasportNumber] [int] NOT NULL,
	[Notes] [ntext] NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeesOrganizations]    Script Date: 15.12.2020 6:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeesOrganizations](
	[OrganizationId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
 CONSTRAINT [PK_EmployeesOrganizations] PRIMARY KEY NONCLUSTERED 
(
	[OrganizationId] ASC,
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organizations]    Script Date: 15.12.2020 6:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organizations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[VAT] [bigint] NOT NULL,
	[LegalAddress] [nvarchar](50) NOT NULL,
	[PostalAddress] [nvarchar](50) NULL,
	[Notes] [ntext] NULL,
 CONSTRAINT [PK_Organizations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[EmployeesOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_EmployeesOrganizations_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[EmployeesOrganizations] CHECK CONSTRAINT [FK_EmployeesOrganizations_Employees]
GO
ALTER TABLE [dbo].[EmployeesOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_EmployeesOrganizations_Organizations] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organizations] ([Id])
GO
ALTER TABLE [dbo].[EmployeesOrganizations] CHECK CONSTRAINT [FK_EmployeesOrganizations_Organizations]
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateEmployeeAndRelations]    Script Date: 15.12.2020 6:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_UpdateEmployeeAndRelations]
	@LastName nvarchar(50),
	@FirstName nvarchar(50),
	@FatherName nvarchar(50),
	@BirthDate date,
	@PasportSerial int,
	@PasportNumber int,
	@Notes text,
	@OrganizationId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @Id INT;

	SELECT @Id=Id FROM Employees
	WHERE PasportNumber = @PasportNumber AND PasportSerial = @PasportSerial

	IF(@Id > 0)
		UPDATE Employees SET 
		LastName = @LastName, 
		FirstName = @FirstName,
		FatherName = @FatherName,
		BirthDate = @BirthDate,
		PasportSerial = @PasportSerial,
		PasportNumber = @PasportNumber,
		Notes = @Notes 
		WHERE Id = @Id
	ELSE
		BEGIN
			INSERT INTO Employees
			(LastName, FirstName, FatherName, BirthDate, PasportSerial, PasportNumber, Notes)
			VALUES (@LastName, @FirstName, @FatherName, @BirthDate, @PasportSerial, @PasportNumber, @Notes);
			SELECT @Id=@@IDENTITY;
		END
	IF NOT EXISTS 
		(SELECT * FROM EmployeesOrganizations 
		WHERE OrganizationId = @OrganizationId AND EmployeeId = @Id)
	BEGIN
		INSERT INTO EmployeesOrganizations (OrganizationId, EmployeeId) VALUES (@OrganizationId, @Id)
	END
	SELECT @Id;
END
GO

USE [master]
GO
ALTER DATABASE [OrganizationData] SET  READ_WRITE 
GO

USE [OrganizationData]
GO
INSERT INTO [dbo].[Organizations]
           ([Name]
           ,[VAT]
		   ,[LegalAddress]
           ,[PostalAddress]
           ,[Notes])
     VALUES
           ('ООО АБВ'
           ,7743181546
           ,'Москва, Солнечногорская 4'
           ,'Москва, Солнечногорская 4'
           ,'Организация по производству строительных материалов')

INSERT INTO [dbo].[Organizations]
           ([Name]
           ,[VAT]
		   ,[LegalAddress]
           ,[PostalAddress]
           ,[Notes])
     VALUES
           ('ООО АМИГО'
           ,7743181580
           ,'Москва, Автомоторная 6'
           ,'Москва, Автомоторная 6'
           ,'Организация по продаже солнцезащитных конструкций')

INSERT INTO [dbo].[Organizations]
           ([Name]
           ,[VAT]
	   ,[LegalAddress]
           ,[PostalAddress]
           ,[Notes])
     VALUES
           ('ООО Джи Эм'
           ,7743184862
           ,'Москва, Адмирала Макарова 8'
           ,'Москва, Адмирала Макарова 8'
           ,'Продажа готовых изделий')

INSERT INTO [dbo].[Organizations]
           ([Name]
           ,[VAT]
	   ,[LegalAddress]
           ,[PostalAddress]
           ,[Notes])
     VALUES
           ('ООО Крафт'
           ,7715481526
           ,'Москва, Земляной вал 10'
           ,'Москва, Земляной вал 10'
           ,'Продажа мебели')

GO

INSERT INTO [dbo].[Employees]
           ([LastName]
           ,[FirstName]
           ,[FatherName]
           ,[BirthDate]
           ,[PasportSerial]
           ,[PasportNumber]
           ,[Notes])
     VALUES
           ('Федоров'
           ,'Александр'
           ,'Петрович'
           ,'10-09-1980 00:00:00'
           ,2547
           ,897823
           ,'Менеджер отдела продаж')

GO

INSERT INTO [dbo].[EmployeesOrganizations]
	([OrganizationId]
	,[EmployeeId])
    VALUES
	 (1, 1)
GO