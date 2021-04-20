CREATE TABLE [dbo].[Customers]
(
	[id] INT NOT NULL PRIMARY KEY, 
    [name] VARCHAR(50) NULL, 
    [representative] VARCHAR(50), 
    [representative_email] VARCHAR(50), 
    [representative_phone] VARCHAR(50)
)
