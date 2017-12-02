#!/usr/bin/env python3

record = '..........100..........99'
SHARES = slice(10, 13)
PRICE = slice(23, 25)
cost = int(record[SHARES]) * float(record[PRICE])
print(cost)
print("SHARES: ", SHARES.start, SHARES.stop, SHARES.step)
print("PRICE: ", PRICE.start, PRICE.stop, PRICE.step)

print("-------------------")

s = 'HelloWorld'
a = slice(5, 50, 2)
a.indices(len(s))  # fit string s: (5, 10, 2)
for i in range(*a.indices(len(s))):
    print(s[i])
