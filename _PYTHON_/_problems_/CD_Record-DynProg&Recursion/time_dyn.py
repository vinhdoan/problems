def time_dyn(n, t1, t2):
    TimeMatrix = [[0] * (t2+1) for k in range(t1+1)]
    LastMatrix = [[-1] * (t2+1) for k in  range(t1+1)]
    for i in range(n):
        for j1 in range(t1+1):
            for j2 in range(t2+1):
                Time = TimeMatrix[j1][j2]
                Last = LastMatrix[j1][j2]
                print "i: " + str(i) + ", j1: " + str(j1) + ", j2: " + str(j2)
                print "Time: " + str(Time) + ", Last: " + str(Last)
                if j1-d[i] >= 0:
                    Last1 = LastMatrix[j1-d[i]][j2]
                    if Last1 != i:
                        Time1 = TimeMatrix[j1-d[i]][j2] + d[i]
                        if Time < Time1 and Time1 <= t1:
                            TimeMatrix[j1][j2] = Time1
                    else:
                        TimeMatrix[j1][j2] = TimeMatrix[j1-d[i]][j2]
                    LastMatrix[j1][j2] = i
                    print "> NewTime1: " + str(TimeMatrix[j1][j2]) + ", NewLast1: " + str(LastMatrix[j1][j2])
                if j2-d[i] >= 0:
                    Last2 = LastMatrix[j1][j2-d[i]]
                    if Last2 != i:
                        Time2 = TimeMatrix[j1][j2-d[i]] + d[i]
                        if Time < Time2 and Time2 <= t1:
                            TimeMatrix[j1][j2] = Time2
                    else:
                        TimeMatrix[j1][j2] = TimeMatrix[j1][j2-d[i]]
                    LastMatrix[j1][j2] = i
                    print "> NewTime2: " + str(TimeMatrix[j1][j2]) + ", NewLast2: " + str(LastMatrix[j1][j2])
                print
    return TimeMatrix[t1-1][t2-1]

d = [15,16,22,25]
print time_dyn(4, 40, 40)
