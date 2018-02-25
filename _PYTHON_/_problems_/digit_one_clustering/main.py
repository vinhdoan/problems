# Python 2

# Input
# The first line of the input contains N, the dimension.
# It is followed by N rows, each line containing N digits.
# Each digit is either 0 or 1.

# Output
# The output should be a single number specifying the number of unique clusters in the image.


def is_on_image(x, y, dim):
    return 0 <= x < dim and 0 <= y < dim

def check_neighbors(x, y, dim, list_one, cluster):
    for i, j in [(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)]:
        x_neighbor, y_neighbor = x + i, y + j
        if not is_on_image(x_neighbor, y_neighbor, dim):
            continue
        if list_one[x_neighbor][y_neighbor]:
            list_one[x_neighbor][y_neighbor] = False
            cluster.append((x_neighbor, y_neighbor))

def any_all(matrix):
    return any([any(row) for row in matrix])

def find_first_true(matrix, dim):
    for i in range(dim):
        for j in range(dim):
            if matrix[i][j]:
                return i, j
      
def algo(list_one, dim):
    count = 0
    while any_all(list_one):
        x, y = find_first_true(list_one, dim)
        cluster = [(x, y)]
        list_one[x][y] = False
        for i, j in cluster:
            check_neighbors(i, j, dim, list_one, cluster)
        count += 1
    return count


N = int(raw_input())
list_one = [[False for i in range(N)] for j in range(N)]
for i in xrange(N):
    line = raw_input().strip()
    for j, pixel in enumerate(line):
        if pixel == '1':
            list_one[i][j] = True

count = algo(list_one, N)

print(count)