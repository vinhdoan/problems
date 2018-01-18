class Singleton():
    _instances = {}  # dict([cls, instance])

    def __new__(cls, *args, **kwargs):
        if cls not in cls._instances:
            instance = super().__new__(cls)
            cls._instances[cls] = instance
        return cls._instances[cls]


s1 = Singleton()
s2 = Singleton()
print("Same s:", s1 is s2, id(s1), id(s2))


class A(Singleton):
    pass


class B(A):
    pass


a1 = A(1)
a2 = A(1, 2)
b1 = B(1)
b2 = B(1, 2)
print("Same a:", a1 is a2, id(a1), id(a2))
print("Same b:", b1 is b2, id(b1), id(b2))
