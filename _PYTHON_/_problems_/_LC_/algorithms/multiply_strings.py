# https://leetcode.com/problems/multiply-strings/description/


class Solution:
    def multiply(self, num1, num2):
        """
        :type num1: str
        :type num2: str
        :rtype: str
        """

        # Example: num1 = "1234"
        #          num2 =  "567"
        num_list_1 = []
        size1, size2 = len(num1), len(num2)
        psize = size1 + size2 - 1

        # num_list_1 is [1, 2, 3, 4]
        for d in num1:
            num_list_1.append(int(d))

        # rl is [[ 5, 10, 15, 20,  0,  0],
        #        [ 0,  6, 12, 18, 24,  0],
        #        [ 0,  0,  7, 14, 21, 28]]
        j = 0
        rl = []
        for d in num2:
            n2 = int(d)
            r = [0 for _i in range(psize)]

            for i, n1 in enumerate(num_list_1):
                r[i+j] = n1 * n2
            j += 1
            rl.append(r)

        # sum_list is [5, 16, 34, 52, 45, 28]
        rl_zip = zip(*rl)
        sum_list = [sum(elems) for elems in rl_zip]

        # sum_list is ['6', '9', '9', '6', '7', '8']
        a = 0
        for i in range(psize):
            s = sum_list[psize-1-i] + a
            sum_list[psize-1-i] = str(s % 10)
            a = s // 10
        print(sum_list)

        # when a is not 0
        if a: return str(a) + ''.join(sum_list)

        # when a is 0, remove prefix '0'
        k = 0
        while k < psize:
            if sum_list[k] != '0': break
            k += 1

        if k == psize: return '0'
        return ''.join(sum_list[k:])
