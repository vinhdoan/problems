#!/usr/bin/env python3


# Example on bad case
class A:
    def __del__(self):
        print("Sth del")


a = A()
del a  # "Sth del"


class B:
    def __del__(self):
        print("Sth del")

    def set(self, c):
        self.x = c


class C:
    def __del__(self):
        print("Sth del")

    def set(self, b):
        self.x = b


b, c = B(), C()
b.set(c)
c.set(b)
del b  # nothing showed, not deleted
del c  # nothing showed, not deleted


# Example on good case
import weakref


class Node:
    def __init__(self, value):
        self.value = value
        self._parent = None
        self.children = []

    def __repr__(self):
        return 'Node({!r:})'.format(self.value)

    # property that manages the parent as a weak-reference
    @property
    def parent(self):
        return self._parent if self._parent is None else self._parent()

    @parent.setter
    def parent(self, node):
        self._parent = weakref.ref(node)

    def add_child(self, child):
        self.children.append(child)
        child.parent = self


root = Node('parent')
c1 = Node('child')
root.add_child(c1)
print(c1.parent)  # Node('parent')
del root
print(c1.parent)  # None
