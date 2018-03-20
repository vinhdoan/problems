# https://leetcode.com/problems/jump-game-ii/description/


class Solution:
    def jump(self, nums):
        """
        :type nums: List[int]
        :rtype: int
        """

        size = len(nums)
        if size <= 1: return 0

        # at least 2 positions in the list
        max_reach = nums[0]  # max index which can be reached
        steps = nums[0]      # remained steps to reach the max_reach
        jumps = 1            # number of jumps so far (at least 1 jump)

        # iterate from position 1 to position next to the last
        for i in range(1, size-1):
            max_reach = max(max_reach, i + nums[i])
            steps -= 1
            if steps == 0:
                if i == max_reach: return -1  # current position is the max_reach while there are still more
                jumps += 1                    # next time of jumps
                steps = max_reach - i         # reset steps to reach the max_reach

        # return jumps when i reach the last position
        return jumps
