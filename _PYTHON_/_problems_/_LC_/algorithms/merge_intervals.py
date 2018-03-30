# https://leetcode.com/problems/merge-intervals/description/


# Definition for an interval.
# class Interval:
#     def __init__(self, s=0, e=0):
#         self.start = s
#         self.end = e

class Solution:
    def merge(self, intervals):
        """
        :type intervals: List[Interval]
        :rtype: List[Interval]
        """

        # MY SOLUTION
        from collections import defaultdict

        d = defaultdict(int)
        for obj in intervals:
            d[obj.start] += 1
            d[obj.end] -= 1

        s = 0
        result = []
        for e in sorted(d.keys()):
            if s == 0: start = e
            s += d.get(e)
            if s == 0: result.append(Interval(start, e))

        return result

        # # BETTER SOLUTION
        # out = []
        # for i in sorted(intervals, key=lambda i: i.start):
        #     if out and i.start <= out[-1].end:
        #         out[-1].end = max(out[-1].end, i.end)
        #     else:
        #         out += i,
        # return out
