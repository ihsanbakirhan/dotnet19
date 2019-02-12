select c.CompanyName
from Customers as c
where c.CustomerID not in (select distinct CustomerID from Orders)

select CompanyName as [Company Name]
from Customers as c
left join Orders as o on c.CustomerID=o.CustomerID
where o.OrderID is null

select c.CompanyName as [COMPANY NAME]
from Customers as c
WHERE NOT EXISTS
( select o.CustomerID from Orders as o where o.CustomerID=c.CustomerID)
