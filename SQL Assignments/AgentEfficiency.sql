
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE AgentEfficiency 
AS
BEGIN
	SET NOCOUNT ON;

    SELECT 
	AR.AreaName,
	COUNT(*) TotalComplaints
	(SELECT COUNT(*) FROM RajatAreas A JOIN RajatAgents AG 
	ON A.AgentId = AG.AgentId
	JOIN RajatComplaint COM ON COM.AssignedAgentId = A.AgentId 
	WHERE A.AreaName = A.AreaName)
	(CAST(SELECT COUNT(*) FROM RajatAreas A JOIN RajatAgents AG ON AG.AgentId = A.AgentId
	JOIN RajatComplaint COM ON A.AgentId = COM.AssignedAgentId WHERE A.AgentId = COM.AssignedAgentId ) AS FLOAT))/(CAST(COUNT(*))AS FLOAT))*100) EFFICIENCY
FROM RajatAreas A JOIN RajatCustomers C ON A.AreaId = C.AreaId JOIN RajatCoplaint COMP
	ON COM.CustomerId = C.CustomerId
Group By
	A.AreaName
END
GO
