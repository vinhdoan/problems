# https://leetcode.com/problems/container-with-most-water/description/


class Solution:
    def maxArea(self, height):
        """
        :type height: List[int]
        :rtype: int
        """

        l, r = 0, len(height) - 1
        max_water = 0
        while l < r:
            water = (r - l) * min(height[r], height[l])
            if water > max_water:
                max_water = water
            if height[l] < height[r]:
                l += 1
            else:
                r -= 1
        return max_water
