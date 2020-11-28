SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

USE `smartcity`;

DROP TABLE IF EXISTS `SubIndex`;
CREATE TABLE `SubIndex` (
  `SubIndexId` int NOT NULL AUTO_INCREMENT,
  `Name` text NOT NUll,
  PRIMARY KEY (`SubIndexId`),
  UNIQUE INDEX `SubIndexId` (`SubIndexId` ASC)
)ENGINE = InnoDB;

DROP TABLE IF EXISTS `Indicator`;
CREATE TABLE `Indicator` (
  `IndicatorId` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(1024) NOT NULL,
  `Number` int NOT NULL,
  `CalculationDescription` varchar(2048) NULL DEFAULT NULL,
  `SubIndexId` int NOT NULL,
  PRIMARY KEY (`IndicatorId`),
  UNIQUE INDEX `IndicatorId` (`IndicatorId` ASC),
  KEY `SubIndex_Indicators` (`SubIndexId`),
 	CONSTRAINT `SubIndex_Indicators` FOREIGN KEY (`SubIndexId`)  REFERENCES `SubIndex` (`SubIndexId`) ON DELETE CASCADE ON UPDATE NO ACTION
)ENGINE = InnoDB;

DROP TABLE IF EXISTS `IndicatorIndex`;
CREATE TABLE `IndicatorIndex` (
  `IndicatorIndexId` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(1024) NOT NULL,
  `Number` varchar(20) NULL DEFAULT NULL,
  `Description` text NULL DEFAULT NULL,  
  `IndicatorId` bigint NOT NULL,
  `Type` varchar(20) NOT NULL,  
  PRIMARY KEY (`IndicatorIndexId`),
  UNIQUE INDEX `IndicatorIndexId` (`IndicatorIndexId` ASC),
  KEY `Indicator_Indexes` (`IndicatorId`),
 	CONSTRAINT `Indicator_Indexes` FOREIGN KEY (`IndicatorId`)  REFERENCES `Indicator` (`IndicatorId`) ON DELETE CASCADE ON UPDATE NO ACTION
)ENGINE = InnoDB;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;