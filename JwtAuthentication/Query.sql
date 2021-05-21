Create Database AuthenticationShop
Go
Use AuthenticationShop
Go
Create Table Users(
	Id uniqueidentifier primary key default NEWID(),
	Name nvarchar(256),
	Username nvarchar(256) NOT NULL unique,
	Password varchar(256) NOT NULL,
	Email varchar(256) NOT NULL,
	Role uniqueidentifier NOT NULL,
	CreateDate datetime default getdate()
)
Go
Create Table Roles(
	Id uniqueidentifier primary key default NEWID(),
	RoleName nvarchar(256),
	CreateDate datetime default getdate()
)
Go
Insert Into Roles (Id, RoleName) Values('9c076c5c-d4d9-4426-b6bf-da7b01c49d81', 'User')
Insert Into Roles (Id, RoleName) Values('72e1494c-cd14-4983-9bb0-0968c559c713', 'Admin')
Go
Create Table Products(
	Id uniqueidentifier primary key default NEWID(),
	Name nvarchar(256),
	Quantity int,
	Price float,
	MinQuantity int,
	CreateDate datetime default getdate()
)
Go
Create Table Orders(
	Id uniqueidentifier primary key default NEWID(),
	UserId uniqueidentifier,
	Status nvarchar(256),
	CreateDate datetime default getdate()
)
Go
Create Table OrderDetail(
	Id uniqueidentifier primary key default NEWID(),
	OrderId uniqueidentifier,
	ProductId uniqueidentifier,
	Quantity int,
	Price float,
	CreateDate datetime default getdate()
)