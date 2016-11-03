USE [Blog]
GO

CREATE PROCEDURE [dbo].[GetBlog]
(
	@BlogID INT = NULL
)
AS
BEGIN
	SET NOCOUNT ON

	SELECT BlogId, Name, Url, CreationDate
	FROM [dbo].[Blog]
	WHERE BlogId = ISNULL(@BlogId, BlogId)
	ORDER BY BlogId

	RETURN @@ROWCOUNT
END

CREATE PROCEDURE [dbo].[InsertBlog]
(
	@CreationDate DATETIME = NULL,
	@Url NVARCHAR(50),
	@Name NVARCHAR(50)
)
AS
BEGIN
	SET NOCOUNT ON

	SET @CreationDate = ISNULL(@CreationDate, GETUTCDATE())

	INSERT INTO dbo.Blog (CreationDate, Url, Name)
	VALUES (@CreationDate, @Url, @Name)

	RETURN SCOPE_IDENTITY()
END

CREATE procedure [dbo].[UpdateBlog]
(
	@BlogId INT,
	@Name VARCHAR(50),
	@Url VARCHAR(50),
	@CreationDate DATETIME = NULL
)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE dbo.Blog
	SET Name = ISNULL(@Name, Name), Url = ISNULL(@Url, URL), CreationDate = ISNULL(@CreationDate, GETUTCDATE())
	WHERE BlogId = @BlogId

	RETURN @@ROWCOUNT
END

CREATE PROCEDURE [dbo].[DeleteBlog]
(
	@BlogId INT
)
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM dbo.Blog
	WHERE BlogId = @BlogId

	RETURN @@ROWCOUNT
END
