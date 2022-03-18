/* MariaDB 10 + */
create database USER_INFO_DB;
use USER_INFO_DB;
set global sql_mode = 'NO_AUTO_VALUE_ON_ZERO';

create table USER
(
	USER_ID int primary key auto_increment,
	USER_EMAIL varchar(30) not null,
	USER_NAME varchar(30) not null,
	USER_PASS varchar(30) not null,
	LIFETIME boolean not null,
	HARDWARE_ID varchar(30) not null,
	IS_ADMIN boolean not null default FALSE,
	RECENT_IP varchar(15),
	CREATION_DATE datetime not null default NOW(),
	AUTH_END_DATE datetime default null
);

create table TIME_KEYS
(
	TIME_KEY varchar(29) primary key not null,
	TIME_VALUE int not null
);

create table USER_KEY
(
	USER_ID int primary key references TIME_KEYS(USER_ID),
	TIME_KEY varchar(29) references TIME_KEYS(TIME_KEY)
);

create table IP_HISTORY
(
  USER_ID int references USER(USER_ID),
  LOGGED_IP varchar(15) not null,
  LOGGIN_DATE datetime not null default NOW()
);

create table GAME_DATA
(
	GAME_ID int primary key auto_increment,
	GAME_NAME varchar(40) not null,
	GAME_CHEAT_VERSION decimal(3,1)
);

create view STORED_IP as select u.USER_NAME, h.LOGGED_IP, h.LOGGIN_DATE from IP_HISTORY h inner join USER u on h.USER_ID = u.USER_ID;

DELIMITER $$ ;

create procedure addUser (IN `EMAIL` VARCHAR(25), IN `NAME` VARCHAR(25), IN `PASS` VARCHAR(25), IN `HW_ID` VARCHAR(30), IN `ADMIN` BOOLEAN)
begin
  insert into USER (USER_EMAIL, USER_NAME, USER_PASS, LIFETIME, HARDWARE_ID, IS_ADMIN) values (EMAIL, NAME, PASS, FALSE, HW_ID, ADMIN);
end
$$

create procedure logIP (in ID int, in IP varchar(15))
begin
  update USER set RECENT_IP = IP where USER_ID = ID;
  insert into IP_HISTORY (USER_ID, LOGGED_IP) values (ID, IP);
end
$$

create procedure addKey (IN `TIME_KEY` VARCHAR(29), IN `TIME_VALUE` INT)
begin
  insert into TIME_KEYS (TIME_KEY, TIME_VALUE) values (TIME_KEY, TIME_VALUE);
end
$$

DELIMITER ; $$

call addUser('test@mail.com', 'pastafarian', 'cheesetoast', 'none', true);

CREATE USER 'admin'@'localhost' IDENTIFIED BY 'password';
GRANT ALL PRIVILEGES ON USER_INFO_DB.* TO 'admin'@'localhost';
FLUSH PRIVILEGES;