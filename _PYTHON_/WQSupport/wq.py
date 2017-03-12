# Calculate base^n (n = 0,1,2,...)
# Example: lib.ex(2015)
         # Press "q" to exit
def ex(Base):
    R = N = 1
    IsStopped = False
    while not IsStopped:
        R *= Base
        print("%d: %d" % (N, R))
        N += 1
        IsStopped = input() == "q"