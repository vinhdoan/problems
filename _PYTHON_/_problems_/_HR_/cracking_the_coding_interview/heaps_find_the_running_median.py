#!/bin/python3


import heapq

n = int(input().strip())
a = []
maxHeap = []  # lower half-list
minHeap = []  # upper half-list
a_i = 0
for a_i in range(n):
    a_t = int(input().strip())
    a.append(a_t)
    val = heapq.heappushpop(minHeap, a_t)
    val = -heapq.heappushpop(maxHeap, -val)
    if len(maxHeap) == len(minHeap):
        heapq.heappush(minHeap, val)
        median = minHeap[0]
    else:
        heapq.heappush(maxHeap, -val)
        median = (minHeap[0] - maxHeap[0]) / 2
    print(round(float(median), 1))
