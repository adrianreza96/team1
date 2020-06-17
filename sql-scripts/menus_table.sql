USE [evaluation]
GO

/****** Object:  Table [dbo].[Menu]    Script Date: 6/17/2020 10:56:37 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Menu](
	[MenuID] [int] NOT NULL,
	[ItemName] [nvarchar](50) NOT NULL,
	[VeganFriendly] [bit] NOT NULL,
	[Price] [money] NOT NULL,
	[KitchenID] [int] NOT NULL,
	[MenuType] nvarchar(20) NOT NULL,
	[Ingredients] nvarchar(max) NOT NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Menu]  WITH CHECK ADD FOREIGN KEY([KitchenID])
REFERENCES [dbo].[Kitchens] ([KitchenID])
GO


