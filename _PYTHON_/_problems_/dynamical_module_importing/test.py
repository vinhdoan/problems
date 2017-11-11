#!/usr/bin/env python3
import importlib.util as iu
import sys


'''
1. Implement run() and walk() method in /path/file.py
2. Test (path can be relative to working dir or absolute path):
     $ python3 test.py /path/file.py
3. Example:
     $ python3 test.py alpha.py
'''
if __name__ == '__main__':
    # Dynamically load the 'alpha' module
    # "module.name" affects the *.__name__
    spec = iu.spec_from_file_location("module.name", sys.argv[1])
    foo = iu.module_from_spec(spec)
    spec.loader.exec_module(foo)

    # Run alpha
    foo.run()
    foo.walk()
