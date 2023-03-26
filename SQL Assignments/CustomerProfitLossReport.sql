SELECT *,  Earnings.[Total Earning] - Expenses.[Total Expense] FROM 
	(
	SELECT 
		C.customerid AS 'Cust. Id',
		c.FirstName AS 'Name',
		sum(PKG.Rate) AS 'Total Earning' 
	FROM RajatCustomers C join RajatMonthlyCollection MC
		on C.customerid = MC.customerid join RajatPackages PKG 
		on C.PackageId = PKG.PackageId
	where MC.ModeOfPayment is not null
	group by
		C.customerid
	) as Earnings 
	join(
		select 
			C.CustomerId AS 'ExpensseId',
			sum(ve.Amount) AS 'Total Expense'
		from RajatCustomers C join RajatComplaint vc1 
			on C.customerid = vc1.customerid join RajatDetailedExpenses ve 
			on vc1.complaintid = ve.complaintid
		group by
		C.customerid
		) as Expenses
	on Earnings.[Cust. Id] = Expenses.[Total Expense]