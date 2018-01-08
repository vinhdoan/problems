#!/usr/bin/env python3


class Person:
    def __init__(self, first_name):
        self.set_first_name(first_name)

    # Getter function
    def get_first_name(self):
        return self._first_name

    # Setter function
    def set_first_name(self, value):
        if not isinstance(value, str):
            raise TypeError('Expected a string')
        self._first_name = value

    # Deleter function (optional)
    def del_first_name(self):
        raise AttributeError("Can't delete attribute")

    # Make a property from existing get/set methods
    name = property(get_first_name, set_first_name, del_first_name)


# >>> Person.name.fget
# <function Person.get_first_name at 0x7fad532f3400>
# >>> Person.name.fset
# <function Person.set_first_name at 0x7fad532f3488>
# >>> Person.name.fdel
# <function Person.del_first_name at 0x7fad532f3510>
# >>>

# >>> a = Person('tuan')
# >>> Person.name.fget(a)
# 'tuan'
# >>>
