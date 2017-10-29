#!/usr/bin/env python3

from threading import Thread, Semaphore


# Worker thread
def worker(n, sema):
    # Wait to be signaled
    sema.acquire()
    # Do some work
    print('Working', n)


# Create some threads
sema = Semaphore(0)
nworkers = 10
for n in range(nworkers):
    t = Thread(target=worker, args=(n, sema,))
    t.start()

[sema.release() for _ in range(nworkers)]
