# https://leetcode.com/problems/permutations/description/


class Solution:
    def permute(self, nums):
        """
        :type nums: List[int]
        :rtype: List[List[int]]
        """

        if nums == []: return [[]]

        ans = []
        for i in range(len(nums)):
            h = nums[i]
            rl = nums[:i] + nums[i+1:]
            for t in self.permute(rl):
                ans += [[h] + t]

        return ans
