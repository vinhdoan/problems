def ex(base):
    R = n = 1
    while True:
        R *= base
        print("%d: %d" % (n, R))
        n += 1
        input()