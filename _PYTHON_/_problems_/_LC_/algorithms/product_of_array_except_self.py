# https://leetcode.com/problems/product-of-array-except-self/description/


class Solution:
    def productExceptSelf(self, nums):
        """
        :type nums: List[int]
        :rtype: List[int]
        """

        length = len(nums)
        prodl = prodr = 1
        left_list, right_list = [], []

        for i in range(length):
            left_list.append(prodl)
            right_list.append(prodr)
            prodl *= nums[i]
            prodr *= nums[length - 1 - i]

        result = []
        for i in range(length):
            result.append(left_list[i] * right_list[length - 1 - i])

        return result
