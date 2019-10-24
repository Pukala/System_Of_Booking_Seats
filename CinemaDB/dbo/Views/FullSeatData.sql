CREATE VIEW [dbo].[FullSeatData]
	AS
	select s.*, p.*
	from dbo.SeatsTable s
	left join dbo.Person p on s.PersonId = p.Id

