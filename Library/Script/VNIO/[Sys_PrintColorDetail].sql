USE [LAB_VNIO_MIX]
GO
/****** Object:  Table [dbo].[Sys_PrintColorDetail]    Script Date: 02/17/2012 08:42:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_PrintColorDetail](
	[ID] [int] NOT NULL,
	[TestType_ID] [int] NOT NULL,
 CONSTRAINT [PK_Sys_PrintColorDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[TestType_ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
