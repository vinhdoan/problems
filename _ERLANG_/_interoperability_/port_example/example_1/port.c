/* port.c */
/* This module contains main program */

//------------------------------------------------------------
// Step 1. Compile the C code: */
//     unix> gcc -o extprg complex.c erl_comm.c port.c
//
// Step 2. Start Erlang and compile the Erlang code:
//     unix> erl */
//     Erlang (BEAM) emulator version 4.9.1.2
//
//     Eshell V4.9.1.2 (abort with ^G)
//     1> c(complex1).
//     {ok,complex1}
//
// Step 3. Run the example:
//     2> complex1:start("extprg").
//     <0.34.0>
//     3> complex1:foo(3).
//     4
//     4> complex1:bar(5).
//     10
//     5> complex1:stop().
//     stop
//------------------------------------------------------------

typedef unsigned char byte;
extern int read_cmd(byte *buf);
extern int read_exact(byte *buf, int len);
extern int write_cmd(byte *buf, int len);
extern int write_exact(byte *buf, int len);
extern int foo(int x);
extern int bar(int y);

int main() {
  int fn, arg, res;
  byte buf[100];

  while (read_cmd(buf) > 0) {
    fn = buf[0];
    arg = buf[1];

    if (fn == 1) {
      res = foo(arg);
    } else if (fn == 2) {
      res = bar(arg);
    }

    buf[0] = res;
    write_cmd(buf, 1);
  }
}
