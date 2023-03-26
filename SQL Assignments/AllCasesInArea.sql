
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE ShowAllCasesInArea
	@AreaId as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
	C.RaisedDate,
	C.ComplaintId,
	C.Description,
	C.CustomerId,
	CU.FirstName,
	AR.AreaName,
	AR.AreaId,
	CASE
	 WHEN C.ResolvedDate IS NULL THEN 'No'
	 ELSE 'Yes'
	 END AS 'Resolved Status'
FROM RajatComplaint C 
	JOIN RajatCustomers CU ON C.CustomerId = CU.CustomerId
	JOIN RajatAreas AR ON CU.AreaId = AR.AreaId
WHERE AR.AreaId = @AreaId
ORDER BY 
	[Resolved Status]
END
GO
