# https://leetcode.com/problems/count-and-say/description/


class Solution:
    def countAndSay(self, n):
        """
        :type n: int
        :rtype: str
        """

        # SOLUTION 1: Recursion
        # if n == 1: return '1'

        # prev = self.countAndSay(n-1)

        # ans = ''

        # count = 1
        # for i in range(1, len(prev)):
        #     if prev[i] == prev[i-1]:
        #         count += 1
        #     else:
        #         ans += str(count) + str(prev[i-1])
        #         count = 1
        # ans += str(count) + str(prev[-1])

        # return ans


        # SOLUTION 2: For loop
        prev = '1'
        for _j in range(n-1):
            ans = ''
            count = 1
            for i in range(1, len(prev)):
                if prev[i] == prev[i-1]: count += 1
                else:
                    ans += str(count) + str(prev[i-1])
                    count = 1
            ans += str(count) + str(prev[-1])
            prev = ans

        return prev
