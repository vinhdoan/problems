# https://leetcode.com/problems/zigzag-conversion/description/


class Solution:
    def convert(self, s, numRows):
        """
        :type s: str
        :type numRows: int
        :rtype: str
        """

        if s == '':
            return s
        if numRows == 1:
            return s
        l = [[] for _i in range(numRows)]
        i = 0
        d = True  # True for down direction, False for up direction
        for c in s:
            l[i].append(c)
            if i == 0:
                i += 1
                d = True
            elif i == numRows - 1:
                i -= 1
                d = False
            else:
                if d: i += 1
                else: i -= 1
        return ''.join([char for row in l for char in row])
