SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

USE `smartcity`;

DROP TABLE IF EXISTS `Region`;
CREATE TABLE `Region` (
  `RegionId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(256) NOT NULL,  
  PRIMARY KEY (`RegionId`),
  UNIQUE INDEX `RegionId` (`RegionId` ASC)
)ENGINE = InnoDB;

DROP TABLE IF EXISTS `CityType`;
CREATE TABLE `CityType` (
  `CityTypeId` int NOT NULL,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`CityTypeId`),
  UNIQUE INDEX `CityTypeId` (`CityTypeId` ASC)
)ENGINE = InnoDB;

INSERT INTO `CityType`(`CityTypeId`, `Name`) VALUES(1, 'админ центры');
INSERT INTO `CityType`(`CityTypeId`, `Name`) VALUES(2, 'крупные');
INSERT INTO `CityType`(`CityTypeId`, `Name`) VALUES(3, 'большие');
INSERT INTO `CityType`(`CityTypeId`, `Name`) VALUES(4, 'крупнейшие');

DROP TABLE IF EXISTS `City`;
CREATE TABLE `City` (
  `CityId` int NOT NULL AUTO_INCREMENT,
  `CityTypeId` int NOT NULL,
  `RegionId` int NOT NULL,
  `Name` varchar(256) NOT NULL,
  `OKTMO` varchar(20) NOT NULL,
  PRIMARY KEY (`CityId`),
  UNIQUE INDEX `CityId` (`CityId` ASC),
  KEY `CityType_Cities` (`CityTypeId`),
 	CONSTRAINT `CityType_Cities` FOREIGN KEY (`CityTypeId`)  REFERENCES `CityType` (`CityTypeId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  KEY `Region_Cities` (`RegionId`),
 	CONSTRAINT `Region_Cities` FOREIGN KEY (`RegionId`)  REFERENCES `Region` (`RegionId`) ON DELETE CASCADE ON UPDATE NO ACTION
)ENGINE = InnoDB;

DROP TABLE IF EXISTS `OuterMetric`;
CREATE TABLE `OuterMetric` (
  `OuterMetricId` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(500) NOT NULL,
  `Code` varchar(500) NOT NULL,
  `Description` varchar(1000) NULL DEFAULT NULL,
  PRIMARY KEY (`OuterMetricId`),
  UNIQUE INDEX `OuterMetricId` (`OuterMetricId` ASC)
)ENGINE = InnoDB;

