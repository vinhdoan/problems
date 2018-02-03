# https://www.hackerrank.com/challenges/s10-basic-statistics/problem


import numpy as np
from scipy import stats

size = int(input())
numbers = list(map(int, input().split()))
mean = np.mean(numbers)
median = np.median(numbers)
mode = int(stats.mode(numbers)[0])

print(mean)
print(median)
print(mode)
