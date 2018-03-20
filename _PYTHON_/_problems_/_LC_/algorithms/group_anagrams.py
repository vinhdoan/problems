# https://leetcode.com/problems/group-anagrams/description/


class Solution:
    def groupAnagrams(self, strs):
        """
        :type strs: List[str]
        :rtype: List[List[str]]
        """

        d = {}
        for s in strs:
            count = [0] * 26
            for c in s:
                count[ord(c) - ord('a')] += 1

            key = tuple(count)
            lst = d.get(key)
            if lst: lst.append(s)
            else: lst = [s]
            d[key] = lst

        return list(d.values())

        # Using defaultdict
        # from collections import defaultdict
        # d = defaultdict(list)
        # for s in strs:
        #     count = [0] * 26
        #     for c in s:
        #         count[ord(c) - ord('a')] += 1
        #     d[tuple(count)].append(s)

        # return list(d.values())
