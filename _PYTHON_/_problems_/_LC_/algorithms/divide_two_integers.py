# https://leetcode.com/problems/divide-two-integers/description/


class Solution:
    def divide(self, dividend, divisor):
        """
        :type dividend: int
        :type divisor: int
        :rtype: int
        """

        MIN_INT = -2147483648
        MAX_INT = 2147483647

        positive = (dividend < 0) is (divisor < 0)
        dividend, divisor = abs(dividend), abs(divisor)

        # dividend = divisor * (2^N + 2^(N-1) + ... + 1) + r  (r < divisor)
        #                      <--------- res --------->
        res = 0
        while dividend >= divisor:
            temp, i = divisor, 1
            while dividend >= temp:
                i <<= 1
                temp <<= 1
            dividend -= temp>>1
            res += i>>1
        if not positive:
            res = -res
        return min(max(MIN_INT, res), MAX_INT)
