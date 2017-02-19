#!/usr/bin/python3

try:
   # testfile.dat exists
   fh = open("testfile.dat", "r")
   try:
      fh.write("This is my test file for exception handling!!")
   finally:
      print ("Going to close the file")
      fh.close()
except IOError:
   print ("Error: can\'t find file or read data")
   
input()