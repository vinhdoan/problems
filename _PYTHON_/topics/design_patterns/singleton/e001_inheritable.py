class Singleton():
    # Derived classes have their own '_instance'
    _instance = None

    def __new__(cls, *args, **kwargs):
        if '_instance' not in cls.__dict__:
            cls._instance = super().__new__(cls)
        return cls._instance


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
