CREATE TABLE [dbo].[NLog] (
    [ID]                 INT            IDENTITY (1, 1) NOT NULL,
    [MachineName]        NVARCHAR (200) NULL,
    [Logged]             DATETIME       NOT NULL,
    [Level]              VARCHAR (5)    NOT NULL,
    [Message]            NVARCHAR (MAX) NOT NULL,
    [Logger]             NVARCHAR (300) NULL,
    [request_method]     NVARCHAR (300) NULL,
    [stacktrace]         NVARCHAR (MAX) NULL,
    [file name]          NVARCHAR (300) NULL,
    [allEventProperties] VARCHAR (300)  NULL,
    CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED ([ID] ASC)
);



