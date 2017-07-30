#!/usr/bin/env python
import numpy as np
from mnist import MNIST
import matplotlib.pyplot as plt
from sklearn.cluster import KMeans
from sklearn.neighbors import NearestNeighbors
from display_network import display_network
import scipy.misc


# LOAD DATASET
mndata = MNIST('/mnt/sharefolder/DS_datasets/THE MNIST DATABASE of handwritten digits')
mndata.test_img_fname = 't10k-images.idx3-ubyte'
mndata.test_lbl_fname = 't10k-labels.idx1-ubyte'
mndata.train_img_fname = 'train-images.idx3-ubyte'
mndata.train_lbl_fname = 'train-labels.idx1-ubyte'
mndata.load_testing()
X = mndata.test_images
X0 = np.asarray(X)[:1000, :]/256.0
X = X0

# K-MEANS CLUSTERING & PREDICTION
K = 10
kmeans = KMeans(n_clusters=K).fit(X)
pred_label = kmeans.predict(X)
print(type(kmeans.cluster_centers_.T))
print(kmeans.cluster_centers_.T.shape)

# PLOT CLUSTER CENTERS
A = display_network(kmeans.cluster_centers_.T, K, 1)
f1 = plt.imshow(A, interpolation='nearest', cmap="jet")
f1.axes.get_xaxis().set_visible(False)
f1.axes.get_yaxis().set_visible(False)
plt.show()

# SAVE PLOT
# a colormap and a normalization instance
cmap = plt.cm.jet
norm = plt.Normalize(vmin=A.min(), vmax=A.max())
# map the normalized data to colors
# image is now RGBA (512x512x4)
image = cmap(norm(A))
scipy.misc.imsave('aa.png', image)  # pip install pillow

# PLOT NEIGHBORS
N0 = 20
X1 = np.zeros((N0*K, 784))
X2 = np.zeros((N0*K, 784))
for k in range(K):
    Xk = X0[pred_label == k, :]
    center_k = [kmeans.cluster_centers_[k]]
    neigh = NearestNeighbors(N0).fit(Xk)
    dist, nearest_id = neigh.kneighbors(center_k, N0)
    X1[N0*k: N0*k + N0, :] = Xk[nearest_id, :]
    X2[N0*k: N0*k + N0, :] = Xk[:N0, :]
plt.axis('off')
A = display_network(X2.T, K, N0)
f2 = plt.imshow(A, interpolation='nearest')
plt.gray()
plt.show()
