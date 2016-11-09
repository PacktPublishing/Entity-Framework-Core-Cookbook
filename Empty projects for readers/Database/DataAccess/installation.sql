USE [master]
GO

/* Blog */

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Blog')
	CREATE DATABASE [Blog];
GO

USE [Blog]
GO

CREATE TABLE [dbo].[Blogs]
(
	[BlogId] [INT] NOT NULL PRIMARY KEY IDENTITY,
	[Title] [NVARCHAR](50) NULL,
	[Name] [NVARCHAR](50) NOT NULL,
	[CreationDate] [DATETIME] NOT NULL,
	[Url] [NVARCHAR](50) NULL
)
GO

CREATE TABLE [dbo].[Post]
(
	[PostId] [INT] IDENTITY NOT NULL PRIMARY KEY,
	[BlogId] [INT] NULL,
	[Body] [NVARCHAR](MAX) NULL,
	[CreatedAt] [DATETIME] NULL,
	[CreatedBy] [NVARCHAR](MAX) NULL,
	[Timestamp] [DATETIME] NOT NULL,
	[Title] [NVARCHAR](MAX) NULL,
	[UpdatedAt] [DATETIME] NULL,
	[UpdatedBy] [NVARCHAR](MAX) NULL
)
GO

ALTER TABLE [dbo].[Post] WITH CHECK ADD CONSTRAINT [FK_Post_Blogs_BlogId] FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blogs] ([BlogId])
GO

ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_Blogs_BlogId]
GO

CREATE TABLE [dbo].[Tag]
(
	[TagId] [INT] IDENTITY NOT NULL PRIMARY KEY,
	[Name] [NVARCHAR](MAX) NULL
)
GO

CREATE TABLE [dbo].[PostTag]
(
	[PostId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
	CONSTRAINT [PK_PostTag] PRIMARY KEY CLUSTERED 
	(
		[PostId] ASC,
		[TagId] ASC
	)
)
GO

ALTER TABLE [dbo].[PostTag] WITH CHECK ADD CONSTRAINT [FK_PostTag_Post_PostId] FOREIGN KEY([PostId])
REFERENCES [dbo].[Post] ([PostId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PostTag] CHECK CONSTRAINT [FK_PostTag_Post_PostId]
GO

ALTER TABLE [dbo].[PostTag] WITH CHECK ADD CONSTRAINT [FK_PostTag_Tag_TagId] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tag] ([TagId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PostTag] CHECK CONSTRAINT [FK_PostTag_Tag_TagId]
GO


/* My */

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'My')
	CREATE DATABASE [My];
GO

USE [My]
GO

CREATE TABLE [dbo].[MyEntities]
(
	[Id] [INT] IDENTITY NOT NULL PRIMARY KEY,
	[Name] [NVARCHAR](50) NOT NULL,
	[Date] [DATETIME] NOT NULL,
	[IsDeleted] [BIT] NULL
)
GO
