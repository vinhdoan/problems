# https://www.hackerrank.com/challenges/ctci-ransom-note/problem


def ransom_note(magazine, ransom):
    # OPTION 1
    bag = {}
    for rWord in ransom:
        bag[rWord] = bag.get(rWord, 0) + 1
    for mWord in magazine:
        mWordCount = bag.get(mWord)
        if mWordCount:
            if mWordCount == 1:
                del bag[mWord]
                if bag == {}:
                    return True
            else:
                bag[mWord] = mWordCount - 1

    # OPTION 2
    # from collections import Counter
    # return not(Counter(ransom) - Counter(magazine))


m, n = map(int, input().strip().split(' '))
magazine = input().strip().split(' ')
ransom = input().strip().split(' ')
answer = ransom_note(magazine, ransom)
if(answer):
    print("Yes")
else:
    print("No")
