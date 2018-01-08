#!/usr/bin/env python3


# Encode each operational state as a separate class and
# arrange for the Connection class to delegate to the state class

class Connection:
    def __init__(self):
        self.new_state(ClosedConnection)

    def new_state(self, newstate):
        self.__class__ = newstate

    def read(self):
        raise NotImplementedError()

    def write(self, data):
        raise NotImplementedError()

    def open(self):
        raise NotImplementedError()

    def close(self):
        raise NotImplementedError()


class ClosedConnection(Connection):
    def read(self):
        raise RuntimeError('Not open')

    def write(self, data):
        raise RuntimeError('Not open')

    def open(self):
        self.new_state(OpenConnection)

    def close(self):
        raise RuntimeError('Already closed')


class OpenConnection(Connection):
    def read(self):
        print('reading')

    def write(self, data):
        print('writing')

    def open(self):
        raise RuntimeError('Already open')

    def close(self):
        self.new_state(ClosedConnection)

# >>> c = Connection()
# >>> c
# <__main__.ClosedConnection object at 0x1006718d0>
# >>> c.read()
# Traceback (most recent call last):
# File "<stdin>", line 1, in <module>
# File "state.py", line 15, in read
# raise RuntimeError('Not open')
# RuntimeError: Not open
# >>> c.open()
# >>> c
# <__main__.OpenConnection object at 0x1006718d0>
# >>> c.read()
# reading
# >>> c.close()
# >>> c
# <__main__.ClosedConnection object at 0x1006718d0>
# >>>
