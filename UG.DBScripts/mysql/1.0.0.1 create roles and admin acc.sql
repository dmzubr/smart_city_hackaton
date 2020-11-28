SET @AdminRoleId = 'b091bcc0-b7af-4d93-a2c0-7ec9279559da';

USE `smartcity`;

INSERT INTO `Role`(`Id`, `Name`)
VALUES(@AdminRoleId, 'admin');

INSERT INTO `Role`(`Id`, `Name`)
VALUES('d24a6f54-2ea0-4fc8-8c32-a01ad08b8d26', 'region_manager');

INSERT INTO `Role`(`Id`, `Name`)
VALUES('248dec13-eba4-4ee1-8d1f-a7e800e4f698', 'central_manager');

/* admin : Gbnec@90 */
INSERT INTO `User`
(
	`Guid`,
	`UserName`,
	`EmailConfirmed`,
	`PasswordHash`,
	`PhoneNumberConfirmed`,
	`TwoFactorEnabled`,
	`LockoutEnabled`,
	`AccessFailedCount`
)
VALUES
(
	'119643c1-5eeb-478a-ad2a-aa14b691578c',
	'admin',
	0,
	'AQAAAAEAACcQAAAAEMaCerRHvs7YySK/K80dTWf+PuXgSmeTB2u0jerawKxB2l/8VaMmEw3jrmXA5jUaug==',
	0,
	0,
	0,
	0
);

SET @UserId=LAST_INSERT_ID();

/* Assign admin account to Admin role */
INSERT INTO `UserRole`(`UserId`, `RoleId`)
VALUES (@UserId, @AdminRoleId);