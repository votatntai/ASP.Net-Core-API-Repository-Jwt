USE [AuthenticationShop]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 6/9/2021 3:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 6/9/2021 3:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[Status] [nvarchar](256) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 6/9/2021 3:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NOT NULL,
	[Maker] [nvarchar](256) NOT NULL,
	[Category] [nvarchar](256) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[MinQuantity] [int] NOT NULL,
	[ImageURL] [nvarchar](256) NOT NULL,
	[CreateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 6/9/2021 3:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 6/9/2021 3:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/9/2021 3:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Username] [nvarchar](256) NOT NULL,
	[Password] [varchar](256) NOT NULL,
	[Email] [varchar](256) NOT NULL,
	[Status] [nvarchar](256) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK__Users__3214EC074D3635C9] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [Quantity], [Price], [CreateDate]) VALUES (N'efd1aa60-4665-43aa-97fb-09d1069b6a11', N'bc3db867-00d5-4d93-b2eb-3e5db4c4b036', 10, 15000000, CAST(N'2021-06-09T10:15:14.347' AS DateTime))
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [Quantity], [Price], [CreateDate]) VALUES (N'6c128d3e-f8c6-47b1-aef2-1191d81d1d63', N'bc3db867-00d5-4d93-b2eb-3e5db4c4b036', 5, 7500000, CAST(N'2021-06-09T10:24:09.613' AS DateTime))
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [Quantity], [Price], [CreateDate]) VALUES (N'8260c4db-9d21-4bb3-a863-f9a92ededf63', N'bc3db867-00d5-4d93-b2eb-3e5db4c4b036', 7, 10500000, CAST(N'2021-06-09T10:27:40.953' AS DateTime))
GO
INSERT [dbo].[Orders] ([Id], [UserId], [Status], [CreateDate]) VALUES (N'efd1aa60-4665-43aa-97fb-09d1069b6a11', N'dc8b6030-cfb4-43f8-908b-d845b82b84fe', N'Unconfirmed', CAST(N'2021-06-09T10:15:14.320' AS DateTime))
INSERT [dbo].[Orders] ([Id], [UserId], [Status], [CreateDate]) VALUES (N'6c128d3e-f8c6-47b1-aef2-1191d81d1d63', N'dc8b6030-cfb4-43f8-908b-d845b82b84fe', N'Unconfirmed', CAST(N'2021-06-09T10:24:09.600' AS DateTime))
INSERT [dbo].[Orders] ([Id], [UserId], [Status], [CreateDate]) VALUES (N'8260c4db-9d21-4bb3-a863-f9a92ededf63', N'dc8b6030-cfb4-43f8-908b-d845b82b84fe', N'Unconfirmed', CAST(N'2021-06-09T10:27:40.950' AS DateTime))
GO
INSERT [dbo].[Products] ([Id], [Name], [Maker], [Category], [Quantity], [Price], [MinQuantity], [ImageURL], [CreateDate]) VALUES (N'bc3db867-00d5-4d93-b2eb-3e5db4c4b036', N'Xe đạp của Giang', N'Janglee', N'Xe', 1500, CAST(1500000 AS Decimal(18, 0)), 10, N'https://i.ibb.co/R04xKbd/giang.jpg', CAST(N'2021-06-08T23:23:37.457' AS DateTime))
INSERT [dbo].[Products] ([Id], [Name], [Maker], [Category], [Quantity], [Price], [MinQuantity], [ImageURL], [CreateDate]) VALUES (N'64aa41a8-aa3a-4f75-9564-bdfdb9446750', N'asd', N'asd', N'asd', 213, CAST(123 AS Decimal(18, 0)), 123, N'https://i.ibb.co/R04xKbd/giang.jpg', CAST(N'2021-06-09T15:08:55.973' AS DateTime))
GO
INSERT [dbo].[Roles] ([Id], [RoleName], [CreateDate]) VALUES (N'72e1494c-cd14-4983-9bb0-0968c559c713', N'Admin', CAST(N'2021-06-06T18:23:34.150' AS DateTime))
INSERT [dbo].[Roles] ([Id], [RoleName], [CreateDate]) VALUES (N'04bd15b7-4810-419a-b358-3a7a7b370cf1', N'Saler', CAST(N'2021-06-06T18:23:34.150' AS DateTime))
INSERT [dbo].[Roles] ([Id], [RoleName], [CreateDate]) VALUES (N'e9004eb7-49de-43e8-b6fe-aa06e9e4d854', N'Modifier', CAST(N'2021-06-06T18:23:34.150' AS DateTime))
INSERT [dbo].[Roles] ([Id], [RoleName], [CreateDate]) VALUES (N'9c076c5c-d4d9-4426-b6bf-da7b01c49d81', N'User', CAST(N'2021-06-06T18:23:34.147' AS DateTime))
GO
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (N'b3f0cca0-e032-4378-b202-026e27da965b', N'04bd15b7-4810-419a-b358-3a7a7b370cf1')
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (N'0648e7f2-e36e-4e08-92aa-9fb0c9d2c030', N'9c076c5c-d4d9-4426-b6bf-da7b01c49d81')
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (N'84c7dfa3-13d8-4517-87ad-ba940d3dfd7b', N'e9004eb7-49de-43e8-b6fe-aa06e9e4d854')
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (N'dc8b6030-cfb4-43f8-908b-d845b82b84fe', N'72e1494c-cd14-4983-9bb0-0968c559c713')
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (N'dc8b6030-cfb4-43f8-908b-d845b82b84fe', N'04bd15b7-4810-419a-b358-3a7a7b370cf1')
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (N'dc8b6030-cfb4-43f8-908b-d845b82b84fe', N'e9004eb7-49de-43e8-b6fe-aa06e9e4d854')
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (N'dc8b6030-cfb4-43f8-908b-d845b82b84fe', N'9c076c5c-d4d9-4426-b6bf-da7b01c49d81')
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (N'14a42f7e-c80d-40c1-b824-d8f796a7b1c7', N'9c076c5c-d4d9-4426-b6bf-da7b01c49d81')
GO
INSERT [dbo].[Users] ([Id], [Name], [Username], [Password], [Email], [Status], [CreateDate]) VALUES (N'b3f0cca0-e032-4378-b202-026e27da965b', N'Saler', N'saler', N'saler', N'saler@gmail.com', N'Not Activated', CAST(N'2021-06-08T23:18:20.520' AS DateTime))
INSERT [dbo].[Users] ([Id], [Name], [Username], [Password], [Email], [Status], [CreateDate]) VALUES (N'0648e7f2-e36e-4e08-92aa-9fb0c9d2c030', N'User', N'user', N'user', N'user@gmail.com', N'Not Activated', CAST(N'2021-06-08T23:26:14.663' AS DateTime))
INSERT [dbo].[Users] ([Id], [Name], [Username], [Password], [Email], [Status], [CreateDate]) VALUES (N'84c7dfa3-13d8-4517-87ad-ba940d3dfd7b', N'Modifier', N'modifier', N'modifier', N'modifier@gmail.com', N'Not Activated', CAST(N'2021-06-08T23:26:02.947' AS DateTime))
INSERT [dbo].[Users] ([Id], [Name], [Username], [Password], [Email], [Status], [CreateDate]) VALUES (N'dc8b6030-cfb4-43f8-908b-d845b82b84fe', N'Admin', N'admin', N'admin', N'admin@gmail.com', N'Activated', CAST(N'2021-06-06T19:56:30.403' AS DateTime))
INSERT [dbo].[Users] ([Id], [Name], [Username], [Password], [Email], [Status], [CreateDate]) VALUES (N'14a42f7e-c80d-40c1-b824-d8f796a7b1c7', N'Banned User', N'banned', N'tantai4899', N'userban@fpt.edu.vn', N'Banned', CAST(N'2021-06-09T14:58:12.560' AS DateTime))
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__536C85E46F7DA8DF]    Script Date: 6/9/2021 3:49:09 PM ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UQ__Users__536C85E46F7DA8DF] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__Id__35BCFE0A]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__CreateDat__36B12243]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK__Orders__UserId__398D8EEE] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK__Orders__UserId__398D8EEE]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK__UserRole__UserId__3B75D760] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK__UserRole__UserId__3B75D760]
GO
