select o.ShipCountry as [Country],
c.CategoryName as [Category],
sum(od.Quantity) as [Sale Qty],
avg(od.UnitPrice) as [Avg Unit Price]

from Orders as o
inner join [Order Details] as od on
	o.OrderID = od.OrderID
inner join Products as p on
	od.ProductID = p.ProductID
inner join Categories as c on
	p.CategoryID = c.CategoryID

group by o.ShipCountry, c.CategoryName

order by [Avg Unit Price] DESC