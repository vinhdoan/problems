# https://leetcode.com/problems/number-of-lines-to-write-string/description/


class Solution:
    def numberOfLines(self, widths, S):
        """
        :type widths: List[int]
        :type S: str
        :rtype: List[int]
        """

        line = 1
        rem = 100
        for c in S:
            space = widths[ord(c) - ord('a')]
            if space > rem:
                line += 1
                rem = 100 - space
            else:
                rem -= space

        return line, 100 - rem
