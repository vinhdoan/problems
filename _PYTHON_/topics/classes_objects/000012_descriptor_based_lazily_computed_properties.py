#!/usr/bin/env python3

import math


class lazyproperty:
    def __init__(self, func):
        self.func = func

    def __get__(self, instance, cls):
        if instance is None:
            return self
        else:
            value = self.func(instance)
            setattr(instance, self.func.__name__, value)
            return value


class Circle:
    def __init__(self, radius):
        self.radius = radius

    @lazyproperty
    def area(self):
        print('Computing area')
        return math.pi * self.radius ** 2

    @lazyproperty
    def perimeter(self):
        print('Computing perimeter')
        return 2 * math.pi * self.radius


if __name__ == '__main__':
    c = Circle(4.0)
    print("* GET INSTANCE VARIABLES:", vars(c))
    # -> {'radius': 4.0}
    print("* COMPUTE AREA:")
    print(c.area)
    # -> Computing area
    #    50.26548245743669
    print("* CHECK VARIABLES AFTER COMPUTING:", vars(c))
    # -> {'area': 50.26548245743669, 'radius': 4.0}
    print("* COMPUTE AREA AGAIN:", c.area)
    # -> 50.26548245743669
    print("* DELETE VARIABLE")
    del c.area
    print("* CHECK VARIABLES AFTER DELETING:", vars(c))
    print("* COMPUTE AREA AGAIN:")
    print(c.area)
