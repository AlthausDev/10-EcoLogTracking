CREATE TABLE [dbo].[NLog] (
    [ID]                   INT            IDENTITY (1, 1) NOT NULL,
    [MachineName]          NVARCHAR (200) NULL,
    [Logged]               DATETIME       NOT NULL,
    [Level]                VARCHAR (300)    NOT NULL,
    [Message]              NVARCHAR (MAX) NOT NULL,
    [Logger]               NVARCHAR (300) NULL,
    [Method]               NVARCHAR (300) NULL,
    [Stacktrace]           NVARCHAR (MAX) NULL,
    [File_name]            NVARCHAR (MAX) NULL,
    [All_event_properties] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED ([ID] ASC)
);

