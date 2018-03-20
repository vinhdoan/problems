# https://leetcode.com/problems/guess-number-higher-or-lower/description/


# The guess API is already defined for you.
# @param num, your guess
# @return -1 if my number is lower, 1 if my number is higher, otherwise return 0
# def guess(num):

class Solution(object):
    def guessNumber(self, n):
        """
        :type n: int
        :rtype: int
        """

        left, right = 1, n
        while left <= right:
            number = (left + right) // 2
            response = guess(number)
            if response == -1:
                right = number - 1
            elif response == 1:
                left = number + 1
            else:
                return number
