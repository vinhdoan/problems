# https://www.hackerrank.com/challenges/ctci-linked-list-cycle/problem

"""
Detect a cycle in a linked list. Note that the head pointer may be 'None' if the list is empty.

A Node is defined as:

    class Node(object):
        def __init__(self, data = None, next_node = None):
            self.data = data
            self.next = next_node
"""


def has_cycle(head):
    p1 = head
    p2 = head
    while p2 is not None:
        p1 = p1.next
        if p2.next is None:
            return False
        else:
            p2 = p2.next.next
        if p1 is p2:
            return True
