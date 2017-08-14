#!/usr/bin/env python

# NOTE: k-NN is BAD for MNIST. It takes time & memory.
import numpy as np
from mnist import MNIST  # require `pip install python-mnist`
# https://pypi.python.org/pypi/python-mnist/
from sklearn import neighbors
from sklearn.metrics import accuracy_score
import time

# you need to download the MNIST dataset first
# at: http://yann.lecun.com/exdb/mnist/
mndata = MNIST('/mnt/sharefolder/DS_datasets/THE MNIST DATABASE of handwritten digits')
mndata.test_img_fname = 't10k-images.idx3-ubyte'
mndata.test_lbl_fname = 't10k-labels.idx1-ubyte'
mndata.train_img_fname = 'train-images.idx3-ubyte'
mndata.train_lbl_fname = 'train-labels.idx1-ubyte'
mndata.load_testing()
mndata.load_training()
X_test = mndata.test_images
X_train = mndata.train_images
y_test = np.asarray(mndata.test_labels)
y_train = np.asarray(mndata.train_labels)


start_time = time.time()
clf = neighbors.KNeighborsClassifier(n_neighbors=1, p=2)
clf.fit(X_train, y_train)
y_pred = clf.predict(X_test)
end_time = time.time()
print "Accuracy of 1NN for MNIST: %.2f %%" % (100 * accuracy_score(y_test, y_pred))
print "Running time: %.2f (s)" % (end_time - start_time)
