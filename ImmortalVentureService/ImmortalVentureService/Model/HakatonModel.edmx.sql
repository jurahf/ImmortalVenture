
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/15/2019 15:29:06
-- Generated from EDMX file: D:\PROJECTS\CSharp\ImmortalVenture\ImmortalVentureService\ImmortalVentureService\Model\HakatonModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ImmortalVentureDatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ПользовательSet'
CREATE TABLE [dbo].[ПользовательSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ФИО] nvarchar(max)  NOT NULL,
    [Логин] nvarchar(max)  NOT NULL,
    [ХэшПароля] nvarchar(max)  NOT NULL,
    [ЭП] nvarchar(max)  NULL
);
GO

-- Creating table 'МедосмотрСВрачомSet'
CREATE TABLE [dbo].[МедосмотрСВрачомSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Дата] datetime  NOT NULL,
    [Жалобы] nvarchar(max)  NOT NULL,
    [ВизуальныйОсмотр] nvarchar(max)  NOT NULL,
    [Температура] decimal(18,0)  NOT NULL,
    [ДавлениеВерхнее] int  NOT NULL,
    [ДавлениеНижнее] int  NOT NULL,
    [Пульс] int  NOT NULL,
    [ОпьянениеПромилле] decimal(18,0)  NOT NULL,
    [Заключение] int  NOT NULL,
    [Комментарий] nvarchar(max)  NOT NULL,
    [Водитель_Id] int  NOT NULL,
    [Врач_Id] int  NOT NULL
);
GO

-- Creating table 'МедосмотрАвтоматическийSet'
CREATE TABLE [dbo].[МедосмотрАвтоматическийSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Дата] datetime  NOT NULL,
    [Температура] decimal(18,0)  NULL,
    [ДавлениеВерхнее] int  NULL,
    [ДавлениеНижнее] int  NULL,
    [Пульс] int  NULL,
    [ОпьянениеПромилле] decimal(18,0)  NULL,
    [Заключение] int  NOT NULL,
    [Водитель_Id] int  NOT NULL
);
GO

-- Creating table 'ВодительSet'
CREATE TABLE [dbo].[ВодительSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ДатаРождения] datetime  NOT NULL,
    [Пол] int  NOT NULL,
    [ВнешнийХэш] nvarchar(max)  NOT NULL,
    [Пользователь_Id] int  NOT NULL
);
GO

-- Creating table 'ВрачSet'
CREATE TABLE [dbo].[ВрачSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ВнешнийХэш] nvarchar(max)  NOT NULL,
    [Пользователь_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ПользовательSet'
ALTER TABLE [dbo].[ПользовательSet]
ADD CONSTRAINT [PK_ПользовательSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'МедосмотрСВрачомSet'
ALTER TABLE [dbo].[МедосмотрСВрачомSet]
ADD CONSTRAINT [PK_МедосмотрСВрачомSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'МедосмотрАвтоматическийSet'
ALTER TABLE [dbo].[МедосмотрАвтоматическийSet]
ADD CONSTRAINT [PK_МедосмотрАвтоматическийSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ВодительSet'
ALTER TABLE [dbo].[ВодительSet]
ADD CONSTRAINT [PK_ВодительSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ВрачSet'
ALTER TABLE [dbo].[ВрачSet]
ADD CONSTRAINT [PK_ВрачSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Пользователь_Id] in table 'ВрачSet'
ALTER TABLE [dbo].[ВрачSet]
ADD CONSTRAINT [FK_ПользовательВрач]
    FOREIGN KEY ([Пользователь_Id])
    REFERENCES [dbo].[ПользовательSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ПользовательВрач'
CREATE INDEX [IX_FK_ПользовательВрач]
ON [dbo].[ВрачSet]
    ([Пользователь_Id]);
GO

-- Creating foreign key on [Пользователь_Id] in table 'ВодительSet'
ALTER TABLE [dbo].[ВодительSet]
ADD CONSTRAINT [FK_ПользовательВодитель]
    FOREIGN KEY ([Пользователь_Id])
    REFERENCES [dbo].[ПользовательSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ПользовательВодитель'
CREATE INDEX [IX_FK_ПользовательВодитель]
ON [dbo].[ВодительSet]
    ([Пользователь_Id]);
GO

-- Creating foreign key on [Водитель_Id] in table 'МедосмотрАвтоматическийSet'
ALTER TABLE [dbo].[МедосмотрАвтоматическийSet]
ADD CONSTRAINT [FK_ВодительМедосмотрАвтоматический]
    FOREIGN KEY ([Водитель_Id])
    REFERENCES [dbo].[ВодительSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ВодительМедосмотрАвтоматический'
CREATE INDEX [IX_FK_ВодительМедосмотрАвтоматический]
ON [dbo].[МедосмотрАвтоматическийSet]
    ([Водитель_Id]);
GO

-- Creating foreign key on [Водитель_Id] in table 'МедосмотрСВрачомSet'
ALTER TABLE [dbo].[МедосмотрСВрачомSet]
ADD CONSTRAINT [FK_ВодительМедосмотрСВрачом]
    FOREIGN KEY ([Водитель_Id])
    REFERENCES [dbo].[ВодительSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ВодительМедосмотрСВрачом'
CREATE INDEX [IX_FK_ВодительМедосмотрСВрачом]
ON [dbo].[МедосмотрСВрачомSet]
    ([Водитель_Id]);
GO

-- Creating foreign key on [Врач_Id] in table 'МедосмотрСВрачомSet'
ALTER TABLE [dbo].[МедосмотрСВрачомSet]
ADD CONSTRAINT [FK_ВрачМедосмотрСВрачом]
    FOREIGN KEY ([Врач_Id])
    REFERENCES [dbo].[ВрачSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ВрачМедосмотрСВрачом'
CREATE INDEX [IX_FK_ВрачМедосмотрСВрачом]
ON [dbo].[МедосмотрСВрачомSet]
    ([Врач_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------