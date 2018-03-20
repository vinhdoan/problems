# https://leetcode.com/problems/combination-sum-ii/description/


class Solution:
    def combinationSum2(self, candidates, target):
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
                if i == index or (i > index and candidates[i] != candidates[i-1]):
                    dfs(candidates, target - candidates[i], i + 1, path + [candidates[i]], result)

        candidates.sort()
        result = []
        dfs(candidates, target, 0, [], result)

        return result
