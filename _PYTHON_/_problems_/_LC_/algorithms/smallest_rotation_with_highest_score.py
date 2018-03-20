# https://leetcode.com/problems/smallest-rotation-with-highest-score/description/


class Solution:
    def bestRotation(self, A):
        """
        :type A: List[int]
        :rtype: int
        """

        N = len(A)
        good = [0] * N

        for i, elem in enumerate(A):
            left = (i - N + 1) % N
            right = (i - elem + 1) % N

            good[left] += 1
            good[right] -= 1

            if left > right:
                good[0] += 1

        best = 0
        ans = score = 0
        for i, e in enumerate(good):
            score += e
            if score > best:
                best = score
                ans = i

        return ans
