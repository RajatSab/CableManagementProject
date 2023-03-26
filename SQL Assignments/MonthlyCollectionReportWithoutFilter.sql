-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE MonthlyCollectionReportWithoutFilter
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT 
	C.FirstName,
	AR.AreaName,
	AG.AgentName,
	C.DateOfConnection,
	(SELECT COUNT(*)
	 FROM RajatComplaint COMP WHERE COMP.CustomerId = C.CustomerId ) AS 'Total Raised Complaints',
	 RM.MonthName AS 'Month',
	 MC.Year,
	 PKG.PackageName,
	 PY.PaymentMode
FROM RajatCustomers C JOIN RajatAreas AR 
	ON C.AreaId = AR.AreaId
	JOIN RajatAgents AG 
	ON AR.AgentId = AG.AgentId
	JOIN RajatComplaint COM
	ON C.CustomerId = COM.CustomerId
	JOIN RajatMonthlyCollection MC 
	ON C.CustomerId = MC.CustomerId
	JOIN RajatMonths RM
	ON MC.MonthId = RM.MonthId
	JOIN RajatPaymentMode PY
	ON MC.ModeOfPayment = PY.PaymentModeId
	JOIN RajatPackages PKG
	ON PKG.PackageId = MC.PackageId
ORDER BY
	MC.Year,
	C.FirstName,
	RM.MonthId;
	
	END
GO
