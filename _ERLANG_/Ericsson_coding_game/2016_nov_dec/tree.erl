-module(tree).
-export([f/1]).

f(L) ->
    f(L, 'NULL').

f([], Tree) ->
    Tree;
f([H|T], Tree) ->
    f(T, f(H, Tree));
f(V, 'NULL') ->
    {'NULL', V, 'NULL'};
f(V, {L, N, R}) when V > N ->
    {f(V, L), N, R};
f(V, {L, N, R}) ->
    {L, N, f(V, R)}.
