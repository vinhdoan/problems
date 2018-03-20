# https://leetcode.com/problems/substring-with-concatenation-of-all-words/description/


class Solution:
    def findSubstring(self, s, words):
        """
        :type s: str
        :type words: List[str]
        :rtype: List[int]
        """

        '''
                                                                   s_len-word_len
        0         word_len                                         |     s_len-1
        |---------|---------|---------|----------------------------|-----|
        [<<--i-->>
             ^
           start
             [<<----------------j (step: word_len)--------------->>]

        '''

        s_len = len(s)
        words_num = len(words)
        word_len = len(words[0])

        # build dict of words
        words_dict = {}
        for word in words:
            words_dict[word] = words_dict.get(word, 0) + 1

        result = []
        i = 0
        while i < word_len:
            j = start = i
            ite_dict = {}
            ite_count = 0  # if equal to words_num, result found
            while j <= s_len - word_len:
                # get substr beginning at j
                string = s[j:j+word_len]

                # if substr defined in words_dict, continue the process
                if words_dict.get(string):
                    # add substr to iterative dict
                    ite_dict[string] = ite_dict.get(string, 0) + 1

                    # not enough number of substr(s), increase counter
                    if ite_dict[string] <= words_dict[string]:
                        ite_count += 1
                    # redundancy substr causes unmatch
                    # move 'start' word by word to decrease the window
                    else:
                        while ite_dict[string] > words_dict[string]:
                            old_str = s[start:start+word_len]
                            start += word_len
                            ite_dict[old_str] -= 1
                            if ite_dict[old_str] < words_dict[old_str]:
                                ite_count -= 1

                    # successful, move 'start' one word to the right to begin new check
                    if ite_count == words_num:
                        result.append(start)
                        ite_count -= 1
                        ite_dict[s[start:start+word_len]] -= 1
                        start += word_len

                # substr is not defined in words_dict, reset parameters for next 'start'
                else:
                    ite_dict = {}
                    ite_count = 0
                    start = j + word_len

                j += word_len
            i += 1

        return result
