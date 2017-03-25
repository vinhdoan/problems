def lessThan(cutoffVal, *vals) :
    ''' Return a list of values less than the cutoff. '''
    arr = []
    for val in vals :
        if val < cutoffVal:
            arr.append(val)
    return arr
    
def printVals(prefix='', **dict):
    # Print out the extra named parameters and their values
    for key, val in dict.items():
        print('%s [%s] => [%s]' % (prefix, str(key), str(val)))
    
print(lessThan(10, 2, 17, -3, 42))              # [2, -3]

printVals(prefix='..', foo=42, bar='!!!')       # [foo] => [42]
                                                # [bar] => [!!!]
printVals(prefix='..', one=1, two=2)            # [one] => [1]
                                                # [two] => [2]
                                                
input()