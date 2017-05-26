#!/usr/bin/env python

from sklearn import datasets
iris = datasets.load_iris()

# - A data array, where, for each instance, we have the real values for
#   sepal length, sepal width, petal length, and petal width, in that order
# - A target array, with values in the range of 0 to 2, corresponding to each
#   instance of Iris species (0: setosa, 1: versicolor, and 2: virginica),
#   as you can verify by printing the iris.target.target_names value
X_iris, y_iris = iris.data, iris.target

# The shape of this array is (150, 4), meaning that we have 150 rows
# (one for each instance) and four columns (one for each feature)
print X_iris.shape, y_iris.shape
print X_iris[0], y_iris[0]
