-module(complex2).
-export([start/1, stop/0, init/1]).
-export([foo/1, bar/1]).

start(ExtPrg) ->
    spawn(?MODULE, init, [ExtPrg]).

stop() ->
    complex ! stop.

foo(X) ->
    call_port({foo, X}).

bar(Y) ->
    call_port({bar, Y}).

call_port(Msg) ->
    complex ! {call, self(), Msg},
    receive
	{complex, Result} ->
	    Result
    end.

init(ExtPrg) ->
    register(complex, self()),
    process_flag(trap_exit, true),
    %% C program uses Erl_Interface => port must be set to use binaries
    %% Each message is preceded by 2 bytes indicating its length
    Port = open_port({spawn, ExtPrg}, [{packet, 2}, binary]),
    loop(Port).

loop(Port) ->
    receive
	{call, Caller, Msg} ->
	    %% Send Data to Port by command:
	    %% Port ! {Pid, {command, Data}}
	    Port ! {self(), {command, term_to_binary(Msg)}},
	    receive
		%% Data is received from Port in format:
		%% {Port, {data, Data}}
		{Port, {data, Data}} ->
		    Caller ! {complex, binary_to_term(Data)}
	    end,
	    loop(Port);
	stop ->
	    Port ! {self(), close},
	    receive
		{Port, closed} ->
		    exit(normal)
	    end;
	{'EXIT', Port, _Reason} ->
	    exit(port_terminated)
    end.
