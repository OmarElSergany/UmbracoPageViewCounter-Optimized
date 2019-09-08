SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReflectionsUmbracoUtilitiesPageViewCounter](
 [NodeId] [int] NOT NULL,
 [ViewsCounter] [int] NOT NULL,
 CONSTRAINT [PK_ReflectionsUmbracoUtilitiesPageViewCounter] PRIMARY KEY CLUSTERED 
(
 [NodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO














SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ReflectionsUmbracoUtilitiesSetPageViewCount]
(
    @NodeId INT
)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS(SELECT 1 FROM ReflectionsUmbracoUtilitiesPageViewCounter WHERE NodeId = @NodeId)
    BEGIN
        UPDATE ReflectionsUmbracoUtilitiesPageViewCounter SET ViewsCounter = ViewsCounter + 1
        WHERE NodeId = @NodeId
    END
    ELSE
    BEGIN
        INSERT INTO ReflectionsUmbracoUtilitiesPageViewCounter(NodeId, ViewsCounter) VALUES (@NodeId, 1)
    END
END
GO




















SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE PROCEDURE [dbo].[ReflectionsUmbracoUtilitiesGetPageViewCount]
(
    @NodeId INT
)
AS
BEGIN

    SET NOCOUNT ON;

    SELECT ViewsCounter
    FROM ReflectionsUmbracoUtilitiesPageViewCounter
    WHERE NodeId = @NodeId  

END

GO