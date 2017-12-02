#!/usr/bin/env python3

from queue import Queue
from functools import wraps


def apply_async(func, args, *, callback):
    # Compute the result
    result = func(*args)
    # Invoke the callback with the result
    callback(result)


class Async:
    def __init__(self, func, args):
        self.func = func
        self.args = args


def inlined_async(func):
    @wraps(func)
    def wrapper(*args):
        f = func(*args)
        print("Tuan f:", f)
        result_queue = Queue()
        result_queue.put(None)
        while True:
            result = result_queue.get()
            print("Tuan result:", result)
            try:
                a = f.send(result)
                print("Tuan a:", a)
                apply_async(a.func, a.args, callback=result_queue.put)
            except StopIteration:
                break
    return wrapper


def add(x, y):
    return x + y


@inlined_async
def test():
    r = yield Async(add, (2, 3))
    print(r)
    r = yield Async(add, ('hello', 'world'))
    print(r)
    for n in range(10):
        r = yield Async(add, (n, n))
        print(r)
    print('Goodbye')


if __name__ == '__main__':
    test()
