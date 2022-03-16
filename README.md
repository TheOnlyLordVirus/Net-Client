**AuthApi**
-------

**CentOS 7 LAMP stack install**
-------------------------------
yum upgrade -y

yum install nano httpd -y
yum install group "Development Tools" -y
yum install php php-fpm php-mysqlnd php-zip php-devel php-gd php-mcrypt php-mbstring php-curl php-xml php-pear php-bcmath php-json php-pdo php-pecl-apcu php-pecl-apcu-devel -y

**Setup DB**
------------
nano /etc/yum.repos.d/MariaDB.repo

**inside /etc/yum.repos.d/MariaDB.repo:**
-----------------------------------------
[mariadb]
name = MariaDB
baseurl = http://yum.mariadb.org/10.2/centos7-amd64
gpgkey=https://yum.mariadb.org/RPM-GPG-KEY-MariaDB
gpgcheck=1

**Running the Api Socket**
--------------------------
cd /var/www/html/CPP-Api
g++ -g -o AuthApi main.cpp -std=gnu++11

**Blocking Apache from accepting outside requests**
---------------------------------------------------
nano /etc/httpd/conf/httpd.conf

**Go to:**
----------
<Directory /var/www/html>
...

**Remove its contents and then add**
------------------------------------
order deny, allow
allow from 127.0.0.1
deny from all

**Start Webserver and DB**
--------------------------
systemctl start httpd
systemctl start mariadb
