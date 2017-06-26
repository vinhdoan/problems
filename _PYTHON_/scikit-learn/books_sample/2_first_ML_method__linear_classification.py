#!/usr/bin/env python

# from sklearn.cross_validation import train_test_split  # deprecated
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import StandardScaler
import numpy as np
import matplotlib.pyplot as plt
from sklearn import datasets
from sklearn.linear_model import SGDClassifier
from sklearn import metrics


iris = datasets.load_iris()
# X_iris shape: 150 rows x 4 columns
# y_iris shape: 150 rows
X_iris, y_iris = iris.data, iris.target

# Get dataset with only the first two attributes
# X shape: 150 rows x 2 columns
# y shape: 150 rows
X, y = X_iris[:, :2], y_iris

# Split the dataset into a training and a testing set
# Test set will be the 25% taken randomly
# X_train shape: 112 rows x 2 columns
# X_test  shape:  38 rows x 2 columns
# y_train shape: 112 rows
# y_test  shape:  38 rows
X_train, X_test, y_train, y_test = train_test_split(X,
                                                    y,
                                                    test_size=0.25,
                                                    random_state=33)
print "*** X_train & y_train shapes:"
print X_train.shape, y_train.shape
print "*** X_test & y_test shapes:"
print X_test.shape, y_test.shape

# Standardize the features
scaler = StandardScaler().fit(X_train)
X_train = scaler.transform(X_train)
X_test = scaler.transform(X_test)

# Take a look at how our training instances are distributed
# in the two dimensional space
colors = ['red', 'greenyellow', 'blue']
for i in xrange(len(colors)):
    px = X_train[:, 0][y_train == i]  # sepal length
    py = X_train[:, 1][y_train == i]  # sepal width
    plt.scatter(px, py, c=colors[i])
plt.legend(iris.target_names)
plt.xlabel('Sepal length')
plt.ylabel('Sepal width')
plt.show()

# Linear classification with Stochastic Gradient Descent (SGD)
clf = SGDClassifier()
clf.fit(X_train, y_train)
print "*** Coefficients of the linear boundary:"
print clf.coef_
print "*** The point of intersection of the line with the y axis:"
print clf.intercept_

x_min, x_max = X_train[:, 0].min() - .5, X_train[:, 0].max() + .5
y_min, y_max = X_train[:, 1].min() - .5, X_train[:, 1].max() + .5
xs = np.arange(x_min, x_max, 0.5)
fig, axes = plt.subplots(1, 3)
fig.set_size_inches(10, 6)
for i in [0, 1, 2]:
    axes[i].set_aspect('equal')
    axes[i].set_title('Class ' + str(i) + ' versus the rest')
    axes[i].set_xlabel('Sepal length')
    axes[i].set_ylabel('Sepal width')
    axes[i].set_xlim(x_min, x_max)
    axes[i].set_ylim(y_min, y_max)
    plt.sca(axes[i])
    # for j in xrange(len(colors)):
    #     px = X_train[:, 0][y_train == j]
    #     py = X_train[:, 1][y_train == j]
    #     plt.scatter(px, py, c=colors[j])
    plt.scatter(X_train[:, 0], X_train[:, 1], c=y_train, cmap=plt.cm.prism)
    ys = (-clf.intercept_[i] - xs * clf.coef_[i, 0]) / clf.coef_[i, 1]
    plt.plot(xs, ys)
plt.show()

# With sepal width of 4.7 and sepal length of 3.1
print "*** Predict class for a new flower:"
print clf.predict(scaler.transform([[4.7, 3.1]]))
# Our prediction procedure combines the result of the three binary classifiers and
# selects the class in which it is more confident.
# In this case, we will select the boundary line whose distance to the instance is longer.
# We can check that using the classifier decision_function method
print "*** Check the decision function to be sure about above result:"
print clf.decision_function(scaler.transform([[4.7, 3.1]]))

# Evaluating our results by "Accuracy" evaluation functions
# Note that:
#   ---------------------------------------------------------------------
#                            Prediction: Positive    Prediction: Negative
#   ---------------------------------------------------------------------
#   Target cass: Positive    True Positive (TP)      False Negative (FN)
#   Target cass: Negative    False Positive (FP)     True Negative (TN)
#   ---------------------------------------------------------------------
#   Accuracy = (TP + TN) / (TP + TN + FP + FN)
#   Precision = TP / (TP + FP)
#   Recall = TP / (TP + FN)
#   F1-score = 2 * Precision * Recall / (Precision + Recall)
print "*** Evaluating our results by X_train itself (really a bad idea):"
y_train_pred = clf.predict(X_train)
print metrics.accuracy_score(y_train, y_train_pred)
print "*** Evaluating our results by X_test:"
y_pred = clf.predict(X_test)
print metrics.accuracy_score(y_test, y_pred)
print "*** Classification report:"
print metrics.classification_report(y_test,
                                    y_pred,
                                    target_names=iris.target_names)
print "*** Confusion matrix (i, j) shows number of class instances i " \
    "that were predicted to be in class j:"
print metrics.confusion_matrix(y_test, y_pred)
