-module(calc).
-compile(export_all).

calc({add, E1, E2}) ->
    calc(E1) + calc(E2);
calc({sub, E1, E2}) ->
    calc(E1) - calc(E2);
calc({mul, E1, E2}) ->
    calc(E1) * calc(E2);
calc({divi, E1, E2}) ->
    calc(E1) / calc(E2);
calc({group, E}) ->
    calc(E);
calc(N) when is_number(N) ->
    N.
