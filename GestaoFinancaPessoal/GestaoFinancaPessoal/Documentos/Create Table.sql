IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Categoria] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(256) NOT NULL,
    [HierarquiaId] int NULL,
    [IsSuspenco] bit NOT NULL,
    CONSTRAINT [PK_Categoria] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Categoria_Categoria_HierarquiaId] FOREIGN KEY ([HierarquiaId]) REFERENCES [Categoria] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Conta] (
    [Id] int NOT NULL IDENTITY,
    [Descricao] nvarchar(256) NULL,
    [Nome] nvarchar(256) NOT NULL,
    [Saldo] float NOT NULL,
    [Tipo] nvarchar(256) NOT NULL,
    [Banco] nvarchar(max) NULL,
    [DataAtualizacao] datetime2 NOT NULL,
    [IsSuspensa] bit NOT NULL,
    CONSTRAINT [PK_Conta] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Contact] (
    [ContactId] int NOT NULL IDENTITY,
    [OwnerID] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    [City] nvarchar(max) NULL,
    [State] nvarchar(max) NULL,
    [Zip] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY ([ContactId])
);

GO

CREATE TABLE [Recorrente] (
    [Id] int NOT NULL IDENTITY,
    [Quantidade] int NOT NULL,
    [Periodo] int NOT NULL,
    [ParcelaInicial] decimal(18,2) NOT NULL,
    [ParcelaTotal] decimal(18,2) NOT NULL,
    [Valor] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Recorrente] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Lancamento] (
    [Id] int NOT NULL IDENTITY,
    [ContaId] int NOT NULL,
    [Valor] decimal(18,2) NOT NULL,
    [ValorPago] decimal(18,2) NOT NULL,
    [Descricao] nvarchar(256) NOT NULL,
    [IsPago] bit NOT NULL,
    [IsAutomatico] bit NOT NULL,
    [DataPagamento] datetime2 NOT NULL,
    [DataVencimento] datetime2 NOT NULL,
    [CategoriaId] int NOT NULL,
    [Tipo] nvarchar(max) NOT NULL,
    [RecorrenteId] int NULL,
    [DataInclusao] datetime2 NOT NULL,
    [ContaDestionId] int NULL,
    CONSTRAINT [PK_Lancamento] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Lancamento_Categoria_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categoria] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Lancamento_Conta_ContaDestionId] FOREIGN KEY ([ContaDestionId]) REFERENCES [Conta] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Lancamento_Conta_ContaId] FOREIGN KEY ([ContaId]) REFERENCES [Conta] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Lancamento_Recorrente_RecorrenteId] FOREIGN KEY ([RecorrenteId]) REFERENCES [Recorrente] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

CREATE INDEX [IX_Categoria_HierarquiaId] ON [Categoria] ([HierarquiaId]);

GO

CREATE INDEX [IX_Lancamento_CategoriaId] ON [Lancamento] ([CategoriaId]);

GO

CREATE INDEX [IX_Lancamento_ContaDestionId] ON [Lancamento] ([ContaDestionId]);

GO

CREATE INDEX [IX_Lancamento_ContaId] ON [Lancamento] ([ContaId]);

GO

CREATE INDEX [IX_Lancamento_RecorrenteId] ON [Lancamento] ([RecorrenteId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190408153614_primeiraEntrega', N'2.2.3-servicing-35854');

GO

