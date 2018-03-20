# https://leetcode.com/problems/two-sum/description/


class Solution:
    def twoSum(self, nums, target):
        """
        :type nums: List[int]
        :type target: int
        :rtype: List[int]
        """

        length = len(nums)

        # BRUTE FORCE
        # for i in range(length):
        #     for j in range(i+1, length):
        #         if nums[i] + nums[j] == target:
        #             return [i, j]

        # HASH MAP
        d = {}
        for i in range(length):
            complement = target - nums[i]
            j = d.get(complement)
            if j is not None:
                return [j, i]
            d[nums[i]] = i
