**CentOS 7 LAMP stack install**
-------------------------------
```
yum upgrade -y

yum install nano httpd -y

yum groupinstall "Development Tools" -y

yum install php php-fpm php-mysqlnd php-zip php-devel php-gd php-mcrypt php-mbstring php-curl php-xml php-pear php-bcmath php-json php-pdo php-pecl-apcu php-pecl-apcu-devel -y
```
**Setup DB**
------------

``nano /etc/yum.repos.d/MariaDB.repo``

**Inside /etc/yum.repos.d/MariaDB.repo Add:**
```
[mariadb]
name = MariaDB
baseurl = http://yum.mariadb.org/10.2/centos7-amd64
gpgkey=https://yum.mariadb.org/RPM-GPG-KEY-MariaDB
gpgcheck=1
```

**Run this command, don't alow remote connections or anonymouse users, Set your root db password.**
``
mysql_secure_installation
``

**Login as root and Run Build_DB.sql querys on DB:**
``
mysql -pYOUR_PASS
``

**Create a new account:**
```
CREATE USER 'admin'@'localhost' IDENTIFIED BY 'password' require ssl;
GRANT ALL PRIVILEGES ON USER_INFO_DB.* TO 'admin'@'localhost';
FLUSH PRIVILEGES;
EXIT;
```

**Login to new account and verify it exists with DB access:**
```
mysql -uadmin -ppassword
```

**Compiling and running the Api Socket**
--------------------------
```
cd /var/www/html/CPP-Api
g++ -g -o AuthApi main.cpp -std=gnu++11
./AuthApi
```

**Blocking Apache from accepting outside requests**
---------------------------------------------------
``nano /etc/httpd/conf/httpd.conf``

**Go to:**
``<Directory /var/www/html>``

**Remove the contents of the Directory Markup and then add:**
```
order deny, allow
allow from 127.0.0.1
deny from all
```

**Start Webserver and DB**
--------------------------
``systemctl start httpd``
``systemctl start mariadb``

**Ddos port filtering** (Work in progress)
-----------------------
```
# Note: the names of the attacks don't nessisarily mean they only work to deny the services provided.
# sysctl.conf ddos protection
echo net.netfilter.nf_conntrack_buckets = 125000 >> /etc/sysctl.conf
echo net.nf_conntrack_max = 1000000 >> /etc/sysctl.conf

# UDP Flood
Iptables -A INPUT -p udp -m limit --limit 6/s --limit-burst 66 -j DROP

# TCP Flood
iptables -A INPUT -p tcp -m limit --limit 6/s --limit-burst 66 -j DROP

# Dropping all common AMP source ports
iptables -t mangle -A PREROUTING -p udp -m multiport --sports 3283,37810,7001,17185,3072,3702,32414,177 -j DROP
iptables -t mangle -A PREROUTING -p udp -m multiport --sports 6881,5683,41794,2362,11211,53413,17,1900,10001,389,137,5351,502 -j DROP

# UDP-Rape Patch
iptables -t filter -A INPUT -p udp -m udp --sport 41460 -j DROP
iptables -I NPUT -p udp --sport 41460 -m state --state NEW -m recent --update --seconds 5 --hitcount 100 -j DROP
iptables -t mangle -A PREROUTING -s 173.245.48.0/20 -j DROP
iptables -t mangle -A PREROUTING -s 141.101.64.0/18 -j DROP
iptables -t mangle -A PREROUTING -s 162.168.0.0/15 -j DROP
iptables -t mangle -A PREROUTING -s 173.245.48.0/24 -j DROP
iptables -t mangle -A PREROUTING -s 107.16.0.0/12 -j DROP
iptables -t mangle -A PREROUTING -s 13.107.14.0/24 -j DROP
iptables -t mangle -A PREROUTING -s 216.239.36.0/24 -j DROP

# StormOG Patch
iptables -A INPUT -p udp --sport 7777 -m limit --limit 6/s --limit-burst 12 -j DROP
iptables -A INPUT -p udp --dport 7777 -m limit --limit 6/s --limit-burst 12 -j DROP
iptables -t filter -A INPUT -p udp -m udp --dport 7777 -j DROP

# ARD Patch
iptables -t mangle -A PREROUTING -p udp --sport 3283 -m length --length 1048 -j DROP
iptables -A INPUT -p udp --sport 50554 -m limit --limit 6/s --limit-burst 12 -j DROP
iptables -A INPUT -p udp --dport 62373 -m limit --limit 6/s --limit-burst 12 -j DROP

#UDPBypass Patch
iptables -t mangle -A PREROUTING -p udp --sport 21 -m length --length 44 -j DROP
iptables -A INPUT -p tcp --sport 21 -m limit --limit 6/s --limit-burst 12 -j DROP

#STKillAll Patch
iptables -t mangle -A PREROUTING -p tcp --sport 80 -m length --length 44 -j DROP

#CODSLOMO Patch
iptables -t mangle -A PREROUTING -p udp --sport 54590 -m length --length 53 -j DROP
iptables -A INPUT -p udp --sport 54590 -m limit --limit 6/s --limit-burst 12 -j DROP
iptables -t mangle -A PREROUTING -p udp --sport 48852  -m length --length 42 -j DROP
iptables -A INPUT -p udp --sport 48852 -m limit --limit 6/s --limit-burst 12 -j DROP
iptables -t mangle -A PREROUTING -p udp --sport 44513 -m length --length 37 -j DROP
iptables -A INPUT -p udp --sport 44513 -m limit --limit 6/s --limit-burst 12 -j DROP
iptables -t mangle -A PREROUTING -p udp --sport 56116 -m length --length 35 -j DROP
iptables -A INPUT -p udp --sport 56116 -m limit --limit 6/s --limit-burst 12 -j DROP
iptables -t mangle -A PREROUTING -p udp --sport 53 -m length --length 70 -j DROP
iptables -t mangle -A PREROUTING -p udp --sport 53 -m length --length 58 -j DROP

#PUBGBypass Patch
iptables -t mangle -A PREROUTING -p udp --sport 29445 -m length --length 28 -j DROP
iptables -A INPUT -p udp --sport 29445 -m limit --limit 6/s --limit-burst 12 -j DROP

#Frag Patch
iptables -t mangle -A PREROUTING -p tcp --sport 12024 -m length --length 40 -j DROP
iptables -t mangle -A PREROUTING -p udp --sport 12321 -m length --length 799 -j DROP

#CPU-DROP
iptables -t mangle -A PREROUTING -p udp --sport 665 -m length --length 116 -j DROP
iptables -t mangle -A PREROUTING -p tcp --sport 665 -m length --length 116 -j DROP
iptables -A INPUT -p udp --sport 665 -m limit --limit 6/s --limit-burst 12 -j DROP
iptables -A INPUT -p tcp --sport 665 -m limit --limit 6/s --limit-burst 12 -j DROP

#TCP-AMP Patch
iptables -t mangle -A PREROUTING -p tcp --sport 21 -m length --length 44 -j DROP
iptables -A INPUT -p tcp --sport 21 -m limit --limit 6/s --limit-burst 12 -j DROP

#Mix of UDP Amplifications, Raw UDP & Bypass
iptables -t mangle -A PREROUTING -p udp --sport 61013 -m length --length 29 -j DROP
iptables -A INPUT -p udp --sport 61013 -m limit --limit 6/s --limit-burst 12 -j DROP
```

**Abstract**
------------
**TODO:**
Update MariaDB to patch this exploit:
https://github.com/Al1ex/CVE-2021-27928

**CPP Socket reference:**
https://www.bogotobogo.com/cplusplus/sockets_server_client.php
