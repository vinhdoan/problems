# https://leetcode.com/problems/remove-nth-node-from-end-of-list/description/


# Definition for singly-linked list.
# class ListNode:
#     def __init__(self, x):
#         self.val = x
#         self.next = None

class Solution:
    def removeNthFromEnd(self, head, n):
        """
        :type head: ListNode
        :type n: int
        :rtype: ListNode
        """

        # dummy to handle cases of deleting first node
        h = t = dummy = ListNode(None)
        dummy.next = head

        # distance from t to h is n
        for i in range(n):
            h = h.next

        while h.next != None:
            h, t = h.next, t.next
        t.next = t.next.next

        return dummy.next
