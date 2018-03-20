# https://leetcode.com/problems/reverse-nodes-in-k-group/description/


# Definition for singly-linked list.
# class ListNode:
#     def __init__(self, x):
#         self.val = x
#         self.next = None

class Solution:
    def reverseKGroup(self, head, k):
        """
        :type head: ListNode
        :type k: int
        :rtype: ListNode
        """

        if k == 1:
            return head

        # 'result'  : to be returned
        # 'current' : the 1st node in the group
        # 'previous': the node right before the group
        current = head
        previous = result = ListNode(None)
        previous.next = head

        # for each group
        while current:
            # 'first': pointer to 1st node in group
            # 'last' : pointer to last node in group
            # find position for the pointer 'last'
            first = last = current
            i = k - 1
            while i > 0:
                last = last.next
                if not last:
                    return result.next
                i -= 1

            # reverse internally
            p = first
            c = p.next
            while c != last:
                n = c.next
                c.next = p
                p = c
                c = n

            # reverse outside the group
            previous.next = last
            previous = first
            first.next = last.next
            current = last.next
            last.next = p

        return result.next
