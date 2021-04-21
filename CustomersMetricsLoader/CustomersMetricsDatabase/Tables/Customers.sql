CREATE TABLE [dbo].[Customers]
(
    [RowId] INT NOT NULL PRIMARY KEY IDENTITY,
	[id] INT NOT NULL, 
    [name] VARCHAR(max) NOT NULL, 
    [representative] VARCHAR(max) NOT NULL, 
    [representative_email] VARCHAR(max) NOT NULL, 
    [representative_phone] VARCHAR(max) NOT NULL
)
