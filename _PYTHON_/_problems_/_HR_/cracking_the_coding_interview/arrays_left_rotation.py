# https://www.hackerrank.com/challenges/ctci-array-left-rotation/problem


def array_left_rotation(a, n, k):
    m = k % n  # in case k > n
    return a[m:] + a[:m]


n, k = map(int, input().strip().split(' '))
a = list(map(int, input().strip().split(' ')))
answer = array_left_rotation(a, n, k)
print(*answer, sep=' ')
