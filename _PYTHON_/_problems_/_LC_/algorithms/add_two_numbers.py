# https://leetcode.com/problems/add-two-numbers/description/


# Definition for singly-linked list.
# class ListNode(object):
#     def __init__(self, x):
#         self.val = x
#         self.next = None

class Solution(object):
    def addTwoNumbers(self, l1, l2):
        """
        :type l1: ListNode
        :type l2: ListNode
        :rtype: ListNode
        """

        carrier = 0
        zero_node = ListNode(0)
        zero_node.next = zero_node
        dumb_node = ListNode(0)
        prev = dumb_node

        while not (l1 == l2 == zero_node):
            s = l1.val + l2.val + carrier

            carrier = s // 10
            prev.next = ListNode(s % 10)
            prev = prev.next

            l1 = l1.next if l1.next else zero_node
            l2 = l2.next if l2.next else zero_node

        if carrier > 0:
            prev.next = ListNode(carrier)

        return dumb_node.next
