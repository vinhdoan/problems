-module(dek).
-compile(export_all).

trim([$ |T]) ->
    trim(T);
trim([H|T]) ->
    trim(T, [H], []).

trim([$ |T], A1, A2) ->
    trim(T, A1, A2 ++ [$ ]);
trim([H|T], A1, A2) ->
    trim(T, A1 ++ A2 ++ [H], []);
trim([], A1, _A2) ->
    A1.
