# https://leetcode.com/problems/longest-valid-parentheses/description/


class Solution:
    def longestValidParentheses(self, s):
        """
        :type s: str
        :rtype: int
        """

        '''
        0                                               size-1
        |--------------------------------------------------|
        counter ( : ll ->                  <- rl : counter (
        counter ) : lr ->                  <- rr : counter )
        '''
        size = len(s)

        result = 0
        ll = lr = rr = rl = 0

        for i in range(size):

            if s[i] == '(': ll += 1
            else: lr += 1

            if ll == lr: result = max(ll+lr, result)
            elif ll < lr: ll = lr = 0

            if s[size-1-i] == '(': rl += 1
            else: rr += 1

            if rl == rr: result = max(rl+rr, result)
            elif rl > rr: rl = rr = 0

        return result
