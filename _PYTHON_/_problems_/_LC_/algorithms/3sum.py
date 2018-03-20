# https://leetcode.com/problems/3sum/description/


class Solution:
    def threeSum(self, nums):
        """
        :type nums: List[int]
        :rtype: List[List[int]]
        """
        length = len(nums)
        result = []
        nums.sort()

        for i in range(length-2):  # left 2 positions for indexes l, r
            if i > 0 and nums[i] == nums[i-1]:
                continue
            l, r = i+1, length-1
            while l < r:
                total = nums[i] + nums[l] + nums[r]
                if total == 0:
                    result.append((nums[i], nums[l], nums[r]))
                    while l < r and nums[l] == nums[l+1]:
                        l += 1
                    while l < r and nums[r] == nums[r-1]:
                        r -= 1
                    l += 1
                    r -= 1
                elif total < 0:
                    l += 1
                else:
                    r -= 1
        return result
