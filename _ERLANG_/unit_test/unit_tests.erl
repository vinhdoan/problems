-module(unit_tests).
-import(unit, [add/2, sub/2, mul/2, dvd/2]).
-include_lib("eunit/include/eunit.hrl").

add_test_() ->
    [{"assert", ?_assert(add(2, 6) == 8)},
     {"assertMatch", ?_assertMatch({_, V} when is_integer(V), {add, add(2,6)})},
     {"assertNotMatch", ?_assertNotMatch(X when is_list(X), add(2, 6))}].

sub_test_() ->
    [{"assertNotEqual", ?_assertNotEqual(sub(6, 2), sub(2, 6))},
     {"assertNot", ?_assertNot(sub(6, 2) == sub(2, 6))}].

mul_test_() ->
    [{"assertEqual", ?_assertEqual(mul(2, 6), mul(6, 2))},
     {"assertException", ?_assertException(error, _, mul(a, b))}].

dvd_test_() ->
    [{"assertError", ?_assertError(divided_by_0, dvd(6, 0))},
     {"assertExit", ?_assertExit(divident_not_an_int, dvd(a, 2))},
     {"assertThrow", ?_assertThrow(divisor_not_an_int, dvd(6, b))}].
