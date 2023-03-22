

CREATE TABLE [dbo].[Category](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[name] [nvarchar](50) NOT NULL)
GO


CREATE TABLE [dbo].[Description](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[text] [nvarchar](500) NOT NULL)
GO


CREATE TABLE [dbo].[Image](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[url] [nvarchar](50) NOT NULL)
GO


CREATE TABLE [dbo].[Order](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[product_id] [int] NOT NULL,
	[count] [int] NOT NULL,
	[message] [text] NULL)
GO


CREATE TABLE [dbo].[Producer](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[name] [nvarchar](50) NOT NULL,
	[address] [nvarchar](50) NOT NULL)
GO


CREATE TABLE [dbo].[Product](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[name] [nvarchar](50) NOT NULL,
	[catrgory_id] [int] NOT NULL,
	[price] [float] NOT NULL,
	[count] [int] NOT NULL,
	[description_id] [int] NULL,
	[image_id] [int] NULL)
GO


CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[login] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[phone] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NULL,
	[position] [nvarchar](50) NULL)
GO
