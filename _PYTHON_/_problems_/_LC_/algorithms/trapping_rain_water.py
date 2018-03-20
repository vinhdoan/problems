# https://leetcode.com/problems/trapping-rain-water/description/


class Solution:
    def trap(self, height):
        """
        :type height: List[int]
        :rtype: int
        """

        # ---
        # SOLUTION 1: Brute force, O(n^2), O(1)
        # ans = 0
        # size = len(height)
        # for i in range(1, size-1):
        #     max_left = max_right = 0
        #     for j in range(i, -1, -1):
        #         max_left = max(max_left, height[j])
        #     for j in range(i, size):
        #         max_right = max(max_right, height[j])
        #     ans += min(max_left, max_right) - height[i]

        # return ans

        # ---
        # SOLUTION 2: Dynamic Programming, O(n), O(n)
        # ans = 0
        # size = len(height)
        # left_max, right_max = [0] * size, [0] * size

        # left_max[0] = height[0]
        # for i in range(1, size):
        #     left_max[i] = max(height[i], left_max[i-1])

        # right_max[size-1] = height[size-1]
        # for i in range(size-2, -1, -1):
        #     right_max[i] = max(height[i], right_max[i+1])

        # for i in range(1, size-1):
        #     ans += min(left_max[i], right_max[i]) - height[i]

        # return ans

        # ---
        # SOLUTION 3: Using stacks, O(n), O(n)
        # ans = 0
        # st = []
        # for current in range(len(height)):
        #     print('Current:', current)
        #     while st and height[current] > height[st[-1]]:
        #         print(st)
        #         top = st[-1]
        #         st.pop()
        #         if not st: break
        #         distance = current - st[-1] - 1
        #         bounded_height = min(height[current], height[st[-1]]) - height[top]
        #         ans += distance * bounded_height
        #     st.append(current)

        # return ans

        # ---
        # SOLUTION 4: Using 2 pointers
        left, right = 0, len(height)-1
        left_max = right_max = 0
        ans = 0
        while left < right:
            if height[left] < height[right]:
                if height[left] >= left_max: left_max = height[left]
                else: ans += left_max - height[left]
                left += 1
            else:
                if height[right] >= right_max: right_max = height[right]
                else: ans += right_max - height[right]
                right -= 1

        return ans
