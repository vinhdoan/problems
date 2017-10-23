-module(fib).
-export([elem/1]).

-define(is_even(N), N band 1 == 0).

%% Needs to be improved with maps
elem(0) -> 0;
elem(1) -> 1;
elem(N) when ?is_even(N) ->
    K = N div 2,
    FK = elem(K),
    FK_ = elem(K-1),
    (2*FK_ + FK) * FK;
elem(N) ->
    K = (N + 1) div 2,
    FK = elem(K),
    FK_ = elem(K-1),
    FK*FK + FK_*FK_.
