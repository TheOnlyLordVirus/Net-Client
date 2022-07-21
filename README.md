Example: 
https://www.youtube.com/watch?v=8B68HYh2MlE

https://github.com/TheOnlyLordVirus/Centos-7-LAMP-Stack-Install

**Login as root and Run Build_DB.sql querys on DB:**
----------------------------------------------------
``
mysql -pYOUR_PASS
``

**Create a new account:**
-------------------------
```
CREATE USER 'admin'@'localhost' IDENTIFIED BY 'password' require ssl;
GRANT ALL PRIVILEGES ON USER_INFO_DB.* TO 'admin'@'localhost';
GRANT ALL PRIVILEGES ON API_NETWORK_INFO_DB.* TO 'admin'@'localhost';
FLUSH PRIVILEGES;
EXIT;
```

**Login to new account and verify it exists with DB access:**
-------------------------------------------------------------
```
mysql -uadmin -ppassword
```
