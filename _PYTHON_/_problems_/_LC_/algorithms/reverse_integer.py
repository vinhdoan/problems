# https://leetcode.com/problems/reverse-integer/description/


class Solution:
    def reverse(self, x):
        """
        :type x: int
        :rtype: int
        """

        if x == 0:
            return 0

        sign = 1
        if x < 0:
            sign = -1
            x = -x

        num = 0
        while x:
            num = (x % 10) + num * 10
            x //= 10

        res = num * sign
        if(abs(res) > (2 ** 31 - 1)):
            return 0
        return res
