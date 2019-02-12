select * from units
select * from suppliers
select * from users
select * from products
select * from productVariants
select s.SupplierName, p.ProductName, v.VariantName, v.UnitPrice 
from Products as p inner join ProductVariants v on
p.ProductId = v.ProductId
inner join Suppliers as s on
s.SupplierId = p.SupplierId
--exec InsertSupplier @SupplierName = 'Blackberry'
--exec InsertUser @Username = 'ayalcin', @Email = 'huseyin@avniyalcin.com', @Password = '12345678'
--exec InsertProduct @ProductName = 'Mi Mix', @UnitId = 1, @CreateUser = 1, @SupplierId = 4
--exec InsertProductVariant @ProductId = 2, @VariantName = 'Black', @UnitPrice = 600, @Cost = 500, @CreateUser = 1

exec AddShoppingCart @UserId = 1, @ProductVariantId=1, @Quantity=2
select * from ShoppingCarts
