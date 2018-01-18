class MonoState:

    _state = {}

    def __new__(cls, *args, **kwargs):
        self = super().__new__(cls)
        # All instance dictionaries refer to class variable '_state'
        self.__dict__ = cls._state
        return self


class A(MonoState):
    pass


a = A()
a.x = 10
b = A()
# Different instances share the same state
print(b.x, id(a), id(b))
