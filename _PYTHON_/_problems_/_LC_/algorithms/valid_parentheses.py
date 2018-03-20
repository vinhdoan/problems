# https://leetcode.com/problems/valid-parentheses/description/


class Solution:
    def isValid(self, s):
        """
        :type s: str
        :rtype: bool
        """

        matching = {
                    '(': ')',
                    '{': '}',
                    '[': ']',
                   }

        stack = []
        for c in s:
            if c in matching.keys(): stack.append(c)
            elif stack == []: return False
            elif matching[stack[-1]] == c: stack.pop()
            else: return False

        if stack: return False
        return True
