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

SELECT * FROM 
(SELECT v.ProductId, v.ProductVariantId, v.Cost, v.UnitPrice,
c.Quantity, v.Stock, 
IIF(v.Stock < c.Quantity, v.Stock, c.Quantity) as OrderQuantity,
IIF(v.Stock < c.Quantity, v.Stock*v.UnitPrice, c.Quantity*v.UnitPrice) as TotalPrice,
IIF(v.Stock < c.Quantity, v.Stock*v.Cost, c.Quantity*v.Cost) as TotalCost
FROM ShoppingCarts c
INNER JOIN ProductVariants v on c.ProductVariantId = v.ProductVariantId
WHERE c.UserId = @UserID) 
as OrderProductItems WHERE OrderQuantity > 0

--update productvariants set stock = 0