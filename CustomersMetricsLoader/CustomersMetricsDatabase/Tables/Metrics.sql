CREATE TABLE [dbo].[Metrics]
(
	[RowId] INT NOT NULL PRIMARY KEY IDENTITY,
	[id] INT NOT NULL, 
	[customer_id] INT NOT NULL,
	[name] VARCHAR(max) NOT NULL,
	[expression] VARCHAR(max) NOT NULL
)
