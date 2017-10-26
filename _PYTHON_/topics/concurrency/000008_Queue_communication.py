#!/usr/bin/env python3

from queue import Queue
from threading import Thread
import time


# A thread that produces data
def producer(out_q):
    while flag:
        # Produce some data
        data = "some string"
        out_q.put(data)


# A thread that consumes data
def consumer(in_q):
    while flag:
        # Get some data
        data = in_q.get()
        # Process the data
        print("Got: " + data)


# Create the shared queue and launch both threads
q = Queue()
flag = True
t1 = Thread(target=consumer, args=(q,))
t2 = Thread(target=producer, args=(q,))
t1.start()
t2.start()
time.sleep(1)
flag = False
