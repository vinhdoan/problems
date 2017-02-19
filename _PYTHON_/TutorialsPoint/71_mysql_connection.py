#!/usr/bin/python3
import pymysql

#------------------------------------------------
# PREPARE MYSQL DATABASE
# 1. Create database and grant permission to user
#       > create database testdb;
#       > create user 'testuser' identified by 'test123';
#       > grant all on testdb.* to 'testuser';
# 2. Create table:
#       > use testdb;
#       > create table employee (
#             id int not null auto_increment,
#             first_name varchar(40) not null,
#             last_name varchar(40) not null,
#             age int,
#             sex varchar(10),
#             income int,
#             primary key (id)
#         );
# 3. Modify /etc/mysql/mysql.conf.d/mysqld.cnf
#       bind-address		= 0.0.0.0
# 4. Restart mysql server:
#       $ service mysql restart
#------------------------------------------------

# Open database connection
db = pymysql.connect("192.168.1.92","testuser","test123","testdb" )

# prepare a cursor object using cursor() method
cursor = db.cursor()

# execute SQL query using execute() method.
cursor.execute("SELECT VERSION()")

# Fetch a single row using fetchone() method.
data = cursor.fetchone()

print ("Database version : %s " % data)

# disconnect from server
db.close()

input()