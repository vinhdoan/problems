#!/usr/bin/python3

# Open a file
fo = open("foo.dat", "w")
fo.write( "Python is a great language.\nYeah its great!!\n")

# Close opend file
fo.close()

print("DONE")
input()