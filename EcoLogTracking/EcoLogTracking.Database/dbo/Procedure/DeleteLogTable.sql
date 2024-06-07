USE [DBEcoLogTracking]
GO

DELETE FROM [dbo].[Log]
DBCC CHECKIDENT ('Log', RESEED, 0);
GO


