# https://leetcode.com/problems/rotate-string/description/


class Solution:
    def rotateString(self, A, B):
        """
        :type A: str
        :type B: str
        :rtype: bool
        """

        A2 = A + A
        len_A2 = len(A2)
        len_B = len(B)

        for i in range(len_A2-len_B+1):
            if A2[i:i+len_B] == B:
                return True

        return False
