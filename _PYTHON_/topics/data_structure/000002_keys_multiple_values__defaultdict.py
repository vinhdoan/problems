#!/usr/bin/env python3

from collections import defaultdict


pairs = [('a', 1), ('b', 2), ('c', 3)]
d = defaultdict(list)
for key, value in pairs:
    d[key].append(value)
print(d)
