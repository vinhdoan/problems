def time(i, t1, t2):
    if t1 < 0 or t2 < 0:
        return -1
    if i == 0:
        return 0
    time1 = time(i-1, t1 - d[i], t2)
    time2 = time(i-1, t1, t2 - d[i])
    if (time1 == time2 == -1):
        return -1
    return max(time1, time2) + d[i]

d = [x,15,16,22,25]
print time(4, 40, 40)
