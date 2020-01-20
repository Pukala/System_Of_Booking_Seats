CREATE TABLE [dbo].[SeatsTable]
(
	[SeatId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PersonId] INT NULL, 
    [IsReserve] BIT NULL, 
    [NumberSeat] INT NULL, 
    [MovieId] INT NULL, 
)
