#!/usr/bin/python3

# Function definition is here
def changeme( myflist ):
   "This changes a passed list into this function"
   print ("Values inside the function before change: ", myflist)
   myflist[2]=50
   print ("Values inside the function after change: ", myflist)
   return

# Now you can call changeme function
mylist = [10,20,30]
changeme( mylist )
print ("Values outside the function: ", mylist)

input()