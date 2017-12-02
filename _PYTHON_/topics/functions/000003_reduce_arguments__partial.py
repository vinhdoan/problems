#!/usr/bin/env python3


def spam(a, b, c, d):
    print(a, b, c, d)


from functools import partial

s1 = partial(spam, 1)  # a = 1
s1(2, 3, 4)  # 1 2 3 4
s1(4, 5, 6)  # 1 4 5 6

s2 = partial(spam, d=42)  # d = 42
s2(1, 2, 3)  # 1 2 3 42
s2(4, 5, 5)  # 4 5 5 42

s3 = partial(spam, 1, 2, d=42)  # a = 1, b = 2, d = 42
s3(3)  # 1 2 3 42
s3(4)  # 1 2 4 42
s3(5)  # 1 2 5 42
