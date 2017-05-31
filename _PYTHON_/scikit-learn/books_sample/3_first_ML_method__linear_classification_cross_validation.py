#!/usr/bin/env python

from sklearn import datasets
# from sklearn.cross_validation import cross_val_score, KFold  # deprecated
from sklearn.model_selection import cross_val_score, KFold
from sklearn.pipeline import Pipeline
from sklearn.preprocessing import StandardScaler
from sklearn.linear_model import SGDClassifier
from scipy.stats import sem
import numpy as np


iris = datasets.load_iris()
# X_iris shape: 150 rows x 4 columns
# y_iris shape: 150 rows
X_iris, y_iris = iris.data, iris.target

# Get dataset with only the first two attributes
# X shape: 150 rows x 2 columns
# y shape: 150 rows
X, y = X_iris[:, :2], y_iris

# Composite estimator made by a pipeline of
# the standardization and linear models.
# We will chose to have k = 5 folds, so each time
# we will train on 80 percent of the data and test on the remaining 20 percent
clf = Pipeline([('scaler', StandardScaler()),
                ('linear_model', SGDClassifier())])
# cv = KFold(X.shape[0], 5, shuffle=True, random_state=33)  # deprecated
cv = KFold(5, shuffle=True, random_state=33)
cv.get_n_splits(X.shape[0])
scores = cross_val_score(clf, X, y, cv=cv)
print "We obtain an array of k scores:"
print scores


def mean_score(scores):
    return("Mean score: {0:.3f} (+/-{1:.3f})").format(np.mean(scores),
                                                      sem(scores))


print mean_score(scores)
