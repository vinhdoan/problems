# https://leetcode.com/problems/swap-nodes-in-pairs/description/


# Definition for singly-linked list.
# class ListNode:
#     def __init__(self, x):
#         self.val = x
#         self.next = None

class Solution:
    def swapPairs(self, head):
        """
        :type head: ListNode
        :rtype: ListNode
        """

        p = dummy = ListNode(None)
        dummy.next = head

        while p.next and p.next.next:
            temp1, temp2 = p.next, p.next.next

            temp1.next = temp2.next
            temp2.next = temp1
            p.next = temp2
            p = temp1

        return dummy.next
