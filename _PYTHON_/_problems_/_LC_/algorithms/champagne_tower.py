# https://leetcode.com/problems/champagne-tower/description/


class Solution:
    def champagneTower(self, poured, query_row, query_glass):
        """
        :type poured: int
        :type query_row: int
        :type query_glass: int
        :rtype: float
        """

        # Pour 'poured' units of water into N glasses.
        # Limit the check to 'query_row'.
        def pour(N, poured, query_row):
            # initiate 0 for all glasses
            glass = [0] * N

            # put all water into 1st glass (0th)
            glass[0] = poured

            for row in range(query_row+1):
                for col in range(row+1):
                    index = row * (row+1) // 2 + col
                    if glass[index] > 1:
                        half_overflow = (glass[index] - 1) / 2
                        glass[index] = 1

                        down_left = (row+1) * (row+2) // 2 + col
                        down_right = down_left + 1

                        glass[down_left] += half_overflow
                        glass[down_right] += half_overflow

            return glass

        if query_glass > query_row:
            return

        # total_rows includes the row below query_row so that
        # water from the glass can spill down
        total_rows = query_row + 1
        # total number of glasses for above number of rows
        N = (total_rows+1) * (total_rows+2) // 2

        glass = pour(N, poured, query_row)
        return glass[query_row * (query_row+1) // 2 + query_glass]
