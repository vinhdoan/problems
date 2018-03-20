# https://leetcode.com/problems/roman-to-integer/description/


class Solution:
    def romanToInt(self, s):
        """
        :type s: str
        :rtype: int
        """

        d = {'I': lambda x: 1 if x < 5 else -1,
             'V': lambda _x: 5,
             'X': lambda x: 10 if num < 50 else -10,
             'L': lambda _x: 50,
             'C': lambda x: 100 if num < 500 else -100,
             'D': lambda _x: 500,
             'M': lambda _x: 1000}
        num = 0
        i = len(s) - 1
        while i >= 0:
            num += d.get(s[i])(num)
            i -= 1

        return num
