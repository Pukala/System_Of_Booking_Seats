CREATE TABLE [dbo].[SeatsTable]
(
	[SeatId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PersonId] INT NULL, 
    [NumberSeat] INT NULL, 
    [IdMovie] INT NULL, 
    [MovieNumber] INT NULL, 
)
