-module(basic).

-include("basic.hrl").
-export([collatz/1,  % B01
         longest_collatz/1, longest_collatz/2,  % B02
         element_reverse/1,  % B03
         remove_duplicate/1,  % B04
         skeleton/1,  % B05
         eliminate/2,  % B06
         quicksort1/1, quicksort2/1,  % B07
         sqrt/1,  % B08
         sqrt_plus/1, compare_sqrt/1,  % B09
         zap_gremlins/1,  % B10
         rot_13/1,  % B11
         tree/1,  % B12
         neighbor_merge/1,  % B13
         percent/1  % B14
        ]).

%% 01. collatz(Integer) -> List
%%     Defined for positive integers n. Returns a list starting with N. Each subsequent
%%     value is computed from the previous value according to this rule:
%%     * The list ends with 1.
%%     * Every even number is followed by N / 2
%%     * Every odd number (except 1) is followed by (3 * N) + 1
%%     Example: collatz(7) ->
%%      [7, 22, 11, 34, 17, 52, 26, 13, 40, 20, 10, 5, 16, 8, 4, 2, 1].

collatz(N) when ?is_pos_integer(N) ->
    {L, _C} = do_collatz(N),
    L;
collatz(_) -> error(?collatz_error).

do_collatz(N) -> do_collatz(N, {[N], 1}).

do_collatz(1, Acc) -> Acc;
do_collatz(N, {AccList, AccCnt}) ->
    N_ = collatz_next(N),
    do_collatz(N_, {AccList ++ [N_], AccCnt+1}).

collatz_next(N) when ?is_even(N) -> N div 2;
collatz_next(N) -> 3*N + 1.

%% 02. longest_collatz(Lo, Hi) -> {TheLongestStep, ListOfLongestNumber}
%%     The Collatz sequence eventually converges to 1.
%%     Find which starting value, in the range Lo to Hi (including both end points) takes
%%     the longest to converge. If two values take equally long to converge, return
%%     either value.
%%     Example: longest_collatz(1, 10000) -> {262,[6171]}.

longest_collatz(N) -> longest_collatz(1, N).

longest_collatz(Fr, To) when ?is_pos_integer(Fr) andalso ?is_pos_integer(To) ->
    if Fr > To -> longest_collatz(To, Fr);
       true -> longest_collatz(Fr, To, {0, []})
    end;
longest_collatz(_, _) -> error(?collatz_error).

longest_collatz(In, Hi, Acc) when In > Hi -> Acc;
longest_collatz(In, Hi, {Length, NumList}) ->
    {_List, Count} = do_collatz(In),
    Acc = if Count > Length -> {Count, [In]};
             Count == Length -> {Length, NumList ++ [In]};
             true -> {Length, NumList}
          end,
    longest_collatz(In+1, Hi, Acc).

%% 03. element_reverse(List) -> List
%%     Reverse the elements but the positions of sub-lists are kept.
%%     Example: element_reverse([1, 2, [3, 4, [5, 6, 7], 8], 9]) ->
%%                              [9, 8, [7, 6, [5, 4, 3], 2], 1].

element_reverse(L) when is_list(L) ->
    {[], R} = element_reverse(L, flatten_reverse(L), []),
    R;
element_reverse(_) -> error(?element_reverse_error).

element_reverse([], In, Acc) -> {In, Acc};
element_reverse([H|T], In, Acc) when is_list(H) ->
    {NewIn, HAcc} = element_reverse(H, In, []),
    element_reverse(T, NewIn, Acc ++ [HAcc]);
element_reverse([_H|T], [HIn|TIn], Acc) -> element_reverse(T, TIn, Acc ++ [HIn]).

flatten_reverse(L) -> flatten_reverse(L, []).

flatten_reverse([], Acc) -> Acc;
flatten_reverse([H|T], Acc) when is_list(H) ->
    flatten_reverse(T, flatten_reverse(H, Acc));
flatten_reverse([H|T], Acc) -> flatten_reverse(T, [H|Acc]).

%% 04. remove_duplicate(List) -> List
%%     Removes duplicate elements of List, the order of elements must be kept.
%%     Example: remove_duplicate([1, 2, 3, 1, 4, 1, 2]) ->
%%                               [1, 2, 3, 4].

remove_duplicate(L) when is_list(L) -> remove_duplicate(L, []);
remove_duplicate(_) -> error(?remove_duplicate_error).

remove_duplicate([], Acc) -> Acc;
remove_duplicate([H|T], Acc) -> remove_duplicate(remove_element(H, T), Acc ++ [H]).

