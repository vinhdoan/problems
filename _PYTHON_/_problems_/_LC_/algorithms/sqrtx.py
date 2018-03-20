# https://leetcode.com/problems/sqrtx/description/


class Solution:
    def mySqrt(self, x):
        """
        :type x: int
        :rtype: int
        """

        l, r = 1, x

        while l <= r:
            m = (l + r) // 2
            p = m*m
            if p == x: return m
            if p < x: l = m + 1
            else: r = m - 1

        return r
