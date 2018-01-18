import abc


class AbsStrategy(metaclass=abc.ABCMeta):

    @abc.abstractmethod
    def calculate(self, order):
        """ Calculate shipping cost """
        pass
