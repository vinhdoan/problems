#!/usr/bin/env python3

from collections import namedtuple
Subscriber = namedtuple('Subscriber', ['addr', 'joined'])
sub = Subscriber('jonesy@example.com', '2012-10-19')
Subscriber(addr='jonesy@example.com', joined='2012-10-19')
print(sub.addr)  # 'jonesy@example.com'
print(sub.joined)  # '2012-10-19'
print(len(sub))  # 2
addr, joined = sub
print(addr)  # 'jonesy@example.com'
print(joined)  # '2012-10-19'

# Note that namedtuple is IMMUTABLE
sub = sub._replace(addr='jonesy2@example.com')
