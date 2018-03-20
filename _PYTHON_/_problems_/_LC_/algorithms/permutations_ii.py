# https://leetcode.com/problems/permutations-ii/description/


class Solution:
    def permuteUnique(self, nums):
        """
        :type nums: List[int]
        :rtype: List[List[int]]
        """

        if nums == []: return [[]]

        nums.sort()

        ans = []
        for i in range(len(nums)):
            if i > 0 and nums[i] == nums[i-1]: continue
            h = nums[i]
            rl = nums[:i] + nums[i+1:]
            for t in self.permuteUnique(rl):
                ans += [[h] + t]

        return ans
