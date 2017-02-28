-module(unit).
-export([add/2, sub/2, mul/2, dvd/2]).

add(X, Y) ->
    X + Y.

sub(X, Y) ->
    X - Y.

mul(X, Y) ->
    X * Y.

dvd(X, _Y) when not is_integer(X) ->
    exit(divident_not_an_int);
dvd(_X, Y) when not is_integer(Y) ->
    throw(divisor_not_an_int);
dvd(_X, 0) ->
    error(divided_by_0);
dvd(X, Y) ->
    X / Y.
