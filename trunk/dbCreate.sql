IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [z160307_megazlo].[dbo].[sp_fulltext_database] @action = 'enable'
end
USE [z160307_megazlo]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 10/19/2011 22:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Por] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EdmMetadata]    Script Date: 10/19/2011 22:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EdmMetadata](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModelHash] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/19/2011 22:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Family] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[DateBorn] [datetime] NOT NULL,
	[DateRegister] [datetime] NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Avatar] [nvarchar](max) NULL,
	[IsAdmin] [bit] NOT NULL,
	[PassWord] [nvarchar](32) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 10/19/2011 22:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 10/19/2011 22:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[WebLink] [nvarchar](200) NULL,
	[DatePost] [datetime] NOT NULL,
	[IsCommentable] [bit] NOT NULL,
	[UserId] [nvarchar](100) NULL,
	[CategoryId] [int] NULL,
	[InCatMenu] [bit] NOT NULL,
	[IsShowInfo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TagPosts]    Script Date: 10/19/2011 22:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TagPosts](
	[Tag_Id] [int] NOT NULL,
	[Post_Id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Tag_Id] ASC,
	[Post_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 10/19/2011 22:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NOT NULL,
	[IsAutor] [bit] NOT NULL,
	[DatePost] [datetime] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](1000) NOT NULL,
	[PostId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [Category_Posts]    Script Date: 10/19/2011 22:07:32 ******/
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [Category_Posts] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [Category_Posts]
GO
/****** Object:  ForeignKey [User_Post]    Script Date: 10/19/2011 22:07:32 ******/
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [User_Post] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [User_Post]
GO
/****** Object:  ForeignKey [Tag_Posts_Source]    Script Date: 10/19/2011 22:07:32 ******/
ALTER TABLE [dbo].[TagPosts]  WITH CHECK ADD  CONSTRAINT [Tag_Posts_Source] FOREIGN KEY([Tag_Id])
REFERENCES [dbo].[Tags] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TagPosts] CHECK CONSTRAINT [Tag_Posts_Source]
GO
/****** Object:  ForeignKey [Tag_Posts_Target]    Script Date: 10/19/2011 22:07:32 ******/
ALTER TABLE [dbo].[TagPosts]  WITH CHECK ADD  CONSTRAINT [Tag_Posts_Target] FOREIGN KEY([Post_Id])
REFERENCES [dbo].[Posts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TagPosts] CHECK CONSTRAINT [Tag_Posts_Target]
GO
/****** Object:  ForeignKey [Comment_Post]    Script Date: 10/19/2011 22:07:32 ******/
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [Comment_Post] FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [Comment_Post]
GO
