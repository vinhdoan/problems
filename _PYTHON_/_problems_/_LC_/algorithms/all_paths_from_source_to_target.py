# https://leetcode.com/problems/all-paths-from-source-to-target/description/


class Solution:
    def allPathsSourceTarget(self, graph):
        """
        :type graph: List[List[int]]
        :rtype: List[List[int]]
        """

        def findPath(start, end, graph):
            if start == end:
                return [[end]]
            ret = []
            for nxt in graph[start]:
                paths = findPath(nxt, end, graph)
                for path in paths:
                    ret.append([start] + path)
            return ret

        N = len(graph)
        end = N-1

        start = 0
        paths = findPath(start, end, graph)

        return paths
