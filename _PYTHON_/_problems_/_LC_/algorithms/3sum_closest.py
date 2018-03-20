# https://leetcode.com/problems/3sum-closest/description/


import math


class Solution:
    def threeSumClosest(self, nums, target):
        """
        :type nums: List[int]
        :type target: int
        :rtype: int
        """

        size = len(nums)

        nums.sort()

        result = math.inf
        for i in range(size-2):
            l = i+1
            r = size-1
            while l < r:
                s = nums[i] + nums[l] + nums[r]
                if s == target:
                    return target
                if s > target:
                    r -= 1
                else:
                    l += 1
                if abs(s-target) < abs(result-target):
                    result = s

        return result
