
CREATE PROCEDURE [dbo].[NLog_AddEntry_p] (
  @machineName nvarchar(200),
  @logged datetime,
  @level varchar(5),
  @message nvarchar(max),
  @logger nvarchar(300),
  @method nvarchar(300)
) AS
BEGIN
  INSERT INTO [dbo].[NLog] (
    [MachineName],
    [Logged],
    [Level],
    [Message],
    [Logger],
	[request_method]
  ) VALUES (
    @machineName,
    @logged,
    @level,
    @message,
    @logger,
	@method
  );
END