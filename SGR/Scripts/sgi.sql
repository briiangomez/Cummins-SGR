USE [master]
GO
/****** Object:  Database [SGI]    Script Date: 30/8/2020 21:39:14 ******/
CREATE DATABASE [SGI]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SGR', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\SGR.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SGR_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\SGR_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [SGI] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SGI].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SGI] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SGI] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SGI] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SGI] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SGI] SET ARITHABORT OFF 
GO
ALTER DATABASE [SGI] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SGI] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SGI] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SGI] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SGI] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SGI] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SGI] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SGI] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SGI] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SGI] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SGI] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SGI] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SGI] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SGI] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SGI] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SGI] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SGI] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SGI] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SGI] SET  MULTI_USER 
GO
ALTER DATABASE [SGI] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SGI] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SGI] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SGI] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SGI] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SGI] SET QUERY_STORE = OFF
GO
USE [SGI]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 30/8/2020 21:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[IdCliente] [uniqueidentifier] NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Telefono] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Direccion] [varchar](100) NULL,
	[NombreContacto] [varchar](100) NULL,
	[TelContacto] [varchar](50) NULL,
	[EmailContacto] [varchar](50) NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Concesionario]    Script Date: 30/8/2020 21:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Concesionario](
	[IdConcesionario] [uniqueidentifier] NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Direccion] [varchar](250) NULL,
	[latitudGps] [bigint] NULL,
	[longitudGps] [bigint] NULL,
 CONSTRAINT [PK_Concesionario] PRIMARY KEY CLUSTERED 
(
	[IdConcesionario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estado]    Script Date: 30/8/2020 21:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estado](
	[IdEstado] [uniqueidentifier] NOT NULL,
	[IdIncidencia] [uniqueidentifier] NULL,
	[Estado] [int] NULL,
	[Fecha] [datetime] NULL,
	[Usuario] [varchar](100) NULL,
 CONSTRAINT [PK_Estado] PRIMARY KEY CLUSTERED 
(
	[IdEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Incidencia]    Script Date: 30/8/2020 21:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Incidencia](
	[IdIncidencia] [uniqueidentifier] NOT NULL,
	[IdCliente] [uniqueidentifier] NULL,
	[IdConcesionario] [uniqueidentifier] NULL,
	[FechaIncidencia] [datetime] NULL,
	[FechaRegistro] [datetime] NULL,
	[FechaCierre] [datetime] NULL,
	[NroReclamoConcesionario] [int] NULL,
	[NroReclamoCummins] [int] NULL,
	[Descripcion] [text] NULL,
	[DireccionInspeccion] [varchar](250) NULL,
	[latitudGps] [bigint] NULL,
	[longitudGps] [bigint] NULL,
	[PathImagenes] [text] NULL,
	[Estado] [int] NULL,
	[Aux1] [varchar](100) NULL,
	[Aux2] [varchar](100) NULL,
	[Aux3] [varchar](100) NULL,
	[Aux4] [varchar](100) NULL,
	[Aux5] [varchar](100) NULL,
 CONSTRAINT [PK_Reclamo] PRIMARY KEY CLUSTERED 
(
	[IdIncidencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motor]    Script Date: 30/8/2020 21:39:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motor](
	[IdMotor] [uniqueidentifier] NOT NULL,
	[IdCliente] [uniqueidentifier] NULL,
	[Numero] [varchar](50) NULL,
	[Modelo] [varchar](50) NULL,
	[HsKm] [int] NULL,
	[FechaCompra] [date] NULL,
	[FechaInicioGarantia] [date] NULL,
	[FechaFalla] [date] NULL,
	[Equipo] [varchar](100) NULL,
	[ModeloEquipo] [varchar](100) NULL,
 CONSTRAINT [PK_Motor] PRIMARY KEY CLUSTERED 
(
	[IdMotor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cliente] ADD  CONSTRAINT [DF_Cliente_IdCliente]  DEFAULT (newsequentialid()) FOR [IdCliente]
GO
ALTER TABLE [dbo].[Concesionario] ADD  CONSTRAINT [DF_Concesionario_IdConcesionario]  DEFAULT (newsequentialid()) FOR [IdConcesionario]
GO
ALTER TABLE [dbo].[Estado] ADD  CONSTRAINT [DF_Estado_IdEstado]  DEFAULT (newsequentialid()) FOR [IdEstado]
GO
ALTER TABLE [dbo].[Incidencia] ADD  CONSTRAINT [DF_Reclamo_IdReclamo]  DEFAULT (newsequentialid()) FOR [IdIncidencia]
GO
ALTER TABLE [dbo].[Motor] ADD  CONSTRAINT [DF_Motor_IdMotor]  DEFAULT (newsequentialid()) FOR [IdMotor]
GO
ALTER TABLE [dbo].[Estado]  WITH CHECK ADD  CONSTRAINT [FK_Estado_Incidencia] FOREIGN KEY([IdIncidencia])
REFERENCES [dbo].[Incidencia] ([IdIncidencia])
GO
ALTER TABLE [dbo].[Estado] CHECK CONSTRAINT [FK_Estado_Incidencia]
GO
ALTER TABLE [dbo].[Incidencia]  WITH CHECK ADD  CONSTRAINT [FK_Incidencia_Cliente] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Cliente] ([IdCliente])
GO
ALTER TABLE [dbo].[Incidencia] CHECK CONSTRAINT [FK_Incidencia_Cliente]
GO
ALTER TABLE [dbo].[Incidencia]  WITH CHECK ADD  CONSTRAINT [FK_Incidencia_Concesionario] FOREIGN KEY([IdConcesionario])
REFERENCES [dbo].[Concesionario] ([IdConcesionario])
GO
ALTER TABLE [dbo].[Incidencia] CHECK CONSTRAINT [FK_Incidencia_Concesionario]
GO
ALTER TABLE [dbo].[Motor]  WITH CHECK ADD  CONSTRAINT [FK_Motor_Cliente] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Cliente] ([IdCliente])
GO
ALTER TABLE [dbo].[Motor] CHECK CONSTRAINT [FK_Motor_Cliente]
GO
USE [master]
GO
ALTER DATABASE [SGI] SET  READ_WRITE 
GO
