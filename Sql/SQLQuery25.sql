DECLARE
@UserId bigint, @OrderId bigint,
@ProductId bigint, @ProductVariantId bigint, @Cost money, @UnitPrice money,
@Quantity int, @Stock int, @OrderQuantity int, @OrderTotalPrice money, 
@OrderTotalCost money, @TotalPrice money, 
@TotalCost money, @CityId int, @CountryId int, @Street nvarchar(250), 
@Zip nvarchar(10), @Telephone nvarchar(15);

--Set Values
SET @UserId = 1; SET @CityId=1; SET @CountryId=1; 
SET @Street = 'BAGDAT ST'; SET @Zip = '34700'; 
SET @Telephone = '12345678910';

--Calculate TotalCost & TotalPrice
SELECT 
@OrderTotalPrice = SUM(IIF(v.Stock < c.Quantity, v.Stock*v.UnitPrice, c.Quantity*v.UnitPrice)),
@OrderTotalCost = SUM(IIF(v.Stock < c.Quantity, v.Stock*v.Cost, c.Quantity*v.Cost))
FROM ShoppingCarts c
INNER JOIN ProductVariants v on c.ProductVariantId = v.ProductVariantId
WHERE c.UserId = @UserID
--DEBUG VALUES
--SELECT @OrderTotalPrice as TotalPrice, @OrderTotalCost as TotalCost

--Create New Order & Return Value

INSERT INTO [Orders] ([UserId], [TotalCost], [TotalPrice], [CityId], 
			[CountryId], [Street], [Zip], [Telephone])
     VALUES (@UserId, @OrderTotalCost, @OrderTotalPrice, @CityId, @CountryId, @Street,
		   @Zip, @Telephone);
SET @OrderId = SCOPE_IDENTITY();
--Debug Order
SELECT * FROM Orders WHERE OrderId = @OrderId;

--Order Products Query
--SELECT * FROM 
--(SELECT v.ProductId, v.ProductVariantId, v.Cost, v.UnitPrice,
--c.Quantity, v.Stock, 
--IIF(v.Stock < c.Quantity, v.Stock, c.Quantity) as OrderQuantity,
--IIF(v.Stock < c.Quantity, v.Stock*v.UnitPrice, c.Quantity*v.UnitPrice) as TotalPrice,
--IIF(v.Stock < c.Quantity, v.Stock*v.Cost, c.Quantity*v.Cost) as TotalCost
--FROM ShoppingCarts c
--INNER JOIN ProductVariants v on c.ProductVariantId = v.ProductVariantId
--WHERE c.UserId = @UserID) as OrderProductItems WHERE OrderQuantity > 0


--Create Order Products
DECLARE orderCursor CURSOR FOR
SELECT * FROM 
(SELECT v.ProductId, v.ProductVariantId, v.Cost, v.UnitPrice,
c.Quantity, v.Stock, 
IIF(v.Stock < c.Quantity, v.Stock, c.Quantity) as OrderQuantity,
IIF(v.Stock < c.Quantity, v.Stock*v.UnitPrice, c.Quantity*v.UnitPrice) as TotalPrice,
IIF(v.Stock < c.Quantity, v.Stock*v.Cost, c.Quantity*v.Cost) as TotalCost
FROM ShoppingCarts c
INNER JOIN ProductVariants v on c.ProductVariantId = v.ProductVariantId
WHERE c.UserId = @UserID) as OrderProductItems WHERE OrderQuantity > 0

OPEN orderCursor
FETCH NEXT FROM orderCursor into @ProductId, @ProductVariantId, @Cost, @UnitPrice,
@Quantity, @Stock, @OrderQuantity, @TotalPrice, @TotalCost

WHILE (@@FETCH_STATUS = 0)
BEGIN
	print(CAST(@ProductId as nvarchar(50)) + ' ' + CAST(@ProductVariantId as nvarchar(50)) + ' ' + CAST(@Cost as nvarchar(50)) + ' ' + CAST(@UnitPrice as nvarchar(50)) + ' ' +
CAST(@Quantity as nvarchar(50)) + ' ' + CAST(@Stock as nvarchar(50)) + ' QTY:' + CAST(@OrderQuantity as nvarchar(50)) + ' ' + CAST(@TotalPrice as nvarchar(50)) + ' ' + CAST(@TotalCost as nvarchar(50)))

	--INSERT INTO OrderProducts
 --   ([OrderId], [ProductVariantId], [Quantity], [UnitPrice], [Cost], [TotalPrice], [TotalCost])
 --   VALUES(@OrderId, @ProductVariantId, @OrderQuantity, @UnitPrice, @Cost, @TotalPrice, @TotalCost);

	--UPDATE ProductVariants SET Stock = Stock - @OrderQuantity where ProductVariantId = @ProductVariantId
	SELECT Stock - @OrderQuantity as removeStock from ProductVariants where ProductVariantId = @ProductVariantId
	FETCH NEXT FROM orderCursor into @ProductId, @ProductVariantId, @Cost, @UnitPrice,
@Quantity, @Stock, @OrderQuantity, @TotalPrice, @TotalCost;
END
CLOSE orderCursor;
DEALLOCATE orderCursor;
--DEBUG OrderProducts
SELECT * FROM OrderProducts WHERE OrderId=@OrderId

--DELETE FROM ShoppingCarts WHERE @UserId = @UserId