CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;

CREATE TABLE `Payments` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Amount` double NOT NULL,
    `ConsumerName` varchar(256) NOT NULL,
    `PhoneNumber` varchar(256) NOT NULL,
    `PaymentState` int NOT NULL,
    `JasonResponseObject` varchar(256) NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `UpdatedAt` datetime(6) NULL,
    `DeletedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`)
);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20231031094612_first', '7.0.13');

COMMIT;

START TRANSACTION;

ALTER TABLE `Payments` DROP COLUMN `PaymentState`;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20231031111648_sec', '7.0.13');

COMMIT;

START TRANSACTION;

ALTER TABLE `Payments` ADD `PaymentServer` int NOT NULL DEFAULT 0;

ALTER TABLE `Payments` ADD `PaymentState` int NOT NULL DEFAULT 0;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20231031111949_addedProprtyPymentserver', '7.0.13');

COMMIT;

START TRANSACTION;

ALTER TABLE `Payments` MODIFY `JasonResponseObject` varchar(5000) NOT NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20231114121546_modyfy', '7.0.13');

COMMIT;

