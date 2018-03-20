# https://www.geeksforgeeks.org/length-largest-subarray-contiguous-elements-set-1/


# code
def findLSCE(a):
    n = len(a)
    result = 1
    for i in range(n-1):
        mx = mn = a[i]
        for j in range(i+1, n):
            mx = max(mx, a[j])
            mn = min(mn, a[j])
            if mx - mn == j - i: result = max(result, j - i + 1)

    return result


# test
import unittest


class Problem(unittest.TestCase):
    def test_3(self):
        self.assertEqual(findLSCE([1, 56, 58, 57, 90, 92, 94, 93, 91, 45]), 5)

    def test_4(self):
        self.assertEqual(findLSCE([10, 12, 11]), 3)

    def test_5(self):
        self.assertEqual(findLSCE([14, 12, 11, 20]), 2)


if __name__ == '__main__':
    unittest.main(verbosity=2)
