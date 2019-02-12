select	top(10) c.CompanyName as [Company Name], 
		count(o.OrderID) as [Order Quantity], 
		sum(od.UnitPrice * od.Quantity) as [Giro]
from Customers as c
inner join Orders as o on 
	c.CustomerID = o.CustomerID
inner join [Order Details] as od on
	o.OrderId = od.OrderId
group by c.CompanyName
order by [Order Quantity] DESC