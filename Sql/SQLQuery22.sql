DECLARE 
@i int = 0
BEGIN
	WHILE @i < 100
	BEGIN
		Declare @num varchar(50) = 'ayalcin_' + CONVERT(varchar(5), @i);
		print @num;
		exec InsertUser @UserName =  @num, 
			@Password = '12345678',
			@Email = 'huseyin@avniyalcin.com',
			@Age = 37,
			@Gender = 1;
			set @i = @i + 1;
	END
END

select * from Users