import abc


class AbsCommand(metaclass=abc.ABCMeta):

    @abc.abstractmethod
    def execute(self):
        pass
