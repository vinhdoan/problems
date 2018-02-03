# https://www.hackerrank.com/challenges/ctci-making-anagrams/problem


def number_needed(a, b):
    abc = [0] * 26
    for c in a:
        abc[ord(c) - ord('a')] += 1
    for c in b:
        abc[ord(c) - ord('a')] -= 1
    result = 0
    for i in abc:
        result += abs(i)
    return result


a = input().strip()
b = input().strip()

print(number_needed(a, b))
