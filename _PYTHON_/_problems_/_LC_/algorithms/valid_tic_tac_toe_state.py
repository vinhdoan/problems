# https://leetcode.com/problems/valid-tic-tac-toe-state/description/


class Solution:
    def validTicTacToe(self, board):
        """
        :type board: List[str]
        :rtype: bool
        """

        d = {'X': 1, ' ': 0, 'O': -1}
        b = [d[char] for string in board for char in string]

        # Number of Xs equal or greater than Os
        s = sum(b)
        if s != 0 and s != 1:
            return False

        # Check lines of 3
        x_win = 0
        o_win = 0
        check_list = [(0, 1, 2), (3, 4, 5), (6, 7, 8),
                      (0, 3, 6), (1, 4, 7), (2, 5, 8),
                      (0, 4, 8), (2, 4, 6)]

        for i1, i2, i3 in check_list:
            if b[i1] + b[i2] + b[i3] == 3:
                x_win += 1
            if b[i1] + b[i2] + b[i3] == -3:
                o_win += 1

        if x_win + o_win > 1:
            return False
        if x_win == 1 and s == 0:
            return False
        if o_win == 1 and s == 1:
            return False

        return True