remove_element(E, L) -> remove_element(E, L, []).

remove_element(_, [], Acc) -> Acc;
remove_element(E, [E|T], Acc) -> remove_element(E, T, Acc);
remove_element(E, [H|T], Acc) -> remove_element(E, T, Acc ++ [H]).

%% 05. skeleton(List) -> List
%%     Removes all the non-list elements of list List, but retains all the list structure
%%     (the brackets).
%%     Example: skeleton([1, [2, [3, 4]], 5, 6, [7], []]) -> [[[]], [], []].

skeleton(L) when is_list(L) -> skeleton(L, []);
skeleton(_) -> error(?skeleton_error).

skeleton([], Acc) -> Acc;
skeleton([H|T], Acc) when is_list(H) -> skeleton(T, Acc ++ [skeleton(H, [])]);
skeleton([_H|T], Acc) -> skeleton(T, Acc).

%% 06. eliminate(Value, List) -> List
%%     Returns the list List with all occurrences of the Value removed, at all levels,
%%     retaining the list (bracket) structure.
%%     Example: eliminate(b, [a, b, [b, c, d, [a, [b]]], e]) ->
%%                        [a, [c, d, [a, []]], e].
%%              eliminate([b], [a, b, [b, c, d, [a, [b]]], e]) ->
%%                        [a, b, [b, c, d, [a]], e].
%%     NOTE: the Value can be anything.

eliminate(Value, List) when is_list(List) -> eliminate(Value, List, []);
eliminate(_, _) -> error(?eliminate_error).

eliminate(_V, [], Acc) -> Acc;
eliminate(V, [V|T], Acc) -> eliminate(V, T, Acc);
eliminate(V, [H|T], Acc) -> eliminate(V, T, Acc ++ [eliminate(V, H, [])]);
eliminate(_V, H, _Acc) -> H.

%% 07. quicksort(List) -> List
%%     Return a quicksorted version of the given list.
%%     Example: quicksort([5, 3, 7, 8, 4, 1, 6, 2]) ->
%%                        [1, 2, 3, 4, 5, 6, 7, 8].

quicksort1([P|T]) ->
    quicksort1([X || X <- T, X < P]) ++ [P] ++ quicksort1([X || X <- T, X >= P]);
quicksort1([]) -> [];
quicksort1(_) -> error(?quicksort_error).

%%--------

quicksort2([P|T]) -> quicksort2(T, P, [], []);
quicksort2([]) -> [];
quicksort2(_) -> error(?quicksort_error).

quicksort2([], P, LList, RList) -> quicksort2(LList) ++ [P] ++ quicksort2(RList);
quicksort2([H|T], P, LList, RList) when H < P -> quicksort2(T, P, [H|LList], RList);
quicksort2([H|T], P, LList, RList) -> quicksort2(T, P, LList, [H|RList]).

%% 08. sqrt(Integer) -> Float
%%     Compute the square root of the positive number S, using Newton's method.
%%     That is, choose some arbitrary number, say 2, as the initial approximation R to
%%     the square root; then to get the next approximation, compute the average of R and
%%     N/R.
%%     Continue until you have five significant digits to the right of the decimal point.
%%     Do this by taking an infinite series of approximations, and taking approximations
%%     until they differ by less than 0.00001. Xo = 2 and Xn+1 = 1/2 (Xn + S/Xn).
%%     Example: sqrt(12) -> 3.4641016151377544.

-define(sqrt_initial, 2).
-define(sqrt_error_threshold, 0.00001).

sqrt(0) -> 0;
sqrt(S) when ?is_pos_integer(S) ->
    {Result, _N} = do_sqrt(S, ?sqrt_initial),
    Result;
sqrt(_) -> error(?sqrt_error).

do_sqrt(S, Xn) -> do_sqrt(S, Xn, infinity, 0).

do_sqrt(_S, Xn, Err, N) when Err < ?sqrt_error_threshold -> {Xn, N};
do_sqrt(S, Xn, _Err, N) ->
    Xn_ = (Xn + S/Xn)/2,
    do_sqrt(S, Xn_, abs(Xn_ - Xn), N+1).

%% 09. sqrt_plus(Integer) -> Float
%%     Same as above, but perform round estimation to find the initial approxamation Xo
%%     first. Compare the the number of iterations between 2 exercises.
%%     Example: sqrt(795286) -> 15 times.
%%              sqrt_plus(795286) -> 6 times.

sqrt_plus(0) -> 0;
sqrt_plus(S) when ?is_pos_integer(S) ->
    {Result, _N} = do_sqrt(S, sqrt_approximate(S)),
    Result;
