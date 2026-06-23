IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Categories] (
    [CategoryId] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId])
);

CREATE TABLE [Flights] (
    [FlightId] int NOT NULL IDENTITY,
    [FlightNumber] nvarchar(20) NOT NULL,
    [DepartureAirport] nvarchar(100) NOT NULL,
    [ArrivalAirport] nvarchar(100) NOT NULL,
    [DepartureTimeUtc] datetime2 NOT NULL,
    [ArrivalTimeUtc] datetime2 NOT NULL,
    [Terminal] nvarchar(10) NULL,
    [Gate] nvarchar(10) NULL,
    [FlightStatus] nvarchar(max) NOT NULL,
    [DelayMinutes] int NOT NULL,
    [AirlineName] nvarchar(max) NOT NULL,
    [AirlineIcaoCode] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Flights] PRIMARY KEY ([FlightId])
);

CREATE TABLE [Passengers] (
    [PassengerId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(100) NOT NULL,
    [LastName] nvarchar(100) NOT NULL,
    [PassportNumber] nvarchar(50) NOT NULL,
    [Nationality] nvarchar(100) NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [PassportExpiryDate] datetime2 NOT NULL,
    [PassengerRole] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Passengers] PRIMARY KEY ([PassengerId])
);

CREATE TABLE [SubCategories] (
    [SubCategoryId] int NOT NULL IDENTITY,
    [CategoryId] int NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_SubCategories] PRIMARY KEY ([SubCategoryId]),
    CONSTRAINT [FK_SubCategories_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE CASCADE
);

CREATE TABLE [Tickets] (
    [TicketId] int NOT NULL IDENTITY,
    [TicketNumber] nvarchar(50) NOT NULL,
    [FlightId] int NOT NULL,
    [PassengerId] int NOT NULL,
    [SeatNumber] nvarchar(10) NOT NULL,
    [TravelClass] nvarchar(max) NOT NULL,
    [BoardingTimeUtc] datetime2 NULL,
    [Terminal] nvarchar(10) NULL,
    [Gate] nvarchar(10) NULL,
    [BarcodeData] nvarchar(500) NULL,
    [BoardingStatus] nvarchar(max) NOT NULL,
    [IssuedAtUtc] datetime2 NOT NULL,
    CONSTRAINT [PK_Tickets] PRIMARY KEY ([TicketId]),
    CONSTRAINT [FK_Tickets_Flights_FlightId] FOREIGN KEY ([FlightId]) REFERENCES [Flights] ([FlightId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Tickets_Passengers_PassengerId] FOREIGN KEY ([PassengerId]) REFERENCES [Passengers] ([PassengerId]) ON DELETE NO ACTION
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

CREATE TABLE [BaggageTags] (
    [BaggageTagId] int NOT NULL IDENTITY,
    [TagNumber] nvarchar(50) NOT NULL,
    [TicketId] int NOT NULL,
    [WeightKg] decimal(5,2) NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_BaggageTags] PRIMARY KEY ([BaggageTagId]),
    CONSTRAINT [FK_BaggageTags_Tickets_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Tickets] ([TicketId]) ON DELETE CASCADE
);

CREATE UNIQUE INDEX [IX_BaggageTags_TagNumber] ON [BaggageTags] ([TagNumber]);

CREATE INDEX [IX_BaggageTags_TicketId] ON [BaggageTags] ([TicketId]);

CREATE UNIQUE INDEX [IX_Categories_Name] ON [Categories] ([Name]);

CREATE UNIQUE INDEX [IX_Flights_FlightNumber] ON [Flights] ([FlightNumber]);

CREATE UNIQUE INDEX [IX_Passengers_PassportNumber] ON [Passengers] ([PassportNumber]);

CREATE INDEX [IX_Products_Name] ON [Products] ([Name]);

CREATE INDEX [IX_Products_SubCategoryId] ON [Products] ([SubCategoryId]);

CREATE INDEX [IX_SubCategories_CategoryId] ON [SubCategories] ([CategoryId]);

CREATE INDEX [IX_Tickets_FlightId_PassengerId] ON [Tickets] ([FlightId], [PassengerId]);

CREATE INDEX [IX_Tickets_PassengerId] ON [Tickets] ([PassengerId]);

CREATE UNIQUE INDEX [IX_Tickets_TicketNumber] ON [Tickets] ([TicketNumber]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260209201456_RemoveArabicNames', N'9.0.0');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260209201656_BackedMigration', N'9.0.0');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260209210758_AddCompleteCustomsSystem', N'9.0.0');

ALTER TABLE [BaggageTags] ADD [CurrentLocation] nvarchar(50) NULL;

ALTER TABLE [BaggageTags] ADD [LastLocationUpdatedAt] datetime2 NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260221190336_AddLocationToBaggageTag', N'9.0.0');

ALTER TABLE [BaggageTags] ADD [PreviousLocation] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260302191231_AddPreviousLocationToBaggageTag', N'9.0.0');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260308133739_AddFlightDateValidation', N'9.0.0');

COMMIT;
GO

