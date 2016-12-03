-module(einstein_2).
-export([query/2, result/0]).

-define(single_rules,
	[r1, r2, r3, r5, r6, r7, r8, r9, r12, r13, r16]).
-define(combined_rules,
	[r4, r10, r11, r14, r15]).
-define(all_rules, ?single_rules ++ ?combined_rules).

%%%-------------------------------------------------------------------
%%% Ex: einstein:query(pet, fish).
%%%-------------------------------------------------------------------
query(K, V) ->
    [begin
	 [R] = [Y || Y = #{K := U} <- X, V == U],
	 R
     end || X <- result()].

%%%-------------------------------------------------------------------
%%% Ex: einstein:result().
%%%-------------------------------------------------------------------
result() -> generate(init()).

%% Initialize 1st state with state = list(map())
init() -> [init(1), init(2), init(3), init(4), init(5)].

%% DFS generator, with stack = list({state, ruleset})
generate(State) -> generate([{State, ?all_rules}], []).

%% Generate successive states from current state and the first rule of
%% ruleset. Each successor takes the tail of ruleset as its ruleset.
generate([{State, [Rule | RuleSet]} | Stack], Acc) ->
    Successors = apply_rule(State, Rule),
    if Successors == [] ->
	    generate(Stack, Acc);
       true ->
	    generate(zip(Successors, RuleSet) ++ Stack, Acc)
    end;
generate([{State, []} | Stack], Acc) ->
    generate(Stack, [State | Acc]);
generate([], Acc) -> Acc.

%% Apply the rule to get successive states:...
apply_rule(State, Rule) ->
    {Info, Type} = rule(Rule),
    case Type of
	is ->
	    apply_rule_IS(State, Info, {State, 1}, []);
	RuleOfSide ->
	    apply_rule_SIDE(State, Info, {State, 1, RuleOfSide}, [])
    end.

%% ...rule of type IS,...
apply_rule_IS([House | Houses], [{K1, V1}, {K2, V2}] = Info,
	      {State, N}, Acc) ->
    #{K1 := HV1, K2 := HV2} = House,
    NewAcc = case {lists:member(HV1, [na, V1]),
		   lists:member(HV2, [na, V2])} of
		 {true, true} ->
		     NewState =
			 lists:sublist(State, N - 1) ++
			 [House#{K1 := V1, K2:= V2}] ++
			 lists:nthtail(N, State),
		     [NewState | Acc];
		 _ -> Acc
	     end,
    apply_rule_IS(Houses, Info, {State, N+1}, NewAcc);
apply_rule_IS([], _, _, Acc) -> Acc.

%% ...rule of type SIDE
apply_rule_SIDE([House1, House2 | Houses],
		[{K1, V1}, {K2, V2}] = Info,
		{State, N, RuleOfSide},
		Acc) ->
    OtherHouses = State -- [House1, House2],
    OtherHousesInvolved =
        lists:any(fun(#{K1 := HV1, K2 := HV2}) ->
                          HV1 == V1 orelse HV2 == V2
                  end, OtherHouses),
    NewAcc =
	if OtherHousesInvolved -> Acc;
	   true ->
		NewPairOfHouses =
		    find_pair_of_houses({House1, House2},
					Info, RuleOfSide),
		NewStates =
		    lists:foldl(
		      fun({}, Res) -> Res;
			 ({NewHouse1, NewHouse2}, Res) ->
			      [lists:sublist(State, N - 1) ++
				   [NewHouse1, NewHouse2] ++
				   lists:nthtail(N + 1, State) | Res]
		      end, [], NewPairOfHouses),
		NewStates ++ Acc
	end,
    apply_rule_SIDE([House2 | Houses], Info,
		    {State, N+1, RuleOfSide}, NewAcc);
apply_rule_SIDE([_House | []], _, _, Acc) -> Acc.

find_pair_of_houses(Pairs, [Attr1, Attr2], RuleOfSide) ->
    Left = lists:member(RuleOfSide, [left_of, next_to]),
    Right = lists:member(RuleOfSide, [right_of, next_to]),
    [match_pair_of_houses(Pairs, [Attr1, Attr2], Left),
     match_pair_of_houses(Pairs, [Attr2, Attr1], Right)].

%% Return {} if not any match
match_pair_of_houses(_, _, false) -> {};
match_pair_of_houses({HouseL, HouseR},
		     [{K1, V1}, {K2, V2}],
		     true) ->
    #{K1 := HLV1, K2 := HLV2} = HouseL,
    #{K1 := HRV1, K2 := HRV2} = HouseR,
    case {lists:member(HLV1, [na, V1]),
	  lists:member(HRV2, [na, V2]),
	  HRV1 /= V1, HLV2 /= V2} of
	{true, true, true, true} ->
	    {HouseL#{K1 := V1},
	     HouseR#{K2 := V2}};
	_ ->
	    {}
    end.

init(N) ->
    #{color => na,
      drink => na,
      nation => na,
      pet => na,
      position => N,
      tobacco => na}.

zip(List, Elem) ->
    lists:zip(List, lists:duplicate(length(List), Elem)).

%% Rule definition
rule(r1) -> {[{nation, england}, {color, red}], is};
rule(r2) -> {[{nation, sweden}, {pet, dog}], is};
rule(r3) -> {[{nation, denmark}, {drink, tea}], is};
rule(r4) -> {[{color, green}, {color, white}], left_of};
rule(r5) -> {[{color, green}, {drink, coffee}], is};
rule(r6) -> {[{tobacco, pall_mall}, {pet, bird}], is};
rule(r7) -> {[{color, yellow}, {tobacco, dunhill}], is};
rule(r8) -> {[{position, 3}, {drink, milk}], is};
rule(r9) -> {[{nation, norway}, {position, 1}], is};
rule(r10) -> {[{tobacco, blends}, {pet, cat}], next_to};
rule(r11) -> {[{pet, horse}, {tobacco, dunhill}], next_to};
rule(r12) -> {[{tobacco, blue_master}, {drink, beer}], is};
rule(r13) -> {[{nation, germany}, {tobacco, prince}], is};
rule(r14) -> {[{nation, norway}, {color, blue}], next_to};
rule(r15) -> {[{tobacco, blends}, {drink, water}], next_to};
rule(r16) -> {[{pet, fish}, {pet, fish}], is}.
