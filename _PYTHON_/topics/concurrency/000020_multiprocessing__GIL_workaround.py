## ------------------------------------------
## Suppose you have the following thread code


# Performs a large calculation (CPU bound)
def some_work(args):
    # ...
    return result


# A thread that calls the above function
def some_thread():
    while True:
        # ...
        r = some_work(args)
        # ...

## -----------------------------
## Modify the code to use a pool


# Processing pool (see below for initiazation)
pool = None


# Performs a large calculation (CPU bound)
def some_work(args):
    # ...
    return result


# A thread that calls the above function
def some_thread():
    while True:
        # ...
        r = pool.apply(some_work, (args))
        # ...


# Initiaze the pool
if __name__ == '__main__':
    import multiprocessing
    pool = multiprocessing.Pool()


## -----------------------------------------------------------------------------
## The second strategy for working around the GIL is to focus on
## C extension programming
##
## OPTION 1:
##   Insert special macros into the C code:
##     #include "Python.h"
##     ...
##
##     PyObject *pyfunc(PyObject *self, PyObject *args) {
##         ...
##         Py_BEGIN_ALLOW_THREADS
##         // Threaded C code
##         ...
##         Py_END_ALLOW_THREADS
##         ...
##     }
##
## OPTION 2:
##   Other tools to access C such as 'ctypes' library or 'Cython'
