exec InsertUser @Username = 'ayalcin', @Email = 'huseyin@avniyalcin.com', @Password = '12345678'

exec InsertSupplier @SupplierName = 'Apple'
exec InsertSupplier @SupplierName = 'Samsung'
exec InsertSupplier @SupplierName = 'Vestel'
exec InsertSupplier @SupplierName = 'Xiaomi'
exec InsertSupplier @SupplierName = 'HTC'
exec InsertSupplier @SupplierName = 'LG'
exec InsertSupplier @SupplierName = 'Sony'
exec InsertSupplier @SupplierName = 'Nokia'
exec InsertSupplier @SupplierName = 'Blackberry'
exec InsertSupplier @SupplierName = 'Huawei'

exec InsertProduct @ProductName = 'iPhoneX', @UnitId = 1, @CreateUser = 1, @SupplierId = 1
exec InsertProduct @ProductName = 'Galaxy S9', @UnitId = 1, @CreateUser = 1, @SupplierId = 2
exec InsertProduct @ProductName = 'Venus', @UnitId = 1, @CreateUser = 1, @SupplierId = 3
exec InsertProduct @ProductName = 'Mi Mix', @UnitId = 1, @CreateUser = 1, @SupplierId = 4
exec InsertProduct @ProductName = 'One', @UnitId = 1, @CreateUser = 1, @SupplierId = 5
exec InsertProduct @ProductName = 'G9', @UnitId = 1, @CreateUser = 1, @SupplierId = 6
exec InsertProduct @ProductName = 'Xperia', @UnitId = 1, @CreateUser = 1, @SupplierId = 7
exec InsertProduct @ProductName = '3310', @UnitId = 1, @CreateUser = 1, @SupplierId = 8
exec InsertProduct @ProductName = 'BB', @UnitId = 1, @CreateUser = 1, @SupplierId = 9
exec InsertProduct @ProductName = 'P20', @UnitId = 1, @CreateUser = 1, @SupplierId = 10



exec InsertProductVariant @ProductId = 1, @VariantName = 'Black', @UnitPrice = 700, @Cost = 500, @CreateUser = 1
exec InsertProductVariant @ProductId = 2, @VariantName = 'Black', @UnitPrice = 650, @Cost = 450, @CreateUser = 1
exec InsertProductVariant @ProductId = 3, @VariantName = 'Black', @UnitPrice = 600, @Cost = 400, @CreateUser = 1
exec InsertProductVariant @ProductId = 4, @VariantName = 'Black', @UnitPrice = 550, @Cost = 350, @CreateUser = 1
exec InsertProductVariant @ProductId = 5, @VariantName = 'Black', @UnitPrice = 500, @Cost = 300, @CreateUser = 1
exec InsertProductVariant @ProductId = 6, @VariantName = 'Black', @UnitPrice = 450, @Cost = 250, @CreateUser = 1
exec InsertProductVariant @ProductId = 7, @VariantName = 'Black', @UnitPrice = 400, @Cost = 200, @CreateUser = 1
exec InsertProductVariant @ProductId = 8, @VariantName = 'Black', @UnitPrice = 350, @Cost = 150, @CreateUser = 1
exec InsertProductVariant @ProductId = 9, @VariantName = 'Black', @UnitPrice = 300, @Cost = 100, @CreateUser = 1
exec InsertProductVariant @ProductId = 10, @VariantName = 'Black', @UnitPrice = 250, @Cost = 50, @CreateUser = 1
exec InsertProductVariant @ProductId = 1, @VariantName = 'White', @UnitPrice = 700, @Cost = 500, @CreateUser = 1
exec InsertProductVariant @ProductId = 2, @VariantName = 'White', @UnitPrice = 650, @Cost = 450, @CreateUser = 1
exec InsertProductVariant @ProductId = 3, @VariantName = 'White', @UnitPrice = 600, @Cost = 400, @CreateUser = 1
exec InsertProductVariant @ProductId = 4, @VariantName = 'White', @UnitPrice = 550, @Cost = 350, @CreateUser = 1
exec InsertProductVariant @ProductId = 5, @VariantName = 'White', @UnitPrice = 500, @Cost = 300, @CreateUser = 1
exec InsertProductVariant @ProductId = 6, @VariantName = 'White', @UnitPrice = 450, @Cost = 250, @CreateUser = 1
exec InsertProductVariant @ProductId = 7, @VariantName = 'White', @UnitPrice = 400, @Cost = 200, @CreateUser = 1
exec InsertProductVariant @ProductId = 8, @VariantName = 'White', @UnitPrice = 350, @Cost = 150, @CreateUser = 1
exec InsertProductVariant @ProductId = 9, @VariantName = 'White', @UnitPrice = 300, @Cost = 100, @CreateUser = 1
exec InsertProductVariant @ProductId = 10, @VariantName = 'White', @UnitPrice = 250, @Cost = 50, @CreateUser = 1

exec AddShoppingCart @UserId = 1, @ProductVariantId=8, @Quantity=2
exec AddShoppingCart @UserId = 1, @ProductVariantId=9, @Quantity=3
exec AddShoppingCart @UserId = 1, @ProductVariantId=10, @Quantity=1
exec AddShoppingCart @UserId = 1, @ProductVariantId=11, @Quantity=1
exec AddShoppingCart @UserId = 1, @ProductVariantId=12, @Quantity=5

select * from ShoppingCarts