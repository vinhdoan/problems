#!/usr/bin/env python3


def print_actor():
    try:
        while True:
            msg = yield  # Get a message
            print('Got:', msg)
    except GeneratorExit:
        print('Actor terminating')


# Sample use
p = print_actor()
next(p)  # Advance to the yield (ready to receive)
p.send('Hello')
p.send('World')
p.close()
