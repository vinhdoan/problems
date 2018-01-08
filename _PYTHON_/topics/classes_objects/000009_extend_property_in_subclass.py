#!/usr/bin/env python3


class Person:
    def __init__(self, name):
        self.name = name

    # Getter function
    @property
    def name(self):
        return self._name

    # Setter function
    @name.setter
    def name(self, value):
        if not isinstance(value, str):
            raise TypeError('Expected a string')
        self._name = value

    # Deleter function
    @name.deleter
    def name(self):
        raise AttributeError("Can't delete attribute")


class SubPerson(Person):
    @property
    def name(self):
        print('Getting name')
        return super().name

    @name.setter
    def name(self, value):
        print('Setting name to', value)
        super(SubPerson, SubPerson).name.__set__(self, value)
        # super().name.__set__(value)  # WRONG
        # Explanation:
        # - super(SubPerson, SubPerson).name accesses class variable 'name'
        # - super().name <=> super(SubPerson, self).name accesses Person
        #   property 'name' getter

    @name.deleter
    def name(self):
        print('Deleting name')
        super(SubPerson, SubPerson).name.__delete__(self)
        # super().name.__delete__()  # WRONG
        # Explanation: same as setter above
