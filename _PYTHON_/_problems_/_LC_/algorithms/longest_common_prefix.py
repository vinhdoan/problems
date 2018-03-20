# https://leetcode.com/problems/longest-common-prefix/description/


class Solution:
    def longestCommonPrefix(self, strs):
        """
        :type strs: List[str]
        :rtype: str
        """

        num = len(strs)
        if num == 0:
            return ''
        if num == 1:
            return strs[0]

        low = 0
        high = min(map(len, strs))

        s = strs[0]

        def all_contain_str(strs, string, start, end):
            return all([s[start:end+1] == string for s in strs])

        prefix = ''
        while low <= high:
            mid = low + (high - low) // 2
            if all_contain_str(strs[1:], s[low:mid+1], low, mid):
                prefix += s[low:mid+1]
                low = mid + 1
            else:
                high = mid - 1

        return prefix
