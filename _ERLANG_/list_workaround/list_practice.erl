-module(list_practice).
-compile(export_all).

%% Get Nth element of deep nested list L
n(L, N) when N > 0 ->
    n2(L, N);
n(_L, _N) ->
    nothing.

n2([H | T], N) when is_list(H) ->
    case n2(H, N) of
        {r, N2} ->
            n2(T, N2);
        N2 ->
            N2
    end;
n2([], N) ->
    {r, N};
n2([H | _T], 1) ->
    H;
n2([_H | T], N) ->
    n2(T, N-1).

%% Reverse a whole nested list
r(H) when not is_list(H) ->
    H;
r(L) ->
    r(L, []).

r([], Acc) ->
    Acc;
r([H | T], Acc) ->
    r(T, [r(H) | Acc]).

%% Reverse a nested list, keeping order of list
r2(L) ->
    {[], R} = r2(L, fr(L), []),
    R.

r2([], In, Acc) ->
    {In, Acc};
r2([H | T], In, Acc) when is_list(H) ->
    {NewIn, HAcc} = r2(H, In, []),
    r2(T, NewIn, Acc ++ [HAcc]);
r2([_H | T], [HIn | TIn], Acc) ->
    r2(T, TIn, Acc ++ [HIn]).

%% Flatten & reverse a nested list
fr(L) ->
    fr(L, []).

fr([], Acc) ->
    Acc;
fr([H | T], Acc) when is_list(H) ->
    fr(T, fr(H, Acc));
fr([H | T], Acc) ->
    fr(T, [H | Acc]).

%% Get the middle element of a list with one-time-traversal
t1(L) ->
    t1(L, L, 0).

t1([_|[]], [H2|_], _) ->
    H2;
t1([_|T1], [_|T2], N) when N rem 2 == 0 ->
    t1(T1, T2, N+1);
t1([_|T1], L2, N) ->
    t1(T1, L2, N+1).

%% Get the three-fourth element of a list whose length is a number divisible by 4
%% with one-time-traversal
t2(L) ->
    t2(L, L, 0).

t2([_|[]], [H2|_], _) ->
    H2;
t2([_|T1], L2, N) when N rem 4 == 0 ->
    t2(T1, L2, N+1);
t2([_|T1], [_|T2], N) ->
    t2(T1, T2, N+1).

%% Get the X-Y(th) element of a list whose length is a number divisible by Y
%% with one-time-traversal
t3(L, {X, Y}) ->
    t3(L, L, {X, Y}, {1, unset}).

t3([H1|_], [_|[]], _, _) ->
    H1;
t3(L1, L2, {X, Y}, {X, unset}) ->
    t3(L1, L2, {X, Y}, {X, set});
t3([_H1|T1], [_H2|T2], {X, Y}, {N, unset}) ->
    t3(T1, T2, {X, Y}, {N+1, unset});
t3([_H1|T1], [_H2|T2], {X, Y}, {Y, set}) ->
    t3(T1, T2, {X, Y}, {1, unset});
t3(L1, [_H2|T2], {X, Y}, {N, set}) ->
    t3(L1, T2, {X, Y}, {N+1, set}).

tuan(L, {X, Y}) ->
    N = tuanlen(L) div Y * X,
    tuan_nth(N, L).

tuan_nth(1, [H|_T]) ->
    H;
tuan_nth(N, [_H|T]) ->
    tuan_nth(N-1, T).

tuanlen(L) ->
    tuanlen(L, 0).

tuanlen([_H|T], N) ->
    tuanlen(T, N+1);
tuanlen([], N) ->
    N.
