# https://leetcode.com/problems/search-insert-position/description/


class Solution:
    def searchInsert(self, nums, target):
        """
        :type nums: List[int]
        :type target: int
        :rtype: int
        """

        size = len(nums)
        l, r = 0, size

        while l < r:
            m = (l + r) // 2
            if nums[m] >= target: r = m
            else: l = m + 1

        return l
