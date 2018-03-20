# https://leetcode.com/problems/next-permutation/description/


class Solution:
    def nextPermutation(self, nums):
        """
        :type nums: List[int]
        :rtype: void Do not return anything, modify nums in-place instead.
        """

        size = len(nums)
        if size < 2: return

        # find index i with nums[i:] is longest non-increasing list
        i = size - 1
        while nums[i] <= nums[i-1]:
            i -= 1
            if i == 0:
                nums.sort()
                return

        # pivot is the index before above list
        pivot = i - 1

        # find j where value is the first one bigger than pivot value from the right
        j = size - 1
        while nums[j] <= nums[pivot]:
            j -= 1

        # swap it with pivot
        nums[pivot], nums[j] = nums[j], nums[pivot]

        # sort tail
        nums[pivot+1:] = sorted(nums[pivot+1:])
