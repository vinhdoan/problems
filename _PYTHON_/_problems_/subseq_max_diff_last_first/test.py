#!/usr/bin/env python
import unittest
from solution import find_sub


class SolutionTest(unittest.TestCase):
    def test_all(self):
        self.assertEqual([0], find_sub([0]))
        self.assertEqual([1, 2, 3], find_sub([2, 1, 2, 3, 2]))
        self.assertEqual([1, 2, 3], find_sub([4, 1, 2, 3, 0]))
        self.assertEqual([0, 1, 2, 3], find_sub([1, 2, 1, 0, 1, 2, 3]))
        self.assertEqual([0, 1, 2, 3], find_sub([0, 1, 2, 3, 2, 3, 4]))
        self.assertEqual([4, 5, 6],
                         find_sub([5, 6, 5, 4, 5, 6, 5, 4, 3, 4, 3, 2, 3]))


if __name__ == '__main__':
        unittest.main(verbosity=2)
