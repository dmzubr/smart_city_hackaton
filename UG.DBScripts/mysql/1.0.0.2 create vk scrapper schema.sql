SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

USE `smartcity`;

-- Types of social networks
DROP TABLE IF EXISTS `SocialNetwork`;
CREATE TABLE `SocialNetwork` (
    `SocialNetworkId` INT NOT NULL,
    `Name` varchar(256) NOT NULL,
    KEY `SocialNetworkId` (`SocialNetworkId`),
    PRIMARY KEY (`SocialNetworkId`),
    UNIQUE INDEX `SocialNetworkId_UNIQUE` (`SocialNetworkId` ASC)
);

INSERT INTO `SocialNetwork`(`SocialNetworkId`, `Name`) VALUES(1, 'VK');

-- Social public example is https://vk.com/arkhangelsk_life
DROP TABLE IF EXISTS `SNPublic`;
CREATE TABLE `SNPublic` (
   `SNPublicId` INT NOT NULL AUTO_INCREMENT,
   `SocialNetworkId` INT NOT NULL,
   `OuterId` bigint NOT NULL, 
   `Name` varchar(256) NOT NULL,
   `URL` varchar(2048),
   `FolowersQuntity` INT NULL DEFAULT NULL,
   `LastModifiedDateTime` DATETIME,
   PRIMARY KEY (`SNPublicId`),
   UNIQUE INDEX `SNPublicId_UNIQUE` (`SNPublicId` ASC),
   KEY `SocialNetwork_Publics` (`SocialNetworkId`),
 	CONSTRAINT `SocialNetwork_Publics` FOREIGN KEY (`SocialNetworkId`)  REFERENCES `SocialNetwork` (`SocialNetworkId`) ON DELETE CASCADE ON UPDATE NO ACTION
);

DROP TABLE IF EXISTS `SNWallPost`;
CREATE TABLE `SNWallPost` (
   `SNWallPostId` bigint NOT NULL AUTO_INCREMENT,
   `OuterId` bigint NOT NULL,
   `PublishDateTime` DATETIME NULL DEFAULT NULL,
   `Text` text NOT NUll,
   `WallOwnerOuterId` INT NULL DEFAULT NULL,
   `OuterAuthorId` INT NULL DEFAULT NULL,
   `WallPostURL` varchar(2048),
   `LikesQuantity` INT NULL DEFAULT NULL,
   `RepostQuantity` INT NULL DEFAULT NULL,
   `CommentQuantity` INT NULL DEFAULT NULL,
   `ViewsQuantity` INT NULL DEFAULT NULL,
   `LastModifiedDateTime` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, 
   `EmotionMark` INT NULL DEFAULT NULL,			#Emotion (negative/neutral/positive),
   `IsTarget` bit NULL DEFAULT NULL,
  PRIMARY KEY (`SNWallPostId`),
  UNIQUE INDEX `SNWallPostId_UNIQUE` (`SNWallPostId` ASC)
);
  
DROP TABLE IF EXISTS `SNComment`;
CREATE TABLE `SNComment` (
  `SNCommentId` bigint NOT NULL AUTO_INCREMENT,
  `OuterId` bigint NOT NULL,
  `PublishDateTime` DATETIME NOT NULL default CURRENT_TIMESTAMP,
  `Text` text NOT NUll,
  `SNWallPostId` bigint NOT NULL,
  `OuterAuthorId` INT NULL DEFAULT NULL,
  `LikesQuantity` INT NULL DEFAULT NULL,
  `CommentsQuantity` INT NULL DEFAULT NULL,
  `LastModifiedDateTime` DATETIME NOT NULL default CURRENT_TIMESTAMP,
  `EmotionMark` INT NULL DEFAULT NULL, 			#Emotion (negative/neutral/positive)
  `IsTarget` bit NULL DEFAULT NULL,
  PRIMARY KEY (`SNCommentId`),
  UNIQUE INDEX `SNCommentId_UNIQUE` (`SNCommentId` ASC),
  KEY `WallPost_Comments` (`SNWallPostId`),
    CONSTRAINT `SNWallPost_Comments` FOREIGN KEY (`SNWallPostId`) REFERENCES `SNWallPost` (`SNWallPostId`) ON DELETE CASCADE ON UPDATE NO ACTION
);
  
