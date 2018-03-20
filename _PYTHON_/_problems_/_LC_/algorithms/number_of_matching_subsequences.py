# https://leetcode.com/problems/number-of-matching-subsequences/description/


class Solution:
    def numMatchingSubseq(self, S, words):
        """
        :type S: str
        :type words: List[str]
        :rtype: int
        """
        def isMatching(d, word):
            prev = -1
            for char in word:
                l = d.get(char)
                if not l:
                    return False
                saved_prev = prev
                for pos in l:
                    if pos > prev:
                        prev = pos
                        break
                if prev == saved_prev:
                    return False
            return True

        d = {}
        for i, char in enumerate(S):
            l = d.get(char, [])
            l.append(i)
            d[char] = l

        count = 0
        for word in words:
            if isMatching(d, word):
                count += 1

        return count
