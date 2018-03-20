# https://leetcode.com/problems/remove-duplicates-from-sorted-array/description/


class Solution:
    def removeDuplicates(self, nums):
        """
        :type nums: List[int]
        :rtype: int
        """

        num_dict = {}
        count = 0

        index = 0
        for i in nums:
            if num_dict.get(i) is None:
                num_dict[i] = True
                count += 1
                nums[index] = i
                index += 1

        return count
