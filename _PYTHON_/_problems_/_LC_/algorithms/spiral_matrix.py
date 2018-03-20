# https://leetcode.com/problems/spiral-matrix/description/


class Solution:
    def spiralOrder(self, matrix):
        """
        :type matrix: List[List[int]]
        :rtype: List[int]
        """

        # SOLUTION 1: Pop first row and rotate matrix recursively
        # return matrix and list(matrix.pop(0)) + self.spiralOrder(list(zip(*matrix))[::-1])

        # SOLUTION 2:
        '''
              c1            c2
              |             |
              V             V
        r1 -> 1   1 1 1 1   1
              ---------------
              1 | 2 2 2 2 | 1
              1 | 2 3 3 2 | 1
              1 | 2 2 2 2 | 1
                | --------|
        r2 -> 1 | 1 1 1 1 | 1

        '''

        m = len(matrix)
        if m == 0: return []
        n = len(matrix[0])

        def roundOrder(r1, r2, c1, c2):
            for c in range(c1, c2+1):
                yield r1, c
            for r in range(r1+1, r2+1):
                yield r, c2
            if c1 < c2 and r1 < r2:
                for c in range(c2-1, c1, -1):
                    yield r2, c
                for r in range(r2, r1, -1):
                    yield r, c1

        ans = []
        r1, r2 = 0, m-1
        c1, c2 = 0, n-1
        while r1 <= r2 and c1 <= c2:
            for r, c in roundOrder(r1, r2, c1, c2):
                ans.append(matrix[r][c])
            r1 += 1; r2 -= 1
            c1 += 1; c2 -= 1

        return ans
