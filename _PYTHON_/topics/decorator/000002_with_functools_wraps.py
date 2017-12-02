#!/usr/bin/env python3

from typing import Callable
from functools import wraps


def dcr1(func: Callable) -> Callable:
    '''
    Decorator docstring
    '''
    @wraps(func)
    def w(*args, **kwargs):
        '''
        Wrapper docstring
        '''
        print("Hello", func(*args, **kwargs))
    return w


def dcr2(msg: str) -> Callable:
    def dcr(func: Callable) -> Callable:
        '''
        Decorator docstring
        '''
        @wraps(func)
        def w(*args, **kwargs):
            '''
            Wrapper docstring
            '''
            print("Hello", func(*args, **kwargs), msg)
        return w
    return dcr


# Decorator w/o argument
@dcr1
def tuan():
    '''
    tuan() docstring
    '''
    return "Tuan!"


# Decorator w/ argument
@dcr2("Tuan's nickname")
def mp():
    '''
    mp() docstring
    '''
    return "MP!"


tuan()
mp()

print("Module name:", tuan.__name__)
print("Docstring:", tuan.__doc__)
print("Call unwrapped function:", tuan.__wrapped__())
