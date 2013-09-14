USE [pdb]
GO
/****** Object:  UserDefinedFunction [dbo].[f_tobase64]    Script Date: 04/25/2013 09:25:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER function [dbo].[f_tobase64] (@bin varbinary(max))
returns varchar(max)
as begin
return cast(N'' as xml).value('xs:base64Binary(xs:hexBinary(sql:variable("@bin")))', 'varchar(max)')
end
