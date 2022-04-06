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
	IS_ADMIN boolean not null default FALSE,
	REGISTRATION_IP varchar(15),
	RECENT_IP varchar(15),
	CREATION_DATE datetime not null default now(),
	AUTH_END_DATE datetime default null
);

create table TIME_KEYS
(
	TIME_KEY varchar(29) primary key not null,
	TIME_VALUE int notdrop null,
	KEY_GEN_DATE datetime not null default now(),
	CREATED_BY int references USER(USER_ID),
	ACTIVE boolean not null default TRUE
);

create table IP_HISTORY
(
  USER_ID int references USER(USER_ID),
  LOGGED_IP varchar(15) not null,
  LOGGIN_DATE datetime not null default now()
);

create table GAME_DATA
(
	GAME_ID int primary key auto_increment,
	GAME_NAME varchar(40) not null,
	GAME_CHEAT_VERSION decimal(3,1)
);

create view STORED_IP as select u.USER_NAME, h.LOGGED_IP, h.LOGGIN_DATE from IP_HISTORY h inner join USER u on h.USER_ID = u.USER_ID;

DELIMITER $$ ;

create procedure addUser (IN `EMAIL` VARCHAR(25), IN `NAME` VARCHAR(25), IN `PASS` VARCHAR(25), IN `IP` VARCHAR(15), IN `ADMIN` BOOLEAN)
begin
  insert into USER (USER_EMAIL, USER_NAME, USER_PASS, REGISTRATION_IP, RECENT_IP, IS_ADMIN) values (EMAIL, NAME, PASS, IP, IP, ADMIN);
end
$$

create procedure logIP (in ID int, in IP varchar(15))
begin
  update USER set RECENT_IP = IP where USER_ID = ID;
  insert into IP_HISTORY (USER_ID, LOGGED_IP) values (ID, IP);
end
$$

create procedure addKey (IN `TIME_KEY` VARCHAR(29), IN `TIME_VALUE` INT, IN `USER_ID` INT)
begin
  insert into TIME_KEYS (TIME_KEY, TIME_VALUE, CREATED_BY) values (TIME_KEY, TIME_VALUE, USER_ID);
end
$$

DELIMITER ; $$

call addUser('test@mail.com', 'pastafarian', 'cheesetoast', '127.0.0.1', true);


/*Log attepmeted calls to our api*/
create database API_NETWORK_INFO_DB;
use API_NETWORK_INFO_DB;
set global sql_mode = 'NO_AUTO_VALUE_ON_ZERO';

create table API_NETWORK_HISTORY
(
	ID int primary key auto_increment,
	CONNECTION_IP varchar(15) not null,
	CREATION_DATE datetime not null default now()
);

DELIMITER $$ ;

create procedure logAnonIp (IN `IP` VARCHAR(29))
begin
  insert into API_NETWORK_HISTORY (CONNECTION_IP) values (IP);
end
$$

DELIMITER ; $$

CREATE USER 'admin'@'localhost' IDENTIFIED BY 'JeffStar';
GRANT ALL PRIVILEGES ON USER_INFO_DB.* TO 'admin'@'localhost';
GRANT ALL PRIVILEGES ON API_NETWORK_INFO_DB.* TO 'admin'@'localhost';
FLUSH PRIVILEGES;
