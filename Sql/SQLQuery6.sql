select c.CompanyName, o.OrderDate, o.OrderID 
from Customers as c
inner join Orders as o on 
c.CustomerID = o.CustomerID

select o.OrderID, o.OrderDate, p.ProductName, 
od.UnitPrice, od.Quantity
from Orders as o
inner join [Order Details] od on
o.OrderID = od.OrderID
inner join Products as p on
od.ProductID = p.ProductID
where o.OrderID = 10248