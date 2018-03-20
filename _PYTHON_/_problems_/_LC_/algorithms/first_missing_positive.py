# https://leetcode.com/problems/first-missing-positive/description/


class Solution:
    def firstMissingPositive(self, nums):
        """
        :type nums: List[int]
        :rtype: int
        """

        N = len(nums)

        # 'left' will be the position of 1st positive integer after swapping
        left = 0
        for i in range(N):
            if nums[i] <= 0:
                nums[i], nums[left] = nums[left], nums[i]
                left += 1

        # reset 'nums' to contain positive integers only
        nums = nums[left:]
        N = len(nums)

        # if there is a value 'num' in the list
        # set the value at corresponding position abs(num)-1 to negative
        # (absolute value not changed)
        for num in nums:
            pos = abs(num) - 1
            if pos < N: nums[pos] = -abs(nums[pos])

        i = 0
        while i < N:
            if nums[i] > 0:
                return i + 1
            i += 1
        return i + 1