DROP TABLE IF EXISTS `SNUser`;
CREATE TABLE `SNUser` (
  `SNUserId` bigint NOT NULL AUTO_INCREMENT,
  `SocialNetworkId` INT NOT NULL,
  `OuterId` INT NOT NULL,
  `NameAlias` varchar(128),
  `UserPageURL` varchar(2048),
  `FirstName` varchar(128),
  `LastName` varchar(128),
  `City` varchar(256),
  `FriendsQuantity` INT NULL DEFAULT NULL,
  `LastModifiedDateTime` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`SNUserId`),
  UNIQUE INDEX `SNUserId_UNIQUE` (`SNUserId` ASC),
  KEY `SocialNetwork_Users` (`SocialNetworkId`),
    CONSTRAINT `SocialNetwork_Users` FOREIGN KEY (`SocialNetworkId`) REFERENCES `SocialNetwork` (`SocialNetworkId`) ON DELETE CASCADE ON UPDATE NO ACTION
);

DROP TABLE IF EXISTS `Category`;
CREATE TABLE `Category` (
  `CategoryId` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(256) NOT NULL,
  `Name` varchar(256) NOT NULL,
  PRIMARY KEY (`CategoryId`),
  UNIQUE INDEX `CategoryId_UNIQUE` (`CategoryId` ASC)
);

DROP TABLE IF EXISTS `TriggerWord`;
CREATE TABLE `TriggerWord` (
  `TriggerWordId` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(512) NOT NULL,
  `IsTarget` bit NOT NULL,
  PRIMARY KEY (`TriggerWordId`),
  UNIQUE INDEX `TriggerWordId_UNIQUE` (`TriggerWordId` ASC)
);
  
DROP TABLE IF EXISTS `SNWallPostTriggerWord`;
CREATE TABLE `SNWallPostTriggerWord` (
  `SNWallPostTriggerWordId` bigint NOT NULL AUTO_INCREMENT,
  `SNWallPostId` bigint NOT NULL,
  `TriggerWordId` bigint NOT NULL,
  PRIMARY KEY (`SNWallPostTriggerWordId`),
  UNIQUE INDEX `SNWallPostTriggerWordId_UNIQUE` (`SNWallPostTriggerWordId` ASC),
  FOREIGN KEY (`SNWallPostId`) REFERENCES `SNWallPost` (`SNWallPostId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  FOREIGN KEY (`TriggerWordId`) REFERENCES `TriggerWord` (`TriggerWordId`) ON DELETE CASCADE ON UPDATE NO ACTION
);

DROP TABLE IF EXISTS `SNCommentTriggerWord`;
CREATE TABLE `SNCommentTriggerWord` (
  `SNCommentTriggerWordId` bigint NOT NULL AUTO_INCREMENT,
  `SNCommentId` bigint NOT NULL,
  `TriggerWordId` bigint NOT NULL,
  PRIMARY KEY (`SNCommentTriggerWordId`),
  UNIQUE INDEX `SNCommentTriggerWordId_UNIQUE` (`SNCommentTriggerWordId` ASC),
  FOREIGN KEY (`SNCommentId`) REFERENCES `SNComment` (`SNCommentId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  FOREIGN KEY (`TriggerWordId`) REFERENCES `TriggerWord` (`TriggerWordId`) ON DELETE CASCADE ON UPDATE NO ACTION
);

DROP TABLE IF EXISTS `CategoryTriggerWord`;
CREATE TABLE `CategoryTriggerWord` (
  `CategoryTriggerWordId` bigint NOT NULL AUTO_INCREMENT,
  `CategoryId` bigint NOT NULL,
  `TriggerWordId` bigint NOT NULL,
  PRIMARY KEY (`CategoryTriggerWordId`),
  UNIQUE INDEX `CategoryTriggerWordId` (`CategoryTriggerWordId` ASC),
  FOREIGN KEY (`CategoryId`) REFERENCES `Category` (`CategoryId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  FOREIGN KEY (`TriggerWordId`) REFERENCES `TriggerWord` (`TriggerWordId`) ON DELETE CASCADE ON UPDATE NO ACTION
);

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

