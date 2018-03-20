# https://leetcode.com/problems/integer-to-roman/description/


class Solution:
    def intToRoman(self, num):
        """
        :type num: int
        :rtype: str
        """

        i = ['', 'I', 'II', 'III', 'IV', 'V', 'VI', 'VII', 'VIII', 'IX']
        x = ['', 'X', 'XX', 'XXX', 'XL', 'L', 'LX', 'LXX', 'LXXX', 'XC']
        c = ['', 'C', 'CC', 'CCC', 'CD', 'D', 'DC', 'DCC', 'DCCC', 'CM']
        m = ['', 'M', 'MM', 'MMM']

        return m[num // 1000] + c[num % 1000 // 100] + x[num % 100 // 10] + i[num % 10]
