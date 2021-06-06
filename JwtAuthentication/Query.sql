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
	Foreign key (RoleId) references Roles(Id),
	Primary key (UserId, RoleId)
)
Go
Create Table Products(
	Id uniqueidentifier primary key default NEWID(),
	Name nvarchar(256) NOT NULL,
	Maker nvarchar(256) NOT NULL,
	Category nvarchar(256) NOT NULL,
	Quantity int NOT NULL,
	Price decimal NOT NULL,
	MinQuantity int NOT NULL,
	ImageURL nvarchar(256) NOT NULL,
	CreateDate datetime default getdate()
)
Go
Create Table Orders(
	Id uniqueidentifier primary key default NEWID(),
	UserId uniqueidentifier Foreign key References Users(Id) NOT NULL,
	Status nvarchar(256) NOT NULL,
	CreateDate datetime default getdate() NOT NULL
)
Go
Create Table OrderDetails(
	OrderId uniqueidentifier NOT NULL,
	ProductId uniqueidentifier NOT NULL,
	Quantity int NOT NULL,
	Price float NOT NULL,
	CreateDate datetime default getdate() NOT NULL,
	Foreign Key (ProductId) References Products(Id),
	Foreign Key (OrderId) References Orders(Id),
	Primary Key (OrderId, ProductID)
)


Insert Into Roles (Id, RoleName) Values('9c076c5c-d4d9-4426-b6bf-da7b01c49d81', 'User')
Insert Into Roles (Id, RoleName) Values('72e1494c-cd14-4983-9bb0-0968c559c713', 'Admin')
Insert Into Roles (Id, RoleName) Values('e9004eb7-49de-43e8-b6fe-aa06e9e4d854', 'Modifier')
Insert Into Roles (Id, RoleName) Values('04bd15b7-4810-419a-b358-3a7a7b370cf1', 'Saler')
