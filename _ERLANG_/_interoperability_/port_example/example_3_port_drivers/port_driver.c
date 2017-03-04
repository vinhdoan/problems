/* port_driver.c */

//------------------------------------------------------------
// Step 1. Compile the C code (output file must be *.so):
//     unix> gcc -o exampledrv.so -fpic -shared \
//               complex.c port_driver.c
//
// Step 2. Start Erlang and compile the Erlang code:
//     unix> erl
//     Erlang (BEAM) emulator version 5.1
//
//     Eshell V5.1 (abort with ^G)
//     1> c(complex3).
//     {ok,complex3}
//
// Step 3. Run the example (driver name "example_drv"):
//     2> complex3:start("example_drv").
//     <0.34.0>
//     3> complex3:foo(3).
//     4
//     4> complex3:bar(5).
//     10
//     5> complex3:stop().
//     stop
//------------------------------------------------------------

#include <stdio.h>
#include "erl_driver.h"

extern int foo(int x);
extern int bar(int y);

typedef struct {
  ErlDrvPort port;
} example_data;

/* All functions in the driver takes a handle (returned from start)
   that is just passed along by the Erlang process */
static ErlDrvData example_drv_start(ErlDrvPort port, char *buff) {
  example_data* d = (example_data*)driver_alloc(sizeof(example_data));
  d->port = port;
  return (ErlDrvData)d;
}

static void example_drv_stop(ErlDrvData handle) {
  driver_free((char*)handle);
}

/* The functions for receiving and sending data are combined into a function. */
/* The data sent into the port is given as arguments,
   and the replied data is sent with the C-function driver_output. */
static void example_drv_output(ErlDrvData handle, char *buff,
                               ErlDrvSizeT bufflen) {
  example_data* d = (example_data*)handle;
  char fn = buff[0], arg = buff[1], res;
  if (fn == 1) {
    res = foo(arg);
  } else if (fn == 2) {
    res = bar(arg);
  }
  driver_output(d->port, &res, 1);
}

ErlDrvEntry example_driver_entry = {
  NULL,              /* F_PTR init,
                          called when driver is loaded */
  example_drv_start, /* L_PTR start,
                          called when port is opened */
  example_drv_stop,  /* F_PTR stop,
                          called when port is closed */
  example_drv_output,/* F_PTR output,
                          called when erlang has sent */
  NULL,              /* F_PTR ready_input,
                          called when input descriptor ready */
  NULL,              /* F_PTR ready_output,
                          called when output descriptor ready */
  "example_drv",      /* char *driver_name,
                          the argument to open_port */
  NULL,              /* F_PTR finish,
                          called when unloaded */
  NULL,              /* void *handle,
                          reserved by VM */
  NULL,              /* F_PTR control,
                          port_command callback */
  NULL,              /* F_PTR timeout,
                          reserved */
  NULL,              /* F_PTR outputv,
                          reserved */
  NULL,              /* F_PTR ready_async,
                          only for async drivers */
  NULL,              /* F_PTR flush,
                          called when port is about to be closed,
                          but there is data in driver queue */
  NULL,              /* F_PTR call,
                          much like control, sync call to driver */
  NULL,              /* F_PTR event,
                          called when an event selected by
                          driver_event() occurs. */
  ERL_DRV_EXTENDED_MARKER,
                     /* int extended marker,
                          should always be set to indicate
                          driver versioning */
  ERL_DRV_EXTENDED_MAJOR_VERSION,
                     /* int major_version,
                          should always be set to this value */
  ERL_DRV_EXTENDED_MINOR_VERSION,
                     /* int minor_version,
                          should always be set to this value */
  0,                 /* int driver_flags,
                          see documentation */
  NULL,              /* void *handle2,
                          reserved for VM use */
  NULL,              /* F_PTR process_exit,
                          called when a monitored process dies */
  NULL               /* F_PTR stop_select,
                          called to close an event object */
};

/* The driver structure is filled with the driver name and function pointers.
   It is returned from the special entry point, declared with the macro DRIVER_INIT(<driver_name>). */
DRIVER_INIT(example_drv) {   /* must match name in driver_entry */
  return &example_driver_entry; /* The ErlDrvEntry declared above */
}
