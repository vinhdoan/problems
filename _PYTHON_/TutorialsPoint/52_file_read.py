#!/usr/bin/python3

# Open a file
fo = open("foo.dat", "r+")
str = fo.read(10)
print ("Read String is : ", str)

# Close opened file
fo.close()

input()