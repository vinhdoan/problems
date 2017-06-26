#!/usr/bin/env python
import sklearn as sk
import numpy as np
import matplotlib.pyplot as plt
from sklearn.datasets import fetch_olivetti_faces
from sklearn.svm import SVC
from sklearn.cross_validation import train_test_split
from sklearn.cross_validation import cross_val_score, KFold
from scipy.stats import sem
from sklearn import metrics


faces = fetch_olivetti_faces()
# We get the following properties: images, data, and target
# - images contain the 400 images represented as 64 x 64 pixel matrices
# - data contains the same 400 images but as array of 4096 pixels
# - target is, as expected, an array with the target classes, ranging from 0 to 39
print "*** Print 'faces' info:"
print faces.DESCR
print faces.keys()
print faces.images.shape
print faces.data.shape
print faces.target.shape
# Verify by running the following snippet that our images already come as
# values in a very uniform range between 0 and 1 (pixel value):
print "*** Verify images come as values in a very uniform range between 0 and 1:"
print np.max(faces.data)  # 1.0
print np.min(faces.data)  # 0.0
print np.mean(faces.data)  # 0.547


def print_faces(images, target, top_n):
    # set up the figure size in inches
    fig = plt.figure(figsize=(12, 12))
    fig.subplots_adjust(left=0, right=1, bottom=0, top=1,
                        hspace=0.05, wspace=0.05)
    for i in range(top_n):
        # plot the images in a matrix of 20x20
        p = fig.add_subplot(20, 20, i + 1, xticks=[], yticks=[])
        p.imshow(images[i], cmap=plt.cm.bone)
        # label the image with the target value
        p.text(0, 14, str(target[i]))
        p.text(0, 60, str(i))


print "*** Print some faces:"
print_faces(faces.images, faces.target, 20)
plt.show()


# By default, SVC class uses the rbf kernel, which allows us to model nonlinear problems
# To start, we will use the simplest kernel, the linear one
svc_1 = SVC(kernel='linear')
# Split our dataset into training and testing datasets
X_train, X_test, y_train, y_test = train_test_split(faces.data, faces.target,
                                                    test_size=0.25, random_state=0)


# Define a function to evaluate K-fold cross-validation
def evaluate_cross_validation(clf, X, y, K):
    # create a k-fold cross validation iterator
    cv = KFold(len(y), K, shuffle=True, random_state=0)
    # by default the score used is the one returned by score method of the estimator (accuracy)
    scores = cross_val_score(clf, X, y, cv=cv)
    print scores
    print ("Mean score: {0:.3f} (+/-{1:.3f})").format(np.mean(scores), sem(scores))


print "*** Evaluate K-fold cross-validation:"
evaluate_cross_validation(svc_1, X_train, y_train, 5)


# Define a function to perform training on the training set and
# evaluate the performance on the testing set
def train_and_evaluate(clf, X_train, X_test, y_train, y_test):
    clf.fit(X_train, y_train)
    print "Accuracy on training set:"
    print clf.score(X_train, y_train)
    print "Accuracy on testing set:"
    print clf.score(X_test, y_test)
    y_pred = clf.predict(X_test)
    print "Classification Report:"
    print metrics.classification_report(y_test, y_pred)
    print "Confusion Matrix:"
    print metrics.confusion_matrix(y_test, y_pred)


print "*** Train and evaluate:"
train_and_evaluate(svc_1, X_train, X_test, y_train, y_test)
