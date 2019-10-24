CREATE TABLE [dbo].[Person] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (50) NULL,
    [LastName]    NVARCHAR (50) NULL,
    [EmailAdress] NVARCHAR(50)    NULL,
    [SeatNumber] INT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

