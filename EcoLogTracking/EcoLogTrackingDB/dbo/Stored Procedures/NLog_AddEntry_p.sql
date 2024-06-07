
CREATE PROCEDURE [dbo].[NLog_AddEntry_p] (
  @machineName nvarchar(200),
  @logged datetime,
  @level varchar(5),
  @message nvarchar(max),
  @logger nvarchar(300),
  @method nvarchar(300),
  @stacktrace nvarchar(max),
  @filename nvarchar(300),
  @allEventProperties nvarchar(max)

) AS
BEGIN
  INSERT INTO [dbo].[NLog] (
    [MachineName],
    [Logged],
    [Level],
    [Message],
    [Logger],
	[request_method],
	[stacktrace],
	[file name],
	[allEventProperties]

  ) VALUES (
    @machineName,
    @logged,
    @level,
    @message,
    @logger,
	@method,
	@stacktrace,
	@filename,
	@allEventProperties
  );
END