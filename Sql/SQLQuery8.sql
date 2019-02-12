select top (10) p.ProductName as [Product Name], sum(od.Quantity) as [Sale Qty],
sum(od.UnitPrice * od.Quantity) as [Giro]
from Products as p
inner join [Order Details] as od on
p.ProductID = od.ProductID
group by p.ProductName
order by [Sale Qty] ASC


select * from Categories

select * from orders