#### Example
```bash
python3 to_improved a.csv
```
With current input a.csv:
```bash
*** DETAILS ***
From 0001-01-01 to 2016-12-31 :  0
From 2017-01-01 to 2017-01-04 :  1
From 2017-01-05 to 2017-01-08 :  2
From 2017-01-09 to 2017-01-10 :  3
From 2017-01-11 to 2017-01-13 :  2
From 2017-01-14 to 2017-01-15 :  1
From 2017-01-16 to 9999-12-31 :  0

*** RESULT ***
{'from': datetime.date(2017, 1, 9), 'count': 3, 'to': datetime.date(2017, 1, 10)}
```
#### Test
```bash
python3 -m unittest test.py
```
