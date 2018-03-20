# https://leetcode.com/problems/median-of-two-sorted-arrays/description/


class Solution:
    def findMedianSortedArrays(self, nums1, nums2):
        """
        :type nums1: List[int]
        :type nums2: List[int]
        :rtype: float
        """

        l1, l2 = len(nums1), len(nums2)
        if l1 > l2:
            nums1, nums2, l1, l2 = nums2, nums1, l2, l1

        # Initiate for left part of nums1 (i elements) and left part of nums2 (j elements)
        # The target for i and j is that whole left part is less than whole right part
        #   (nums1) ...i... | ...l1 - i...      (i elements: nums1[0] ... nums1[i-1])
        #   (nums2) ...j... | ...l2 - j...      (j elements: nums2[0] ... nums2[j-1])
        #            <left> | <right>

        i_min, i_max = 0, l1
        while i_min <= i_max:
            i = (i_min + i_max) // 2
            j = (l1 + l2 + 1) // 2 - i  # assume that left part have equal or more elements than right part
                                        # j is non-negative then l1 must NOT be greater than l2
            if i >= 1 and nums1[i-1] > nums2[j]:    # i >= 1 -> j < l2, proved by math based on l1 <= l2
                i_max = i - 1
            elif i < l1 and nums2[j-1] > nums1[i]:  # i < l1 -> j > 0, proved by math based on l1 <= l2
                i_min = i + 1
            else:
                # Appropriate i, j found
                if i == 0:
                    max_left = nums2[j-1]
                elif j == 0:
                    max_left = nums1[i-1]
                else:
                    max_left = max(nums1[i-1], nums2[j-1])

                if (l1 + l2) % 2 == 1:
                    return max_left

                if i == l1:
                    min_right = nums2[j]
                elif j == l2:
                    min_right = nums1[i]
                else:
                    min_right = min(nums1[i], nums2[j])

                return (max_left + min_right) / 2
