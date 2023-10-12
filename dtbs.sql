USE [dbBookStore]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 10/12/2023 11:20:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[idAdmin] [int] NOT NULL,
	[codeAdmin] [char](13) NOT NULL,
	[nameAdmin] [nvarchar](50) NOT NULL,
	[phone] [nvarchar](15) NOT NULL,
	[userName] [nvarchar](50) NOT NULL,
	[passWord] [nvarchar](50) NOT NULL,
	[access] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[idAdmin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 10/12/2023 11:20:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[idAuthor] [int] NOT NULL,
	[codeAuthor] [char](13) NOT NULL,
	[nameAuthor] [nvarchar](100) NOT NULL,
	[address] [nvarchar](200) NULL,
	[story] [nvarchar](200) NULL,
	[phone] [nvarchar](15) NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[idAuthor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookCategory]    Script Date: 10/12/2023 11:20:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookCategory](
	[idBookCat] [int] NOT NULL,
	[codeBookCat] [char](13) NOT NULL,
	[idTopic] [int] NOT NULL,
	[nameBookCat] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_BookCategory] PRIMARY KEY CLUSTERED 
(
	[idBookCat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 10/12/2023 11:20:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[idBooks] [int] NOT NULL,
	[codeBooks] [char](13) NOT NULL,
	[nameBooks] [nvarchar](200) NOT NULL,
	[describe] [nvarchar](max) NOT NULL,
	[Thumb] [varchar](2000) NOT NULL,
	[updateDay] [date] NOT NULL,
	[quantity] [int] NOT NULL,
	[price] [float] NOT NULL,
	[idBookCat] [int] NOT NULL,
	[idPublisher] [int] NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[idBooks] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 10/12/2023 11:20:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[idCustomer] [int] NOT NULL,
	[codeCustomer] [char](13) NOT NULL,
	[nameCustomer] [nvarchar](100) NOT NULL,
	[userName] [nvarchar](50) NOT NULL,
	[passWord] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[address] [nvarchar](200) NOT NULL,
	[phone] [nvarchar](15) NOT NULL,
	[birthDay] [date] NOT NULL,
	[creDate] [datetime] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[idCustomer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 10/12/2023 11:20:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[idOrder] [int] IDENTITY(1,1) NOT NULL,
	[codeOrder] [char](50) NOT NULL,
	[checkPay] [bit] NOT NULL,
	[deliveryStatus] [int] NOT NULL,
	[orderDate] [datetime] NOT NULL,
	[deliveryDate] [datetime] NOT NULL,
	[idCustomer] [int] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[idOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 10/12/2023 11:20:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[idOrder] [int] NOT NULL,
	[idBooks] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[price] [decimal](9, 2) NOT NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[idOrder] ASC,
	[idBooks] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publisher]    Script Date: 10/12/2023 11:20:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publisher](
	[idPublisher] [int] NOT NULL,
	[codePublisher] [char](13) NOT NULL,
	[namePublisher] [nvarchar](100) NOT NULL,
	[address] [nvarchar](max) NULL,
	[Phone] [nvarchar](15) NULL,
 CONSTRAINT [PK_Publisher] PRIMARY KEY CLUSTERED 
(
	[idPublisher] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Topic]    Script Date: 10/12/2023 11:20:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topic](
	[idTopic] [int] NOT NULL,
	[codeTopic] [char](13) NOT NULL,
	[nameTopic] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Topic] PRIMARY KEY CLUSTERED 
(
	[idTopic] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WriteBook]    Script Date: 10/12/2023 11:20:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WriteBook](
	[idAuthor] [int] NOT NULL,
	[idBooks] [int] NOT NULL,
	[access] [nvarchar](50) NOT NULL,
	[location] [nvarchar](50) NOT NULL,
	[idWriteBook] [int] NOT NULL,
 CONSTRAINT [PK_WriteBook] PRIMARY KEY CLUSTERED 
(
	[idWriteBook] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Admin] ([idAdmin], [codeAdmin], [nameAdmin], [phone], [userName], [passWord], [access]) VALUES (1, N'1            ', N'Nguyễn Kim Hoàng', N'0352931831', N'kimhoang', N'kimhoang', N'1')
GO
INSERT [dbo].[Author] ([idAuthor], [codeAuthor], [nameAuthor], [address], [story], [phone]) VALUES (1, N'TG2123BS     ', N'hoang', NULL, NULL, NULL)
GO
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (1, N'TL2123BS     ', 1, N'Truyện Tranh')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (2, N'TL2223BS     ', 1, N'Bách khoa tri thức')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (3, N'TL2323BS     ', 1, N'Sách song ngữ thiếu nhi')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (4, N'TL2423BS     ', 1, N'Sách tô màu')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (5, N'TL2523BS     ', 1, N'Văn học thiếu nhi')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (6, N'TL2623BS     ', 1, N'Manga - Comic')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (7, N'TL2723BS     ', 2, N'Kỹ năng sống')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (8, N'TL2823BS     ', 2, N'Kỹ năng nghề nghiệp')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (9, N'TL2923BS     ', 2, N'Phát triển tâm thức')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (10, N'TL21023BS    ', 2, N'Phổ biến kiến thức')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (11, N'TL21123BS    ', 3, N'Nuôi dạy con')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (12, N'TL21223BS    ', 3, N'Chăm sóc sức khỏe')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (13, N'TL21323BS    ', 3, N'Hôn nhân gia đình')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (14, N'TL21423BS    ', 3, N'Cẩm nang du lịch')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (15, N'TL21523BS    ', 3, N'Dạy nấu ăn')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (16, N'TL21623BS    ', 1, N'Thời trang - Làm đẹp')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (17, N'TL21723BS    ', 4, N'Công nghệ thông tin')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (18, N'TL21823BS    ', 4, N'Tạp chí khoa học')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (19, N'TL21923BS    ', 4, N'Khoa học tự nhiên')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (20, N'TL22023BS    ', 4, N'Y - dược')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (21, N'TL22123BS    ', 4, N'Cơ khí, chế tạo máy')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (22, N'TL22223BS    ', 5, N'Quản trị - Kinh doanh')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (23, N'TL22323BS    ', 5, N'Marketing')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (24, N'TL22423BS    ', 5, N'Doanh nhân')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (25, N'TL22523BS    ', 6, N'Sách lịch sử')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (26, N'TL22623BS    ', 6, N'Văn hóa - Xã hội')
INSERT [dbo].[BookCategory] ([idBookCat], [codeBookCat], [idTopic], [nameBookCat]) VALUES (27, N'TL22723BS    ', 6, N'Pháp luật')
GO
INSERT [dbo].[Books] ([idBooks], [codeBooks], [nameBooks], [describe], [Thumb], [updateDay], [quantity], [price], [idBookCat], [idPublisher]) VALUES (1, N'SACH2123BS   ', N'  Bạn chưa đăng nhập  Sách - Pettson & Findus - Ổ Bánh Sinh Nhật', N'Pettson và Findus - bộ truyện dành cho bạn đọc yêu mèo, mến những ông già lập dị, ưa các tình huống oái oăm, thích trò nghịch ngợm và đang cần lắm tiếng cười. Hàng xóm kháo nhau rằng lão Pettson bị điên. Có Gustavsson làm chứng, hôm sinh nhật Findus, lão Pettson đã cột đuôi chú mèo vào một cái rèm, trộn trứng với bùn cùng… cái quần của lão để làm bột bánh, và bắc thang trèo lên mái nhà để đi chợ. Thế thì chắc mười mươi là ông già bị điên rồi, chứ còn gì nữa. Thế nhưng Findus lại nghĩ khác. Tất cả chỉ vì lão Pettson muốn làm bánh sinh nhật cho chú thôi. Ổ bánh sinh nhật này có gì đặc biệt thế nhỉ, cùng đọc cuốn sách để khám phá cách làm bánh hết sức kỳ công của ông lão nhé!', N'3sdl4lxokhinfet20wqi61xd5xcp3z4p-20231011204922117.jpeg', CAST(N'2023-10-11' AS Date), 20, 36000, 1, 2)
INSERT [dbo].[Books] ([idBooks], [codeBooks], [nameBooks], [describe], [Thumb], [updateDay], [quantity], [price], [idBookCat], [idPublisher]) VALUES (2, N'SACH2223BS   ', N' Kỹ Năng Sống Cho Trẻ - Tránh Uất Ức Khi Bị Chọc Tức', N'Với kinh nghiệm gần 10 năm nghiên cứu và giảng dạy kỹ năng tại hệ thống giáo dục Citysmart, Thạc sỹ Nguyễn Thu Hương đã tạo ra những hướng dẫn gần gũi, dễ nhớ, dễ hiểu cho các bạn nhỏ.Bộ sách gồm 6 cuốn, minh họa bằng hình hình ảnh đáng yêu, giúp các bé hào hứng hơn với việc ghi nhớ, giúp các bé đối phó với những tình huống xấu trong cuộc sống và hình thành nhân cách tốt hơn. Kỹ Năng Sống Cho Trẻ - Làm Nguội Cơn Giận Giữ Kỹ Năng Sống Cho Trẻ - Học Cách Thể Hiện Yêu Thương Kỹ Năng Sống Cho Trẻ - Tránh Uất Ức Khi Bị Chọc Tức Kỹ Năng Sống Cho Trẻ - Xử Lí Khi Bị Lạc Kỹ Năng Sống Cho Trẻ - Xử Lí Khi Bị Côn Trùng Đốt Kỹ Năng Sống Cho Trẻ - An Toàn Với Điện', N'wbf99g33bd8yzi50xfgsbgzomhkb38ma-20231011205826029.jpeg', CAST(N'2023-10-11' AS Date), 20, 14400, 1, 3)
INSERT [dbo].[Books] ([idBooks], [codeBooks], [nameBooks], [describe], [Thumb], [updateDay], [quantity], [price], [idBookCat], [idPublisher]) VALUES (3, N'SACH2323BS   ', N'Kỹ Năng Sống Cho Trẻ - Xử Lí Khi Bị Lạc', N'Với kinh nghiệm gần 10 năm nghiên cứu và giảng dạy kỹ năng tại hệ thống giáo dục Citysmart, Thạc sỹ Nguyễn Thu Hương đã tạo ra những hướng dẫn gần gũi, dễ nhớ, dễ hiểu cho các bạn nhỏ.  Bộ sách gồm 6 cuốn, minh họa bằng hình hình ảnh đáng yêu, giúp các bé hào hứng hơn với việc ghi nhớ, giúp các bé đối phó với những tình huống xấu trong cuộc sống và hình thành nhân cách tốt hơn.', N'h8snm9vy9lvi3f3d987ohmz9pp4vjmg4-20231011210151479.jpeg', CAST(N'2023-10-11' AS Date), 20, 14400, 1, 3)
INSERT [dbo].[Books] ([idBooks], [codeBooks], [nameBooks], [describe], [Thumb], [updateDay], [quantity], [price], [idBookCat], [idPublisher]) VALUES (4, N'SACH2423BS   ', N'Kỹ Năng Sống Cho Trẻ - Làm Nguội Cơn Giận Giữ', N'Với kinh nghiệm gần 10 năm nghiên cứu và giảng dạy kỹ năng tại hệ thống giáo dục Citysmart, Thạc sỹ Nguyễn Thu Hương đã tạo ra những hướng dẫn gần gũi, dễ nhớ, dễ hiểu cho các bạn nhỏ.  Bộ sách gồm 6 cuốn, minh họa bằng hình hình ảnh đáng yêu, giúp các bé hào hứng hơn với việc ghi nhớ, giúp các bé đối phó với những tình huống xấu trong cuộc sống và hình thành nhân cách tốt hơn.', N'xn66pjvkn1r34o3taec4xyr7cd5h803y-20231011210313767.jpeg', CAST(N'2023-10-11' AS Date), 20, 14400, 1, 3)
INSERT [dbo].[Books] ([idBooks], [codeBooks], [nameBooks], [describe], [Thumb], [updateDay], [quantity], [price], [idBookCat], [idPublisher]) VALUES (5, N'SACH2523BS   ', N'  Bạn chưa đăng nhập  15 Bí Kíp Giúp Tớ An Toàn – Cẩm Nang Phòng Tránh Bắt Nạt Và Bạo Lực Học Đường', N'Mỗi ngày, có hàng ngàn bạn nhỏ bị bắt nạt ở trường học, lớp học thêm, ở chung cư, trong xóm, thậm chí ở nhà, hay trên mạng xã hội và có khi là qua tin nhắn. Vậy chúng mình nên làm thế nào để tránh những tình huống bắt nạt và bạo lực học đường đây?  Cuốn sách 15 Bí Kíp Giúp Tớ An Toàn – Cẩm Nang Phòng Tránh Bắt Nạt Và Bạo Lực Học Đường sẽ cung cấp cho chúng ta một số kiến thức cơ bản trong việc', N'10nf1i3dnp95t9kybdw085colmuu8hqy-20231011210802971.jpeg', CAST(N'2023-10-11' AS Date), 20, 42500, 2, 4)
INSERT [dbo].[Books] ([idBooks], [codeBooks], [nameBooks], [describe], [Thumb], [updateDay], [quantity], [price], [idBookCat], [idPublisher]) VALUES (6, N'SACH2623BS   ', N'Thanh gươm diệt quỷ Tập 16', N'Vào thời Taisho, có một cậu bé bán than với tấm lòng nhân hậu tên Tanjiro. Những ngày yên bình đã chẳng còn khi Quỷ đến làm hại cả gia đình cậu, chỉ duy nhất người em gái Nezuko còn sống sót nhưng lại bị biến thành Quỷ. Mang trong mình quyết tâm chữa cho em gái, Tanjiro đã cùng Nezuko bắt đầu cuộc hành trình tìm kiếm tung tích con quỷ đã ra tay với gia đình cậu!!Cuộc phiêu lưu trên con đường kiếm sĩ đã bắt đầu!!Cùng tìm đọc bộ truyện tranh Thanh gươm diệt quỷ đã phát hành trọn bộ tại thị trường Việt Nam', N'fea4sah08lou28l3nx6qy6zbi4kqb1wr-20231011211031297.jpeg', CAST(N'2023-10-11' AS Date), 20, 23750, 6, 4)
INSERT [dbo].[Books] ([idBooks], [codeBooks], [nameBooks], [describe], [Thumb], [updateDay], [quantity], [price], [idBookCat], [idPublisher]) VALUES (7, N'SACH2723BS   ', N'Thanh gươm diệt quỷ Tập 23', N'Vào thời Taisho, có một cậu bé bán than với tấm lòng nhân hậu tên Tanjiro. Những ngày yên bình đã chẳng còn khi Quỷ đến làm hại cả gia đình cậu, chỉ duy nhất người em gái Nezuko còn sống sót nhưng lại bị biến thành Quỷ. Mang trong mình quyết tâm chữa cho em gái, Tanjiro đã cùng Nezuko bắt đầu cuộc hành trình tìm kiếm tung tích con quỷ đã ra tay với gia đình cậu!!Cuộc phiêu lưu trên con đường kiếm sĩ đã bắt đầu!!Cùng tìm đọc bộ truyện tranh Thanh gươm diệt quỷ đã phát hành trọn bộ tại thị trường Việt Nam', N'c0teg51o0bae785fn1bni3zfcstb2zoy-20231011211155313.jpeg', CAST(N'2023-10-11' AS Date), 20, 23750, 6, 4)
GO
INSERT [dbo].[Customer] ([idCustomer], [codeCustomer], [nameCustomer], [userName], [passWord], [email], [address], [phone], [birthDay], [creDate]) VALUES (1, N'KH2123BS     ', N'Nguyễn Kim Hoàng', N'kimhoang1', N'kimhoang1', N'hoanggnk.it@gmail.com', N'Lộc Ninh', N'0352931831', CAST(N'2003-03-18' AS Date), CAST(N'2023-10-12T00:04:16.060' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([idOrder], [codeOrder], [checkPay], [deliveryStatus], [orderDate], [deliveryDate], [idCustomer]) VALUES (1, N'Order20231012010302                               ', 0, 1, CAST(N'2023-10-12T01:03:02.777' AS DateTime), CAST(N'2023-10-15T01:03:02.777' AS DateTime), 1)
INSERT [dbo].[Order] ([idOrder], [codeOrder], [checkPay], [deliveryStatus], [orderDate], [deliveryDate], [idCustomer]) VALUES (2, N'Order20231012013359                               ', 0, 1, CAST(N'2023-10-12T01:33:59.717' AS DateTime), CAST(N'2023-10-15T01:33:59.717' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
INSERT [dbo].[OrderDetail] ([idOrder], [idBooks], [quantity], [price]) VALUES (1, 7, 2, CAST(23750.00 AS Decimal(9, 2)))
INSERT [dbo].[OrderDetail] ([idOrder], [idBooks], [quantity], [price]) VALUES (2, 5, 3, CAST(42500.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Publisher] ([idPublisher], [codePublisher], [namePublisher], [address], [Phone]) VALUES (2, N'NXB2223BS    ', N'NXB Mỹ Thuật', NULL, NULL)
INSERT [dbo].[Publisher] ([idPublisher], [codePublisher], [namePublisher], [address], [Phone]) VALUES (3, N'NXB2323BS    ', N'NXB Đông Tây', NULL, NULL)
INSERT [dbo].[Publisher] ([idPublisher], [codePublisher], [namePublisher], [address], [Phone]) VALUES (4, N'NXB2423BS    ', N'NXB Kim Đồng', NULL, NULL)
GO
INSERT [dbo].[Topic] ([idTopic], [codeTopic], [nameTopic]) VALUES (1, N'CÐ2123BS     ', N'Sách thiếu nhi')
INSERT [dbo].[Topic] ([idTopic], [codeTopic], [nameTopic]) VALUES (2, N'CÐ2223BS     ', N'Sách dành cho giới trẻ')
INSERT [dbo].[Topic] ([idTopic], [codeTopic], [nameTopic]) VALUES (3, N'CÐ2323BS     ', N'Tủ sách gia đình')
INSERT [dbo].[Topic] ([idTopic], [codeTopic], [nameTopic]) VALUES (4, N'CÐ2423BS     ', N'Sách khoa học - Công nghệ')
INSERT [dbo].[Topic] ([idTopic], [codeTopic], [nameTopic]) VALUES (5, N'CÐ2523BS     ', N'Sách quản lý - kinh tế')
INSERT [dbo].[Topic] ([idTopic], [codeTopic], [nameTopic]) VALUES (6, N'CÐ2623BS     ', N'Sách chính trị - xã hội')
GO
ALTER TABLE [dbo].[BookCategory]  WITH CHECK ADD  CONSTRAINT [FK_BookCategory_Topic] FOREIGN KEY([idTopic])
REFERENCES [dbo].[Topic] ([idTopic])
GO
ALTER TABLE [dbo].[BookCategory] CHECK CONSTRAINT [FK_BookCategory_Topic]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_BookCategory] FOREIGN KEY([idBookCat])
REFERENCES [dbo].[BookCategory] ([idBookCat])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_BookCategory]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Publisher] FOREIGN KEY([idPublisher])
REFERENCES [dbo].[Publisher] ([idPublisher])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Publisher]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([idCustomer])
REFERENCES [dbo].[Customer] ([idCustomer])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Books] FOREIGN KEY([idBooks])
REFERENCES [dbo].[Books] ([idBooks])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Books]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([idOrder])
REFERENCES [dbo].[Order] ([idOrder])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[WriteBook]  WITH CHECK ADD  CONSTRAINT [FK_WriteBook_Author] FOREIGN KEY([idAuthor])
REFERENCES [dbo].[Author] ([idAuthor])
GO
ALTER TABLE [dbo].[WriteBook] CHECK CONSTRAINT [FK_WriteBook_Author]
GO
ALTER TABLE [dbo].[WriteBook]  WITH CHECK ADD  CONSTRAINT [FK_WriteBook_Books] FOREIGN KEY([idBooks])
REFERENCES [dbo].[Books] ([idBooks])
GO
ALTER TABLE [dbo].[WriteBook] CHECK CONSTRAINT [FK_WriteBook_Books]
GO
