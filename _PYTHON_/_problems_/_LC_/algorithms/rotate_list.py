# https://leetcode.com/problems/rotate-list/description/


# Definition for singly-linked list.
# class ListNode:
#     def __init__(self, x):
#         self.val = x
#         self.next = None

class Solution:
    def rotateRight(self, head, k):
        """
        :type head: ListNode
        :type k: int
        :rtype: ListNode
        """

        # SOLUTION 1: Two pointers

        # Null list or no rotation
        if head is None or k == 0: return head

        # h is running pointer, t is waiting pointer
        h = t = dummy = ListNode(None)
        dummy.next = head

        # h runs to position (k % size) from t
        n = 0
        while k != 0:
            if h.next is None:
                k %= n
                if k == 0: break
                h = t.next
            else:
                if k >= n: n += 1
                h = h.next
            k -= 1

        # h, t run together until the end of the list
        while h.next:
            h = h.next
            t = t.next

        # if t moves away from initial position,
        # rotate the list by breaking it
        if head != t.next:
            head = t.next
            h.next = dummy.next
            t.next = None

        return head

        # # SOLUTION 2: No pointers
        # if not head: return None

        # # find l: length of the list
        # l = 1
        # p = head
        # while p.next:
        #     p = p.next
        #     l += 1

        # # compact k value an rotate list
        # k = k % l
        # p.next = head

        # p = head
        # for i in range(l-k-1):
        #     p = p.next
        # res = p.next
        # p.next = None

        # return res
