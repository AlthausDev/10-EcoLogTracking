USE [DBEcoLogTracking]
GO

DELETE FROM Users;
DBCC CHECKIDENT ('Users', RESEED, 0);
GO