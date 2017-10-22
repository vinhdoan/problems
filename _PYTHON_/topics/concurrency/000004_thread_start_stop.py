#!/usr/bin/env python3

# Code to execute in an independent thread
import time
from threading import Thread


def countdown(n):
    while n > 0:
        print('T-minus', n)
        n -= 1
        time.sleep(1)


# Create and launch a thread
t = Thread(target=countdown, args=(3,), daemon=True)
t.start()  # done immediately
print("All done.")  # program ends immediately
