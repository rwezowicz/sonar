CREATE TABLE [dbo].[Metrics]
(
	[id] INT NOT NULL PRIMARY KEY,
	[customer_Id] INT NOT NULL,
	[name] VARCHAR(50) NOT NULL,
	[expression] VARCHAR(50) NOT NULL
)
