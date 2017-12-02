#!/usr/bin/env python3

x = 10
a = lambda y, x=x: x + y  # w/o this, x is loaded at runtime
x = 20
print(a(10))  # 20
