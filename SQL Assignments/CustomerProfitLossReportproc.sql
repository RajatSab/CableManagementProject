
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE ProfitLossReport
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		select *,  Earnings.amt    - Expenses.amt from 
		(
		select C.customerid,sum(PKG.Rate) amt
		from RajatCustomers C join RajatMonthlyCollection MC on C.customerid = MC.customerid join RajatPackages PKG on C.PackageId = PKG.PackageId
		where MC.ModeOfPayment is not null
		group by
		C.customerid
		) as Earnings join

		(
		select C.customerid,sum(ve.Amount) amt
		from RajatCustomers C join RajatComplaint vc1 on C.customerid = vc1.customerid join RajatDetailedExpenses ve on vc1.complaintid = ve.complaintid
		
		group by
		C.customerid) as Expenses on Earnings.CustomerId = Expenses.customerid
END
GO
