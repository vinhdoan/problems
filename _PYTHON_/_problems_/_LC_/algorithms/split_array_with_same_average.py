# https://leetcode.com/problems/split-array-with-same-average/description/


class Solution:
    def splitArraySameAverage(self, A):
        """
        :type A: List[int]
        :rtype: bool
        """

        def exist(tosum, item_count, arr):
            if item_count == 0:
                return False if tosum else True
            if item_count > len(arr) or not arr:
                return False
            if exist(tosum-arr[0], item_count-1, arr[1:]) or exist(tosum, item_count, arr[1:]):
                return True

            return False


        if len(A) == 1: return False
        global_avg = sum(A) / float(len(A))
        for lenB in range(1, len(A) // 2+1):
            if (lenB * global_avg).is_integer():
                if exist(lenB * global_avg, lenB, A):
                    return True

        return False
