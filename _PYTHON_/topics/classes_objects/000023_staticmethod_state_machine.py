#!/usr/bin/env python3


# Encode each operational state as a separate class and
# arrange for the Connection class to delegate to the state class

class Connection:
    def __init__(self):
        self.new_state(ClosedConnectionState)

    def new_state(self, newstate):
        self._state = newstate

    # Delegate to the state class
    def read(self):
        return self._state.read(self)

    def write(self, data):
        return self._state.write(self, data)

    def open(self):
        return self._state.open(self)

    def close(self):
        return self._state.close(self)


# Connection state base class
class ConnectionState:
    @staticmethod
    def read(conn):
        raise NotImplementedError()

    @staticmethod
    def write(conn, data):
        raise NotImplementedError()

    @staticmethod
    def open(conn):
        raise NotImplementedError()

    @staticmethod
    def close(conn):
        raise NotImplementedError()


# Implementation of different states
class ClosedConnectionState(ConnectionState):
    @staticmethod
    def read(conn):
        raise RuntimeError('Not open')

    @staticmethod
    def write(conn, data):
        raise RuntimeError('Not open')

    @staticmethod
    def open(conn):
        conn.new_state(OpenConnectionState)

    @staticmethod
    def close(conn):
        raise RuntimeError('Already closed')


class OpenConnectionState(ConnectionState):
    @staticmethod
    def read(conn):
        print('reading')

    @staticmethod
    def write(conn, data):
        print('writing')

    @staticmethod
    def open(conn):
        raise RuntimeError('Already open')

    @staticmethod
    def close(conn):
        conn.new_state(ClosedConnectionState)


# >>> c = Connection()
# >>> c._state
# <class '__main__.ClosedConnectionState'>
# >>> c.read()
# Traceback (most recent call last):
# File "<stdin>", line 1, in <module>
# File "example.py", line 10, in read
# return self._state.read(self)
# File "example.py", line 43, in read
# raise RuntimeError('Not open')
# RuntimeError: Not open
# >>> c.open()
# >>> c._state
# <class '__main__.OpenConnectionState'>
# >>> c.read()
# reading
# >>> c.write('hello')
# writing
# >>> c.close()
# >>> c._state
# <class '__main__.ClosedConnectionState'>
# >>>
