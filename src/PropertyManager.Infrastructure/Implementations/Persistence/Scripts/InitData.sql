-- =========================================
-- Crear la base de datos si no existe
-- =========================================
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'PropertyManager')
BEGIN
    CREATE DATABASE [PropertyManager];
END
GO

USE [PropertyManager];
GO

-- =========================================
-- Tabla: Owner
-- =========================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Owner]') AND type = 'U')
BEGIN
    CREATE TABLE [dbo].[Owner] (
        [IdOwner]       INT IDENTITY(1,1) NOT NULL,
        [Name]          NVARCHAR(200) NOT NULL,
        [Address]       NVARCHAR(300) NULL,
        [Photo]         NVARCHAR(500) NULL,
        [Birthday]      DATE NULL,
        [UserName]      NVARCHAR(100) NOT NULL,
        [Email]         NVARCHAR(200) NOT NULL,
        [PasswordHash]  NVARCHAR(500) NOT NULL,
        [PasswordSalt]  NVARCHAR(500) NULL,
        CONSTRAINT [PK_Owner] PRIMARY KEY CLUSTERED ([IdOwner] ASC)
    );

    CREATE UNIQUE INDEX [IX_Owner_UserName] ON [dbo].[Owner]([UserName]);
    CREATE UNIQUE INDEX [IX_Owner_Email] ON [dbo].[Owner]([Email]);
END
GO

-- =========================================
-- Tabla: Property
-- =========================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Property]') AND type = 'U')
BEGIN
    CREATE TABLE [dbo].[Property] (
        [IdProperty]   INT IDENTITY(1,1) NOT NULL,
        [Name]         NVARCHAR(200) NOT NULL,
        [Address]      NVARCHAR(300) NULL,
        [Price]        DECIMAL(18,2) NULL,
        [CodeInternal] NVARCHAR(50) NOT NULL,
        [CreatedAt]    DATETIME NOT NULL DEFAULT GETDATE(),
        [UpdatedAt]    DATETIME NOT NULL DEFAULT GETDATE(),
        [Year]         INT NOT NULL,
        [IdOwner]      INT NOT NULL,
        CONSTRAINT [PK_Property] PRIMARY KEY CLUSTERED ([IdProperty] ASC),
        CONSTRAINT [FK_Property_Owner] FOREIGN KEY ([IdOwner]) REFERENCES [dbo].[Owner]([IdOwner])
    );

    CREATE UNIQUE INDEX [IX_Property_CodeInternal] ON [dbo].[Property]([CodeInternal]);
END
GO

-- =========================================
-- Tabla: PropertyImage
-- =========================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PropertyImage]') AND type = 'U')
BEGIN
    CREATE TABLE [dbo].[PropertyImage] (
        [IdPropertyImage] INT IDENTITY(1,1) NOT NULL,
        [ImageUrl]        NVARCHAR(MAX) NOT NULL,
        [Enabled]         BIT NOT NULL,
        [CreatedAt]       DATETIME NULL,
        [IdProperty]      INT NOT NULL,
        CONSTRAINT [PK_PropertyImage] PRIMARY KEY CLUSTERED ([IdPropertyImage] ASC),
        CONSTRAINT [FK_PropertyImage_Property] FOREIGN KEY ([IdProperty]) REFERENCES [dbo].[Property]([IdProperty])
    );
END
GO

-- =========================================
-- Tabla: PropertyTrace
-- =========================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PropertyTrace]') AND type = 'U')
BEGIN
    CREATE TABLE [dbo].[PropertyTrace] (
        [IdPropertyTrace] INT IDENTITY(1,1) NOT NULL,
        [DateSale]        DATE NOT NULL,
        [Name]            NVARCHAR(200) NOT NULL,
        [Value]           DECIMAL(18,2) NOT NULL,
        [Tax]             DECIMAL(18,2) NOT NULL,
        [IdProperty]      INT NOT NULL,
        CONSTRAINT [PK_PropertyTrace] PRIMARY KEY CLUSTERED ([IdPropertyTrace] ASC),
        CONSTRAINT [FK_PropertyTrace_Property] FOREIGN KEY ([IdProperty]) REFERENCES [dbo].[Property]([IdProperty]) ON DELETE CASCADE
    );
END
GO
