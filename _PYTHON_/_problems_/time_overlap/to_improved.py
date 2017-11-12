#!/usr/bin/env python3

import sys
import pandas as pd
from datetime import timedelta, date


def append_if_possible(result, period):
    if period['to'] >= period['from']:
        if not result or period['count'] != result[-1]['count']:
            result.append(period)
        else:
            result[-1]['to'] = period['to']


def to(beginL, endL):
    oneDay = timedelta(1)
    count = 0
    period = {'from': date.min}
    result = []
    while beginL or endL:
        b = beginL[0].date() if beginL else None
        e = endL[0].date()
        period['count'] = count
        if not b or b > e:
            period['to'] = e
            append_if_possible(result, period)
            count -= 1
            period = {'from': e + oneDay}
            endL = endL[1:]
        else:
            period['to'] = b - oneDay
            append_if_possible(result, period)
            count += 1
            period = {'from': b}
            beginL = beginL[1:]
    period['count'] = count
    period['to'] = date.max
    result.append(period)
    return result


def pretty_print(result):
    for elem in result:
        print("From", elem['from'], "to", elem['to'], ": ", elem['count'])


def get_max_count(inputList):
    m = max(inputList, key=lambda x: x['count'])['count']
    maxCntPeriods = [(x['from'], x['to']) for x in inputList if x['count'] == m]
    return (m, maxCntPeriods)


if __name__ == '__main__':
    data = pd.read_csv(sys.argv[1], parse_dates=['start', 'stop'])
    result = to(sorted(data['start']), sorted(data['stop']))
    print("*** DETAILS ***")
    pretty_print(result)
    print()
    print("*** RESULT ***")
    print(get_max_count(result))
