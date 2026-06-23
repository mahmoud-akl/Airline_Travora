BEGIN TRANSACTION;

CREATE TABLE [Categories] (
    [CategoryId] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId])
);

CREATE TABLE [SubCategories] (
    [SubCategoryId] int NOT NULL IDENTITY,
    [CategoryId] int NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_SubCategories] PRIMARY KEY ([SubCategoryId]),
    CONSTRAINT [FK_SubCategories_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE CASCADE
);

CREATE TABLE [Products] (
    [ProductId] int NOT NULL IDENTITY,
    [SubCategoryId] int NOT NULL,
    [Name] nvarchar(200) NOT NULL,
    [CustomsRate] decimal(5,4) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId]),
    CONSTRAINT [FK_Products_SubCategories_SubCategoryId] FOREIGN KEY ([SubCategoryId]) REFERENCES [SubCategories] ([SubCategoryId]) ON DELETE CASCADE
);

CREATE UNIQUE INDEX [IX_Categories_Name] ON [Categories] ([Name]);
CREATE INDEX [IX_Products_Name] ON [Products] ([Name]);
CREATE INDEX [IX_Products_SubCategoryId] ON [Products] ([SubCategoryId]);
CREATE INDEX [IX_SubCategories_CategoryId] ON [SubCategories] ([CategoryId]);

COMMIT;
GO
