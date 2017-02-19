#!/usr/bin/python3

import sys

list = [1,2,3,4]
it = iter(list) # this builds an iterator object

while True:
    try:
        print (next(it))
    except StopIteration:
        input()
        sys.exit() #you have to import sys module for this
