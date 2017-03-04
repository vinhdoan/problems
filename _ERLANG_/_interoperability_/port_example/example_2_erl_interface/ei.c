/* ei.c */
/* This module contains main program */

//------------------------------------------------------------
// Step 1. Compile the C code: */
//     unix> gcc -o extprg.out \
//               -I /usr/lib/erlang/lib/erl_interface-3.8.2/include/ \
//               -L /usr/lib/erlang/lib/erl_interface-3.8.2/lib/ \
//               complex.c erl_comm.c ei.c \
//               -lerl_interface -lei -pthread
//
// Step 2. Start Erlang and compile the Erlang code:
//     unix> erl
//     Erlang (BEAM) emulator version 4.9.1.2
//
//     Eshell V4.9.1.2 (abort with ^G)
//     1> c(complex2).
//     {ok,complex2}
//
// Step 3. Run the example:
//     2> complex2:start("./extprg.out").
//     <0.34.0>
//     3> complex2:foo(3).
//     4
//     4> complex2:bar(5).
//     10
//     5> complex2:bar(352).
//     704
//     6> complex2:stop().
//     stop
//------------------------------------------------------------

#include "erl_interface.h"
#include "ei.h"
#include <string.h>

typedef unsigned char byte;
extern int read_cmd(byte *buf);
extern int read_exact(byte *buf, int len);
extern int write_cmd(byte *buf, int len);
extern int write_exact(byte *buf, int len);
extern int foo(int x);
extern int bar(int y);

int main() {
  ETERM *tuplep, *intp;
  ETERM *fnp, *argp;
  int res;
  byte buf[100];
  long allocated, freed;

  // The memory handling must be initiated before calling
  // any other Erl_Interface function
  erl_init(NULL, 0);

  while (read_cmd(buf) > 0) {
    // Convert the binary into an ETERM struct
    // by erl_decode() from erl_marshal.
    // In this example, buf will be decoded to an ETERM struct tuple
    tuplep = erl_decode(buf);
    // erl_element() from erl_eterm extracts elements
    fnp = erl_element(1, tuplep);
    argp = erl_element(2, tuplep);

    // Macros ERL_ATOM_PTR and ERL_INT_VALUE from erl_eterm can be used to
    // obtain the actual values of the atom and the integer.
    // Atom value is represented as a string.
    if (strncmp(ERL_ATOM_PTR(fnp), "foo", 3) == 0) {
      res = foo(ERL_INT_VALUE(argp));
    } else if (strncmp(ERL_ATOM_PTR(fnp), "bar", 3) == 0) {
      res = bar(ERL_INT_VALUE(argp));
    }

    // Make an ETERM struct that represents the integer result using the function
    // erl_mk_int() from erl_eterm.
    // (function erl_format() from module erl_format can also be used)
    intp = erl_mk_int(res);
    // Resulting ETERM struct is converted into the Erlang external term format
    // using the function erl_encode() from erl_marshal.
    erl_encode(intp, buf);
    // Send the result to Erlang
    write_cmd(buf, erl_term_len(intp));

    // The memory allocated by ETERM creating functions must be freed
    erl_free_compound(tuplep);
    erl_free_term(fnp);
    erl_free_term(argp);
    erl_free_term(intp);
  }
}
