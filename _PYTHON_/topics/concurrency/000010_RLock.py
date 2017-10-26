#!/usr/bin/env python3

import threading


class SharedCounter:
    '''
    - A counter object that can be shared by multiple threads.
    - Specifically, this lock ensures that only one thread is
    allowed to be using the methods of the class at once.
    - However, it is OK for methods that already have the lock to
    call other methods that also use the lock.
    '''
    _lock = threading.RLock()

    def __init__(self, initial_value=0):
        self._value = initial_value

    def incr(self, delta=1):
        '''
        Increment the counter with locking
        '''
        with SharedCounter._lock:
            self._value += delta

    def decr(self, delta=1):
        '''
        Decrement the counter with locking
        '''
        with SharedCounter._lock:
            self.incr(-delta)
