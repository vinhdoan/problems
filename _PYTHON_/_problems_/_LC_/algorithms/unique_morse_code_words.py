# https://leetcode.com/problems/unique-morse-code-words/description/


class Solution:
    def uniqueMorseRepresentations(self, words):
        """
        :type words: List[str]
        :rtype: int
        """

        from collections import defaultdict
        abc = [".-","-...","-.-.","-..",".","..-.","--.","....","..",".---",
               "-.-",".-..","--","-.","---",".--.","--.-",".-.","...","-",
               "..-","...-",".--","-..-","-.--","--.."]
        saved = defaultdict(int)
        for word in words:
            morse = ""
            for c in word:
                morse += abc[ord(c) - ord('a')]
            saved[tuple(morse)] += 1

        return len(saved.keys())
