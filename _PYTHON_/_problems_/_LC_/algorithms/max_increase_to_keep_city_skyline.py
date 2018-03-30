# https://leetcode.com/problems/max-increase-to-keep-city-skyline/description/


class Solution:
    def maxIncreaseKeepingSkyline(self, grid):
        """
        :type grid: List[List[int]]
        :rtype: int
        """

        size_r = len(grid)
        size_c = len(grid[0])

        max_r = [0] * size_r
        max_c = [0] * size_c

        for i, row in enumerate(grid):
            max_r[i] = max(row)

        for j, col in enumerate(zip(*grid)):
            max_c[j] = max(col)

        count = 0
        for i in range(size_r):
            for j in range(size_c):
                count += min(max_r[i], max_c[j]) - grid[i][j]

        return count
