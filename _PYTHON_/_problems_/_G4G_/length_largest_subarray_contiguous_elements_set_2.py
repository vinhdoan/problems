# https://www.geeksforgeeks.org/length-largest-subarray-contiguous-elements-set-2/


# code
def findLSCE(a):
    n = len(a)

    result = 1
    for i in range(n-1):
        mx = mn = a[i]
        uniq_dict = {a[i]: True}
        for j in range(i+1, n):
            if uniq_dict.get(a[j]): break
            uniq_dict[a[j]] = True
            mx = max(mx, a[j])
            mn = min(mn, a[j])
            if mx - mn == j - i: result = max(result, j - i + 1)

    return result


# test
import unittest


class Problem(unittest.TestCase):
    def test_3(self):
        self.assertEqual(findLSCE([10, 12, 11]), 3)

    def test_4(self):
        self.assertEqual(findLSCE([10, 12, 12, 10, 10, 11, 10]), 2)


if __name__ == '__main__':
    unittest.main(verbosity=2)