sqrt_plus(_) -> error(?sqrt_error).

compare_sqrt(0) -> [{sqrt, 0}, {sqrt_plus, 0}];
compare_sqrt(S) when ?is_pos_integer(S) ->
    {_Result, N} = do_sqrt(S, ?sqrt_initial),
    {_ResultPlus, NPlus} = do_sqrt(S, sqrt_approximate(S)),
    [{sqrt, N}, {sqrt_plus, NPlus}];
compare_sqrt(_) -> error(?sqrt_error).

%% https://en.wikipedia.org/wiki/Methods_of_computing_square_roots#Rough_estimation
sqrt_approximate(S) ->
    {N, AdjBit} = sqrt_check_bits(S),
    sqrt_shift_bits(N rem 2, N div 2, AdjBit).

sqrt_check_bits(S) -> sqrt_check_bits(S bsr 1, S band 1, 1).

sqrt_check_bits(0, _AdjBit, _N) -> {1, 0};
sqrt_check_bits(1, AdjBit, N) -> {N+1, AdjBit};
sqrt_check_bits(S, _AdjBit, N) -> sqrt_check_bits(S bsr 1, S band 1, N+1).

sqrt_shift_bits(1, N, _AdjBit) -> 1 bsl N;
sqrt_shift_bits(0, N, AdjBit) -> 1 bsl (N - 1 + AdjBit).

%% 10. zap_gremlins(Text) -> Text
%%     Remove from the given text all invalid ASCII characters.
%%     The Valid characters are decimal 10 (linefeed), 13 (carriage return) and 32
%%     through 126, inclusive.
%%     Example:
%%     zap_gremlins([84,104,105,115,32,235,105,115,3,32,97,12,32,115,116,114,105,
%%                   110,103,32,105,127,110,32,69,114,108,11,14,97,110,103,33]) -> ?

zap_gremlins(Text) when is_list(Text) ->
    [X || X <- Text, X == 10 orelse X == 13 orelse (X >= 32 andalso X =< 126)];
zap_gremlins(_) -> error(?zap_gremlins_error).

%% 11. rot_13(Text) -> Text
%%     Apply the rot-13 transformation to text.
%%     This is a simple encoding in which each letter is replaced by the letter 13
%%     further along, end-around.
%%     Both uppercase and lowercase letters should be rotated; other characters should
%%     be unaffected.
%%     Example: rot_13("This is a string in Erlang!") ->
%%                     "Guvf vf n fgevat va Reynat!".

rot_13(Text) when is_list(Text) -> rot_13(Text, []);
rot_13(_) -> error(?rot_13_error).

rot_13([H|T], Acc) when ?is_lowercase(H) ->
    rot_13(T, Acc ++ [(H + 13 - $a) rem 26 + $a]);
rot_13([H|T], Acc) when ?is_uppercase(H) ->
    rot_13(T, Acc ++ [(H + 13 - $A) rem 26 + $A]);
rot_13([H|T], Acc) -> rot_13(T, Acc ++ [H]);
rot_13([], Acc) -> Acc.

%% 12. Tree contruction
%%     Given a list (L) of values (V) which can be compared for equality.
%%     Write a function that traverses the list from Left to Right. The output of
%%     function is a tree (T) which each node is a 3-tuple {X, V, Y} where V is the
%%     Value and X, Y are the Left, Right nodes.
%%     Insertion of elements follow the following rules:
%%     * Starting at the root node, element greater than the current node element
%%       go LEFT, element less than or equal to the current node elements go RIGHT.
%%       Leafs have Left and Right element of 'NULL'.
%%     Example: tree([2]) -> {'NULL', 2, 'NULL'}
%%              tree([2, 4, 1]) -> {{'NULL', 4, 'NULL'}, 2, {'NULL', 1, 'NULL'}}
%%              tree([4, 1, 3, 2, 5]) -> {{'NULL', 5, 'NULL'},
%%                                        4,
%%                                        {{'NULL', 3, {'NULL', 2, 'NULL'}}, 1, 'NULL'}}

tree(L) when is_list(L) -> tree(L, 'NULL');
tree(_) -> error(?tree_arg_error).

tree([], Tree) -> Tree;
tree([H|T], Tree) when is_integer(H) -> tree(T, tree_insert_elem(H, Tree));
tree(_, _) -> error(?tree_element_error).

tree_insert_elem(V, 'NULL') -> {'NULL', V, 'NULL'};
tree_insert_elem(V, {L, N, R}) when V > N -> {tree_insert_elem(V, L), N, R};
tree_insert_elem(V, {L, N, R}) -> {L, N, tree_insert_elem(V, R)}.

