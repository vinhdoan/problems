# https://leetcode.com/problems/lru-cache/description/


# NOTE: same way with collections.OrderedDict is faster

class LRUCache:

    def __init__(self, capacity):
        """
        :type capacity: int
        """

        self.keys = []  # LRU <--> MRU
        self.dict = {}
        self.cap = capacity


    def get(self, key):
        """
        :type key: int
        :rtype: int
        """

        #print('Get:', key, self.keys, self.dict)

        if key in self.keys:
            self.keys.remove(key)
            self.keys.append(key)
            return self.dict[key]
        return -1

    def put(self, key, value):
        """
        :type key: int
        :type value: int
        :rtype: void
        """

        #print('Put:', key, self.keys, self.dict)

        if key in self.keys:
            self.keys.remove(key)
        elif len(self.keys) == self.cap:
            del self.dict[self.keys[0]]
            self.keys = self.keys[1:]
        self.dict[key] = value
        self.keys.append(key)

# Your LRUCache object will be instantiated and called as such:
# obj = LRUCache(capacity)
# param_1 = obj.get(key)
# obj.put(key,value)
