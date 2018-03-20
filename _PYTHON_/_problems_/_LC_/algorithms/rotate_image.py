# https://leetcode.com/problems/rotate-image/description/


class Solution:
    def rotate(self, matrix):
        """
        :type matrix: List[List[int]]
        :rtype: void Do not return anything, modify matrix in-place instead.
        """

        # SOLUTION 1: Not in-place
        # i = 0
        # for e in zip(*matrix[::-1]):
        #     matrix[i] = list(e)
        #     i += 1

        # SOLUTION 2: In-place
        n = len(matrix)

        for i in range(n // 2):
            for j in range(i, n-1-i):
                tmp = matrix[i][j]
                matrix[i][j] = matrix[n-1-j][i]
                matrix[n-1-j][i] = matrix[n-1-i][n-1-j]
                matrix[n-1-i][n-1-j] = matrix[j][n-1-i]
                matrix[j][n-1-i] = tmp
