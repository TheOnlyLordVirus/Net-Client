/* MariaDB 10 + */
create database USER_INFO_DB;
use USER_INFO_DB;
set global sql_mode = 'NO_AUTO_VALUE_ON_ZERO';

create table USER
(
	USER_ID int primary key auto_increment,
	USER_EMAIL varchar(30) not null,
	USER_NAME varchar(30) not null,
	USER_PASS varchar(8000) not null,
	IS_ADMIN boolean not null default false,
	REGISTRATION_IP varchar(15),
	RECENT_IP varchar(15),
	CREATION_DATE datetime not null default now(),
	AUTH_END_DATE datetime default null,
	ACTIVE boolean not null default true
);

CREATE TABLE TIME_KEYS
(
	TIME_KEY varchar(23) primary key not null,
	TIME_VALUE int null,
	KEY_GEN_DATE datetime not null default now(),
  	CREATED_BY int not null,
	CONSTRAINT FK_CREATED_BY_CON FOREIGN KEY (CREATED_BY) REFERENCES USER(USER_ID) ON DELETE CASCADE,
	ACTIVE boolean not null default true
);

create table COMMAND_HISTORY
(
  ID int primary key auto_increment,
  USER_ID int not null,
  CONSTRAINT FK_USER_COMMAND_CON FOREIGN KEY (USER_ID) REFERENCES USER(USER_ID) ON DELETE CASCADE,
  COMMAND varchar(255) default null,
  PARAMETERS varchar(255) default null,
  LOGGED_IP varchar(15) not null,
  DATE_RECEVED datetime not null default now()
);

create view STORED_HISTORY as select u.USER_NAME, h.COMMAND, h.PARAMETERS, h.LOGGED_IP, h.DATE_RECEVED from COMMAND_HISTORY h inner join USER u on h.USER_ID = u.USER_ID;

DELIMITER $$ ;

create procedure addUser (IN `EMAIL` VARCHAR(25), IN `NAME` VARCHAR(25), IN `PASS` VARCHAR(8000), IN `IP` VARCHAR(15), IN `ADMIN` BOOLEAN)
begin
  IF((SELECT u.USER_ID
        FROM USER as u
        WHERE u.USER_NAME = NAME) is null 
        AND 
    (SELECT u.USER_ID
      FROM USER as u
        WHERE u.USER_EMAIL = EMAIL) is null)
  THEN
    insert into USER (USER_EMAIL, USER_NAME, USER_PASS, REGISTRATION_IP, RECENT_IP, IS_ADMIN) values (EMAIL, NAME, PASS, IP, IP, ADMIN);
  END IF;
end
$$

create procedure disableUser (IN `NAME` VARCHAR(30))
begin
    SET @UID = (SELECT ux.USER_ID
                        FROM USER as ux
                        WHERE ux.USER_NAME = NAME);

    IF(@UID is not null)
    THEN
        update USER as u set u.ACTIVE = false where u.USER_ID = @UID;
    END IF;
end
$$

create procedure logCommand (in ID int, in IP varchar(15), in COMM varchar(255), in PARAMS varchar(255))
begin
  update USER set RECENT_IP = IP where USER_ID = ID;
  insert into COMMAND_HISTORY (USER_ID, LOGGED_IP, COMMAND, PARAMETERS) values (ID, IP, COMM, PARAMS);
end
$$

create procedure addKey (IN `TIME_KEY` VARCHAR(23), IN `TIME_VALUE` INT, IN `USER_ID` INT)
begin
    IF((select u.IS_ADMIN from USER as u where u.USER_ID = USER_ID))
    THEN
        insert into TIME_KEYS (TIME_KEY, TIME_VALUE, CREATED_BY) values (TIME_KEY, TIME_VALUE, USER_ID);
    END IF;
end
$$

create procedure redeemKey (IN `TIME_KEY` VARCHAR(23), IN `USER_ID` INT)
begin
  IF((SELECT u.USER_ID
        FROM USER as u
        WHERE u.USER_ID = USER_ID) is not null
        AND
        (SELECT tk.ACTIVE
        FROM TIME_KEYS as tk
        WHERE tk.TIME_KEY = TIME_KEY and tk.ACTIVE = true) is not null)
  THEN
      SET @keytime = (SELECT tk.TIME_VALUE
                      FROM TIME_KEYS as tk
                      WHERE tk.TIME_KEY = TIME_KEY);

      IF((select u.AUTH_END_DATE from USER as u where u.USER_ID = USER_ID) is null)
      THEN
          update USER as u set u.AUTH_END_DATE = DATE_ADD(now(), interval @keytime day) where u.USER_ID = USER_ID;
      ELSE
          update USER as u set u.AUTH_END_DATE = DATE_ADD(u.AUTH_END_DATE, interval @keytime day) where u.USER_ID = USER_ID;
      END IF;

      update TIME_KEYS as tk set tk.ACTIVE = false where tk.TIME_KEY = TIME_KEY;
  END IF;
end
$$

create procedure userCommandHistory (IN `USERNAME` VARCHAR(30))
begin
  select * from STORED_HISTORY where USER_NAME = USERNAME;
end
$$

DELIMITER ; $$

call addUser('test@mail.com', 'lordvirus', 'y2tg/nQbaCK34eofbKCGtA=='/*'kush007' encrypted*/, '184.55.158.226', true); 
call addKey('00000-00000-00000-00000', 7/*Days*/, 1);
call redeemKey('00000-00000-00000-00000', 1);

/* Used to edit existing constraints.
ALTER TABLE COMMAND_HISTORY 
ADD CONSTRAINT FK_USER_COMMAND_CON FOREIGN KEY (USER_ID) REFERENCES USER(USER_ID) ON DELETE CASCADE;
*/

/*Log attepmeted calls to our api*/
create database API_NETWORK_INFO_DB;
use API_NETWORK_INFO_DB;
set global sql_mode = 'NO_AUTO_VALUE_ON_ZERO';

create table API_NETWORK_HISTORY
(
	ID int primary key auto_increment,
	CONNECTION_IP varchar(15) not null,
  POST_DATA varchar(8000),
  GET_DATA varchar(8000),
	CREATION_DATE datetime not null default now()
);

DELIMITER $$ ;

create procedure logAnonIp (IN `_IP` VARCHAR(15), IN `_POST` VARCHAR(8000), IN `_GET` VARCHAR(8000))
begin
  insert into API_NETWORK_HISTORY (CONNECTION_IP, POST_DATA, GET_DATA) values (_IP, _POST, _GET);
end
$$

DELIMITER ; $$

CREATE USER 'admin'@'localhost' IDENTIFIED BY '$JohnnyBravo47592';
GRANT ALL PRIVILEGES ON USER_INFO_DB.* TO 'admin'@'localhost';
GRANT ALL PRIVILEGES ON API_NETWORK_INFO_DB.* TO 'admin'@'localhost';
FLUSH PRIVILEGES;
