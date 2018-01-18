from strategy.strategy_abc import AbsStrategy


class FedexStrategy(AbsStrategy):
    def calculate(self, order):
        return 3.00
