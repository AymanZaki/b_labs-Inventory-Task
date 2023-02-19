CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Categories` (
    `CategoryId` int NOT NULL AUTO_INCREMENT,
    `CategoryKey` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `IsActive` tinyint(1) NOT NULL,
    `CreatedOn` datetime(6) NOT NULL,
    `ModifiedOn` datetime(6) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `ModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Categories` PRIMARY KEY (`CategoryId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Languages` (
    `LanguageId` int NOT NULL AUTO_INCREMENT,
    `LanguageName` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `IsActive` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Languages` PRIMARY KEY (`LanguageId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductStatuses` (
    `ProductStatusId` int NOT NULL AUTO_INCREMENT,
    `ProductStatusName` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_ProductStatuses` PRIMARY KEY (`ProductStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Users` (
    `UserId` int NOT NULL AUTO_INCREMENT,
    `UserKey` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FullName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`UserId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserSearchHistories` (
    `UserSearchHistoryId` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `SearchKeyword` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedOn` datetime(6) NOT NULL,
    `ModifiedOn` datetime(6) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `ModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_UserSearchHistories` PRIMARY KEY (`UserSearchHistoryId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CategoryDetails` (
    `CategoryDetailsId` int NOT NULL AUTO_INCREMENT,
    `CategoryId` int NOT NULL,
    `LanguageId` int NOT NULL,
    `CateogryName` varchar(256) CHARACTER SET utf8mb4 NOT NULL,
    `CateogryUrl` varchar(256) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_CategoryDetails` PRIMARY KEY (`CategoryDetailsId`),
    CONSTRAINT `FK_CategoryDetails_Categories_CategoryId` FOREIGN KEY (`CategoryId`) REFERENCES `Categories` (`CategoryId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Products` (
    `ProductId` int NOT NULL AUTO_INCREMENT,
    `ProductKey` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CategoryId` int NOT NULL,
    `ProductStatusId` int NOT NULL,
    `Price` double NOT NULL,
    `ImageUrl` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedOn` datetime(6) NOT NULL,
    `ModifiedOn` datetime(6) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `ModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Products` PRIMARY KEY (`ProductId`),
    CONSTRAINT `FK_Products_Categories_CategoryId` FOREIGN KEY (`CategoryId`) REFERENCES `Categories` (`CategoryId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Products_ProductStatuses_ProductStatusId` FOREIGN KEY (`ProductStatusId`) REFERENCES `ProductStatuses` (`ProductStatusId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductDetails` (
    `ProductDetailsId` int NOT NULL AUTO_INCREMENT,
    `ProductId` int NOT NULL,
    `LanguageId` int NOT NULL,
    `ProductName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ProductUrl` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_ProductDetails` PRIMARY KEY (`ProductDetailsId`),
    CONSTRAINT `FK_ProductDetails_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `Products` (`ProductId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductRatings` (
    `ProductRatingId` int NOT NULL AUTO_INCREMENT,
    `ProductId` int NOT NULL,
    `ProductRatingCount` int NOT NULL,
    `ProductRatings` int NOT NULL,
    `CreatedOn` datetime(6) NOT NULL,
    `ModifiedOn` datetime(6) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `ModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_ProductRatings` PRIMARY KEY (`ProductRatingId`),
    CONSTRAINT `FK_ProductRatings_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `Products` (`ProductId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_CategoryDetails_CategoryId` ON `CategoryDetails` (`CategoryId`);

CREATE INDEX `IX_ProductDetails_ProductId` ON `ProductDetails` (`ProductId`);

CREATE UNIQUE INDEX `IX_ProductRatings_ProductId` ON `ProductRatings` (`ProductId`);

CREATE INDEX `IX_Products_CategoryId` ON `Products` (`CategoryId`);

CREATE INDEX `IX_Products_ProductStatusId` ON `Products` (`ProductStatusId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230219061958_Init', '7.0.3');

COMMIT;

