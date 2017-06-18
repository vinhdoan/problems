#!/usr/bin/env python
def find_sub(ts):
    INF_POS = float("inf")
    firstElem = {'val': ts[0], 'pos': 0}
    curMax = curMin = lastElem = firstElem
    prevElem = {'val': INF_POS, 'pos': -1}
    decreasingFlag = False
    for index, value in enumerate(ts):
        curElem = {'val': value, 'pos': index}
        curMaxDiff = lastElem['val'] - firstElem['val']
        if value > prevElem['val']:
            curMax = curElem
            if decreasingFlag:
                curMin = prevElem
            if curMax['val'] - curMin['val'] > curMaxDiff:
                firstElem = curMin
                lastElem = curMax
            decreasingFlag = False
        else:
            if not decreasingFlag and curMax['val'] - curMin['val'] > curMaxDiff:
                firstElem = curMin
                lastElem = curMax
            decreasingFlag = True
        prevElem = curElem
    return ts[firstElem['pos']:lastElem['pos']+1]
