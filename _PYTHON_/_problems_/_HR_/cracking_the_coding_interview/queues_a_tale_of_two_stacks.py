# https://www.hackerrank.com/challenges/ctci-queue-using-two-stacks/problem


class MyQueue(object):
    def __init__(self):
        self._stackIn = []
        self._stackOut = []

    def peek(self):
        self.do_transfer()
        return self._stackOut[-1]

    def pop(self):
        self.do_transfer()
        self._stackOut.pop()

    def put(self, value):
        self._stackIn.append(value)

    def do_transfer(self):
        if not self._stackOut:
            while self._stackIn:
                e = self._stackIn.pop()
                self._stackOut.append(e)


queue = MyQueue()
t = int(input())
for line in range(t):
    values = map(int, input().split())
    values = list(values)
    if values[0] == 1:
        queue.put(values[1])
    elif values[0] == 2:
        queue.pop()
    else:
        print(queue.peek())
