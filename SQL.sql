IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Order')
BEGIN
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) PRIMARY KEY,
	[Id] [nvarchar](450) NOT NULL,
	[DiscountId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[quantity] [int] NULL,
	[price] [int] NULL,
	)
END 
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Product')
BEGIN
CREATE TABLE [dbo].[Product] (
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Supplier] [nvarchar](255) NULL,
	)
END 
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Discount')
BEGIN
    CREATE TABLE [dbo].[Discount] (
        [DiscountId] [int] IDENTITY(1,1) NOT NULL,
        [Percentage] [decimal](5, 2) NULL,
        [StartDate] [date] NULL,
        [EndDate] [date] NULL,
        CONSTRAINT [PK_Discount] PRIMARY KEY CLUSTERED ([DiscountId] ASC)
    )
END
