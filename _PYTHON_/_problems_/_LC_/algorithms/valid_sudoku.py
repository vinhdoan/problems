# https://leetcode.com/problems/valid-sudoku/description/


class Solution:
    def isValidSudoku(self, board):
        """
        :type board: List[List[str]]
        :rtype: bool
        """

        side = 9

        rowset = [set() for _i in range(side)]
        colset = [set() for _i in range(side)]
        blkset = [set() for _i in range(side)]

        for i in range(side):
            for j in range(side):
                num = board[i][j]
                if num == '.': continue
                k = 3 * (i//3) + j//3  # block number
                if num in rowset[i] or num in colset[j] or num in blkset[k]:
                    return False

                rowset[i].add(num)
                colset[j].add(num)
                blkset[k].add(num)

        return True
