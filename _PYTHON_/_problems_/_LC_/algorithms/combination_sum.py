# https://leetcode.com/problems/combination-sum/description/


class Solution:
    def combinationSum(self, candidates, target):
        """
        :type candidates: List[int]
        :type target: int
        :rtype: List[List[int]]
        """

        def dfs(candidates, target, index, path, result):
            if target < 0: return
            if target == 0:
                result.append(path)
                return
            for i in range(index, len(candidates)):
                dfs(candidates, target - candidates[i], i, path + [candidates[i]], result)

        result = []
        dfs(candidates, target, 0, [], result)
        return result
