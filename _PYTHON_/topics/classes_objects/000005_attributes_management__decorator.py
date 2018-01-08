#!/usr/bin/env python3


class Person:
    def __init__(self, first_name):
        self.first_name = first_name

    # Getter function
    @property
    def first_name(self):
        return self._first_name

    # Setter function
    @first_name.setter
    def first_name(self, value):
        if not isinstance(value, str):
            raise TypeError('Expected a string')
        self._first_name = value

    # Deleter function (optional)
    @first_name.deleter
    def first_name(self):
        raise AttributeError("Can't delete attribute")


# NOTE: first_name must be established as a property using @property
# >>> a = Person('Guido')
# >>> a.first_name # Calls the getter
# 'Guido'
# >>> a.first_name = 42 # Calls the setter
# Traceback (most recent call last):
# File "<stdin>", line 1, in <module>
# File "prop.py", line 14, in first_name
# raise TypeError('Expected a string')
# TypeError: Expected a string
# >>> del a.first_name
# Traceback (most recent call last):
# File "<stdin>", line 1, in <module>
# AttributeError: can't delete attribute
# >>>