%% 13. Neighboring interval merging
%%     Give a list L of 2-tuple {V, V} representing intervals.
%%     Write a function that traverses the list, Left to Right and merges overlapping
%%     intervals that are neighbors.
%%     Example: neighbor_merge([{1,5}, {3,7}, {10,14}]) -> [{1,7}, {10, 14}]
%%              neighbor_merge([{1,5}, {3,7}, {10,14}, {1,9}, {13, 20}]) ->
%%                             [{1,7}, {10,14}, {1,9}, {13, 20}]
%%              neighbor_merge([{1, 5}, {3, 7}, {1, 14}]) -> [{1,14}]

neighbor_merge(L) when is_list(L) -> neighbor_merge(L, whole_axis, [], []);
neighbor_merge(_) -> error(?neighbor_merge_arg_error).

neighbor_merge([], _, _, Acc) -> Acc;
neighbor_merge([{F, L} = H | T], LastOfAcc, AccNoLast, Acc)
  when is_integer(F), is_integer(L), F =< L ->
    case merge(H, LastOfAcc) of
        no_merge -> neighbor_merge(T, {F, L}, Acc, Acc ++ [{F, L}]);
        Result -> neighbor_merge(T, Result, AccNoLast, AccNoLast ++ [Result])
    end;
neighbor_merge(_, _, _, _) -> error(?neighbor_merge_element_error).

merge(X, whole_axis) -> X;
merge({A, B}, {C, D}) when A > C -> merge({C, D}, {A, B});
merge({A, B}, {C, D}) when C =< B -> {A, max(B, D)};
merge(_, _) -> no_merge.

%% 14. Calculate percentages
%%     Give a list of integer.
%%     Write a function that calculate the percentage (integer) of each value in
%%     the list. NOTE: make sure that the sum of all percentages = 100%
%%     Example: percent([10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110]) ->
%%                      [ 1,  3,  4,  6,  8,  9, 11, 12, 14,  15,  17]

percent([]) -> [];
percent(List) when is_list(List) ->
    Sum = percent_sum(List),
    {FloorPercents, RemaindersWithIndex, SurplusTotal} = percent_remainder(List, Sum),
    SortedListWithIndex = percent_quicksort_remainder_list(RemaindersWithIndex),
    Indices = percent_get_indices_to_surplus(SurplusTotal, SortedListWithIndex),
    percent_surplus(FloorPercents, quicksort1(Indices));
percent(_) -> error(?percent_arg_error).

percent_sum(List) -> percent_sum(List, 0).

percent_sum([], Acc) -> Acc;
percent_sum([H|T], Acc) when ?is_pos_integer(H) -> percent_sum(T, Acc+H);
percent_sum(_, _) -> error(?percent_element_error).

percent_remainder(List, Sum) -> percent_remainder(List, Sum, [], [], 0, 1).

percent_remainder([], _Sum, AccL, AccR, AccS, _Index) -> {AccL, AccR, 100 - AccS};
percent_remainder([H|T], Sum, AccL, AccR, AccS, Index) ->
    RealPercent = H * 100 / Sum,
    Percent = floor(RealPercent),
    Remainder = RealPercent - Percent,
    percent_remainder(T, Sum, AccL ++ [Percent], AccR ++ [{Remainder, Index}],
                      AccS + Percent, Index+1).

percent_quicksort_remainder_list([P = {V, _I} | T]) ->
    percent_quicksort_remainder_list([X || X = {XV, _XI} <- T, XV > V])
        ++ [P] ++
        percent_quicksort_remainder_list([X || X = {XV, _XI} <- T, XV =< V]);
percent_quicksort_remainder_list([]) -> [].

percent_get_indices_to_surplus(Number, TupList) ->
    percent_get_indices_to_surplus(Number, TupList, []).

percent_get_indices_to_surplus(0, _TupList, Acc) -> Acc;
percent_get_indices_to_surplus(N, [{_V, I} | T], Acc) ->
    percent_get_indices_to_surplus(N-1, T, Acc ++ [I]).

percent_surplus(List, Indices) -> percent_surplus(List, Indices, 1, []).

percent_surplus(List, [], _Count, Acc) -> Acc ++ List;
percent_surplus([H|T], [Count|IT], Count, Acc) ->
    percent_surplus(T, IT, Count+1, Acc ++ [H+1]);
percent_surplus([H|T], Indices, Count, Acc) ->
    percent_surplus(T, Indices, Count+1, Acc ++ [H]);
percent_surplus([], _Indices, _Count, Acc) -> Acc.
