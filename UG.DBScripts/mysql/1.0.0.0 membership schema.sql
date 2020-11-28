SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

DROP SCHEMA IF EXISTS `smartcity`;
CREATE SCHEMA IF NOT EXISTS `smartcity` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
USE `smartcity`;

DROP TABLE IF EXISTS `User`;
CREATE TABLE IF NOT EXISTS `User` (
  `UserId` INT NOT NULL AUTO_INCREMENT,
  `Guid` varchar(128) NOT NULL, 
  `UserName` varchar(256) NOT NULL,
  `Email` VARCHAR(256) DEFAULT NULL,
  `LastName` VARCHAR(150) NULL,
  `FirstName` VARCHAR(150) NULL,
  `RegistrationDate` DATETIME NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEndDateUtc` datetime DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  KEY `UserId` (`UserId`),
  PRIMARY KEY (`UserId`),
  UNIQUE INDEX `UserId_UNIQUE` (`UserId` ASC))
ENGINE = InnoDB;

DROP TABLE IF EXISTS `Role`;
CREATE TABLE IF NOT EXISTS `Role` (
  `Id` CHAR(36) NOT NULL,
  `Name` varchar(256) NOT NULL,
  PRIMARY KEY (`Id`)
);

DROP TABLE IF EXISTS `UserClaim`;
CREATE TABLE IF NOT EXISTS `UserClaim` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` INT NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id` (`Id`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `ApplicationUser_Claims` 
	FOREIGN KEY (`UserId`) REFERENCES `User` (`UserId`) ON DELETE CASCADE ON UPDATE NO ACTION
);

DROP TABLE IF EXISTS `UserLogin`;
CREATE TABLE IF NOT EXISTS `UserLogin` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `UserId` INT NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`,`UserId`),
  KEY `User_Logins` (`UserId`),
	CONSTRAINT `User_Logins` FOREIGN KEY (`UserId`) REFERENCES `User` (`UserId`) ON DELETE CASCADE ON UPDATE NO ACTION
);

DROP TABLE IF EXISTS `UserRole`;
CREATE TABLE IF NOT EXISTS `UserRole` (
  `UserId` INT NOT NULL,
  `RoleId` CHAR(36) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IdentityRole_Users` (`RoleId`),
  CONSTRAINT `ApplicationUser_Roles` FOREIGN KEY (`UserId`) REFERENCES `User` (`UserId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `IdentityRole_Users` FOREIGN KEY (`RoleId`) REFERENCES `Role` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;