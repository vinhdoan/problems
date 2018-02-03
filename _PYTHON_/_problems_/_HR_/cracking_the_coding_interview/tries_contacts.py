# https://www.hackerrank.com/challenges/ctci-contacts/problem


def partialize(contact):
    return [contact[0:i] for i in range(1, len(contact) + 1)]


contactParts = {}


def add(contact):
    for part in partialize(contact):
        contactParts[part] = contactParts.get(part, 0) + 1


def find(name):
    return contactParts.get(name, 0)


n = int(input().strip())
for a0 in range(n):
    op, contact = input().strip().split(' ')
    if op == 'add':
        add(contact)
    elif op == 'find':
        print(find(contact))
