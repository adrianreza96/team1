CREATE TABLE Roles(
[RoleID] [int] PRIMARY KEY identity(1,1),
[Role] [varchar](20) NOT NULL,
);

CREATE table Users(
[UserID] [int] PRIMARY KEY identity(1,1),
[Name] [nvarchar](50) NOT NULL,
[Email] [nvarchar](50) NOT NULL,
[Password] [varchar](24) NOT NULL,
[Address] [nvarchar](max) NOT NULL,
[SecurityQuestionOne] [nvarchar](50),
[SecurityQuestionTwo] [nvarchar](50),
[RoleID] [int] foreign key references Roles(RoleID) NOT NULL default 4,
);

CREATE table Kitchens(
[KitchenID] [int] PRIMARY KEY identity(1,1),
[KitchenName] [nvarchar](max) NOT NULL,
UserID int foreign key references Users(UserID) NOT NULL,
[WorkingDays] [nvarchar](50) NOT NULL,
[StartTime] [smalldatetime] NOT NULL,
[CloseTime] [smalldatetime] NOT NULL,
[Image] [image] NULL,
);

CREATE table Menu(
MenuID int PRIMARY KEY identity(1,1),
KitchenID int foreign key references Kitchens(KitchenID),
ItemName nvarchar(50) NOT NULL,
VeganFriendly varchar(20) NOT NULL,
MenuType nvarchar(50) NOT NULL,
Ingredients nvarchar(max) NOT NULL,
Price money NOT NULL,
);

CREATE table Orders(
OrderID int PRIMARY KEY identity(1,1),
UserID int foreign key references Users(UserID),
MenuID int foreign key references Menu(MenuID),
Quantity int NOT NULL,
TotalPrice int NOT NULL,
OrderDate DateTime NOT NULL,
ShippingAddress nvarchar(max) NOT NULL,
);


