
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE filterComplaints
    @custName NVARCHAR(50) = NULL,
    @complaintId INT = NULL,
    @areaName NVARCHAR(50) = NULL,
    @description NVARCHAR(50) = NULL
AS
BEGIN
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
            SELECT ', ' + TRY_CONVERT (nvarchar(25), ExpenseType) + '(' + TRY_CONVERT(VARCHAR(10), Amount) + ')'
            FROM RajatDetailedExpenses de
            WHERE de.ComplaintId = C.ComplaintId
            ORDER BY ExpenseType
            FOR XML PATH('')), 1, 2, '') AS ExpenseSummary,
        SUM(dex.Amount) AS TotalExpenses
    FROM
        RajatComplaint C 
        JOIN RajatCustomers CU ON C.CustomerId = CU.CustomerId 
        JOIN RajatAreas AR ON CU.AreaId = AR.AreaId 
        JOIN RajatAgents AG ON AR.AgentId = AG.AgentId 
        LEFT JOIN RajatDetailedExpenses dex ON dex.ComplaintId = C.ComplaintId
    WHERE 
        (@custName IS NULL OR CU.FirstName LIKE '%' + @custName + '%')
        AND (@complaintId IS NULL OR C.ComplaintId = @complaintId)
        AND (@areaName IS NULL OR AR.AreaName LIKE '%' + @areaName + '%')
        AND (@description IS NULL OR C.Description LIKE '%' + @description + '%')
    GROUP BY 
        C.CustomerId,
        CU.FirstName,
        C.Description,
        AR.AreaName,
        AG.AgentName,
        C.ResolvedDate,
        C.ComplaintId;
END;
