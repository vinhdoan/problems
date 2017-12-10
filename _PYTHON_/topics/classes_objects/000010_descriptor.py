#!/usr/bin/env python3


# Descriptor attribute for an integer type-checked attribute
class Integer:
    '''
    Define a type with extra functionality
    '''
    def __init__(self, name):
        print("Integer init", self, name)
        self.name = name

    def __get__(self, instance, cls):
        print("Get", self, instance, cls)
        if instance is None:
            return self
        else:
            return instance.__dict__[self.name]

    def __set__(self, instance, value):
        print("Set", self, instance, value)
        if not isinstance(value, int):
            raise TypeError('Expected an int')
        instance.__dict__[self.name] = value

    def __delete__(self, instance):
        print("Delete", self, instance)
        del instance.__dict__[self.name]


class Point:
    '''
    Class with attributes which utilize new type
    '''
    x = Integer('x')
    y = Integer('y')

    def __init__(self, x, y):
        print("Point init", self, x, y)
        self.x = x
        self.y = y


if __name__ == '__main__':
    print("* MAIN")
    p = Point(2, 3)
    print("* Get x of class:")
    print(Point.x)
    print("* Get x of instance:")
    print(p.x)
    print("* Set x of instance:")
    p.x = 2.3
