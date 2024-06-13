CREATE TABLE [dbo].[Log] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Logged] DATETIME NULL,
    [Level] NVARCHAR(50) NULL,
    [Logger] NVARCHAR(250) NULL,
    [Message] NVARCHAR(MAX) NULL,
    [MachineName] NVARCHAR(50) NULL,
    [Request_method] NVARCHAR(50) NULL,
    [Stacktrace] NVARCHAR(MAX) NULL,
    [File_name] NVARCHAR(250) NULL,
    [All_event_properties] NVARCHAR(MAX) NULL,
    [Status_code] NVARCHAR(MAX) NULL,
    [Origin] NVARCHAR(250) NULL,
    CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED ([Id] ASC)
        WITH (
            PAD_INDEX = OFF, 
            STATISTICS_NORECOMPUTE = OFF, 
            IGNORE_DUP_KEY = OFF, 
            ALLOW_ROW_LOCKS = ON, 
            ALLOW_PAGE_LOCKS = ON
        ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

