CREATE TABLE [dbo].Roles(
[RoleId] [int] PRIMARY KEY identity(1,1),
[Role] [varchar](20) NOT NULL UNIQUE,
);

insert into Roles(Role) values
('guest'), ('customer'), ('kitchen_provider'), ('admin');

select * from Roles


CREATE table [dbo].[Users](
[UserId] [int] PRIMARY KEY identity(1,1),
FirstName varchar(50) NOT NULL,
LastName varchar(50) NOT NULL,
[Email] [nvarchar](50) Unique,
[Password] [varchar](24),
[BillingAddress] [nvarchar](100),
[SecurityQuestionOne] [nvarchar](50),
[SecurityQuestionTwo] [nvarchar](50),
[RoleId] [int] DEFAULT 1,
CONSTRAINT fk_user_role foreign key(RoleId) references Roles(RoleId)
);


CREATE table [dbo].[Kitchens](
[KitchenId] [int] PRIMARY KEY identity(1,1),
[KitchenName] [nvarchar](50) NOT NULL,
UserId int,
[WorkingDays] [nvarchar](50),
[StartTime] [smalldatetime],
[CloseTime] [smalldatetime],
[Image] [image] NULL,
CONSTRAINT fk_kitchen_user foreign key(UserId) references Users(UserId)
);


CREATE table [dbo].[Menu](
MenuId int PRIMARY KEY identity(1,1),
KitchenId int foreign key references Kitchens(KitchenID),
ItemName nvarchar(50) NOT NULL,
VeganFriendly varchar(20),
MenuType nvarchar(50),
Ingredients nvarchar(max),
Image image,
Price money,
CONSTRAINT fk_menu_kitchen foreign key(KitchenId) references Kitchens(KitchenId)
);


CREATE table [dbo].Orders(
OrderId int PRIMARY KEY identity(1,1),
UserId int,
MenuId int,
Quantity int,
IsPaid bit,
OrderDate DateTime,
ShippingAddress nvarchar(max),
CONSTRAINT fk_order_menu foreign key(MenuId) references Menu(MenuId),
CONSTRAINT fk_order_user foreign key(UserId) references Users(UserId)
);


