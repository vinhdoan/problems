# https://www.hackerrank.com/challenges/s10-weighted-mean/problem

n = int(input())
X = list(map(int, input().strip().split(' ')))
W = list(map(int, input().strip().split(' ')))
sumWX = sum([w * x for w, x in zip(W, X)])
print(round((sumWX / sum(W)), 1))
