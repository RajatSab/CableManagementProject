
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE CheckStringInColumn
    @TableName nvarchar(50),
    @ColumnName nvarchar(50),
    @SearchString nvarchar(50)
AS
BEGIN
    DECLARE @SQL nvarchar(max)
    SET @SQL = 'SELECT * FROM ' + QUOTENAME(@TableName) + ' WHERE ' + QUOTENAME(@ColumnName) + ' LIKE ''%' + @SearchString + '%'''
    EXEC sp_executesql @SQL
END