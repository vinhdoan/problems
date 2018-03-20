# https://leetcode.com/problems/remove-element/description/


class Solution:
    def removeElement(self, nums, val):
        """
        :type nums: List[int]
        :type val: int
        :rtype: int
        """

        index = count = 0
        for i in nums:
            if i != val:
                nums[index] = i
                index += 1
                count += 1

        return count
