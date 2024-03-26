SET IDENTITY_INSERT [dbo].[Customers] ON
INSERT INTO [dbo].[Customers] ([CustomerId], [CustomerName], [CustAddress], [City]) VALUES (200, N'User 1', N'Vijaynagar', N'Bangalore')
INSERT INTO [dbo].[Customers] ([CustomerId], [CustomerName], [CustAddress], [City]) VALUES (201, N'varun', N'MehendiPatnam', N'Hyderabad')
INSERT INTO [dbo].[Customers] ([CustomerId], [CustomerName], [CustAddress], [City]) VALUES (202, N'User 3', N'WagaBorder', N'Amritsar')
INSERT INTO [dbo].[Customers] ([CustomerId], [CustomerName], [CustAddress], [City]) VALUES (203, N'User 4', N'MarinaBeach', N'Chennai')
INSERT INTO [dbo].[Customers] ([CustomerId], [CustomerName], [CustAddress], [City]) VALUES (204, N'tanish', N'Rajajinagar', N'Bangalore')
SET IDENTITY_INSERT [dbo].[Customers] OFF
