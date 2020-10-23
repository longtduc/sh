
USE [ShareHolderMeeting]
GO

delete from [dbo].[VotingByHandLines]
delete from [dbo].[VotingByHands]
GO
DBCC CHECKIDENT ('[VotingByHandLines]', RESEED, 0) 
DBCC CHECKIDENT ('[VotingByHands]', RESEED, 0) 
GO

delete from [dbo].[VotingCardLines]
delete from [dbo].[VotingCards]
GO
DBCC CHECKIDENT ('[VotingCardLines]', RESEED, 0) 
DBCC CHECKIDENT ('[VotingCards]', RESEED, 0) 
GO

delete from [dbo].[Statements]
GO
DBCC CHECKIDENT ('[Statements]', RESEED, 0) 
GO

--delete from [dbo].[ShareHolders]
--GO
--DBCC CHECKIDENT ('[ShareHolders]', RESEED, 0) 
--GO

--delete from [dbo].[Candidates]
--GO
--DBCC CHECKIDENT ('[Candidates]', RESEED, 0) 
--GO



