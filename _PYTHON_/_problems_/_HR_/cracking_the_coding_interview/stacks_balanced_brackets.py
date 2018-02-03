# https://www.hackerrank.com/challenges/ctci-balanced-brackets/problem


pairs = {'(': ')',
         '{': '}',
         '[': ']'}


def is_matched(expression):
    stack = []
    for bracket in expression:
        if pairs.get(bracket):  # bracket is an opening one
            stack.append(bracket)
        else:  # bracket is a closing one
            if stack == [] or pairs[stack.pop()] != bracket:
                return False
    return stack == []


t = int(input().strip())
for a0 in range(t):
    expression = input().strip()
    if is_matched(expression):
        print("YES")
    else:
        print("NO")
