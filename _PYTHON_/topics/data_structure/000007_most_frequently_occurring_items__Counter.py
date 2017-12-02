#!/usr/bin/env python3
from collections import Counter


words = [
    'look', 'into', 'my', 'eyes', 'look', 'into', 'my', 'eyes',
    'the', 'eyes', 'the', 'eyes', 'the', 'eyes', 'not', 'around', 'the',
    'eyes', "don't", 'look', 'around', 'the', 'eyes', 'look', 'into',
    'my', 'eyes', "you're", 'under'
]
word_counts = Counter(words)
top_three = word_counts.most_common(3)
print("Top 3:", top_three)  # Outputs [('eyes', 8), ('the', 5), ('look', 4)]
print("Word 'not':", word_counts['not'])
print("Word 'eyes'", word_counts['eyes'])

morewords = ['why', 'are', 'you', 'not', 'looking', 'in', 'my', 'eyes']
word_counts.update(morewords)
print(word_counts)
