USE [evaluation]
GO

/****** Object:  Table [dbo].[Kitchens]    Script Date: 6/17/2020 10:55:26 AM  hello******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Kitchens](
	[KitchenID] [int] NOT NULL,
	[KitchenName] [nvarchar](max) NOT NULL,
	[UserID] [int] NOT NULL,
	[WorkingDays] [nvarchar](50) NOT NULL,
	[StartTime] [smalldatetime] NOT NULL,
	[CloseTime] [smalldatetime] NOT NULL,
	[Image] [image] NULL,
 CONSTRAINT [PK_Kitchens] PRIMARY KEY CLUSTERED 
(
	[KitchenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Kitchens]  WITH CHECK ADD  CONSTRAINT [FK_Users_Kitchen] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[Kitchens] CHECK CONSTRAINT [FK_Users_Kitchen]
GO


