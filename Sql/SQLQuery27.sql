exec CreateOrder @UserId = 1, @CityId=1, @CountryId=1, 
@Street = 'BAGDAT ST', @Zip = '34700', @Telephone = '12345678910'

select o.OrderId, o.UserId, 
p.ProductName, pv.VariantName, op.Quantity, op.UnitPrice, op.TotalPrice
from Orders as o
Inner Join OrderProducts as op on o.OrderId = op.OrderId
Inner Join ProductVariants as pv on op.ProductVariantId = pv.ProductVariantId
Inner Join Products as p on pv.ProductId = p.ProductId
where o.OrderId = 8
select OrderId, UserId, TotalPrice, TotalCost
from Orders where OrderId = 8
select * from ProductVariants