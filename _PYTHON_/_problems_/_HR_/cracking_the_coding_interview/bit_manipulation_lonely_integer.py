# https://www.hackerrank.com/challenges/ctci-lonely-integer/problem


from operator import xor
from functools import reduce


def lonely_integer(a):
    return reduce(xor, a)


n = int(input().strip())
a = [int(a_temp) for a_temp in input().strip().split(' ')]
print(lonely_integer(a))
