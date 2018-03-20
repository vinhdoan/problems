# https://leetcode.com/problems/longest-palindromic-substring/description/


class Solution:

    @staticmethod
    def expandPalindrome(s, i, j):
        r = 0
        while i-r >= 0 and j+r < len(s) and s[i-r] == s[j+r]:
            r += 1
        return (j+r) - (i-r) - 1

    def longestPalindrome(self, s):
        """
        :type s: str
        :rtype: str
        """

        """
        |<----length---->|     (odd length)
        s       <i>      e

        |<----length---->|     (even length)
        s     <i> i'     e

        length = e - s + 1
        i = (e + s) // 2 <= (e + s) / 2
        -> e + s >= 2i

        -> e >= i + (length - 1)/2
           s >= i - (length - 1)/2

        About e:
           x/2 >= x//2 >= (x-1)/2
        -> i + x//2 >= i + (x-1)/2
        -> e = i + length//2

        About s:
           (x-1)/2 >= (x-1)//2 >= (x-2)/2
        -> i - (x-1)//2 >= i - (x-1)/2
        -> s = i - (length-1)//2
        """

        length = len(s)
        start = end = 0
        for i in range(length):
            len1 = self.expandPalindrome(s, i, i)
            len2 = self.expandPalindrome(s, i, i+1)
            length = max(len1, len2)
            if length > end - start:
                start = i - (length-1)//2
                end = i + length//2
        return s[start:end+1]
