SELECT 
	ComplaintId,
	Description,
	RaisedDate,
	ResolvedDate,
	CASE 
		WHEN ResolvedDate IS NULL THEN 'Not Resolved'
		ELSE 'Resolved on ' + CONVERT(VARCHAR(10), ResolvedDate, 103)
	END AS ResolvedStatus
FROM RajatComplaint 
WHERE CustomerId = 12;

select * from RajatComplaint;


	
SELECT 
	C.CustomerId AS 'CUSTOMER ID',
	CU.FirstName AS FirstName,
	C.Description,
	AR.AreaName,
	AG.AgentName,
	C.ResolvedDate, 
		CASE 
        WHEN ResolvedDate IS NULL THEN 'Not Resolved'
        ELSE 'Resolved'
		END AS ResolvedStatus, 
	STUFF((
        SELECT ', ' + ExpenseType + '(' + CAST(Amount AS VARCHAR(10)) + ')'
        FROM RajatDetailedExpenses
        ORDER BY ExpenseType
        FOR XML PATH('')
    ), 1, 2, '') AS ExpenseSummary
FROM
	RajatComplaint C JOIN RajatCustomers CU ON C.CustomerId = CU.CustomerId JOIN RajatAreas AR ON CU.AreaId = AR.AreaId 
	JOIN RajatAgents AG ON AR.AgentId = AG.AgentId JOIN RajatDetailedExpenses DE on de.ComplaintId = c.ComplaintId


------
SELECT 
    C.CustomerId AS 'CUSTOMER ID',
    CU.FirstName AS FirstName,
    C.Description,
    AR.AreaName,
    AG.AgentName,
    C.ResolvedDate, 
    CASE 
        WHEN C.ResolvedDate IS NULL THEN 'Not Resolved'
        ELSE 'Resolved'
    END AS ResolvedStatus, 
    STUFF((
        SELECT ', ' + ExpenseType + '(' + CONVERT(VARCHAR(10), Amount) + ')'
        FROM RajatDetailedExpenses
        WHERE ComplaintId = C.ComplaintId
        ORDER BY ExpenseType
        FOR XML PATH('')
    ), 1, 2, '') AS ExpenseSummary
FROM
    RajatComplaint C 
    JOIN RajatCustomers CU ON C.CustomerId = CU.CustomerId 
    JOIN RajatAreas AR ON CU.AreaId = AR.AreaId 
    JOIN RajatAgents AG ON AR.AgentId = AG.AgentId 
GROUP BY 
    C.CustomerId,
    CU.FirstName,
    C.Description,
    AR.AreaName,
    AG.AgentName,
    C.ResolvedDate,
    C.ComplaintId;
