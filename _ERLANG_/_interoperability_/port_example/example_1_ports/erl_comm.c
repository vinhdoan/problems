/* erl_comm.c */
/* This module contains functions for communication with Erlang data */

#include <unistd.h>

typedef unsigned char byte;
int read_cmd(byte *buf);
int read_exact(byte *buf, int len);
int write_cmd(byte *buf, int len);
int write_exact(byte *buf, int len);

int read_cmd(byte *buf)
{
  int len;
  // Read 2 first bytes to know length of data received
  if (read_exact(buf, 2) != 2)
    return(-1);
  len = (buf[0] << 8) | buf[1];
  // Read data whose length is 'len'
  return read_exact(buf, len);
}

int write_cmd(byte *buf, int len)
{
  byte li;
  // Send (write) 2 first bytes to indicate message length
  li = (len >> 8) & 0xff;
  write_exact(&li, 1);
  li = len & 0xff;
  write_exact(&li, 1);
  // Send data whose length is 'len'
  return write_exact(buf, len);
}

int read_exact(byte *buf, int len)
{
  int i, got = 0;
  do {
    if ((i = read(0, buf + got, len - got)) <= 0)
      return(i);
    got += i;
  } while (got < len);
  return(len);
}

int write_exact(byte *buf, int len)
{
  int i, wrote = 0;
  do {
    if ((i = write(1, buf + wrote, len - wrote)) <= 0)
      return (i);
    wrote += i;
  } while (wrote < len);
  return(len);
}
