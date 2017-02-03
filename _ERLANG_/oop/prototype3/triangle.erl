-module(triangle).
-compile(export_all).

func1(Args) ->
    io:format("~p:~p called with ~p~n", [?MODULE, func1, Args]).

func2(Args) ->
    io:format("~p:~p called with ~p~n", [?MODULE, func2, Args]).
