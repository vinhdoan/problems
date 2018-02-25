import multiprocessing
from mpdaemon import DaemonWrapper
import time


def sender(conn):
    """
    function to send messages to other end of pipe
    """
    while 1:
        time.sleep(1)
        conn.send('Hello')
    conn.close()


def receiver(conn, daemon):
    """
    function to print the messages received from other
    end of pipe
    """
    while 1:
        msg = conn.recv()
        daemon.logger.info(msg)


def main(daemon):
    # creating a pipe
    parent_conn, child_conn = multiprocessing.Pipe()

    # creating new processes
    p1 = multiprocessing.Process(target=sender, args=(parent_conn,))
    p2 = multiprocessing.Process(target=receiver, args=(child_conn, daemon))

    # running processes
    p1.start()
    p2.start()

    import signal
    import sys

    def signal_handler(signal, frame):
        daemon.logger.info('MAIN GOT KILLED')
        p1.terminate()
        p2.terminate()
        sys.exit(0)

    signal.signal(signal.SIGTERM, signal_handler)

    # wait until processes finish
    p1.join()
    p2.join()


if __name__ == '__main__':
    daemon = DaemonWrapper('daemon')
    daemon.run(main, daemon)
