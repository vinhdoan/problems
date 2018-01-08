#!/usr/bin/env python3

from abc import ABCMeta, abstractmethod


# Note that @abstractmethod appears immediately before the function definition
class IStream(metaclass=ABCMeta):
    @abstractmethod
    def read(self, maxbytes=-1):
        pass

    @abstractmethod
    def write(self, data):
        pass

    @property
    @abstractmethod
    def name(self):
        pass

    @name.setter
    @abstractmethod
    def name(self, value):
        pass

    @classmethod
    @abstractmethod
    def method1(cls):
        pass

    @staticmethod
    @abstractmethod
    def method2():
        pass


if __name__ == '__main__':
    # An abstract base class cannot be instantiated directly
    # a = IStream()  # TypeError: Can't instantiate abstract class

    # ABCs allow other classes to be registered as implementing the required interface
    import io
    # Register the built-in I/O classes as supporting our interface
    IStream.register(io.IOBase)
    # Open a normal file and type check
    f = open('foo.txt')
    isinstance(f, IStream)  # Returns True
