







SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReflectionsUmbracoUtilitiesFileDownloadCounter](
 [NodeId] [int] NOT NULL,
 [MediaId] [int] NOT NULL,
 [DownloadCounter] [int] NOT NULL,
 CONSTRAINT [PK_ReflectionsUmbracoUtilitiesFileDownloadCounter] PRIMARY KEY CLUSTERED 
(
 [NodeId] ASC, [MediaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO






SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ReflectionsUmbracoUtilitiesSetFileDownloadCount]
(
    @NodeId INT,
	@MediaId INT
)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS(SELECT 1 FROM ReflectionsUmbracoUtilitiesFileDownloadCounter WHERE NodeId = @NodeId and MediaId = @MediaId)
    BEGIN
        UPDATE ReflectionsUmbracoUtilitiesFileDownloadCounter SET DownloadCounter = DownloadCounter + 1
        WHERE NodeId = @NodeId and  MediaId = @MediaId
    END
    ELSE
    BEGIN
        INSERT INTO ReflectionsUmbracoUtilitiesFileDownloadCounter(NodeId,MediaId, DownloadCounter) VALUES (@NodeId,@MediaId, 1)
    END
END
GO




















SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE PROCEDURE [dbo].[ReflectionsUmbracoUtilitiesGetFileDownloadCount]
(
    @NodeId INT,
	@MediaId INT
)
AS
BEGIN

    SET NOCOUNT ON;

    SELECT ISNULL(DownloadCounter,0)
    FROM ReflectionsUmbracoUtilitiesFileDownloadCounter
    WHERE NodeId = @NodeId  and MediaId = @MediaId 

END

GO