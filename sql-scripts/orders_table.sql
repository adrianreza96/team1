USE [evaluation]
GO

/****** Object:  Table [dbo].[orders_table]    Script Date: 6/17/2020 10:59:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[orders_table](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[MenuID] [int] NULL,
	[Quantity] [int] NOT NULL,
	[TotalPrice] [int] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[ShippingAddress] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[orders_table]  WITH CHECK ADD FOREIGN KEY([MenuID])
REFERENCES [dbo].[Menu] ([MenuID])
GO

ALTER TABLE [dbo].[orders_table]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO


