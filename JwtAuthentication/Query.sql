Create Database AuthenticationShop
Go
Use AuthenticationShop
Go
Create Table Users(
	Id uniqueidentifier primary key default NEWID(),
	Name nvarchar(256) NOT NULL,
	Username nvarchar(256) NOT NULL unique,
	Password varchar(256) NOT NULL,
	Email varchar(256) NOT NULL,
	CreateDate datetime default getdate() NOT NULL
)
Go
Create Table Roles(
	Id uniqueidentifier primary key default NEWID(),
	RoleName nvarchar(256) NOT NULL,
	CreateDate datetime default getdate() NOT NULL
)
Go
Create Table UserRole(
	UserId uniqueidentifier NOT NULL,
	RoleId uniqueidentifier NOT NULL,
	Foreign key (UserId) references Users(Id),
	Foreign key (RoleId) references Roles(Id)
)
Go
Create Table Products(
	Id uniqueidentifier primary key default NEWID(),
	Name nvarchar(256) NOT NULL,
	Maker nvarchar(256) NOT NULL,
	Quantity int NOT NULL,
	Price decimal NOT NULL,
	MinQuantity int NOT NULL,
	CreateDate datetime default getdate()
)
Go
Create Table Orders(
	Id uniqueidentifier primary key default NEWID(),
	UserId uniqueidentifier Foreign key References Users(Id),
	Status nvarchar(256) NOT NULL,
	CreateDate datetime default getdate() NOT NULL
)
Go
Create Table OrderDetails(
	Id uniqueidentifier primary key default NEWID(),
	OrderId uniqueidentifier NOT NULL,
	ProductId uniqueidentifier unique NOT NULL,
	Quantity int NOT NULL,
	Price float NOT NULL,
	CreateDate datetime default getdate() NOT NULL,
	Foreign Key (ProductId) References Products(Id),
	Foreign Key (OrderId) References Orders(Id)
)
Go

Insert Into Roles (Id, RoleName) Values('9c076c5c-d4d9-4426-b6bf-da7b01c49d81', 'User')
Insert Into Roles (Id, RoleName) Values('72e1494c-cd14-4983-9bb0-0968c559c713', 'Admin')