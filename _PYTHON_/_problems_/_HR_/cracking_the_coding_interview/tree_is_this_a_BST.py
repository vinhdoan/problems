# https://www.hackerrank.com/challenges/ctci-is-binary-search-tree/problem

""" Node is defined as
class node:
    def __init__(self, data):
        self.data = data
        self.left = None
        self.right = None
"""


def checkBST(root):
    if root is None:
        return True
    stack = [(float('-inf'), root, float('+inf'))]
    while stack:
        mind, node, maxd = stack.pop()
        if not (mind < node.data < maxd):
            return False
        if node.left is not None:
            stack.append((mind, node.left, node.data))
        if node.right is not None:
            stack.append((node.data, node.right, maxd))
    return True
