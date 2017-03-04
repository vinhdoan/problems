-module(complex3).
-export([start/1, stop/0, init/1]).
-export([foo/1, bar/1]).

start(SharedLib) ->
    %% The driver must be loaded before the port is created.
    %% SharedLib is the name of port driver.
    case erl_ddll:load_driver(".", SharedLib) of
        ok ->
            ok;
        {error, already_loaded} ->
            ok;
        Error ->
            exit({error, could_not_load_driver, Error})
    end,
    spawn(?MODULE, init, [SharedLib]).

init(SharedLib) ->
    register(complex, self()),
    Port = open_port({spawn, SharedLib}, []),
    loop(Port).

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

loop(Port) ->
    receive
        {call, Caller, Msg} ->
            %% Send Data to Port by command:
	    %% Port ! {Pid, {command, Data}}
            %% Data is encoded into a sequence of bytes
            Port ! {self(), {command, encode(Msg)}},
            receive
                %% Data is received from Port in format:
		%% {Port, {data, Data}}
                {Port, {data, Data}} ->
                    Caller ! {complex, decode(Data)}
            end,
            loop(Port);
        stop ->
            Port ! {self(), close},
            receive
                {Port, closed} ->
                    exit(normal)
            end;
        {'EXIT', Port, Reason} ->
            io:format("~p ~n", [Reason]),
            exit(port_terminated)
    end.

encode({foo, X}) -> [1, X];
encode({bar, Y}) -> [2, Y].

decode([Int]) -> Int.
