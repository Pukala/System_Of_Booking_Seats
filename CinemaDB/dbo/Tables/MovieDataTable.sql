CREATE TABLE [dbo].[MovieDataTable]
(
	[NameOfMovie] VARCHAR(50) NULL, 
	[ImagePath] VARCHAR(MAX) NULL, 
	[Movieid] INT NOT NULL PRIMARY KEY IDENTITY, 
	[NumberOfMovie] INT NULL
)
