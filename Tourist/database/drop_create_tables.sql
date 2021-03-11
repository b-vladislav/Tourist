USE [bustravel]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Drop tables
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Bus') 
	DROP TABLE [dbo].[Bus]
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'BusService') 
	DROP TABLE [dbo].[BusService]
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Category') 
	DROP TABLE [dbo].[Category]
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Group') 
	DROP TABLE [dbo].[Group]
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Point') 
	DROP TABLE [dbo].[Point]
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Rout') 
	DROP TABLE [dbo].[Rout]
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Service') 
	DROP TABLE [dbo].[Service] 
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Tour') 
	DROP TABLE [dbo].[Tour]

-- Create tables
CREATE TABLE [dbo].[Bus](
	[bus_id] [int] IDENTITY(1,1) NOT NULL,
	[category_id] [int] NOT NULL,
	[number] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Bus_1] PRIMARY KEY CLUSTERED 
(
	[bus_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Bus]  WITH CHECK ADD  CONSTRAINT [Category_Bus] FOREIGN KEY([category_id])
REFERENCES [dbo].[Category] ([category_id])
GO

ALTER TABLE [dbo].[Bus] CHECK CONSTRAINT [Category_Bus]
GO

--__________________________________________________[BusService]__________________________________________________--
CREATE TABLE [dbo].[BusService](
	[bus_id] [int] NOT NULL,
	[service_id] [int] NOT NULL,
 CONSTRAINT [PK_BusService] PRIMARY KEY CLUSTERED 
(
	[bus_id] ASC,
	[service_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BusService]  WITH CHECK ADD  CONSTRAINT [Bus_BusService] FOREIGN KEY([bus_id])
REFERENCES [dbo].[Bus] ([bus_id])
GO
ALTER TABLE [dbo].[BusService] CHECK CONSTRAINT [Bus_BusService]
GO
ALTER TABLE [dbo].[BusService]  WITH CHECK ADD  CONSTRAINT [Service_BusService] FOREIGN KEY([service_id])
REFERENCES [dbo].[Service] ([service_id])
GO
ALTER TABLE [dbo].[BusService] CHECK CONSTRAINT [Service_BusService]
GO

--__________________________________________________[Category]__________________________________________________--
CREATE TABLE [dbo].[Category](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[price] [float] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


--__________________________________________________[Group]__________________________________________________--
CREATE TABLE [dbo].[Group](
	[group_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[group_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


--__________________________________________________[Point]__________________________________________________--
CREATE TABLE [dbo].[Point](
	[point_id] [int] IDENTITY(1,1) NOT NULL,
	[dtstart] [datetime] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[price] [float] NOT NULL,
 CONSTRAINT [PK_Point] PRIMARY KEY CLUSTERED 
(
	[point_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



--__________________________________________________[Rout]__________________________________________________--
CREATE TABLE [dbo].[Rout](
	[rout_id] [int] IDENTITY(1,1) NOT NULL,
	[p_rout_id] [int] NULL,
	[tour_id] [int] NOT NULL,
	[bus_id] [int] NOT NULL,
	[point_start] [int] NOT NULL,
	[point_end] [int] NOT NULL,
	[dtstart] [datetime] NOT NULL,
	[dtend] [datetime] NOT NULL,
 CONSTRAINT [PK_Rout] PRIMARY KEY CLUSTERED 
(
	[rout_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Rout]  WITH CHECK ADD  CONSTRAINT [Bus_Rout] FOREIGN KEY([bus_id])
REFERENCES [dbo].[Bus] ([bus_id])
GO

ALTER TABLE [dbo].[Rout] CHECK CONSTRAINT [Bus_Rout]
GO

ALTER TABLE [dbo].[Rout]  WITH CHECK ADD  CONSTRAINT [Point_Rout_End] FOREIGN KEY([point_end])
REFERENCES [dbo].[Point] ([point_id])
GO

ALTER TABLE [dbo].[Rout] CHECK CONSTRAINT [Point_Rout_End]
GO

ALTER TABLE [dbo].[Rout]  WITH CHECK ADD  CONSTRAINT [Point_Rout_Start] FOREIGN KEY([point_start])
REFERENCES [dbo].[Point] ([point_id])
GO

ALTER TABLE [dbo].[Rout] CHECK CONSTRAINT [Point_Rout_Start]
GO

ALTER TABLE [dbo].[Rout]  WITH CHECK ADD  CONSTRAINT [Tour_Rout] FOREIGN KEY([tour_id])
REFERENCES [dbo].[Tour] ([tour_id])
GO

ALTER TABLE [dbo].[Rout] CHECK CONSTRAINT [Tour_Rout]
GO


--__________________________________________________[Service]__________________________________________________--
CREATE TABLE [dbo].[Service](
	[service_id] [int] IDENTITY(1,1) NOT NULL,
	[serveice_type] [varchar](50) NOT NULL,
	[min_time] [int] NOT NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[service_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


--__________________________________________________[Tour]__________________________________________________--
CREATE TABLE [dbo].[Tour](
	[tour_id] [int] IDENTITY(1,1) NOT NULL,
	[group_id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[dtstart] [datetime] NOT NULL,
 CONSTRAINT [PK_Tour] PRIMARY KEY CLUSTERED 
(
	[tour_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tour]  WITH CHECK ADD  CONSTRAINT [Tour_Group] FOREIGN KEY([group_id])
REFERENCES [dbo].[Group] ([group_id])
GO

ALTER TABLE [dbo].[Tour] CHECK CONSTRAINT [Tour_Group]
GO









