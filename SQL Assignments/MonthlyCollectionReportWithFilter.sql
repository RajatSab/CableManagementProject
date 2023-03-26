ALTER PROCEDURE MonthlyCollectionReport
    @startYear INT = NULL,
    @endYear INT = NULL
AS
BEGIN
    SELECT 
        C.FirstName,
        AR.AreaName,
        AG.AgentName,
        C.DateOfConnection,
        (SELECT COUNT(*)
         FROM RajatComplaint COMP 
         WHERE COMP.CustomerId = C.CustomerId ) AS 'Total Raised Complaints',
         RM.MonthName AS 'Month',
         MC.Year,
         PKG.PackageName,
         PY.PaymentMode
    FROM RajatCustomers C 
    JOIN RajatAreas AR ON C.AreaId = AR.AreaId
    JOIN RajatAgents AG ON AR.AgentId = AG.AgentId
    JOIN RajatComplaint COM ON C.CustomerId = COM.CustomerId
    CROSS JOIN (
        SELECT DISTINCT Year,
		ModeOfPayment,
		PackageId
        FROM RajatMonthlyCollection coll
        WHERE (@startYear IS NULL OR Year >= @startYear) AND (@endYear IS NULL OR Year <= @endYear)
    ) AS MC
    JOIN RajatMonths RM ON MC.Year = RM.MonthId
    JOIN RajatPaymentMode PY ON MC.ModeOfPayment = PY.PaymentModeId
    JOIN RajatPackages PKG ON PKG.PackageId = MC.PackageId
    WHERE (@startYear IS NULL OR MC.Year >= @startYear) AND (@endYear IS NULL OR MC.Year <= @endYear)
    ORDER BY C.FirstName, MC.Year, RM.MonthId
END
