# https://www.geeksforgeeks.org/find-minimum-element-in-a-sorted-and-rotated-array/


class Solution:
    def find(self, nums, target):
        """
        :type nums: List[int]
        :type target: int
        :rtype: int
        """

        size = len(nums)

        l, r = 0, size-1
        while l < r:
            m = l + (r-l)//2
            if nums[m] > nums[m+1]: break

            if nums[m] >= nums[l]: l = m+1
            elif nums[m] < nums[l]: r = m-1

        # m is index of max value
        if l == r: m = r

        r, l = m, (m+1) % size
        print(l, r)  # l, r: indexes of min and max value
