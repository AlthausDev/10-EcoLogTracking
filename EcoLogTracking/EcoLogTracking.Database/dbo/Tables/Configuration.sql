CREATE TABLE [dbo].[Configuration]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1),
	[Period] INT NULL,
	[DeletedDate] DATETIME NULL
)
