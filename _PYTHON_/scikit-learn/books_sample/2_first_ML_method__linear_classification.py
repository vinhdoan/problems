#!/usr/bin/env python

# from sklearn.cross_validation import train_test_split  # deprecated
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import StandardScaler
from sklearn import datasets
import matplotlib.pyplot as plt

iris = datasets.load_iris()
X_iris, y_iris = iris.data, iris.target

# Get dataset with only the first two attributes
X, y = X_iris[:, :2], y_iris

# Split the dataset into a training and a testing set
# Test set will be the 25% taken randomly
X_train, X_test, y_train, y_test = train_test_split(X,
                                                    y,
                                                    test_size=0.25,
                                                    random_state=33)
print X_train.shape, y_train.shape

# Standarize the features
scaler = StandardScaler().fit(X_train)
X_train = scaler.transform(X_train)
X_test = scaler.transform(X_test)

# Take a look at how our training instances are distributed
# in the twodimensional space
colors = ['red', 'greenyellow', 'blue']
for i in xrange(len(colors)):
    px = X_train[:, 0][y_train == i]
    py = X_train[:, 1][y_train == i]
    plt.scatter(px, py, c=colors[i])

plt.legend(iris.target_names)
plt.xlabel('Sepal length')
plt.ylabel('Sepal width')
plt.show()
