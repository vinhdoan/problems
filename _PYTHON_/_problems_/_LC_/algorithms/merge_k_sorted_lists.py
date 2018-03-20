# https://leetcode.com/problems/merge-k-sorted-lists/description/


# Definition for singly-linked list.
# class ListNode:
#     def __init__(self, x):
#         self.val = x
#         self.next = None

class Solution:
    def mergeKLists(self, lists):
        """
        :type lists: List[ListNode]
        :rtype: ListNode
        """

        # SOLUTION 1: Brute Force
        # nodes = []
        # result = head = ListNode(0)
        # for l in lists:
        #     while l:
        #         nodes.append(l.val)
        #         l = l.next
        # for x in sorted(nodes):
        #     head.next = ListNode(x)
        #     head = head.next

        # return result.next

        # SOLUTION 2: Divide and conquer
        k = len(lists)
        if k == 0: return []

        def merge2Lists(list1, list2):
            p = result = ListNode(None)
            while list1 and list2:
                if list1.val <= list2.val:
                    p.next = list1
                    list1 = list1.next
                else:
                    p.next = list2
                    list2 = list2.next
                p = p.next
            p.next = list1 if list1 else list2

            return result.next

        distance = 1
        while distance < k:
            for i in range(0, k - distance, distance * 2):
                lists[i] = merge2Lists(lists[i], lists[i+distance])
            distance *= 2

        return lists[0]
