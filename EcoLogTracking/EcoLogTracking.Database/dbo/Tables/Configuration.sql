CREATE TABLE [dbo].[Configuration]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1),
	[Period] INT NOT NULL,
	[DeletedDate] DATETIME NOT NULL
)
