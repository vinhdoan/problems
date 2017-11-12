#!/usr/bin/env python3

import unittest
from to_improved import to
from pandas import Timestamp
from datetime import date


class TOImproved(unittest.TestCase):
    def test_to_1(self):
        # Build input
        initBeginList = [(2017, 1, 1),
                         (2017, 1, 2),
                         (2017, 1, 3),
                         (2017, 1, 6)]
        initEndList = [(2017, 1, 3),
                       (2017, 1, 4),
                       (2017, 1, 5),
                       (2017, 1, 7)]
        inputBeginList = [Timestamp(y, m, d) for y, m, d in initBeginList]
        inputEndList = [Timestamp(y, m, d) for y, m, d in initEndList]

        # Build output
        initExpectedResult = [((1, 1, 1), (2016, 12, 31), 0),
                              ((2017, 1, 1), (2017, 1, 1), 1),
                              ((2017, 1, 2), (2017, 1, 2), 2),
                              ((2017, 1, 3), (2017, 1, 3), 3),
                              ((2017, 1, 4), (2017, 1, 4), 2),
                              ((2017, 1, 5), (2017, 1, 7), 1),
                              ((2017, 1, 8), (9999, 12, 31), 0)]
        expectedResult = [{'from': date(fy, fm, fd), 'to': date(ty, tm, td), 'count': c}
                          for (fy, fm, fd), (ty, tm, td), c in initExpectedResult]

        # Comparison
        self.assertEqual(to(inputBeginList, inputEndList), expectedResult)


if __name__ == '__main__':
    unittest.main(verbosity=2)
