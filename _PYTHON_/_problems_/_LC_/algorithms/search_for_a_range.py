# https://leetcode.com/problems/search-for-a-range/description/


class Solution:
    def searchRange(self, nums, target):
        """
        :type nums: List[int]
        :type target: int
        :rtype: List[int]
        """

        # Search for the first position whose value >= target
        def searchPosition(nums, target):
            size = len(nums)
            l, r = 0, size-1
            while l < r:
                m = (l + r) // 2
                if target <= nums[m]:
                    r = m
                else:
                    l = m + 1

            return l


        if len(nums) == 0: return (-1, -1)

        # Search for the 1st position of target
        l1 = searchPosition(nums, target)

        # Search for the 1st position of value greater than target,
        # then minus 1 to get the last position of target.
        # In case nums[l2] == target, such value hasn't been found,
        # and returned index is already the last position of target.
        l2 = searchPosition(nums, target+1)
        l2 = l2 if nums[l2] == target else l2 - 1

        print(l1, l2)

        return (l1, l2) if nums[l1] == target else (-1, -1)
