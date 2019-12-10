USE [TesteMD]
GO
/****** Object:  Table [dbo].[Artigos]    Script Date: 06/12/2019 16:40:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artigos](
	[nId] [int] IDENTITY(1,1) NOT NULL,
	[cCode] [nvarchar](30) NOT NULL,
	[cDescription] [nvarchar](150) NULL,
	[cUnitCode] [nvarchar](10) NOT NULL,
	[nUnitPrice] [decimal](20, 8) NOT NULL,
	[dCreateDateTime] [datetime2](7) NOT NULL,
	[dChangedDateTime] [datetime2](7) NULL,
	[uId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Artigos] PRIMARY KEY CLUSTERED 
(
	[nId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Unidades]    Script Date: 06/12/2019 16:40:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unidades](
	[nId] [int] IDENTITY(1,1) NOT NULL,
	[cCode] [nvarchar](10) NOT NULL,
	[cDescription] [nvarchar](30) NULL,
	[dCreateDateTime] [datetime2](7) NOT NULL,
	[dChangedDateTime] [datetime2](7) NULL,
	[uId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Unidades] PRIMARY KEY CLUSTERED 
(
	[nId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UnidadesArtigos]    Script Date: 06/12/2019 16:40:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnidadesArtigos](
	[nId] [int] IDENTITY(1,1) NOT NULL,
	[nBaseUnitQty] [decimal](20, 10) NOT NULL,
	[nPrice] [decimal](20, 8) NOT NULL,
	[dCreateDateTime] [datetime2](7) NOT NULL,
	[dChangedDateTime] [datetime2](7) NULL,
	[uId] [uniqueidentifier] NOT NULL,
	[uProductId] [int] NOT NULL,
	[uUnidadeId] [int] NOT NULL,
 CONSTRAINT [PK_UnidadesArtigos] PRIMARY KEY CLUSTERED 
(
	[nId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UnidadesArtigos]  WITH CHECK ADD  CONSTRAINT [FK_UnidadesArtigos_Artigos_nId] FOREIGN KEY([uProductId])
REFERENCES [dbo].[Artigos] ([nId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UnidadesArtigos] CHECK CONSTRAINT [FK_UnidadesArtigos_Artigos_nId]
GO
ALTER TABLE [dbo].[UnidadesArtigos]  WITH CHECK ADD  CONSTRAINT [FK_UnidadesArtigos_Unidades_nId] FOREIGN KEY([uUnidadeId])
REFERENCES [dbo].[Unidades] ([nId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UnidadesArtigos] CHECK CONSTRAINT [FK_UnidadesArtigos_Unidades_nId]
GO
