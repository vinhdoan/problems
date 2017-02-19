#!/usr/bin/python3

class JustCounter:
   __secretCount = 0    # prefix __ to make it private
  
   def count(self):
      self.__secretCount += 1
      print (self.__secretCount)

counter = JustCounter()
counter.count()
counter.count()

print (counter._JustCounter__secretCount)
   # accessible by specifying object._className__attrName
print (counter.__secretCount)   # causes an exception

input()