DROP TABLE IF EXISTS `OuterMetricCityValue`;
CREATE TABLE `OuterMetricCityValue` (
  `OuterMetricCityValueId` bigint NOT NULL AUTO_INCREMENT,
  `OuterMetricId` bigint NOT NULL,
  `CityId` int NOT NULL,
  `Value` decimal NOT NULL,
  `CalcDate` datetime NULL DEFAULT NULL,
  `PeriodStart` datetime NOT NULL,
  `PeriodEnd` datetime NOT NULL DEFAULT current_timestamp,
  PRIMARY KEY (`OuterMetricCityValueId`),
  UNIQUE INDEX `OuterMetricCityValueId` (`OuterMetricCityValueId` ASC),
  KEY `OuterMetric_Values` (`OuterMetricId`),
 	CONSTRAINT `OuterMetric_Values` FOREIGN KEY (`OuterMetricId`)  REFERENCES `OuterMetric` (`OuterMetricId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  KEY `City_OuterMetricValues` (`CityId`),
 	CONSTRAINT `City_OuterMetricValues` FOREIGN KEY (`CityId`)  REFERENCES `City` (`CityId`) ON DELETE CASCADE ON UPDATE NO ACTION
)ENGINE = InnoDB;

DROP TABLE IF EXISTS `IndicatorIndexCityValue`;
CREATE TABLE `IndicatorIndexCityValue` (
  `IndicatorIndexCityValueId` bigint NOT NULL AUTO_INCREMENT,
  `IndicatorIndexId` bigint NOT NULL,
  `CityId` int NOT NULL,
  `Value` decimal NOT NULL,
  `CalcDate` datetime NULL DEFAULT NULL,
  `PeriodStart` datetime NOT NULL,
  `PeriodEnd` datetime NOT NULL DEFAULT current_timestamp,
  PRIMARY KEY (`IndicatorIndexCityValueId`),
  UNIQUE INDEX `IndicatorIndexCityValueId` (`IndicatorIndexCityValueId` ASC),
  KEY `IndicatorIndex_Values` (`IndicatorIndexId`),
 	CONSTRAINT `IndicatorIndex_Values` FOREIGN KEY (`IndicatorIndexId`)  REFERENCES `IndicatorIndex` (`IndicatorIndexId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  KEY `City_IndicatorIndexValues` (`IndicatorIndexId`),
 	CONSTRAINT `City_IndicatorIndexValues` FOREIGN KEY (`IndicatorIndexId`)  REFERENCES `IndicatorIndex` (`IndicatorIndexId`) ON DELETE CASCADE ON UPDATE NO ACTION
)ENGINE = InnoDB;

DROP TABLE IF EXISTS `IndicatorCityValue`;
CREATE TABLE `IndicatorCityValue` (
  `IndicatorCityValueId` bigint NOT NULL AUTO_INCREMENT,
  `IndicatorId` bigint NOT NULL,
  `CityId` bigint NOT NULL,
  `Value` decimal NOT NULL,
  `CalcDate` datetime NULL DEFAULT NULL,
  `PeriodStart` datetime NOT NULL,
  `PeriodEnd` datetime NOT NULL,
  PRIMARY KEY (`IndicatorCityValueId`),
  UNIQUE INDEX `IndicatorCityValueId` (`IndicatorCityValueId` ASC),
  KEY `Indicator_Values` (`IndicatorId`),
 	CONSTRAINT `Indicator_Values` FOREIGN KEY (`IndicatorId`)  REFERENCES `Indicator` (`IndicatorId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  KEY `City_IndicatorValues` (`CityId`),
 	CONSTRAINT `City_IndicatorValues` FOREIGN KEY (`CityId`)  REFERENCES `City` (`CityId`) ON DELETE CASCADE ON UPDATE NO ACTION
)ENGINE = InnoDB;

ALTER TABLE `Category` 
ADD COLUMN `IndicatorId` BIGINT NOT NULL AFTER `Name`;
ALTER TABLE `Category` 
ADD INDEX `Indicator_Categories_idx` (`IndicatorId` ASC);
ALTER TABLE `Category` 
ADD CONSTRAINT `Indicator_Categories`  FOREIGN KEY (`IndicatorId`)  REFERENCES `Indicator` (`IndicatorId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

USE `smartcity`;
DROP TABLE IF EXISTS `IndicatorSocialVerification`;
CREATE TABLE `IndicatorSocialVerification` (
  `IndicatorSocialVerificationId` bigint NOT NULL AUTO_INCREMENT,
  `IndicatorId` bigint NOT NULL,
  `CityId` bigint NOT NULL,
  `Value` decimal NOT NULL,
  `CalcDate` datetime NULL DEFAULT NULL,
  `PeriodStart` datetime NOT NULL,
  `PeriodEnd` datetime NOT NULL,
  PRIMARY KEY (`IndicatorSocialVerificationId`),
  UNIQUE INDEX `IndicatorSocialVerificationId` (`IndicatorSocialVerificationId` ASC),
  KEY `Indicator_SocialVerificationValues` (`IndicatorId`),
 	CONSTRAINT `Indicator_SocialVerificationValues` FOREIGN KEY (`IndicatorId`)  REFERENCES `Indicator` (`IndicatorId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  KEY `City_SocialVerificationValues` (`CityId`),
 	CONSTRAINT `City_SocialVerificationValues` FOREIGN KEY (`CityId`)  REFERENCES `City` (`CityId`) ON DELETE CASCADE ON UPDATE NO ACTION
)ENGINE = InnoDB;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;