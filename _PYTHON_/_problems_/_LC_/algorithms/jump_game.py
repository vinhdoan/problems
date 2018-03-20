# https://leetcode.com/problems/jump-game/description/


class Solution:
    def canJump(self, nums):
        """
        :type nums: List[int]
        :rtype: bool
        """

        '''
        If current target cannot be reached,
        then last position cannot be reached also.
        '''

        size = len(nums)

        target = size - 1
        for i in range(size-2, -1, -1):
            if i + nums[i] >= target: target = i

        return target == 0
