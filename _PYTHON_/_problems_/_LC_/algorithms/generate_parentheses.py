# https://leetcode.com/problems/generate-parentheses/description/


class Solution:
    def generateParenthesis(self, n):
        """
        :type n: int
        :rtype: List[str]
        """

        def generate(string, left, right, result=[]):
            if left: generate(string + '(', left-1, right)
            if right > left: generate(string + ')', left, right-1)
            if not right: result.append(string)
            return result

        return generate('', n, n)
