#!/usr/bin/env python3

from socket import AF_INET, SOCK_STREAM, socket
from concurrent.futures import ThreadPoolExecutor


def echo_client(sock, client_addr):
    '''
    Handle a client connection
    '''
    print('Got connection from', client_addr)
    while True:
        msg = sock.recv(65536)
        if not msg:
            break
        print('Got message:', msg)
        sock.sendall(msg)
    print('Client closed connection')
    sock.close()


def echo_server(addr):
    pool = ThreadPoolExecutor(128)
    sock = socket(AF_INET, SOCK_STREAM)
    sock.bind(addr)
    sock.listen(5)
    while True:
        client_sock, client_addr = sock.accept()
        pool.submit(echo_client, client_sock, client_addr)


# Start server at port 15000
# Use 'telnet localhost 15000' to test the connection
echo_server(('', 15000))
