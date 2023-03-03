# digitalia-challenge_back

Proyecto backend del reto digitalia

##Script para tablas en sql server

USE [digitalia_challenge]
GO

/****** Object:  Table [dbo].[recibo]    Script Date: 03/03/2023 9:00:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[recibo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[usuarioId] [int] NOT NULL,
	[moneda] [varchar](20) NULL,
	[monto] [decimal](10, 2) NULL,
	[titulo] [varchar](150) NULL,
	[descripcion] [varchar](500) NULL,
	[direccion] [varchar](200) NULL,
	[nombres] [varchar](200) NULL,
	[apellidos] [varchar](200) NULL,
	[tipoDoc] [varchar](100) NULL,
	[numDoc] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [digitalia_challenge]
GO

/****** Object:  Table [dbo].[usuario]    Script Date: 03/03/2023 9:00:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombres] [varchar](200) NULL,
	[apellidos] [varchar](250) NULL,
	[email] [varchar](200) NULL,
	[password] [varchar](500) NULL,
	[logoMarca] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



