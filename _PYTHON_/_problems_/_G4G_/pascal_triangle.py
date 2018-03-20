# https://www.geeksforgeeks.org/pascal-triangle/


def printPascal(n):
    line = 1
    while line <= n:
        C = 1
        i = 1
        while i <= line:
            print(C, end='')
            C = C * (line-i) // i
            i += 1
        print()
        line += 1
