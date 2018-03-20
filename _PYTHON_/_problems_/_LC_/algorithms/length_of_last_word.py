# https://leetcode.com/problems/length-of-last-word/description/


class Solution:
    def lengthOfLastWord(self, s):
        """
        :type s: str
        :rtype: int
        """

        size = len(s)

        # no chars
        if size == 0: return 0

        # find first non-space char
        i = size - 1
        while i >= 0 and s[i] == ' ':
            i -= 1

        # space chars only
        if i == -1: return 0

        count = 0
        while i >= 0 and s[i] != ' ':
            count += 1
            i -= 1

        return count
