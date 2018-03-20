# https://leetcode.com/problems/maximum-subarray/description/


class Solution:
    def maxSubArray(self, nums):
        """
        :type nums: List[int]
        :rtype: int
        """

        size = len(nums)

        cur_sum = max_sum = nums[0]

        # If indexes are not required
        # for num in nums[1:]:
        #     cur_sum = max(cur_sum, cur_sum + num)
        #     max_sum = max(max_sum, cur_sum)
        # return max_sum

        max_l = max_r = l = r = 0

        for i in range(1, size):
            if cur_sum <= 0:
                cur_sum = nums[i]
                l = i
            else:
                cur_sum += nums[i]
            r = i
            if cur_sum > max_sum:
                max_l, max_r = l, r
                max_sum = cur_sum
        print(max_l, max_r)
        return max_sum
