#!/usr/bin/env python3


_formats = {
    'ymd': '{d.year}-{d.month}-{d.day}',
    'mdy': '{d.month}/{d.day}/{d.year}',
    'dmy': '{d.day}/{d.month}/{d.year}'
}


class Date:
    def __init__(self, year, month, day):
        self.year = year
        self.month = month
        self.day = day

    def __format__(self, code):
        if code == '':
            code = 'ymd'
        fmt = _formats[code]
        return fmt.format(d=self)


# >>> d = Date(2012, 12, 21)
# >>> format(d)
# '2012-12-21'
# >>> format(d, 'mdy')
# '12/21/2012'
# >>> 'The date is {:ymd}'.format(d)
# 'The date is 2012-12-21'
# >>> 'The date is {:mdy}'.format(d)
# 'The date is 12/21/2012'
# >>>
