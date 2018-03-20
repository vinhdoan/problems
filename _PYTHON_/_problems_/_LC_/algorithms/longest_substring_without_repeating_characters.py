# https://leetcode.com/problems/longest-substring-without-repeating-characters/description/


class Solution:
    def lengthOfLongestSubstring(self, s):
        """
        :type s: str
        :rtype: int
        """

        max_len = 0      # length between 'start_pos' and 'current_pos'
        used_chars = {}  # chars (with their latest positions) which have been scanned
        start_pos = 0    # position chosen as starting point

        for current_pos, char in enumerate(s):
            char_pos_in_dict = used_chars.get(char)               # get latest position of saved 'char'
            if char_pos_in_dict is not None:                      # if 'char' position has been saved before...
                start_pos = max(start_pos, char_pos_in_dict + 1)  # change 'start_pos' to after that position

            used_chars[char] = current_pos                        # update the latest position of 'char'
            max_len = max(max_len, current_pos - start_pos + 1)

        return max_len
